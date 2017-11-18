using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulatorLib.Extends.Base
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类是一个任务工作线程抽象类
     * 
     */
    public abstract class BaseTaskWorkerThread : ITaskWorkerThread
    {
        public Entitys.Task Task { get; set; }

        public Entitys.DeviceUser User { get; set; }

        public abstract Entitys.CommandResult Process(Entitys.Command commandObj);

        public ITaskWorkerThread Clone()
        {
            return (ITaskWorkerThread)this.MemberwiseClone();
        }

        public object Tag { get; set; }
    }
}