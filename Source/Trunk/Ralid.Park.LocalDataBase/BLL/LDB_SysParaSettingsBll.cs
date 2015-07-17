﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.DAL.IDAL;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.Park.LocalDataBase.Model;
using Ralid.Park.LocalDataBase.DAL.IDAL;

namespace Ralid.Park.LocalDataBase.BLL
{
    /// <summary>
    ///用于从SysParameter表中保存或获取一些系统设置,这些设置的实例被序列化为XML字串保存在ParameterValue字段中
    /// </summary>
    public class LDB_SysParaSettingsBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public LDB_SysParaSettingsBll(string repoUri)
        {
            _RepoUri = repoUri;
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        private IUnitWork _unitWork;
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取事物操作
        /// </summary>
        private IUnitWork unitWork
        {
            get
            {
                if (_unitWork == null)
                {
                    _unitWork = LDB_ProviderFactory.Create<IUnitWork>(_RepoUri);
                }
                return _unitWork;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        ///保存到数据库
        /// </summary>
        /// <param name="info"></param>
        public CommandResult SaveSetting<T>(T info) where T : class
        {
            try
            {
                LDB_SystemParameterBll bll = new LDB_SystemParameterBll(_RepoUri);
                Type t = typeof(T);
                LDB_SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new LDB_SysparameterInfo(t.Name, value, string.Empty);
                    }
                }
                else
                {
                    para = new LDB_SysparameterInfo(t.Name, string.Empty, string.Empty);
                }
                return bll.Insert(para);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        ///使用事物保存到数据库，使用事物是为了访问数据库的次数，多次访问数据会导致操作时间过长
        /// </summary>
        /// <param name="info"></param>
        public void SaveSettingWithUnitWork<T>(T info) where T : class
        {
            try
            {
                Type t = typeof(T);
                LDB_SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new LDB_SysparameterInfo(t.Name, value, string.Empty);
                    }
                }
                else
                {
                    para = new LDB_SysparameterInfo(t.Name, string.Empty, string.Empty);
                }
                LDB_ISysParameterProvider provider = LDB_ProviderFactory.Create<LDB_ISysParameterProvider>(_RepoUri);
                provider.Insert(para, unitWork);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 执行事物操作
        /// </summary>
        /// <returns></returns>
        public CommandResult UnitWorkCommit()
        {
            try
            {
                return unitWork.Commit();
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }


        /// <summary>
        /// 从数据库中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSetting<T>() where T : class
        {
            try
            {
                LDB_SystemParameterBll bll = new LDB_SystemParameterBll(_RepoUri);
                LDB_SysparameterInfo para = null;
                QueryResult<LDB_SysparameterInfo> result = null;

                Type t = typeof(T);
                result = bll.GetSysParameterByID(t.Name);

                if (result.Result == ResultCode.Successful)
                {
                    para = result.QueryObject;
                    string value = para.ParameterValue;
                    if (!string.IsNullOrEmpty(value))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(value);
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            stream.Position = 0;
                            DataContractSerializer ser = new DataContractSerializer(t);
                            return ser.ReadObject(stream) as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 从持久层获取设置，如果不存在就创建一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOrCreateSetting<T>() where T : class ,new()
        {
            T t = GetSetting<T>();

            if (t == null)
            {
                t = new T();
            }
            return t;
        }

        /// <summary>
        ///保存到数据库
        /// </summary>
        /// <param name="info"></param>
        public CommandResult SaveSetting<T>(T info,string name) where T : class
        {
            try
            {
                LDB_SystemParameterBll bll = new LDB_SystemParameterBll(_RepoUri);
                Type t = typeof(T);
                LDB_SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new LDB_SysparameterInfo(name, value, string.Empty);
                    }
                }
                else
                {
                    para = new LDB_SysparameterInfo(t.Name, string.Empty, string.Empty);
                }
                return bll.Insert(para);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 从数据库中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSetting<T>(string name) where T : class
        {
            try
            {
                LDB_SystemParameterBll bll = new LDB_SystemParameterBll(_RepoUri);
                LDB_SysparameterInfo para = null;
                QueryResult<LDB_SysparameterInfo> result = null;

                Type t = typeof(T);
                result = bll.GetSysParameterByID(name);

                if (result.Result == ResultCode.Successful)
                {
                    para = result.QueryObject;
                    string value = para.ParameterValue;
                    if (!string.IsNullOrEmpty(value))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(value);
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            stream.Position = 0;
                            DataContractSerializer ser = new DataContractSerializer(t);
                            return ser.ReadObject(stream) as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}
