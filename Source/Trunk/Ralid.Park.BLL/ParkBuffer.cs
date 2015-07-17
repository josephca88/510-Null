﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 表示一个停车场硬件信息的数据缓存
    /// </summary>
    public class ParkBuffer
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置应用程序当前的停车场缓存
        /// </summary>
        public static ParkBuffer Current { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 停车场缓存构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public ParkBuffer(string repoUri)
        {
            _RepoUri = repoUri;
            _Parks = new List<ParkInfo>();
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        private static List<ParkInfo> _Parks;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取所有的停车场
        /// </summary>
        public List<ParkInfo> Parks
        {
            get
            {
                return _Parks;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 缓存的数据已无效,从数据库中获取最新数据,使用构造函数时的数据库连接字符串
        /// </summary>
        public void InValid()
        {
            InValid(_RepoUri);
        }

        /// <summary>
        /// 缓存的数据已无效,从数据库中获取最新数据
        /// </summary>
        /// <param name="repoUri">数据库连接字符串</param>
        public void InValid(string repoUri)
        {
            ParkBll parkbll = new ParkBll(repoUri);
            List<ParkInfo> parkList = parkbll.GetAllParks().QueryObjects;
            parkList = (from p in parkList orderby p.ParkName ascending select p).ToList();

            EntranceBll entranceBll = new EntranceBll(repoUri);
            List<EntranceInfo> entrances = entranceBll.GetAllEntraces().QueryObjects;
            entrances = (from en in entrances orderby en.EntranceName ascending select en).ToList();

            VideoSourceBll videoBll = new VideoSourceBll(repoUri);
            List<VideoSourceInfo> videoSources = videoBll.GetAllVideoSources().QueryObjects;
            videoSources = (from v in videoSources orderby v.VideoName ascending select v).ToList();

            foreach (ParkInfo p in parkList)
            {
                if (p.ParentID == null)
                {
                    p.RootParkID = p.ParkID;
                    p.SubParks.Clear();
                    p.SubParks.AddRange(parkList.Where(d => d.ParentID == p.ParkID));
                    p.SubParks.ForEach(d => d.RootParkID = p.RootParkID);  //设置所有的子车场的顶层车场ID
                }
            }

            foreach (ParkInfo p in parkList)
            {
                p.Entrances.Clear();
                p.Entrances.AddRange(entrances.Where(e => e.ParkID == p.ParkID));
                p.Entrances.ForEach(e => e.RootParkID = p.RootParkID);  //设置所有的通道的顶层车场ID
            }

            foreach (EntranceInfo en in entrances)
            {
                en.VideoSources.Clear();
                en.VideoSources.AddRange(videoSources.Where(v => v.EntranceID == en.EntranceID));
            }
            _Parks = parkList;
        }

        /// <summary>
        /// 获取停车场信息
        /// </summary>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public ParkInfo GetPark(int parkID)
        {
            return _Parks.SingleOrDefault(p => p.ParkID == parkID);
        }

        /// <summary>
        /// 获取通道控制器信息
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public EntranceInfo GetEntrance(int entranceID)
        {
            EntranceInfo entrance = null;
            foreach (ParkInfo park in _Parks)
            {
                entrance = park.Entrances.SingleOrDefault(en => en.EntranceID == entranceID);
                if (entrance != null)
                {
                    break;
                }
            }
            return entrance;
        }

        /// <summary>
        /// 获取通道控制器信息
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <param name="entranceID">控制器ID</param>
        /// <returns></returns>
        public EntranceInfo GetEntrance(int parkID, int entranceID)
        {
            EntranceInfo entrance = null;
            ParkInfo park = _Parks.SingleOrDefault(p => p.ParkID == parkID);
            if (park != null)
            {
                entrance = park.Entrances.SingleOrDefault(en => en.EntranceID == entranceID);
            }
            return entrance;
        }

        /// <summary>
        /// 获取所有通道控制器信息
        /// </summary>
        /// <returns></returns>
        public List<EntranceInfo> GetEntrances()
        {
            List<EntranceInfo> entrances = new List<EntranceInfo>();
            foreach (ParkInfo park in _Parks)
            {
                entrances.AddRange(park.Entrances);
            }
            return entrances; 
        }

        /// <summary>
        /// 已通道ID获取通道名称，已逗号分开
        /// </summary>
        /// <param name="entranceIDs"></param>
        /// <returns></returns>
        public string GetEntrancesName(List<int> entranceIDs)
        {
            if (entranceIDs == null || entranceIDs.Count == 0) return string.Empty;

            List<string> entrances = new List<string>();
            foreach (ParkInfo park in _Parks)
            {
                foreach (EntranceInfo entrance in park.Entrances)
                {
                    if (entranceIDs.Any(en => en == entrance.EntranceID))
                    {
                        entrances.Add(entrance.EntranceName);
                    }
                }
            }
            return string.Join(",", entrances);
        }

        /// <summary>
        /// 查看控制器中是否与有工作站要侦听的控制器在同一个停车场
        /// </summary>
        /// <param name="park"></param>
        /// <returns></returns>
        public bool IsInSameParkList(List<int> entranceIDs1, List<int> entranceIDs2)
        {
            if (entranceIDs1 != null && entranceIDs1.Count > 0
                &&entranceIDs2 != null && entranceIDs2.Count > 0)
            {
                foreach (int id1 in entranceIDs1)
                {
                    if (entranceIDs2.Exists(e => e == id1))
                    {
                        return true;
                    }
                    EntranceInfo entrance1 = GetEntrance(id1);
                    if (entrance1 != null)
                    {
                        foreach (int id2 in entranceIDs2)
                        {
                            EntranceInfo entrance2 = GetEntrance(id2);
                            if (entrance2 != null && entrance1.ParkID == entrance2.ParkID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
