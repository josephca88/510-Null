﻿using System.IO;
using System.Data.SqlClient ;
using System.Data.Linq.Mapping;
using Ralid.Park .BusinessModel.Configuration;

namespace Ralid.Park.DAL.LinqDataProvider
{
    class ParkDataContextFactory
    {
        private static SqlConnection _Connection;

        public static ParkDataContext CreateParking(string connStr)
        {
            //System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(connStr), "没有找到有效的数据库连接!");
            if (string.IsNullOrEmpty(connStr)) return null;
            Stream stream = typeof(ParkDataContextFactory).Assembly.GetManifestResourceStream("Ralid.Park.DAL.LinqDataProvider.ParkingMapping.xml");
            MappingSource mappingSource = XmlMappingSource.FromStream(stream);
            ParkDataContext parking = new ParkDataContext(connStr, mappingSource);
            //parking.Log = System.Console.Out;
            return parking;
        }
    }
}
