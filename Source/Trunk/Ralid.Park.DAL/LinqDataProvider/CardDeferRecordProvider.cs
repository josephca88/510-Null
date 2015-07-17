﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardDeferRecordProvider : ProviderBase<CardDeferRecord,string>, ICardDeferRecordProvider
    {
        public CardDeferRecordProvider()
        {
        }

        public CardDeferRecordProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override List<CardDeferRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition con = search as RecordSearchCondition;
                IQueryable<CardDeferRecord> result =parking.CardDefer;
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.DeferDateTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.DeferDateTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (con.Operator != null) result = result.Where(c => c.OperatorID == con.Operator.OperatorName);
                if (con.StationID != null && con.StationID != "") result = result.Where(c => c.StationID == con.StationID);
                if (!string.IsNullOrEmpty(con.CardID)) result = result.Where(c => c.CardID.Contains(con.CardID));
                if (!string.IsNullOrEmpty(con.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                if (!string.IsNullOrEmpty(con.OwnerName)) result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                if (!string.IsNullOrEmpty(con.CarPlate)) result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
                if (con.IsUnSettled != null)
                {
                    if (con.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (con.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == con.SettleDateTime.Value);
                if (con.CardType != null) result = result.Where(c => c._CardType == con.CardType.ID);
                return result.ToList();
            }
            return new List<CardDeferRecord>();
        }
        #endregion
    }
}
