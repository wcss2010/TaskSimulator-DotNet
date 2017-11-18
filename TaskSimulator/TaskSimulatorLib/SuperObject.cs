using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib
{
    public class SuperObject
    {
       public static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SuperObject));

       public static log4net.ILog GetLogger(Type t)
       {
           return log4net.LogManager.GetLogger(t);
       }

       ConcurrentDictionary<string, DeviceUser> userDict = new ConcurrentDictionary<string, DeviceUser>();
       /// <summary>
       /// 无人船用户字典
       /// </summary>
       public ConcurrentDictionary<string, DeviceUser> UserDict
       {
           get { return userDict; }
       }
    }
}