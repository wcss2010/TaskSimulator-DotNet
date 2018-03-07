using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Monitors;

namespace TaskSimulatorLib.Entitys
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于这艘无人船的基本用户信息(包括支持什么样的任务，起始坐标等)
     * 
     */
    public class RobotUser
    {
        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 参数对象(key=参数名，Value=参数值)
        /// </summary>
        public Dictionary<string, object> Objects
        {
            get { return objects; }
        }

        ConcurrentDictionary<string, RobotTask> supportedTask = new ConcurrentDictionary<string, RobotTask>();
        /// <summary>
        /// 所支持的任务
        /// </summary>
        public ConcurrentDictionary<string, RobotTask> SupportedTask
        {
            get { return supportedTask; }
        }

        ConcurrentDictionary<string, IMonitor> supportedMonitor = new ConcurrentDictionary<string, IMonitor>();
        /// <summary>
        /// 所支持的监视器
        /// </summary>
        public ConcurrentDictionary<string, IMonitor> SupportedMonitor
        {
            get { return supportedMonitor; }
        }

        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}