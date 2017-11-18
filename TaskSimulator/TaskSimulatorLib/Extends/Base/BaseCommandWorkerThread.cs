using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors.Command;

namespace TaskSimulatorLib.Extends.Base
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类是一个指令工作线程抽象类
     * 
     */
    public abstract class BaseCommandWorkerThread : ICommandWorkerThread
    {
        public string Cmd { get; set; }

        public Entitys.Task Task { get; set; }

        public Entitys.DeviceUser User { get; set; }

        public abstract Entitys.CommandResult Process(Entitys.Command commandObj);

        public ICommandWorkerThread Clone()
        {
            return (ICommandWorkerThread)this.MemberwiseClone();
        }

        public object Tag { get; set; }
    }
}