using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors.Action;

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
    public abstract class BaseActionWorkerThread : IActionWorkerThread
    {
        public string Cmd { get; set; }

        public Entitys.RobotTask Task { get; set; }

        public Entitys.RobotUser User { get; set; }

        public abstract Entitys.CommandResult Process(Entitys.Command commandObj);

        public IActionWorkerThread Clone()
        {
            return (IActionWorkerThread)this.MemberwiseClone();
        }

        public object Tag { get; set; }
    }
}