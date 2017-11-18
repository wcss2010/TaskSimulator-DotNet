using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulatorLib.Extends.Base
{
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