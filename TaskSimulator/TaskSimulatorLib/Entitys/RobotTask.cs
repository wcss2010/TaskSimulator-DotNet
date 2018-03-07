using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulatorLib.Entitys
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于描述无人船的自主任务
     * 
     */
    public class RobotTask
    {
        /// <summary>
        /// 任务代码
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 任务处理器(用于生成Command)
        /// </summary>
        public ITaskWorkerThread TaskWorkerThread { get; set; }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 参数对象(key=参数名，Value=参数值)
        /// </summary>
        public Dictionary<string, object> Objects
        {
            get { return objects; }
        }

        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}