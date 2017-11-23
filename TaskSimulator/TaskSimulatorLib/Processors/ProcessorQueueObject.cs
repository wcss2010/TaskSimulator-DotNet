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
        public ProcessorQueueObject() { }

        public ProcessorQueueObject(RobotUser u, RobotTask t, Command c)
        {
            this.User = u;
            this.Task = t;
            this.Command = c;
        }

        /// <summary>
        /// 所属任务
        /// </summary>
        public RobotTask Task { get; set; }

        /// <summary>
        /// 要执行的指令参数
        /// </summary>
        public Command Command { get; set; }

        /// <summary>
        /// 所属设备用户
        /// </summary>
        public RobotUser User { get; set; }
    }
}