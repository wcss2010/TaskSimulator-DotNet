using CSScriptLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.BoatRobot.Entitys;
using TaskSimulatorLib;
using TaskSimulatorLib.Monitors;
using TaskSimulatorLib.Processors.Action;
using TaskSimulatorLib.Processors.Task;

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

        /// <summary>
        /// 动态组件目录
        /// </summary>
        public const string ROBOT_DYNAMIC_COMPONENT_DIR = "components";

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

        /// <summary>
        /// 初始化机器人管理器
        /// </summary>
        public static void Init()
        {
            if (SimulatorConfig != null)
            {
                Dictionary<string, ITaskWorkerThread> taskDict = new Dictionary<string, ITaskWorkerThread>();
                Dictionary<string, IMonitor> monitorDict = new Dictionary<string, IMonitor>();

                #region Socket控制器编译选项
                //SimulatorConfig.SocketController;

                #endregion

                #region Task控制器编译选项
                if (SimulatorConfig.TaskComponentMap != null)
                {
                    foreach (DynamicComponent kvp in SimulatorConfig.TaskComponentMap.Values)
                    {
                        if (string.IsNullOrEmpty(kvp.ComponentId) || string.IsNullOrEmpty(kvp.ComponentName) || string.IsNullOrEmpty(kvp.ComponentClassFullName) || string.IsNullOrEmpty(kvp.ComponentClassFile))
                        {
                            continue;
                        }
                        else
                        {
                            string classFile = kvp.ComponentClassFile;
                            if (kvp.ComponentClassFile.StartsWith("./"))
                            {
                                //需要修饰一下目录
                                classFile = Path.Combine(Application.StartupPath, Path.Combine(ROBOT_DYNAMIC_COMPONENT_DIR, kvp.ComponentClassFile.Replace("./", string.Empty)));
                            }

                            if (File.Exists(classFile))
                            {
                                try
                                {
                                   Assembly result = CSScript.Evaluator.CompileCode(File.ReadAllText(classFile));
                                   Type objType = result.GetType(kvp.ComponentClassFullName);

                                   Type[] faceTypes = objType.GetInterfaces();
                                   foreach (Type interfaceType in faceTypes)
                                   {
                                       if (interfaceType.FullName.Equals(typeof(ITaskWorkerThread).FullName))
                                       {
                                           taskDict.Add(kvp.ComponentId, (ITaskWorkerThread)objType.GetConstructor(Type.EmptyTypes).Invoke(null));
                                           break;
                                       }
                                   }
                                   
                                }
                                catch (Exception ex)
                                {
                                    SimulatorObject.logger.Error(ex.ToString());
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Monitor控制器编译选项
                if (SimulatorConfig.MonitorComponentMap != null)
                {
                    foreach (DynamicComponent kvp in SimulatorConfig.MonitorComponentMap.Values)
                    {
                        if (string.IsNullOrEmpty(kvp.ComponentId) || string.IsNullOrEmpty(kvp.ComponentName) || string.IsNullOrEmpty(kvp.ComponentClassFullName) || string.IsNullOrEmpty(kvp.ComponentClassFile))
                        {
                            continue;
                        }
                        else
                        {
                            string classFile = kvp.ComponentClassFile;
                            if (kvp.ComponentClassFile.StartsWith("./"))
                            {
                                //需要修饰一下目录
                                classFile = Path.Combine(Application.StartupPath, Path.Combine(ROBOT_DYNAMIC_COMPONENT_DIR, kvp.ComponentClassFile.Replace("./", string.Empty)));
                            }

                            if (File.Exists(classFile))
                            {
                                try
                                {
                                    Assembly result = CSScript.Evaluator.CompileCode(File.ReadAllText(classFile));
                                    Type objType = result.GetType(kvp.ComponentClassFullName);

                                    Type[] faceTypes = objType.GetInterfaces();
                                    foreach (Type interfaceType in faceTypes)
                                    {
                                        if (interfaceType.FullName.Equals(typeof(IMonitor).FullName))
                                        {
                                            monitorDict.Add(kvp.ComponentId, (IMonitor)objType.GetConstructor(Type.EmptyTypes).Invoke(null));
                                            break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    SimulatorObject.logger.Error(ex.ToString());
                                }
                            }
                        }
                    }
                }
                #endregion

            }
        }
    }
}
