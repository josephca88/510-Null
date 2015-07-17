﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示一段时间间隔
    /// </summary>
    public class DatetimeInterval
    {
        #region 构造函数
        public DatetimeInterval()
        {
        }

        public DatetimeInterval(DateTime begin, DateTime end)
        {
            Begin = begin;
            End = end;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 或取或设置开始时间
        /// </summary>
        public DateTime Begin { get; set; }

        /// <summary>
        /// 获取或设置结束时间
        /// </summary>
        public DateTime End { get; set; }

        #endregion

        #region 公共方法
        public override string ToString()
        {
            string ret = string.Empty;
            //DateTime b = new DateTime(Begin.Year, Begin.Month, Begin.Day, Begin.Hour, Begin.Minute, 0);
            //DateTime e = new DateTime(End.Year, End.Month, End.Day, End.Hour, End.Minute, 0);
            DateTime b = Begin;
            DateTime e = End;
            TimeSpan span = new TimeSpan(e.Ticks - b.Ticks);
            if (span.Days <= 0 && span.Hours <= 0)
            {
                return string.Format("{0}{1}{2}{3}", span.Minutes, Resouce.Resource1.Minute, span.Seconds, Resouce.Resource1.Second);
            }
            else
            {
                return string.Format("{0}{1}{2}{3}{4}{5}", span.Days * 24 + span.Hours, Resouce.Resource1.Hour, span.Minutes, Resouce.Resource1.Minute, span.Seconds, Resouce.Resource1.Second);
            }
        }

        /// <summary>
        /// 日期时间是否在时间段中
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool IsInDatetimeInterval(DateTime dt)
        {
            bool ret = false;
            if (dt >= Begin && dt <= End)
            {
                ret = true;
            }
            return ret;
        }
        #endregion
    }
}
