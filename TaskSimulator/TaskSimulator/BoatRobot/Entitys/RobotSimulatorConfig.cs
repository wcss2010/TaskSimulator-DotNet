using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulator.Util;

namespace TaskSimulator.BoatRobot.Entitys
{
    /// <summary>
    /// 机器人模拟器配置
    /// </summary>
    [Serializable]
    public class RobotSimulatorConfig
    {
        public RobotSimulatorConfig()
        {
            Robots = new Robot[0];
            SocketController = new DynamicComponent();
            MonitorComponentMap = new SerializableDictionary<string, DynamicComponent>();
            TaskComponentMap = new SerializableDictionary<string, DynamicComponent>();
        }

        /// <summary>
        /// 机器人列表
        /// </summary>
        public Robot[] Robots { get; set; }

        /// <summary>
        /// Socket控制器
        /// </summary>
        public DynamicComponent SocketController { get; set; }

        /// <summary>
        /// 监视器动态组件列表(Key=MonitorId,Value=.cs定义)
        /// </summary>
        public SerializableDictionary<string, DynamicComponent> MonitorComponentMap { get; set; }

        /// <summary>
        /// 任务动态组件列表(Key=TaskId,Value=.cs定义)
        /// </summary>
        public SerializableDictionary<string, DynamicComponent> TaskComponentMap { get; set; }
    }
}