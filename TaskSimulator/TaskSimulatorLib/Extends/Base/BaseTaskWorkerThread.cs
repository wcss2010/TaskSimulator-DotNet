using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;
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
        public RobotTask Task { get; set; }

        public RobotUser User { get; set; }

        public abstract Entitys.CommandResult Process(Entitys.Command commandObj);

        public ITaskWorkerThread Clone()
        {
            return (ITaskWorkerThread)this.MemberwiseClone();
        }

        public object Tag { get; set; }

        public virtual WorkerThreadStateType WorkerThreadState { get; set; }
    }
}