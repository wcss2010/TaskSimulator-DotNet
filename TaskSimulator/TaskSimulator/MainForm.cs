using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaskSimulatorLib;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;
using TaskSimulatorLib.Processors;

namespace TaskSimulator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 地图上的图层，用户显示船只
        /// </summary>
        private GMapOverlay objects = null;
        public static SimulatorObject so = new SimulatorObject();
        private DeviceUser firstDeviceUser = new DeviceUser();
        private Task moveTask = new Task();
        private ShipMoveCommand shipMoveCmd = null;
        private GpsMapMonitor gpsMonitor = new GpsMapMonitor();
        private MoveTaskWorkerThread taskWorkerThread = new MoveTaskWorkerThread();

        public MainForm()
        {
            InitializeComponent();

            //开启任务模拟器
            so.Start();
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitUsers()
        {
            firstDeviceUser.UserCode = "FirstShip";
            firstDeviceUser.UserName = "第一艘船";

            moveTask.TaskCode = "Move";
            moveTask.TaskState = StateType.Ready;
            moveTask.TaskType = TaskType.NowTask;
            moveTask.TaskPriority = 1;

            shipMoveCmd = new ShipMoveCommand(objects);
            shipMoveCmd.User = firstDeviceUser;
            shipMoveCmd.Task = moveTask;

            moveTask.CommandWorkerDict.TryAdd(shipMoveCmd.Cmd, shipMoveCmd);

            firstDeviceUser.SupportedTask.TryAdd(moveTask.TaskCode, moveTask);

            firstDeviceUser.SupportedMonitor.TryAdd("GPS", gpsMonitor);

            taskWorkerThread.User = firstDeviceUser;
            taskWorkerThread.Task = moveTask;

            moveTask.TaskWorkerThread = taskWorkerThread;

            so.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
        }

        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            System.Console.WriteLine(args.Task.TaskCode +  "任务完成！");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            mapControl.CacheLocation = Environment.CurrentDirectory + "\\GMapCache\\"; //缓存位置
            mapControl.MaxZoom = 8;
            mapControl.MinZoom = 6;
            mapControl.Zoom = 6;
            mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = System.Windows.Forms.MouseButtons.Left; //左键拖拽地图
            mapControl.MapProvider = GMapProviders.GoogleChinaMap;

            //检查是否需要导入地图到缓存
            //if (!File.Exists(Path.Combine(Application.StartupPath, @"GMapCache\TileDBv5\en\Data.gmdb")))
            //{
                if (mapControl.Manager.ImportFromGMDB(Path.Combine(Application.StartupPath, "SeaMap201711192033.gmdb")))
                {
                    mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
                }
            //}

            try
            {
                //1.美国空军嘉手纳空军基地
                //位置： 日本冲绳县中头郡    
                mapControl.Position = new GMap.NET.PointLatLng(26.2120, 127.4603);
            }
            catch (Exception ex) { }

            //添加图层
            objects = new GMapOverlay("objects");
            mapControl.Overlays.Add(this.objects);

            //初始化用户信息
            InitUsers();
        }

        private void btnAddShip_Click(object sender, EventArgs e)
        {
            //Command cmds = new Command();
            //cmds.Cmd = shipMoveCmd.Cmd;
            //cmds.Objects.Add("lat", 26.4615);
            //cmds.Objects.Add("lng", 127.4453);

            //shipMoveCmd.Process(cmds);

            objects.Markers.Clear();

            taskWorkerThread.InitQueues();

            ProcessorQueueObject pqo = new ProcessorQueueObject();
            pqo.User = firstDeviceUser;
            pqo.Task = moveTask;

            pqo.Command = new Command();
            pqo.Command.Cmd = "AA";

            so.TaskProcessor.Queues.Enqueue(pqo);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           so.Stop();
        }
    }

    public class ShipMoveCommand : BaseCommandWorkerThread
    {
        GMapOverlay objects = null;

        public ShipMoveCommand(GMapOverlay objects)
        {
            this.Cmd = "ShipMove";
            this.objects = objects;
        }

        public override TaskSimulatorLib.Entitys.CommandResult Process(TaskSimulatorLib.Entitys.Command commandObj)
        {
            TaskSimulatorLib.Entitys.CommandResult cr = new TaskSimulatorLib.Entitys.CommandResult();
            cr.Cmd = this.Cmd;


            if (commandObj.Cmd != null && commandObj.Cmd.Equals(this.Cmd) && commandObj.Objects.ContainsKey("lat") && commandObj.Objects.ContainsKey("lng"))
            {
                try
                {
                    objects.Markers.Clear();

                    double lat = double.Parse(commandObj.Objects["lat"].ToString());
                    double lng = double.Parse(commandObj.Objects["lng"].ToString());

                    GMarkerGoogle gmg = new GMarkerGoogle(new GMap.NET.PointLatLng(lat, lng), new Bitmap(Bitmap.FromFile(Path.Combine(Application.StartupPath, "ship.png"))));
                    objects.Markers.Add(gmg);

                    cr.IsOK = true;
                }
                catch (Exception ex)
                {
                    cr.IsOK = false;
                    cr.Reason = ex.ToString();
                }
            }
            else
            {
                cr.IsOK = false;
            }

            return cr;
        }
    }

    public class GpsMapMonitor : BaseMonitor
    {
        public override CommandResult Process(Command commandObj)
        {   
            return null;
        }
    }

    public class MoveTaskWorkerThread : BaseTaskWorkerThread
    {
        Queue<Command> cmdList = new Queue<Command>();

        DateTime lastSendTime = DateTime.Now;

        public MoveTaskWorkerThread()
        {
            InitQueues();
        }

        public void InitQueues()
        {
            cmdList.Enqueue(GetCommand(26.1615, 127.4453));

            cmdList.Enqueue(GetCommand(26.2615, 127.4453));

            cmdList.Enqueue(GetCommand(26.3615, 127.4453));

            cmdList.Enqueue(GetCommand(26.4615, 127.4453));

            cmdList.Enqueue(GetCommand(26.5615, 127.4453));

            cmdList.Enqueue(GetCommand(26.6615, 127.4453));

            cmdList.Enqueue(GetCommand(26.7615, 127.4453));

            cmdList.Enqueue(GetCommand(26.8615, 127.4453));
        }

        protected Command GetCommand(double lat, double lng)
        {
            Command cmds = new Command();
            cmds.Cmd = "ShipMove";
            cmds.Objects.Add("lat", lat);
            cmds.Objects.Add("lng", lng);

            return cmds;
        }

        public override CommandResult Process(Command commandObj)
        {
            CommandResult cr = new CommandResult();
            cr.Cmd = commandObj.Cmd;
            cr.IsOK = true;

            this.Task.TaskState = StateType.Running;

            if (commandObj.Cmd.Equals("AA"))
            {
                if ((DateTime.Now - lastSendTime).TotalSeconds >= 2)
                {
                    lastSendTime = DateTime.Now;

                    TaskSimulatorLib.Processors.ProcessorQueueObject pqo = new TaskSimulatorLib.Processors.ProcessorQueueObject();
                    pqo.User = User;
                    pqo.Task = Task;
                    pqo.Command = cmdList.Dequeue();
                    MainForm.so.CommandProcessor.Queues.Enqueue(pqo);
                }
            }

            if (cmdList.Count <= 0)
            {
                this.Task.TaskState = StateType.Ended;
            }

            return cr;
        }
    }
}