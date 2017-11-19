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

namespace TaskSimulator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 地图上的图层，用户显示船只
        /// </summary>
        private GMapOverlay objects = null;
        private SimulatorObject so = new SimulatorObject();
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

            moveTask.TaskWorkerThread = taskWorkerThread;
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
        public override CommandResult Process(Command commandObj)
        {
            return null;
        }
    }
}