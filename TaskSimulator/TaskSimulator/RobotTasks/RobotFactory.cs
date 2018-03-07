using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulator.RobotTasks
{


    /// <summary>
    /// 机器人工厂
    /// </summary>
    public class RobotFactory
    {
        /// <summary>
        /// 位置移动任务
        /// </summary>
        public const string Task_RobotMove = "RobotMoveTask";

        /// <summary>
        /// GPS监视器
        /// </summary>
        public const string Monitor_GPS = "GPSMonitor";

        /// <summary>
        /// 位置移动指令
        /// </summary>
        public const string Action_RebotMove = "RebotMove";

        /// <summary>
        /// UI动作移动小船到新坐标
        /// </summary>
        public const string UIAction_Move = "Move";

        /// <summary>
        /// 每秒移动步长
        /// </summary>
        public static double StepWithSecond = 0.15;

        /// <summary>
        /// 虚拟摄像头图片宽度
        /// </summary>
        public static int VirtualCameraImageWidth = 200;

        /// <summary>
        /// 虚拟摄像头图片高度
        /// </summary>
        public static int VirtualCameraImageHeight = 180;

        /// <summary>
        /// 虚拟摄像头图片背景色
        /// </summary>
        public static Color[] VirtualCameraImageBackgroundColors = new Color[] { Color.White, Color.Yellow, Color.Red, Color.CadetBlue, Color.Green, Color.Orange };

        /// <summary>
        /// 虚拟摄像头图片默认字体颜色
        /// </summary>
        public static Color VirtualCameraImageFontColor = Color.Black;

        /// <summary>
        /// 虚拟摄像头图片中默认字体
        /// </summary>
        public static Font VirtualCameraImageFont = new Font("宋体", 16);

        /// <summary>
        /// 虚拟摄像头背景
        /// </summary>
        public static string[] VirtualCameraBackgroundImages { get; set; }


        /// <summary>
        /// 任务执行器
        /// </summary>
        public static SimulatorObject Simulator = new SimulatorObject();

        /// <summary>
        /// 创建机器人用户
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="objects"></param>
        public static void CreateRobot(string userCode, string userName, double defaultLat, double defaultLng, KeyValuePair<string, string>[] virtualCameras)
        {
            //用户名称和用户代码
            RobotUser curUser = new RobotUser();
            curUser.UserCode = userCode;
            curUser.UserName = userName;

            //移动任务
            RobotTask rebotMoveTask = new RobotTask();
            rebotMoveTask.TaskCode = Task_RobotMove;
            rebotMoveTask.TaskName = "自主航行任务";

            //位置移动指令
            RebotMoveActionWorkerThread shipMoveCmd = new RebotMoveActionWorkerThread();
            shipMoveCmd.User = curUser;
            shipMoveCmd.Task = rebotMoveTask;
            curUser.SupportedAction.TryAdd(shipMoveCmd.SupportedActionCommand, shipMoveCmd);

            //位置移动任务
            RebotMoveTaskWorkerThread taskWorkerThread = new RebotMoveTaskWorkerThread();
            taskWorkerThread.Task = rebotMoveTask;
            taskWorkerThread.User = curUser;
            taskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;
            rebotMoveTask.TaskWorkerThread = taskWorkerThread;

            curUser.SupportedTask.TryAdd(rebotMoveTask.TaskCode, rebotMoveTask);

            //GPS监视器
            GPSMonitor gpsmonitor = new GPSMonitor(defaultLat, defaultLng);
            curUser.SupportedMonitor.TryAdd(Monitor_GPS, gpsmonitor);

            //虚拟摄像头
            if (virtualCameras != null)
            {
                foreach (KeyValuePair<string, string> kvp in virtualCameras)
                {
                    CameraMonitor cm = new CameraMonitor();
                    cm.User = curUser;
                    cm.Name = kvp.Value;

                    curUser.SupportedMonitor.TryAdd(kvp.Key, cm);
                }
            }

            //添加用户
            Simulator.UserDict.TryAdd(curUser.UserCode, curUser);

            //显示初始位置
            RobotFactory.OnUiAction(RobotFactory.UIAction_Move, curUser, rebotMoveTask, new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("lat", defaultLat), new KeyValuePair<string, object>("lng", defaultLng) });
        }

        /// <summary>
        /// 让一艘船按照指定的PosList运动
        /// </summary>
        /// <param name="userCode"></param>
        public static void StartMoveShipWithPosList(string userCode, List<double[]> posList)
        {
            if (Simulator.UserDict.ContainsKey(userCode))
            {
                //取出用户
                RobotUser selectedUser = Simulator.UserDict[userCode];

                //如果正在运行，则不添加新任务
                if (selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Running)
                {
                    return;
                }

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UsePosList;

                //参数
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>(RebotMoveTaskWorkerThread.Property_PosList, posList));

                //添加到任务队列
                AddTaskToRobot(new ProcessorQueueObject(selectedUser, selectedUser.SupportedTask[Task_RobotMove], new Command(currentCommand, paramList.ToArray())));
            }
        }

        /// <summary>
        /// 让一艘船按指定的Limit画方框
        /// </summary>
        /// <param name="userCode"></param>
        public static void StartMoveShipWithRect(string userCode, double limit)
        {
            if (Simulator.UserDict.ContainsKey(userCode))
            {
                //取出用户
                RobotUser selectedUser = Simulator.UserDict[userCode];

                //如果正在运行，则不添加新任务
                if (selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Running)
                {
                    return;
                }

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UseDefaultRect;

                //参数
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>(RebotMoveTaskWorkerThread.Property_Limit, limit));

                //添加到任务队列
                AddTaskToRobot(new ProcessorQueueObject(selectedUser, selectedUser.SupportedTask[Task_RobotMove], new Command(currentCommand, paramList.ToArray())));
            }
        }

        /// <summary>
        /// 让一艘船按指定的Limit画圆
        /// </summary>
        /// <param name="userCode"></param>
        public static void StartMoveShipWithRound(string userCode, double limit)
        {
            if (Simulator.UserDict.ContainsKey(userCode))
            {
                //取出用户
                RobotUser selectedUser = Simulator.UserDict[userCode];

                //如果正在运行，则不添加新任务
                if (selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Running)
                {
                    return;
                }

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UseDefaultRound;

                //参数
                List<KeyValuePair<string, object>> paramList = new List<KeyValuePair<string, object>>();
                paramList.Add(new KeyValuePair<string, object>(RebotMoveTaskWorkerThread.Property_Limit, limit));

                //添加到任务队列
                AddTaskToRobot(new ProcessorQueueObject(selectedUser, selectedUser.SupportedTask[Task_RobotMove], new Command(currentCommand, paramList.ToArray())));
            }
        }

        /// <summary>
        /// 添加一个任务到任务队列中准备执行
        /// </summary>
        /// <param name="pqo"></param>
        private static void AddTaskToRobot(ProcessorQueueObject pqo)
        {
            bool enabledAdd = true;
            foreach (ProcessorQueueObject queue in Simulator.TaskProcessor.Queues)
            {
                if (queue.User.Equals(pqo.User) && queue.Task.Equals(pqo.Task))
                {
                    enabledAdd = false;
                    break;
                }
            }
            if (enabledAdd)
            {
                pqo.Task.TaskWorkerThread.WorkerThreadState = WorkerThreadStateType.Started;
                Simulator.TaskProcessor.Queues.Enqueue(pqo);
            }
        }
    }

    /// <summary>
    /// 内部使用一个队列来指示机器人移动坐标
    /// </summary>
    public class RebotMoveTaskWorkerThread

    /// <summary>
    /// 负责按照坐标来让机器人移动
    /// </summary>
    public class RebotMoveActionWorkerThread : BaseActionWorkerThread
    {
        public RebotMoveActionWorkerThread()
        {
            this.SupportedActionCommand = RobotFactory.Action_RebotMove;
        }

        public override CommandResult Process(Command commandObj)
        {
            CommandResult cr = new CommandResult();
            cr.CommandText = this.SupportedActionCommand;

            if (commandObj.CommandText != null && commandObj.CommandText.Equals(this.SupportedActionCommand) && commandObj.Objects.ContainsKey("lat") && commandObj.Objects.ContainsKey("lng"))
            {
                try
                {
                    double lat = double.Parse(commandObj.Objects["lat"].ToString());
                    double lng = double.Parse(commandObj.Objects["lng"].ToString());



                    cr.IsOK = true;
                }
                catch (Exception ex)
                {
                    cr.IsOK = false;
                    cr.ErrorReason = ex.ToString();
                }
            }
            else
            {
                cr.IsOK = false;
            }

            return cr;
        }
    }




}