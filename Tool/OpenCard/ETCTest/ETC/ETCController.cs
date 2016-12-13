﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCController
    {
        public ETCDevice[] ETCDevices { get; set; }

        #region 公共方法
        public bool Init()
        {
            try
            {
                ETCDevices = null;
                StringBuilder pRet = new StringBuilder(100 * 1000);
                StringBuilder err = new StringBuilder(1000);
                int count = 0;
                var n = ETCInterop.Initialize(pRet, ref count, err);
                if (n != 0) return false;
                if (n == 0 && count > 0)
                {
                    var str = pRet.ToString().Trim();
                    ETCDevices = JsonConvert.DeserializeObject<ETCDevice[]>(str);
                    if (ETCDevices != null && ETCDevices.Length > 0)
                    {
                        foreach (var device in ETCDevices)
                        {
                            //device.IsExit = true;
                            device.Init();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return false;
        }

        public int HeartBeatEx(string laneNo)
        {
            return ETCInterop.HeartBeat(int.Parse(laneNo));
        }

        public void UnInit()
        {
            ETCDevices = null;
            ETCInterop.Uninstall();
        }
        #endregion
    }
}
