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
using TaskSimulator.RobotTasks;
using System.Net;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using System.Security.Cryptography.X509Certificates;

namespace TaskSimulator
{
    public partial class GISTempForm : Form
    {
        RobotTaskSocket taskSocket = null;

        Dictionary<RobotUser, GMarkerGoogle> MarkerDict = new Dictionary<RobotUser, GMarkerGoogle>();
        /// <summary>
        /// 地图上的图层，用户显示船只
        /// </summary>
        private GMapOverlay objects = null;

        public GISTempForm()
        {
            InitializeComponent();

            //开启任务模拟器
            RobotFactory.Simulator.Start();
        }

        /// <summary>
        /// 初始化用户信息
        /// </summary>
        private void InitData()
        {
            RobotFactory.OnUiActionEvent += new UIActionDelegate(RobotFactory_OnShipMoveEvent);

            List<KeyValuePair<string, string>> virtualCameras = new List<KeyValuePair<string, string>>();
            virtualCameras.Add(new KeyValuePair<string, string>("C1", "1号前视摄像头"));
            virtualCameras.Add(new KeyValuePair<string, string>("C2", "2号后视摄像头"));
            virtualCameras.Add(new KeyValuePair<string, string>("C3", "3号左侧摄像头"));
            virtualCameras.Add(new KeyValuePair<string, string>("C4", "4号右侧摄像头"));

            RobotFactory.VirtualCameraImageHeight = 160;
            RobotFactory.VirtualCameraImageWidth = 400;
            RobotFactory.VirtualCameraImageFont = new Font("宋体", 18);

            //RobotFactory.CreateRobot("test1", "测试无人船1", 21.2120, 133.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test2", "测试无人船2", 16.2120, 133.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test3", "测试无人船3", 16.2120, 138.4603, virtualCameras.ToArray());

            //RobotFactory.CreateRobot("test4", "测试无人船4", 21.2120, 128.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test5", "测试无人船5", 16.2120, 128.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test6", "测试无人船6", 21.2120, 138.4603, virtualCameras.ToArray());

            RobotFactory.CreateRobot("test7", "测试无人船7", 26.2120, 129.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test8", "测试无人船8", 26.2120, 134.4603, virtualCameras.ToArray());
            //RobotFactory.CreateRobot("test9", "测试无人船9", 26.2120, 139.4603, virtualCameras.ToArray());

            //RobotFactory.CreateRobot("test10", "测试无人船10", 30.2120, 135.4603, virtualCameras.ToArray());

            // create client instance 
            taskSocket = new RobotTaskSocket(RobotFactory.Simulator.UserDict["test7"], "47.104.7.244", 61613, "admin", "password",false);
            taskSocket.BoatMoveLimit = RobotFactory.StepWithSecond * 15;
            taskSocket.DefaultCameraMonitorId = "C1";
        }

        void RobotFactory_OnShipMoveEvent(object sender, UIActionEventArgs args)
        {
            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (args.ActionName.Equals(RobotFactory.UIAction_Move))
                    {
                        try
                        {
                            double lat = double.Parse(args.Objects["lat"].ToString());
                            double lng = double.Parse(args.Objects["lng"].ToString());

                            //Send Board GPS Position
                            if (taskSocket.EnabledAutoSendBoardPosition)
                            {
                                //BOAT POS=23.227N,37.223E	船的位置为北纬23.227度，东经37.223度
                                taskSocket.PublishBoatPos(lat, lng);
                            }

                            GMarkerGoogle marker = null;
                            if (MarkerDict.ContainsKey(args.User))
                            {
                                marker = MarkerDict[args.User];
                                marker.Position = new GMap.NET.PointLatLng(lat, lng);
                            }
                            else
                            {
                                marker = new GMarkerGoogle(new GMap.NET.PointLatLng(lat, lng), new Bitmap(Bitmap.FromFile(Path.Combine(Application.StartupPath, "ship.png"))));
                                marker.ToolTipMode = MarkerTooltipMode.Always;
                                marker.ToolTipText = args.User.UserName;
                                marker.Tag = args.Task;

                                MarkerDict.Add(args.User, marker);
                                objects.Markers.Add(marker);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }
                    }
                }));
            }
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
                mapControl.Position = new GMap.NET.PointLatLng(24.2120, 135.4603);
            }
            catch (Exception ex) { }

            //添加图层
            objects = new GMapOverlay("objects");
            mapControl.Overlays.Add(this.objects);

            //初始化用户信息
            InitData();

            RobotFactory.Simulator.TaskProcessor.OnTaskCompleteEvent += new TaskSimulatorLib.Processors.Task.TaskCompleteDelegate(TaskProcessor_OnTaskCompleteEvent);
        }

        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            taskSocket.PublishCommand("NOTICE=DESTINATION ARRIVED");

            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                    {
                        this.Text = "任务完成!" + DateTime.Now;
                    }));
            }
        }

        private void btnAddShip_Click(object sender, EventArgs e)
        {
            List<double[]> posList = new List<double[]>();
            double lat = 26.2120;
            double lng = 127.4603;

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat, lng - (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRect("test1", RobotFactory.StepWithSecond * 12);
            taskSocket.RandomRectOrRoundTask();

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 1, lng - (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRound("test2", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 2, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRect("test3", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 3, lng - (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRound("test4", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 4, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRect("test5", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 4, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRound("test6", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 4, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRect("test7", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 4, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRound("test8", RobotFactory.StepWithSecond * 12);

            //posList = new List<double[]>();
            //for (int kkk = 0; kkk < 15; kkk++)
            //{
            //    posList.Add(new double[] { lat + 4, lng + (RobotFactory.StepWithSecond * kkk) });
            //}
            //RobotFactory.StartMoveShipWithRect("test9", RobotFactory.StepWithSecond * 12);

            //lat = ((GPSMonitor)RobotFactory.Simulator.UserDict["test10"].SupportedMonitor[RobotFactory.Monitor_GPS]).Lat;
            //lng = ((GPSMonitor)RobotFactory.Simulator.UserDict["test10"].SupportedMonitor[RobotFactory.Monitor_GPS]).Lng;
            //posList = new List<double[]>();
            //if (lng > 135.4603)
            //{
            //    for (int kkk = 0; kkk < 15; kkk++)
            //    {
            //        posList.Add(new double[] { lat, lng - (RobotFactory.StepWithSecond * kkk) });
            //    }
            //}
            //else
            //{
            //    for (int kkk = 0; kkk < 15; kkk++)
            //    {
            //        posList.Add(new double[] { lat, lng + (RobotFactory.StepWithSecond * kkk) });
            //    }
            //}
            //RobotFactory.StartMoveShipWithPosList("test10", posList);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RobotFactory.Simulator.Stop();

            taskSocket.Client.Disconnect();
        }

        private void mapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            RobotUser du = ((RobotTask)item.Tag).TaskWorkerThread.User;

            Bitmap b1 = (Bitmap)du.SupportedMonitor["C1"].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;
            Bitmap b2 = (Bitmap)du.SupportedMonitor["C2"].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;
            Bitmap b3 = (Bitmap)du.SupportedMonitor["C3"].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;
            Bitmap b4 = (Bitmap)du.SupportedMonitor["C4"].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;

            pbCamera1.Image = b1;
            pbCamera2.Image = b2;
            pbCamera3.Image = b3;
            pbCamera4.Image = b4;
        }

        public MqttClient mqttClient { get; set; }

        private void trSendPic_Tick(object sender, EventArgs e)
        {
            if (taskSocket.EnabledAutoSendBoardPic)
            {
                Bitmap b12111 = (Bitmap)taskSocket.RobotUser.SupportedMonitor[taskSocket.DefaultCameraMonitorId].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;
                taskSocket.PublishPicture(b12111);
            }
        }
    }
}