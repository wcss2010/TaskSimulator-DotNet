using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Monitors;

namespace TaskSimulatorLib.Extends
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于描述一个指令
     * 
     */
    public class GpsMapMonitor:IMonitor
    {
        public Entitys.DeviceUser User { get; set; }

        public Entitys.CommandResult Process(Entitys.Command commandObj)
        {
            
        }

        public IMonitor Clone()
        {
            return (IMonitor)this.MemberwiseClone();
        }

        public object Tag { get; set; }
    }
}