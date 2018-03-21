using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib.Entitys
{
    /// <summary>
    /// 机器人模拟器配置
    /// </summary>
    [Serializable]
    public class RobotSimulatorConfig
    {
        public RobotSimulatorConfig()
        {
            Robots = new SerializableDictionary<string, Robot>();
            
            SocketController = new DynamicComponent();
            SocketController.ComponentType = DynamicComponentType.SocketController;
            SocketController.ComponentId = "SocketController";
            SocketController.ComponentName = "Socket控制器";

            MonitorComponentMap = new SerializableDictionary<string, DynamicComponent>();
            TaskComponentMap = new SerializableDictionary<string, DynamicComponent>();
        }

        /// <summary>
        ///  编译所需DLL引用
        /// </summary>
        public string[] RefDLL { get; set; }

        /// <summary>
        /// 机器人字典(Key=RobotId,Value=RobotConfig)
        /// </summary>
        public SerializableDictionary<string, Robot> Robots { get; set; }

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