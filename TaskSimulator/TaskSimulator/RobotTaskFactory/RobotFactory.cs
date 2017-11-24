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

namespace TaskSimulator.RobotTaskFactory
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
        public const double StepWithSecond = 0.15;

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

        public static event UIActionDelegate OnUiActionEvent;
        public static void OnUiAction(string actionName,RobotUser user, RobotTask task,KeyValuePair<string,object>[] dataTeam)
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
        /// 任务执行器
        /// </summary>
        public static SimulatorObject Simulator = new SimulatorObject();

        /// <summary>
        /// 创建机器人用户
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="objects"></param>
        public static void CreateRobot(string userCode, string userName, double defaultLat, double defaultLng,KeyValuePair<string,string>[] virtualCameras)
        {
            //用户名称和用户代码
            RobotUser curUser = new RobotUser();
            curUser.UserCode = userCode;
            curUser.UserName = userName;

            //移动任务
            RobotTask rebotMoveTask = new RobotTask();
            rebotMoveTask.TaskCode = Task_RobotMove;
            rebotMoveTask.TaskName = "机器人移动任务";

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

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UsePosList;

                //参数
                List<KeyValuePair<string,object>> paramList = new List<KeyValuePair<string,object>>();
                paramList.Add(new KeyValuePair<string,object>(RebotMoveTaskWorkerThread.Property_PosList, posList));

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

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UseDefaultRect;

                //参数
                List<KeyValuePair<string,object>> paramList = new List<KeyValuePair<string,object>>();
                paramList.Add(new KeyValuePair<string,object>(RebotMoveTaskWorkerThread.Property_Limit, limit));

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

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[Task_RobotMove].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //当前要执行的命令
                string currentCommand = RebotMoveTaskWorkerThread.Command_UseDefaultRound;

                //参数
                List<KeyValuePair<string,object>> paramList = new List<KeyValuePair<string,object>>();
                paramList.Add(new KeyValuePair<string,object>(RebotMoveTaskWorkerThread.Property_Limit, limit));

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
                Simulator.TaskProcessor.Queues.Enqueue(pqo);
            }
        }
    }

    /// <summary>
    /// 内部使用一个队列来指示机器人移动坐标
    /// </summary>
    public class RebotMoveTaskWorkerThread : BaseTaskWorkerThread
    {
        /// <summary>
        /// 使用外部定义好的PosList来播放
        /// </summary>
        public const string Command_UsePosList = "UsePosList";

        /// <summary>
        /// 从一个初始坐标点开始画方框(根据Limit)
        /// </summary>
        public const string Command_UseDefaultRect = "UseDefaultRect";

        /// <summary>
        /// 从一个初始坐标点开始画圆圈(根据Limit)
        /// </summary>
        public const string Command_UseDefaultRound = "UseDefaultRound";

        /// <summary>
        /// 外部坐标列表
        /// </summary>
        public const string Property_PosList = "PosList";

        /// <summary>
        /// 圆圈半径或方框边长
        /// </summary>
        public const string Property_Limit = "Limit";

        private Queue<Command> CommandQueues = new Queue<Command>();
        private DateTime lastSendTime = DateTime.Now;

        /// <summary>
        /// 创建移动指令
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        private Command CreateMoveCommand(double lat, double lng)
        {
            List<KeyValuePair<string,object>> paramList = new List<KeyValuePair<string,object>>();
            paramList.Add(new KeyValuePair<string,object>("lat", lat));
            paramList.Add(new KeyValuePair<string,object>("lng", lng));

            return new Command(RobotFactory.Action_RebotMove, paramList.ToArray());
        }

        public override CommandResult Process(Command commandObj)
        {
            TaskSimulatorLib.Entitys.CommandResult cr = new TaskSimulatorLib.Entitys.CommandResult();
            cr.IsOK = true;

            //设置状态为正在运行
            WorkerThreadState = WorkerThreadStateType.Running;

            //尝试初始化队列
            if (CommandQueues.Count == 0)
            {
                if (commandObj.CommandText.Equals(Command_UsePosList))
                {
                    if (commandObj.Objects.ContainsKey(Property_PosList))
                    {
                        //位置队列为空时初始化列表
                        List<double[]> posList = (List<double[]>)commandObj.Objects[Property_PosList];
                        if (posList.Count > 0)
                        {
                            foreach (double[] pos in posList)
                            {
                                CommandQueues.Enqueue(CreateMoveCommand(pos[0], pos[1]));
                            }
                        }
                    }
                }
                else if (commandObj.CommandText.Equals(Command_UseDefaultRect))
                {
                    //画方框
                    if (commandObj.Objects.ContainsKey(Property_Limit))
                    {
                        double limit = (double)commandObj.Objects[Property_Limit];

                        commandObj.CommandText = GPSMonitor.Command_GetGPS;
                        CommandResult gps = User.SupportedMonitor[RobotFactory.Monitor_GPS].Process(commandObj);
                        double lat = double.Parse(gps.Objects["lat"].ToString());
                        double lng = double.Parse(gps.Objects["lng"].ToString());

                        int stepCount = (int)(limit / RobotFactory.StepWithSecond);
                        stepCount++;

                        //-
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lng = lng + RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }
                        //|
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat - RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }
                        //_
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lng = lng - RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }
                        //|
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat + RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }
                    }
                }
                else if (commandObj.CommandText.Equals(Command_UseDefaultRound))
                {
                    //画圆圈
                    if (commandObj.Objects.ContainsKey(Property_Limit))
                    {
                        double limit = (double)commandObj.Objects[Property_Limit];

                        commandObj.CommandText = GPSMonitor.Command_GetGPS;
                        CommandResult gps = User.SupportedMonitor[RobotFactory.Monitor_GPS].Process(commandObj);
                        double lat = double.Parse(gps.Objects["lat"].ToString());
                        double lng = double.Parse(gps.Objects["lng"].ToString());

                        int stepCount = (int)(limit / RobotFactory.StepWithSecond);
                        if (stepCount % 2 > 0)
                        {
                            stepCount++;
                        }

                        //先向上走一段距离,到达圆的顶点
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat + RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }

                        //生成圆边的坐标点阵
                        int totalCount = stepCount * 2;
                        //生成右边的坐标点阵
                        for (int kkk = 1; kkk <= totalCount; kkk++)
                        {
                            if (kkk <= stepCount)
                            {
                                CommandQueues.Enqueue(CreateMoveCommand(lat - ((kkk - 1) * RobotFactory.StepWithSecond), lng + (kkk * RobotFactory.StepWithSecond)));
                            }
                            else
                            {
                                double newStep = stepCount * RobotFactory.StepWithSecond;
                                newStep -= (kkk - stepCount) * RobotFactory.StepWithSecond;
                                CommandQueues.Enqueue(CreateMoveCommand(lat - ((kkk - 1) * RobotFactory.StepWithSecond), lng + newStep));
                            }
                        }

                        //生成左边的坐标点阵
                        int xxx = 0;
                        for (int kkk = totalCount; kkk >= 1; kkk--)
                        {
                            if (kkk >= stepCount)
                            {
                                xxx++;
                            }
                            else
                            {
                                xxx--;
                            }
                            CommandQueues.Enqueue(CreateMoveCommand(lat - (kkk * RobotFactory.StepWithSecond), lng - (xxx * RobotFactory.StepWithSecond)));
                        }

                        //向下走一段距离,回到圆心
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat - RobotFactory.StepWithSecond;
                            CommandQueues.Enqueue(CreateMoveCommand(lat, lng));
                        }
                    }
                }
            }

            //指示CommandProcessor去播放队列中的位置点,1秒一条
            if ((DateTime.Now - lastSendTime).TotalSeconds >= 1)
            {
                lastSendTime = DateTime.Now;

                //从队列中取一个Position放到CommandProcessor中
                RobotFactory.Simulator.ActionProcessor.Queues.Enqueue(new ProcessorQueueObject(User, Task, CommandQueues.Dequeue()));
            }

            //队列中没有记录则结束任务
            if (CommandQueues.Count <= 0)
            {
                WorkerThreadState = WorkerThreadStateType.Ended;
            }

            return cr;
        }
    }

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

                    //移动屏幕坐标点
                    RobotFactory.OnUiAction(RobotFactory.UIAction_Move, User, Task, new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("lat", lat), new KeyValuePair<string, object>("lng", lng) });

                    //向GPSMonitor报告自己的位置
                    commandObj.CommandText = GPSMonitor.Command_ReportGPS;
                    User.SupportedMonitor[RobotFactory.Monitor_GPS].Process(commandObj);

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

    /// <summary>
    /// GPS监视器(主要用于保存现在的GPS地址)
    /// </summary>
    public class GPSMonitor : BaseMonitor
    {
        /// <summary>
        /// 报告GPS位置命令
        /// </summary>
        public const string Command_ReportGPS = "ReportGPS";

        /// <summary>
        /// 获得GPS位置命令
        /// </summary>
        public const string Command_GetGPS = "GetGPS";

        public GPSMonitor(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public override CommandResult Process(Command commandObj)
        {
            TaskSimulatorLib.Entitys.CommandResult cr = new TaskSimulatorLib.Entitys.CommandResult();
            cr.CommandText = commandObj.CommandText;
            cr.IsOK = true;

            switch (commandObj.CommandText)
            {
                case Command_ReportGPS:
                    if (commandObj.Objects.ContainsKey("lat") && commandObj.Objects.ContainsKey("lng"))
                    {
                        Lat = double.Parse(commandObj.Objects["lat"].ToString());
                        Lng = double.Parse(commandObj.Objects["lng"].ToString());
                    }
                    break;
                case Command_GetGPS:
                    cr.Objects.Add("lat", Lat);
                    cr.Objects.Add("lng", Lng);
                    break;
            }

            return cr;
        }

        public double Lat { get; set; }

        public double Lng { get; set; }
    }

    /// <summary>
    /// 摄像头监视器
    /// </summary>
    public class CameraMonitor :BaseMonitor
    {
        /// <summary>
        /// 获得摄像头指令
        /// </summary>
        public const string Command_GetCameraImage = "GetCameraImage";

        Random random = new Random((int)DateTime.Now.Ticks);

        public override CommandResult Process(Command commandObj)
        {
            List<KeyValuePair<string,object>> resultParams = new List<KeyValuePair<string,object>>();

            Bitmap bmp = null;
            if (Command_GetCameraImage.Equals(commandObj.CommandText))
            {
                //绘制虚拟摄像头图像
                bmp = new Bitmap(RobotFactory.VirtualCameraImageWidth, RobotFactory.VirtualCameraImageHeight);
                Graphics g = Graphics.FromImage(bmp);
                try
                {
                    List<Color> backgroundColors = new List<Color>();
                    backgroundColors.AddRange(RobotFactory.VirtualCameraImageBackgroundColors);

                    //填充一个随机背景色
                    g.FillRectangle(new SolidBrush(backgroundColors[random.Next(0, backgroundColors.Count)]), new Rectangle(0, 0, RobotFactory.VirtualCameraImageWidth, RobotFactory.VirtualCameraImageHeight));
                    //写RobotUser名称
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    g.DrawString(User.UserName + (Name != null ? "-" + Name : string.Empty), RobotFactory.VirtualCameraImageFont, new SolidBrush(RobotFactory.VirtualCameraImageFontColor), new RectangleF(0, 0, RobotFactory.VirtualCameraImageWidth, (int)(RobotFactory.VirtualCameraImageHeight * 0.8)), sf);
                    //写时间
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    g.DrawString(DateTime.Now.ToLongTimeString(), RobotFactory.VirtualCameraImageFont, new SolidBrush(RobotFactory.VirtualCameraImageFontColor), new RectangleF(0, (int)(RobotFactory.VirtualCameraImageHeight * 0.8), RobotFactory.VirtualCameraImageWidth, RobotFactory.VirtualCameraImageHeight), sf);
                }
                finally
                {
                    g.Dispose();
                }
            }

            return new CommandResult(commandObj.CommandText, true, string.Empty, bmp,resultParams.ToArray());
        }
    }
}