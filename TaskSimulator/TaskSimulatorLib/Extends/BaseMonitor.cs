﻿using System;
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
     *  本类是一个监视器抽象类
     * 
     */
    public abstract class BaseMonitor:IMonitor
    {
        public Entitys.DeviceUser User { get; set; }

        public abstract Entitys.CommandResult Process(Entitys.Command commandObj);

        public IMonitor Clone()
        {
            return (IMonitor)this.MemberwiseClone();
        }

        public object Tag { get; set; }
    }
}