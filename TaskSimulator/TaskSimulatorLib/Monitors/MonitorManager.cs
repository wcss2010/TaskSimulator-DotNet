using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Monitors
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于管理每艘无人船上所有的监视器
     * 
     */
    public class MonitorManager
    {
        private static SortedList<User, SortedList<string, IMonitor>> monitors = new SortedList<User, SortedList<string, IMonitor>>();
        /// <summary>
        /// 所有无人船的监视器(key=无人船用户，Value=监视器列表(key=监视器名称,Value=监视器))
        /// </summary>
        public static SortedList<User, SortedList<string, IMonitor>> Monitors
        {
            get { return MonitorManager.monitors; }
        }

        /// <summary>
        /// 注册监视器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="monitorCode"></param>
        /// <param name="monitor"></param>
        public static void RegisterMonitor(User user, string monitorCode, IMonitor monitor)
        {
        
        }

        /// <summary>
        /// 反注册监视器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="monitorCode"></param>
        public static void UnRegisterMonitor(User user, string monitorCode)
        {

        }

        /// <summary>
        /// 获得一个监视器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="monitorCode"></param>
        /// <returns></returns>
        public static IMonitor GetMonitor(User user, string monitorCode)
        {
            return null;
        }
    }
}