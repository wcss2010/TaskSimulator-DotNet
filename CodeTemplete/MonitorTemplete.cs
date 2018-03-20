using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib;
using TaskSimulatorLib.Sockets;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;
using TaskSimulatorLib.Monitors;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Extends;
using TaskSimulatorLib.Processors.Task;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib.Templete
{
    public class MonitorTemplete : BaseMonitor
    {
        public override CommandResult Process(Command commandObj)
        {
            return new CommandResult("指令", true, string.Empty, null, new KeyValuePair<string, object>[] { });
        }
    }
}