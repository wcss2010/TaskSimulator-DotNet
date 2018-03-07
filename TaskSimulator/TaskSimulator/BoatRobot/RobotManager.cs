using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.BoatRobot.Entitys;

namespace TaskSimulator.BoatRobot
{
    /// <summary>
    /// 无人船管理器
    /// 作者:李文龙
    /// 
    /// 主要用于管理系统中的无人船
    /// </summary>
    public class RobotManager
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ROBOT_CONFIG_FILENAME = "config.xml";

        private static RobotSimulatorConfig simulatorConfig = null;
        /// <summary>
        /// 机器人配置文件
        /// </summary>
        public static RobotSimulatorConfig SimulatorConfig
        {
            get { return RobotManager.simulatorConfig; }
            set { RobotManager.simulatorConfig = value; }
        }

        /// <summary>
        /// 载入配置文件
        /// </summary>
        public static void LoadConfig()
        {
            SimulatorConfig = XmlSerializeTool.Deserialize<RobotSimulatorConfig>(File.ReadAllText(Path.Combine(Application.StartupPath, ROBOT_CONFIG_FILENAME)));
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void SaveConfig()
        {
            if (SimulatorConfig == null)
            {
                return;
            }

            string xml = XmlSerializeTool.Serializer<RobotSimulatorConfig>(SimulatorConfig);
            if (string.IsNullOrEmpty(xml))
            {
                return;
            }
            else
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, ROBOT_CONFIG_FILENAME), xml);
            }
        }
    }
}
