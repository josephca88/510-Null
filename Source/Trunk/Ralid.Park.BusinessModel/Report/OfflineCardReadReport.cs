﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 表示控制板离线模式下产生的卡片等待、车牌对比确认事件或有效事件
    /// </summary>
    [DataContract]
    public class OfflineCardReadReport : ReportBase
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置识车牌对比结果
        /// </summary>
        [DataMember]
        public CarPlateComparisonResult CarPlateComparisonResult { get; set; }

        /// <summary>
        /// 获取或设置识别到的车牌号码
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }

        /// <summary>
        /// 获取或设置上次识别到的车牌号码
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }

        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        [DataMember]
        public byte CardType { get; set; }

        /// <summary>
        /// 获取或设置控制板上读到卡号的读头
        /// </summary>
        [DataMember]
        public EntranceReader Reader { get; set; }

        /// <summary>
        /// 脱机读卡事件中卡片上一次刷卡的时间
        /// </summary>
        [DataMember]
        public DateTime? LastDateTime { get; set; }

        /// <summary>
        /// 获取或设置事件状态，是卡等待还是卡有效
        /// </summary>
        [DataMember]
        public CardEventStatus EventStatus { get; set; }

        
        /// <summary>
        /// 获取或设置事件是否需要缴费
        /// </summary>
        [DataMember]
        public bool NeedPay { get; set; }


        /// <summary>
        /// 获取或设置是否按车牌事件处理
        /// </summary>
        [DataMember]
        public bool IsCarPlateEventHandle { get; set; }

        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}{3} Reader:{4} 车牌号:{5} {6} {7}",
                    EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    SourceName,
                    EventStatus == CardEventStatus.Valid ? Resouce.Resource1.Report_OfflineCardPermitted : Resouce.Resource1.Report_OfflineCardWait,
                    CardID,
                    EventStatus == CardEventStatus.Valid ? string.Empty : Reader.ToString(),
                    CarPlate,
                    NeedPay ? Resouce.Resource1.Report_NeedPay : string.Empty,
                    IsCarPlateEventHandle ? "车牌事件" : string.Empty);//脱机有效抬闸事件时，没有读卡器上传
            }
        }
    }
}
