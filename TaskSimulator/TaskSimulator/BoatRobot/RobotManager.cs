using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.BoatRobot.Components;
using TaskSimulator.Util;
using TaskSimulatorLib;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Monitors;
using TaskSimulatorLib.Processors.Task;
using TaskSimulatorLib.Sockets;

namespace TaskSimulator.BoatRobot
{
    public delegate void UIActionDelegate(object sender, UIActionEventArgs args);

    public class UIActionEventArgs : EventArgs
    {
        public string ActionName { get; set; }

        public RobotUser User { get; set; }

        public RobotTask Task { get; set; }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 参数对象(key=参数名，Value=参数值)
        /// </summary>
        public Dictionary<string, object> Objects
        {
            get { return objects; }
        }
    }

    /// <summary>
    /// 无人船管理器
    /// 作者:李文龙
    /// 
    /// 主要用于管理系统中的无人船
    /// </summary>
    public class RobotManager
    {
        /// <summary>
        /// 船模拟航行任务
        /// </summary>
        public const string Task_BoatFly = "Task_BoatFly";

        /// <summary>
        /// GPS监视器
        /// </summary>
        public const string Monitor_GPS = "Monitor_GPS";

        /// <summary>
        /// UI动作移动小船到新坐标
        /// </summary>
        public const string UIAction_Move = "Move";

        /// <summary>
        /// 动态组件目录
        /// </summary>
        public const string ROBOT_DYNAMIC_COMPONENT_DIR = "componentFiles";

        /// <summary>
        /// 动态组件第三方库的存放位置
        /// </summary>
        public const string ROBOT_DYNAMIC_DLLFILES_DIR = "dllFiles";

        /// <summary>
        /// 资源文件
        /// </summary>
        public const string ROBOT_DYNAMIC_RESFILES_DIR = "resFiles";

        /// <summary>
        /// 摄像头ID前缀
        /// </summary>
        public const string CAMERA_ID_HEAD = "VirtualCamera-";

        public static TaskSimulatorLib.Entitys.RobotSimulatorConfig SimulatorConfig
        {
            get { return SimulatorObject.Simulator.SimulatorConfig; }
        }

        public static event UIActionDelegate OnUiActionEvent;
        public static void OnUiAction(string actionName, RobotUser user, RobotTask task, KeyValuePair<string, object>[] dataTeam)
        {
            if (OnUiActionEvent != null)
            {
                UIActionEventArgs eventargs = new UIActionEventArgs();
                eventargs.ActionName = actionName;
                eventargs.User = user;
                eventargs.Task = task;

                if (dataTeam != null)
                {
                    foreach (KeyValuePair<string, object> kvp in dataTeam)
                    {
                        eventargs.Objects.Add(kvp.Key, kvp.Value);
                    }
                }

                OnUiActionEvent(null, eventargs);
            }
        }

        /// <summary>
        /// 初始化无人船管理器
        /// </summary>
        public static void Init()
        {
            if (SimulatorConfig != null)
            {
                Dictionary<string, ITaskProcessorThread> taskDict = new Dictionary<string, ITaskProcessorThread>();
                Dictionary<string, IMonitor> monitorDict = new Dictionary<string, IMonitor>();
                ISocketController robotSocketTemp = null;
                Assembly compileResult = null;
                List<string> codeFiles = new List<string>();
                string currentClassFullName = string.Empty;

                #region 收集需要编译的.cs文件
                string codeFile = string.Empty;

                if (string.IsNullOrEmpty(SimulatorConfig.SocketController.ComponentClassFullName) || string.IsNullOrEmpty(SimulatorConfig.SocketController.ComponentClassFile))
                {
                    SimulatorObject.logger.Error("对不起，Socket控制器不存在！");
                    SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起，Socket控制器不存在！");
                    return;
                }
                else
                {
                    //Socket控制器代码
                    codeFile = SimulatorConfig.SocketController.ComponentClassFile;
                    if (codeFile.StartsWith("./"))
                    {
                        //需要修饰一下目录
                        codeFile = Path.Combine(Application.StartupPath, Path.Combine(ROBOT_DYNAMIC_COMPONENT_DIR, codeFile.Replace("./", string.Empty)));
                    }
                    if (File.Exists(codeFile))
                    {
                        codeFiles.Add(codeFile);
                    }
                    else
                    {
                        SimulatorObject.logger.Error("对不起，类文件(" + codeFile + ")没有找到!");
                        if (SimulatorObject.ConsoleLoggerWindow != null)
                        {
                            SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起，类文件(" + codeFile + ")没有找到!");
                        }
                    }

                    //Monitor代码
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
                                codeFile = kvp.ComponentClassFile;
                                if (codeFile.StartsWith("./"))
                                {
                                    //需要修饰一下目录
                                    codeFile = Path.Combine(Application.StartupPath, Path.Combine(ROBOT_DYNAMIC_COMPONENT_DIR, codeFile.Replace("./", string.Empty)));
                                }
                                if (File.Exists(codeFile))
                                {
                                    codeFiles.Add(codeFile);
                                }
                                else
                                {
                                    SimulatorObject.logger.Error("对不起，类文件(" + codeFile + ")没有找到!");
                                    if (SimulatorObject.ConsoleLoggerWindow != null)
                                    {
                                        SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起，类文件(" + codeFile + ")没有找到!");
                                    }
                                }
                            }
                        }
                    }

                    //Task代码
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
                                codeFile = kvp.ComponentClassFile;
                                if (codeFile.StartsWith("./"))
                                {
                                    //需要修饰一下目录
                                    codeFile = Path.Combine(Application.StartupPath, Path.Combine(ROBOT_DYNAMIC_COMPONENT_DIR, codeFile.Replace("./", string.Empty)));
                                }
                                if (File.Exists(codeFile))
                                {
                                    codeFiles.Add(codeFile);
                                }
                                else
                                {
                                    SimulatorObject.logger.Error("对不起，类文件(" + codeFile + ")没有找到!");
                                    if (SimulatorObject.ConsoleLoggerWindow != null)
                                    {
                                        SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起，类文件(" + codeFile + ")没有找到!");
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                //编译.cs文件
                compileResult = CSharpCompiler.Compile(SimulatorConfig.RefDLL, codeFiles.ToArray());

                #region Socket控制器编译选项
                try
                {
                    currentClassFullName = SimulatorConfig.SocketController.ComponentClassFullName;

                    Type objType = compileResult.GetType(currentClassFullName);
                    if (objType != null)
                    {
                        Type[] faceTypes = objType.GetInterfaces();
                        foreach (Type interfaceType in faceTypes)
                        {
                            if (interfaceType.FullName.Equals(typeof(ISocketController).FullName))
                            {
                                robotSocketTemp = (ISocketController)objType.GetConstructor(Type.EmptyTypes).Invoke(null);
                                break;
                            }
                        }
                    }
                    else
                    {
                        SimulatorObject.logger.Error("对不起,类(" + currentClassFullName + ")没有找到！");

                        if (SimulatorObject.ConsoleLoggerWindow != null)
                        {
                            SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起,类(" + currentClassFullName + ")没有找到！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SimulatorObject.logger.Error(ex.ToString());

                    if (SimulatorObject.ConsoleLoggerWindow != null)
                    {
                        SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(ex.ToString());
                    }
                }
                #endregion

                #region Monitor控制器编译选项
                if (SimulatorConfig.MonitorComponentMap != null)
                {
                    foreach (DynamicComponent kvp in SimulatorConfig.MonitorComponentMap.Values)
                    {
                        try
                        {
                            currentClassFullName = kvp.ComponentClassFullName;

                            Type objType = compileResult.GetType(currentClassFullName);

                            if (objType != null)
                            {
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
                            else
                            {
                                SimulatorObject.logger.Error("对不起,类(" + currentClassFullName + ")没有找到！");

                                if (SimulatorObject.ConsoleLoggerWindow != null)
                                {
                                    SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起,类(" + currentClassFullName + ")没有找到！");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SimulatorObject.logger.Error(ex.ToString());

                            if (SimulatorObject.ConsoleLoggerWindow != null)
                            {
                                SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(ex.ToString());
                            }
                        }

                    }
                }
                #endregion

                #region Task控制器编译选项
                if (SimulatorConfig.TaskComponentMap != null)
                {
                    foreach (DynamicComponent kvp in SimulatorConfig.TaskComponentMap.Values)
                    {
                        try
                        {
                            currentClassFullName = kvp.ComponentClassFullName;

                            Type objType = compileResult.GetType(currentClassFullName);

                            if (objType != null)
                            {
                                Type[] faceTypes = objType.GetInterfaces();
                                foreach (Type interfaceType in faceTypes)
                                {
                                    if (interfaceType.FullName.Equals(typeof(ITaskProcessorThread).FullName))
                                    {
                                        taskDict.Add(kvp.ComponentId, (ITaskProcessorThread)objType.GetConstructor(Type.EmptyTypes).Invoke(null));
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                SimulatorObject.logger.Error("对不起,类(" + currentClassFullName + ")没有找到！");

                                if (SimulatorObject.ConsoleLoggerWindow != null)
                                {
                                    SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("对不起,类(" + currentClassFullName + ")没有找到！");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SimulatorObject.logger.Error(ex.ToString());

                            if (SimulatorObject.ConsoleLoggerWindow != null)
                            {
                                SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(ex.ToString());
                            }
                        }

                    }
                }
                #endregion

                #region 生成无人船列表
                if (SimulatorConfig.RobotMap != null)
                {
                    foreach (Robot rb in SimulatorConfig.RobotMap.Values)
                    {
                        try
                        {
                            //创建新的无人船
                            RobotUser curUser = new RobotUser();
                            curUser.UserCode = rb.RobotId;
                            curUser.UserName = rb.RobotName;

                            //检查Socket是否可用
                            if (robotSocketTemp != null)
                            {
                                //克隆一个Socket对象
                                curUser.SocketController = (ISocketController)robotSocketTemp.Clone();

                                //设置所属用户
                                curUser.SocketController.User = curUser;

                                //初始化Socket
                                curUser.SocketController.Init(rb);
                            }

                            #region 加载无人船默认的模块
                            //加载摄像头
                            int cameraIndex = 0;
                            foreach (string cameraName in rb.CameraNames)
                            {
                                cameraIndex++;

                                CameraMonitor cm = new CameraMonitor();
                                cm.Name = cameraName;
                                cm.Enabled = true;
                                cm.User = curUser;
                                cm.VirtualCameraImageWidth = rb.CameraPictureWidth;
                                cm.VirtualCameraImageHeight = rb.CameraPictureHeight;
                                cm.VirtualCameraImageFont = new Font(rb.CameraHintTextFontName, rb.CameraHintTextFontSize);
                                cm.VirtualCameraBackgroundImages = rb.CameraBackgrounds;

                                curUser.SupportedMonitor.TryAdd(CAMERA_ID_HEAD + cameraIndex, cm);
                            }

                            //加载GPS监视器
                            GPSMonitor gpsmonitor = new GPSMonitor();
                            gpsmonitor.Enabled = true;
                            gpsmonitor.Name = "GPS监视器";
                            gpsmonitor.User = curUser;
                            gpsmonitor.GPSCurrent = rb.DefaultGpsPos;

                            curUser.SupportedMonitor.TryAdd(Monitor_GPS, gpsmonitor);

                            //加载行动任务
                            RobotTask boatFly = new RobotTask();
                            boatFly.TaskCode = Task_BoatFly;
                            boatFly.TaskName = "自主航行任务";
                            boatFly.Enabled = true;
                            boatFly.TaskProcessorThread = new BoatFlyTask();
                            ((BoatFlyTask)boatFly.TaskProcessorThread).StepWithSecond = rb.StepWithSecond;
                            ((BoatFlyTask)boatFly.TaskProcessorThread).Task = boatFly;
                            ((BoatFlyTask)boatFly.TaskProcessorThread).User = curUser;
                            ((BoatFlyTask)boatFly.TaskProcessorThread).WorkerThreadState = WorkerThreadStateType.Ready;

                            curUser.SupportedTask.TryAdd(boatFly.TaskCode, boatFly);
                            #endregion

                            #region 加载自定义模块
                            //监视器
                            foreach (DynamicComponent dc in SimulatorConfig.MonitorComponentMap.Values)
                            {
                                if (monitorDict.ContainsKey(dc.ComponentId))
                                {
                                    curUser.SupportedMonitor.TryAdd(dc.ComponentId, (IMonitor)monitorDict[dc.ComponentId].Clone());

                                    curUser.SupportedMonitor[dc.ComponentId].Name = dc.ComponentName;
                                    curUser.SupportedMonitor[dc.ComponentId].User = curUser;
                                    curUser.SupportedMonitor[dc.ComponentId].Enabled = rb.MonitorStateMap.ContainsKey(dc.ComponentId) ? rb.MonitorStateMap[dc.ComponentId] : true;
                                }
                            }

                            //任务处理器
                            foreach (DynamicComponent dc in SimulatorConfig.TaskComponentMap.Values)
                            {
                                if (taskDict.ContainsKey(dc.ComponentId))
                                {
                                    RobotTask rt = new RobotTask();
                                    rt.TaskCode = dc.ComponentId;
                                    rt.TaskName = dc.ComponentName;
                                    rt.TaskProcessorThread = (ITaskProcessorThread)taskDict[dc.ComponentId].Clone();
                                    rt.TaskProcessorThread.WorkerThreadState = WorkerThreadStateType.Ready;
                                    rt.TaskProcessorThread.User = curUser;
                                    rt.TaskProcessorThread.Task = rt;
                                    rt.Enabled = rb.TaskStateMap.ContainsKey(dc.ComponentId) ? rb.TaskStateMap[dc.ComponentId] : true;

                                    curUser.SupportedTask.TryAdd(dc.ComponentId, rt);
                                }
                            }
                            #endregion

                            //添加用户
                            SimulatorObject.Simulator.UserDict.TryAdd(curUser.UserCode, curUser);
                        }
                        catch (Exception ex)
                        {
                            SimulatorObject.logger.Error(ex.ToString());

                            if (SimulatorObject.ConsoleLoggerWindow != null)
                            {
                                SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(ex.ToString());
                            }
                        }
                    }
                }
                #endregion
            }
        }
    }
}