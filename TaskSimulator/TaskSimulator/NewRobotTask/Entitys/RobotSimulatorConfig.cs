using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulator.Util;

namespace TaskSimulator.NewRobotTask.Entitys
{
    /// <summary>
    /// 机器人模拟器配置
    /// </summary>
    [Serializable]
    public class RobotSimulatorConfig
    {
        /// <summary>
        /// 机器人列表
        /// </summary>
        public Robot[] Robots { get; set; }

        
    }
}