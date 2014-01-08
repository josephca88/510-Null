﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.LocalDataBase.Model
{
    /// <summary>
    /// 卡片停车收费记录
    /// </summary>
    [Table(Name = "CardPaymentRecord")]
    public class LDB_CardPaymentInfo
    {
        #region 私有变量
        [Column(Name = "CardType")]
        private byte _CardType;
        #endregion

        #region 公共属性
        /// <summary>
        /// 记录ID
        /// </summary>
        [Column(IsPrimaryKey = true, Name = "ID")]
        public int? ID { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        [Column(Name = "CardID")]
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置卡片的编号
        /// </summary>
        [Column(Name = "CardCertificate")]
        public string CardCertificate { get; set; }
        /// <summary>
        /// 获取或设置车牌号
        /// </summary>
        [Column(Name = "CarPlate")]
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置计费出场时间
        /// </summary>
        [Column(Name = "ChargeDateTime")]
        public DateTime ChargeDateTime { get; set; }
        /// <summary>
        /// 获取或设置计费入场时间
        /// </summary>
        [Column(Name = "EnterDateTime")]
        public DateTime? EnterDateTime { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public CardType CardType
        {
            get
            {
                return CardType.GetSystemCardType(_CardType);
            }
            set
            {
                _CardType = (byte)value;
            }
        }
        /// <summary>
        /// 获取或设置收费车型
        /// </summary>
        [Column(Name = "CarType")]
        public Byte CarType { get; set; }
        /// <summary>
        /// 获取或设置收费类型
        /// </summary>
        [Column(Name = "TariffType")]
        public TariffType TariffType { get; set; }
        /// <summary>
        /// 获取或设置卡片本次收费之前累计的实收金额
        /// </summary>
        [Column(Name = "LastTotalPaid")]
        public decimal LastTotalPaid { get; set; }
        /// <summary>
        /// 获取或设置卡片本次收费之前累计的折扣
        /// </summary>
        [Column(Name = "LastTotalDiscount")]
        public decimal LastTotalDiscount { get; set; }
        /// <summary>
        /// 获取或设置应收停车费用
        /// </summary>
        [Column(Name = "Accounts")]
        public decimal Accounts { get; set; }
        /// <summary>
        /// 获取或设置本次收取的费用
        /// </summary>
        [Column(Name = "Paid")]
        public decimal Paid { get; set; }
        /// <summary>
        /// 获取或设置本次折扣额
        /// </summary>
        [Column(Name = "Discount")]
        public decimal Discount { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        [Column(Name = "PaymentMode")]
        public PaymentMode PaymentMode { get; set; }
        /// <summary>
        /// 获取或设置卡片未用的优惠时数
        /// </summary>
        [Column(Name = "DiscountHour")]
        public int DiscountHour { get; set; }
        /// <summary>
        /// 获取或设置操作员编号
        /// </summary>
        [Column(Name = "OperatorNum")]
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置工作站
        /// </summary>
        [Column(Name = "StationID")]
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置收费说明
        /// </summary>
        [Column(Name = "Memo")]
        public string Memo { get; set; }
        /// <summary>
        /// 获取或设置结算时间,没有进行结算时为空
        /// </summary>
        [Column(Name = "SettleDateTime")]
        public DateTime? SettleDateTime { get; set; }
        /// <summary>
        /// 获取或设置是否是中央收费记录
        /// </summary>
        [Column(Name = "IsCenterCharge")]
        public bool? IsCenterCharge { get; set; }
        /// <summary>
        /// 获取或设置外车场由进场到缴费时所产生的停车费用
        /// </summary>
        [Column(Name = "ParkFee")]
        public decimal ParkFee { get; set; }
        /// <summary>
        /// 获取或设置收费代码
        /// </summary>
        [Column(Name = "PaymentCode")]
        public PaymentCode PaymentCode { get; set; }
        /// <summary>
        /// 获取或设置操作员卡编号（收费功能卡）
        /// </summary>
        [Column(Name = "OperatorCardID")]
        public string OperatorCardID { get; set; }
        /// <summary>
        /// 获取或设置是否已导出数据到停车场数据库
        /// </summary>
        [Column(Name = "UpdateFlag")]
        public bool UpdateFlag { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string TimeInterval
        {
            get
            {
                string ret = string.Empty;
                if (EnterDateTime != null)
                {
                    DatetimeInterval di = new DatetimeInterval(EnterDateTime.Value, ChargeDateTime);
                    return di.ToString();
                }
                return ret;
            }
        }

        /// <summary>
        /// 获取总共已收金额
        /// </summary>
        public decimal TotalPaid
        {
            get { return LastTotalPaid + Paid; }
        }

        /// <summary>
        /// 获取总共折扣金额
        /// </summary>
        public decimal TotalDiscount
        {
            get { return LastTotalDiscount + Discount; }
        }

        /// <summary>
        /// 获取卡片本次收费之前累计的总共已收费用
        /// </summary>
        public decimal LastTotalFee
        {
            get { return LastTotalPaid + LastTotalDiscount; }
        }

        /// <summary>
        /// 获取卡片的总共应收费用（由进场到缴费时所产生的停车费用）
        /// </summary>
        public decimal TotalFee
        {
            get { return LastTotalFee + Accounts; }
        }
        #endregion
    }
}
