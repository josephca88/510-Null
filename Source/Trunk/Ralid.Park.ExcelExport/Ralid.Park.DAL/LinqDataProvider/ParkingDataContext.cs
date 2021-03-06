﻿using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ParkDataContext : System.Data.Linq.DataContext
    {
        public ParkDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
        }

        public System.Data.Linq.Table<AlarmInfo> Alarm
        {
            get
            {
                return this.GetTable<AlarmInfo>();
            }
        }

        public System.Data.Linq.Table<CarInfo> Car
        {
            get
            {
                return this.GetTable<CarInfo>();
            }
        }

        public System.Data.Linq.Table<CardChangeRecord> CardChange
        {
            get
            {
                return this.GetTable<CardChangeRecord>();
            }
        }

        public System.Data.Linq.Table<CardChargeRecord> CardCharge
        {
            get
            {
                return this.GetTable<CardChargeRecord>();
            }
        }

        public System.Data.Linq.Table<CardDeferRecord> CardDefer
        {
            get
            {
                return this.GetTable<CardDeferRecord>();
            }
        }

        public System.Data.Linq.Table<CardDisableEnableRecord> CardDisableEnable
        {
            get
            {
                return this.GetTable<CardDisableEnableRecord>();
            }
        }

        public System.Data.Linq.Table<CardEventRecord> CardEvent
        {
            get
            {
                return this.GetTable<CardEventRecord>();
            }
        }


        public System.Data.Linq.Table<CardLostRestoreRecord> CardLostRestore
        {
            get
            {
                return this.GetTable<CardLostRestoreRecord>();
            }
        }

        public System.Data.Linq.Table<CardRecycleRecord> CardRecycle
        {
            get
            {
                return this.GetTable<CardRecycleRecord>();
            }
        }

        public System.Data.Linq.Table<CardReleaseRecord> CardRelease
        {
            get
            {
                return this.GetTable<CardReleaseRecord>();
            }
        }

        public System.Data.Linq.Table<EntranceInfo> Entrance
        {
            get
            {
                return this.GetTable<EntranceInfo>();
            }
        }

        public System.Data.Linq.Table<OperatorLogInfo> OperatorLog
        {
            get
            {
                return this.GetTable<OperatorLogInfo>();
            }
        }

        public System.Data.Linq.Table<SysparameterInfo> Sysparameter
        {
            get
            {
                return this.GetTable<SysparameterInfo>();
            }
        }

        public System.Data.Linq.Table<VideoSourceInfo> VideoSource
        {
            get
            {
                return this.GetTable<VideoSourceInfo>();
            }
        }

        public System.Data.Linq.Table<WaitingCommandInfo> WaitingCommand
        {
            get
            {
                return this.GetTable<WaitingCommandInfo>();
            }
        }

        public System.Data.Linq.Table<WorkStationInfo> WorkStation
        {
            get
            {
                return this.GetTable<WorkStationInfo>();
            }
        }

        public System.Data.Linq.Table<ParkInfo> Park
        {
            get
            {
                return this.GetTable<ParkInfo>();
            }
        }

        public System.Data.Linq.Table<CardEventReport> EventReport
        {
            get
            {
                return this.GetTable<CardEventReport>();
            }
        }

        public System.Data.Linq.Table<SnapShot> SnapShot
        {
            get
            {
                return this.GetTable<SnapShot>();
            }
        }

        public System.Data.Linq.Table<CardInfo> Card
        {
            get
            {
                return this.GetTable<CardInfo>();
            }
        }

        public System.Data.Linq.Table<OperatorInfo> Operator
        {
            get
            {
                return this.GetTable<OperatorInfo>();
            }
        }

        public System.Data.Linq.Table<RoleInfo> Role
        {
            get
            {
                return this.GetTable<RoleInfo>();
            }
        }
    }
}