﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UserControls;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UserControls.VideoPanels;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.SnapShotCapture;

namespace Ralid.Park.UI
{
    public partial class FrmSnapShoter : Form, IReportHandler, ISnapShotCapture
    {
        #region 静态方法和变量
        private static FrmSnapShoter _Instance;
        public static FrmSnapShoter GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmSnapShoter();
            }
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmSnapShoter()
        {
            InitializeComponent();
            InitVideoGrid();
        }
        #endregion

        #region 私有变量
        private object _TagLocker = new object();
        #endregion

        #region 私有方法
        private void InitVideoGrid()
        {
            int row = 2;
            int columns = 2;
            int videoCounts = 0;
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark)
                {
                    if (park.HostWorkstation == WorkStationInfo.CurrentStation.StationID)
                    {
                        foreach (EntranceInfo entrance in park.Entrances)
                        {
                            videoCounts += entrance.VideoSources.Count;
                            while (videoCounts > row * columns)
                            {
                                row++;
                                columns++;
                            }
                            this.videoGrid.SetShowMode(row, columns);
                            videoGrid.RenderVideoes(entrance.VideoSources);
                        }
                    }
                }
                else   //子车场则由最顶层车场来判断是否需要初始化视频
                {
                    ParkInfo p = ParkBuffer.Current.GetPark(park.RootParkID);
                    if (p != null && p.HostWorkstation == WorkStationInfo.CurrentStation.StationID)
                    {
                        foreach (EntranceInfo entrance in park.Entrances)
                        {
                            videoCounts += entrance.VideoSources.Count;
                            while (videoCounts > row * columns)
                            {
                                row++;
                                columns++;
                            }
                            this.videoGrid.SetShowMode(row, columns);
                            videoGrid.RenderVideoes(entrance.VideoSources);
                        }
                    }
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="video">要抓拍的摄像机</param>
        /// <param name="path">抓拍图片路径，如摄像机自动抓拍，返回抓拍到的图片路径</param>
        /// <param name="optimized">如果启用优化,则发现视频没有打开时不再尝试打开视频,而是直接返回</param>
        /// <returns></returns>
        public bool SnapShotTo(VideoSourceInfo video, ref string path, bool optimized, bool force)
        {
            bool success = false;
            VideoPanel vp = videoGrid.VideoPanelCollection.SingleOrDefault(v => (v.VideoSource == video));
            if (vp != null)
            {
                //如果没有启用视频抓拍性能优化功能,则在抓拍时如果视频没有打开,会先尝试打开视频,启用优化后,视频由地感检测车到时打开,在抓拍时就不负责打开视频
                //这样的话如果系统中存在有问题的视频,也不会影响软件的处理速度
                if (!vp.IsReadyForSnapshot && !optimized)
                {
                    vp.OpenForSnapshot(false);
                }
                if (vp.IsReadyForSnapshot)
                {
                    success = vp.SnapShotTo(ref path, 1000, force);
                    if (optimized)
                    {
                        lock (_TagLocker)
                        {
                            vp.Tag = success ? path : "fail"; //抓拍失败时也要通知下一次不要再继续抓拍了
                        }
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// 清除最近保存的抓拍图片信息
        /// </summary>
        /// <param name="video">摄像机</param>
        public void ClearSnapShot(VideoSourceInfo video)
        {
            VideoPanel vp = videoGrid.VideoPanelCollection.SingleOrDefault(v => (v.VideoSource == video));
            if (vp != null)
            {
                vp.ClearSnapShot();
            }
        }
        #endregion

        #region ISnapShot接口实现
        /// <summary>
        /// 对通道进行图片抓拍,要求此通道有一个用于车牌识别的摄像机
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="entranceID"></param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        public string SnapShot(int parkID, int entranceID)
        {
            string dir = TempFolderManager.GetCurrentFolder();
            try
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(entranceID);
                if (entrance != null)
                {
                    foreach (VideoSourceInfo video in entrance.VideoSources)
                    {
                        if (video.IsForCarPlate)
                        {
                            string path = Path.Combine(dir, string.Format("{0}_{1}_{2}.jpg", "SnapShot", Guid.NewGuid().ToString(), video.VideoID));
                            if (SnapShotTo(video, ref path, false, false))
                            {
                                return path;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return string.Empty;
        }
        #endregion

        #region IReportHandler 成员
        public void ProcessReport(ReportBase r)
        {
            ParkInfo park = ParkBuffer.Current.GetPark(r.ParkID);
            if (park != null && park.RootParkID > 0) park = ParkBuffer.Current.GetPark(park.RootParkID);
            if (park == null) return;
            if (park.HostWorkstation == WorkStationInfo.CurrentStation.StationID) //如果本机是停车场的通讯主机,则它要负责抓拍图片
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(r.EntranceID);
                if (entrance != null)
                {
                    foreach (VideoSourceInfo video in entrance.VideoSources)
                    {
                        if (r is CarSenseReport)
                        {
                            CarSenseReport cp = r as CarSenseReport;
                            if (cp.InOrOutFlag == 1)  //车到时打开视频
                            {
                                VideoPanel vp = videoGrid.VideoPanelCollection.SingleOrDefault(v => (v.VideoSource == video));
                                if (vp != null)
                                {
                                    if (UserSetting.Current.SnapshotWhenCarArrive)  //车压地感时抓拍图片
                                    {
                                        string path = Path.Combine(TempFolderManager.GetCurrentFolder(),
                                              string.Format("{0}_{1}_{2}.jpg", "CarArrive", Guid.NewGuid().ToString(), video.VideoID));
                                        if (SnapShotTo(video, ref path, false, false))
                                        {
                                            SnapShot shot = new SnapShot(cp.EventDateTime, video.VideoID, string.Empty, path);
                                            string master = AppSettings.CurrentSetting.ImageDBConnStr;
                                            string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
                                            CommandResult result = (new SnapShotBll(master)).Insert(shot);
                                            if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);

                                            //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
                                        }
                                        else
                                        {
                                            if (AppSettings.CurrentSetting.Debug)
                                            {
                                                string logmsg = string.Format("【{0}】车压地感时抓拍图片失败 ", vp.VideoSource.VideoName);
                                                Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        vp.OpenForSnapshot(true);
                                    }
                                }
                            }
                            else
                            {
                                if (AppSettings.CurrentSetting.Optimized)  //启用视频优化车走时关闭视频
                                {
                                    VideoPanel vp = videoGrid.VideoPanelCollection.SingleOrDefault(v => (v.VideoSource == video));
                                    if (vp != null) vp.Close();
                                }
                            }
                        }
                        else if (r is CardEventReport)
                        {
                            CardEventReport cardEvent = r as CardEventReport;
                            if (cardEvent.EventStatus == CardEventStatus.Valid)
                            {
                                VideoPanel vp = videoGrid.VideoPanelCollection.SingleOrDefault(v => (v.VideoSource == video));
                                if (vp != null) //如果视频已经抓拍了一张,则此次用同一张图
                                {
                                    object tag = null;
                                    lock (_TagLocker)  //加锁是防止多个线程同时写
                                    {
                                        tag = vp.Tag;
                                        vp.Tag = null;
                                    }
                                    if (tag != null)
                                    {
                                        string path = tag.ToString();
                                        if (path != "fail")
                                        {
                                            SnapShot shot = new SnapShot(cardEvent.EventDateTime, video.VideoID, cardEvent.CardID, path);
                                            string master = AppSettings.CurrentSetting.ImageDBConnStr;
                                            string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
                                            CommandResult result = (new SnapShotBll(master)).Insert(shot);
                                            if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);

                                            //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
                                        }
                                        else
                                        {
                                            if (AppSettings.CurrentSetting.Debug)
                                            {
                                                string logmsg = string.Format("【{0}】刷卡事件之前的视频抓拍失败，无图片 ", vp.VideoSource.VideoName);
                                                Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string path = Path.Combine(TempFolderManager.GetCurrentFolder(),
                                                      string.Format("{0}_{1}_{2}.jpg", "CardEvent", Guid.NewGuid().ToString(), video.VideoID));
                                        if (SnapShotTo(video, ref path, false, false))
                                        {
                                            SnapShot shot = new SnapShot(cardEvent.EventDateTime, video.VideoID, cardEvent.CardID, path);
                                            string master = AppSettings.CurrentSetting.ImageDBConnStr;
                                            string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
                                            CommandResult result = (new SnapShotBll(master)).Insert(shot);
                                            if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);

                                            //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
                                        }
                                        else
                                        {
                                            if (AppSettings.CurrentSetting.Debug)
                                            {
                                                string logmsg = string.Format("【{0}】刷卡事件抓拍图片失败 ", vp.VideoSource.VideoName);
                                                Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                                            }
                                        }
                                    }

                                    vp.ClearSnapShot();//清空抓拍图片信息
                                }
                            }
                        }
                        else if (r is AlarmReport)
                        {
                            AlarmReport ar = r as AlarmReport;
                            if (ar.AlarmType == Ralid.Park.BusinessModel.Enum.AlarmType.Opendoor
                                || ar.AlarmType == Ralid.Park.BusinessModel.Enum.AlarmType.GateAlarm)
                            {
                                string path = Path.Combine(TempFolderManager.GetCurrentFolder(),
                                              string.Format("{0}_{1}_{2}.jpg", "OpenDoor", Guid.NewGuid().ToString(), video.VideoID));
                                if (SnapShotTo(video, ref path, false, true))
                                {
                                    //这里清除抓拍图片信息，是因为使用信路通或大华进行抓拍时，会保存抓拍图片和识别到的车牌，如果不清除，有可能会用到下一辆车上
                                    ClearSnapShot(video);
                                    SnapShot shot = new SnapShot(ar.EventDateTime, video.VideoID, string.Empty, path);
                                    string master = AppSettings.CurrentSetting.ImageDBConnStr;
                                    string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
                                    CommandResult result = (new SnapShotBll(master)).Insert(shot);
                                    if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);

                                    //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);                                    
                                }
                                else
                                {
                                    if (AppSettings.CurrentSetting.Debug)
                                    {
                                        string logmsg = string.Format("【{0}】报警抓拍图片失败 ", video.VideoName);
                                        Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
