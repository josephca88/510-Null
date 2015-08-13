﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTService : IOpenCardService
    {
        #region 构造函数
        public YCTService()
        {
        }

        public YCTService(YCTSetting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private List<YCTItem> _Readers = new List<YCTItem>();
        private Dictionary<YCTItem, Thread> _PollRoutes = new Dictionary<YCTItem, Thread>();
        private Timer _ChkComport = null;
        private Timer _FreshBlacklist=null ;
        private Dictionary<string, YCTBlacklist> _Blacklists = new Dictionary<string, YCTBlacklist>();
        private ReaderWriterLock _BlacklistLocker = new ReaderWriterLock();
        #endregion

        #region 公共属性
        public YCTSetting Setting { get; set; }
        #endregion

        #region 私有方法
        private void CheckComport(object state)
        {
            foreach (var item in _Readers)
            {
                if (item.Reader != null && !item.Reader.IsOpened)
                {
                    item.Reader.Open();
                    if (item.Reader.IsOpened) //需要正常初始化后才能加到列表中
                    {
                        item.Reader.SetServiceCode(Setting.ServiceCode);
                        item.Reader.InitPaidMode();
                    }
                }
            }
        }

        private void FreshBlacklist(object state)
        {
            List<YCTBlacklist> bls = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetItems(null).QueryObjects;
            if (bls != null && bls.Count > 0)
            {
                try
                {
                    _BlacklistLocker.AcquireWriterLock(20000);
                    foreach (var bl in bls)
                    {
                        if (!_Blacklists.ContainsKey(bl.CardID)) _Blacklists.Add(bl.CardID, bl);
                    }
                    _BlacklistLocker.ReleaseWriterLock();
                }
                catch (Exception)
                {
                }
            }
        }

        private void PollRoute(object obj)
        {
            YCTItem item = obj as YCTItem;
            if (item == null || item.Reader == null) return;
            try
            {
                while (true)
                {
                    if (item.Reader.IsOpened)
                    {
                        YCTWallet w = item.Reader.Poll();
                        if (w != null)
                        {
                            if (InBlackList(w.LogicCardID, w.PhysicalCardID)) HandleBlacklist(item, w);
                            else HandleWallet(item, w);
                        }
                        else
                        {
                            if (item.Reader.LastError ==0x80) //没有卡片，继续读卡
                            {
                                Thread.Sleep(500);
                                continue ;
                            }
                            else
                            {
                                HandleError(item, item.Reader.GetErrDescr(item.Reader.LastError));
                            }
                        }
                    }
                    Thread.Sleep(3000);
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private bool InBlackList(string logicCardID, string physicalCardID)
        {
            try
            {
                _BlacklistLocker.AcquireReaderLock(20000);
                bool ret = _Blacklists.ContainsKey(logicCardID) || _Blacklists.ContainsKey(physicalCardID);
                _BlacklistLocker.ReleaseReaderLock();
                return ret;
            }
            catch (Exception)
            {
            }
            return false;
        }

        private void HandleWallet(YCTItem item, YCTWallet w)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = w.LogicCardID,
                CardType = w.WalletType == 0 ? string.Empty : "羊城通卡",
                EntranceID = entrance != null ? entrance.EntranceID : 0,
                EntranceName = entrance != null ? entrance.EntranceName : "中央收费",
                Balance = (decimal)w.Balance / 100,
            };
            if (entrance != null && entrance.IsExitDevice && args.CardType == "羊城通卡") //出口,并且是羊城通卡,则产生收费记录
            {
                if (this.OnPaying != null) this.OnPaying(this, args); //产生收费事件
                if (args.Payment == null) return;
                if (args.Payment.Accounts == 0) //不用收费直接返回收款成功事件
                {
                    if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                }
                else //扣费
                {
                    int balance;
                    if (Paid(item, w, args.Payment, out balance))
                    {
                        args.Paid = args.Payment.Accounts;
                        args.Balance = (decimal)balance / 100;
                        args.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                        args.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.YangChengTong;
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                    }
                    else
                    {
                        args.LastError = item.Reader.LastError.ToString();
                        if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                    }
                }
            }
            else
            {
                if (this.OnReadCard != null) this.OnReadCard(this, args);
            }
        }

        private void HandleBlacklist(YCTItem item, YCTWallet w)
        {
            item.Reader.CatchBlackList();
            HandleError(item, "黑名单卡");
        }

        private void HandleError(YCTItem item, string error)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            if (this.OnError != null) //
            {
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    EntranceID = entrance != null ? entrance.EntranceID : 0,
                    EntranceName = entrance != null ? entrance.EntranceName : null,
                    LastError = error,
                };
                this.OnError(this, args);
            }
        }

        private bool Paid(YCTItem item, YCTWallet w, CardPaymentInfo paid, out int balance)
        {
            balance = 0;
            YCTPaymentInfo payment = item.Reader.Prepaid((int)(paid.Accounts * 100), w.WalletType);
            if (payment == null) return false;
            //这里应该保存记录,保存记录成功然后再进行下一步
            YCTPaymentRecord record = CreateRecord(payment);
            record.WalletType = w.WalletType;
            record.EnterDateTime = paid.EnterDateTime.Value;
            record.State = YCTPaymentRecordState.PaidFail;
            YCTPaymentRecordBll bll = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect);
            CommandResult result = bll.Insert(record);
            if (result.Result != ResultCode.Successful) return false;

            string tac = item.Reader.CompletePaid();
            if (string.IsNullOrEmpty(tac))
            {
                int err = item.Reader.LastError;
                //if (err == 0x01) bll.Delete(record); //失败 删除记录
                return false;
            }
            YCTPaymentRecord newVal = record.Clone();
            if (w.WalletType == 0x02) newVal.TAC = tac; //cpu钱包将TAC写到记录中
            newVal.State = YCTPaymentRecordState.PaidOk; //标记为完成
            result = bll.Update(newVal, record);
            balance = record.BAL; //返回余额
            return result.Result == ResultCode.Successful;
        }

        private YCTPaymentRecord CreateRecord(YCTPaymentInfo payment)
        {
            YCTPaymentRecord record = new YCTPaymentRecord();
            record.PID = payment.本次交易设备编号;
            record.PSN = payment.终端交易流水号;
            record.TIM = payment.本次交易日期时间;
            record.FCN = payment.物理卡号;
            record.LCN = payment.逻辑卡号;
            record.TF = payment.票价;
            record.FEE = payment.交易金额;
            record.BAL = payment.本次余额;
            record.TT = payment.交易类型;
            record.ATT = payment.附加交易类型;
            record.CRN = payment.票卡充值交易计数;
            record.XRN = payment.票卡消费交易计数;
            record.DMON = payment.累计门槛月份;
            record.BDCT = payment.公交门槛计数;
            record.MDCT = payment.地铁门槛计数;
            record.UDCT = payment.联乘门槛计数;
            record.EPID = payment.本次交易入口设备编号;
            record.ETIM = payment.本次交易入口日期时间;
            record.LPID = payment.上次交易设备编号;
            record.LTIM = payment.上次交易日期时间;
            record.AREA = payment.区域代码;
            record.ACT = payment.区域卡类型;
            record.SAREA = payment.区域子码;
            record.TAC = payment.交易认证码;
            return record;
        }
        #endregion

        #region 实现接口IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public event EventHandler<OpenCardEventArgs> OnError;

        public void Init()
        {
            if (Setting == null) throw new InvalidOperationException("没有提供羊城通参数");
            if (_ChkComport != null) _ChkComport.Dispose();
            if (_FreshBlacklist != null) _FreshBlacklist.Dispose();
            List<YCTItem> keys = _Readers.ToList();
            if (keys != null && keys.Count > 0)//将所有不在新设置中的读卡器删除
            {
                foreach (var key in keys)
                {
                    var item = Setting.Items != null ? Setting.Items.SingleOrDefault(it => it.ID == key.ID) : null;
                    if (item == null)
                    {
                        var reader = key.Reader;
                        reader.Close();
                        _Readers.Remove(key);
                        if (_PollRoutes.ContainsKey(key))
                        {
                            Thread t = _PollRoutes[key];
                            if (t.ThreadState != ThreadState.Stopped) t.Abort();
                            _PollRoutes.Remove(key);
                        }
                    }
                    else
                    {
                        key.EntranceID = item.EntranceID;
                    }
                }
            }
            if (Setting.Items != null)
            {
                foreach (var item in Setting.Items)
                {
                    if (_Readers == null || !_Readers.Exists(it => it.ID == item.ID))
                    {
                        var reader = new YCTPOS((byte)item.Comport, 57600);
                        reader.Open();
                        item.Reader = reader;
                        _Readers.Add(item);
                        if (reader.IsOpened) //需要正常初始化后才能加到列表中
                        {
                            reader.SetServiceCode(Setting.ServiceCode);
                            reader.InitPaidMode();
                        }
                        Thread t = new Thread(new ParameterizedThreadStart(PollRoute));
                        t.IsBackground = true;
                        _PollRoutes[item] = t;
                        t.Start(item);
                    }
                }
            }
            _ChkComport = new Timer(new TimerCallback(CheckComport), null, 5000, 10000); //启动检查串口状态的定时器
            _FreshBlacklist = new Timer(new TimerCallback(FreshBlacklist), null, 5000, 1000 * 60 * 10);//10分钟刷新一次
        }

        public void Dispose()
        {
            if (_ChkComport != null) _ChkComport.Dispose();
            if (_FreshBlacklist != null) _FreshBlacklist.Dispose();
            foreach (var item in _Readers)
            {
                if (item.Reader != null && item.Reader.IsOpened) item.Reader.Close();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
