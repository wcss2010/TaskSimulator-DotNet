using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors
{
    /// <summary>
    /// 处理队列对象
    /// </summary>
    public class ProcessorQueueObject
    {
        /// <summary>
        /// 所属任务
        /// </summary>
        public Entitys.Task Task { get; set; }

        /// <summary>
        /// 要执行的指令参数
        /// </summary>
        public Entitys.Command Command { get; set; }

        /// <summary>
        /// 所属设备用户
        /// </summary>
        public DeviceUser User { get; set; }
    }
}