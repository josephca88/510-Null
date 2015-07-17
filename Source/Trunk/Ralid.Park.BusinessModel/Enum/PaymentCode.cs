﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 表示收费代码
    /// </summary>
    public enum PaymentCode
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = 0x00,

        /// <summary>
        /// 互联网收费
        /// </summary>
        Internet = 0x01,

        /// <summary>
        /// 缴费机收费
        /// </summary>
        APM = 0xA1,

        /// <summary>
        /// 电脑收费
        /// </summary>
        Computer = 0xB1,

        /// <summary>
        /// 功能卡收费
        /// </summary>
        FunctionCard = 0xB2,

        /// <summary>
        /// 手持机收费
        /// </summary>
        POS = 0xB3,
    }
}
