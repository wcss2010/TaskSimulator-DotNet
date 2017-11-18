using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors.Command;

namespace TaskSimulatorLib.Extends.Base
{
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