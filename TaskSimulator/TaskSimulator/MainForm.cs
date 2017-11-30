using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TaskSimulator.RobotTasks;
using TaskSimulatorLib.Entitys;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace TaskSimulator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 最大日志显示行
        /// </summary>
        public const int Max_Log_Line_Count = 800;

        /// <summary>
        /// 机器人配置
        /// </summary>
        public static VirtualRobotListConfig RobotListConfig { get; set; }

        /// <summary>
        /// 虚拟机器人配置字典(Key=机器人ID,Value=配置)
        /// </summary>
        public static Dictionary<string, VirtualRobotConfig> VirtualRobotConfigDict = new Dictionary<string, VirtualRobotConfig>();

        /// <summary>
        /// 虚拟机器人MQTT连接配置(Key=机器人ID,Value=MQTT连接)
        /// </summary>
        public static Dictionary<string, RobotTaskSocket> VirtualRobotSocketDict = new Dictionary<string, RobotTaskSocket>();

        private BackgroundWorker initWorker = new BackgroundWorker();
        private BackgroundWorker MQTTKeepConnectionWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();

            MQTTKeepConnectionWorker.WorkerSupportsCancellation = true;
            MQTTKeepConnectionWorker.DoWork += MQTTKeepConnectionWorker_DoWork;

            initWorker.WorkerSupportsCancellation = true;
            initWorker.DoWork += initWorker_DoWork;
            initWorker.RunWorkerCompleted += initWorker_RunWorkerCompleted;
        }

        void MQTTKeepConnectionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!MQTTKeepConnectionWorker.CancellationPending)
            {
                try
                {
                    foreach (RobotTaskSocket socket in VirtualRobotSocketDict.Values)
                    {
                        if (socket.Client.IsConnected)
                        {
                            continue;
                        }
                        else
                        {
                            socket.ConnectToServer();

                            if (socket.Client.IsConnected)
                            {
                                TaskSimulatorLib.SimulatorObject.logger.Warn("无人船" + socket.RobotUser.UserName + "的连接已经恢复！");
                                ShowLogTextWithThread("无人船" + socket.RobotUser.UserName + "的连接已经恢复！");
                            }
                            else
                            {
                                TaskSimulatorLib.SimulatorObject.logger.Warn("无人船" + socket.RobotUser.UserName + "的连接恢复失败！");
                                ShowLogTextWithThread("无人船" + socket.RobotUser.UserName + "的连接恢复失败！");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
                }

                try
                {
                    Thread.Sleep(500);
                }catch(Exception ex){}
            }
        }

        void initWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (MQTTKeepConnectionWorker.IsBusy)
            {
                return;
            }

            MQTTKeepConnectionWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 机器人初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void initWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //载入基本配置
                RobotFactory.StepWithSecond = RobotListConfig.StepWithSecond;
                RobotFactory.VirtualCameraImageWidth = RobotListConfig.VirtualCameraPictureWidth;
                RobotFactory.VirtualCameraImageHeight = RobotListConfig.VirtualCameraPictureHeight;
                RobotFactory.VirtualCameraImageFont = new Font(RobotListConfig.VirtualCameraHintTextFontName, RobotListConfig.VirtualCameraHintTextFontSize);

                if (RobotListConfig.RobotList != null && RobotListConfig.RobotList.Length > 0)
                {
                    foreach (VirtualRobotConfig vrc in RobotListConfig.RobotList)
                    {
                        ShowLogTextWithThread("正在创建机器人" + vrc.VirtualRobotId + "......");
                        TaskSimulatorLib.SimulatorObject.logger.Info("正在创建机器人" + vrc.VirtualRobotId + "......");

                        //添加配置字典
                        VirtualRobotConfigDict.Add(vrc.VirtualRobotId, vrc);

                        List<KeyValuePair<string, string>> virtualCamerasList = new List<KeyValuePair<string, string>>();
                        for (int kkk = 0; kkk < RobotListConfig.VirtualCameraCount; kkk++)
                        {
                            virtualCamerasList.Add(new KeyValuePair<string, string>("Camera" + kkk, (kkk + 1) + "号摄像头"));
                        }
                        //创建机器人
                        RobotFactory.CreateRobot(vrc.VirtualRobotId, vrc.VirtualRobotName, vrc.DefaultLat, vrc.DefaultLng, virtualCamerasList.ToArray());

                        //连接MQTT
                        RobotTaskSocket robotTaskSocket = new RobotTaskSocket(RobotFactory.Simulator.UserDict[vrc.VirtualRobotId], RobotListConfig.MQTTServerIP, RobotListConfig.MQTTServerPort, vrc.MQTTUser, vrc.MQTTPassword, RobotListConfig.IsTlsModeLoginMQTT);
                        robotTaskSocket.BoatMoveLimit = RobotListConfig.RobotMoveLimit;
                        robotTaskSocket.DefaultCameraMonitorId = virtualCamerasList[0].Key;
                        robotTaskSocket.EnabledPosSensor = vrc.EnabledPosSensor;
                        robotTaskSocket.EnabledCameraSensor = vrc.EnabledCameraSensor;
                        robotTaskSocket.EnabledMQTTControlTaskStartAndStop = vrc.EnabledMQTTControlTaskStartAndStop;
                        robotTaskSocket.qosLevels = vrc.DefaultQosLevels;
                        robotTaskSocket.ListenSubject = vrc.ListenSubject;
                        robotTaskSocket.CommandSendSubject = vrc.CommandSendSubject;
                        robotTaskSocket.PictureSendSubject = vrc.PictureSendSubject;

                        robotTaskSocket.ConnectToServer();
                        VirtualRobotSocketDict.Add(vrc.VirtualRobotId, robotTaskSocket);

                        ShowLogTextWithThread("创建机器人" + vrc.VirtualRobotId + "成功！");
                        TaskSimulatorLib.SimulatorObject.logger.Info("创建机器人" + vrc.VirtualRobotId + "成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLogTextWithThread(ex.ToString());
                TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RobotTasks.RobotFactory.OnUiActionEvent += RobotFactory_OnUiActionEvent;
            RobotTasks.RobotFactory.Simulator.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
            RobotTasks.RobotFactory.Simulator.Start();

            OpenAllRobot();
        }

        /// <summary>
        /// 显示一段提示文字(UI操作)
        /// </summary>
        /// <param name="txt"></param>
        protected void ShowLogTextWithUI(string txt)
        {
            if (tbLogs.Lines != null && tbLogs.Lines.Length >= Max_Log_Line_Count)
            {
                tbLogs.Clear();
            }

            tbLogs.AppendText(DateTime.Now.ToString() + ":" + txt + "\n");

            if (tbLogs.Text.Length > 0)
            {
                tbLogs.SelectionStart = tbLogs.Text.Length; //Set the current caret position at the end
                tbLogs.ScrollToCaret(); //Now scroll it automatically
            }
        }

        /// <summary>
        /// 显示一段提示文字(线程中)
        /// </summary>
        /// <param name="txt"></param>
        protected void ShowLogTextWithThread(string txt)
        {
            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                    {
                        ShowLogTextWithUI(txt);
                    }));
            }
        }

        private void OpenAllRobot()
        {
            #region 读取配置文件
            string xmlConfig = Path.Combine(Application.StartupPath, "robot.config");
            if (File.Exists(xmlConfig))
            {
                RobotListConfig = XmlSerializeTool.Deserialize<VirtualRobotListConfig>(File.ReadAllText(xmlConfig));
            }
            else
            {
                RobotListConfig = new VirtualRobotListConfig();
                RobotListConfig.StepWithSecond = RobotFactory.StepWithSecond;
                RobotListConfig.RobotMoveLimit = RobotListConfig.StepWithSecond * 10;
                RobotListConfig.MQTTServerIP = "47.104.7.244";
                RobotListConfig.MQTTServerPort = 61613;
                RobotListConfig.IsTlsModeLoginMQTT = false;
                RobotListConfig.VirtualCameraPictureWidth = RobotFactory.VirtualCameraImageWidth;
                RobotListConfig.VirtualCameraPictureHeight = RobotFactory.VirtualCameraImageHeight;
                RobotListConfig.VirtualCameraCount = 2;
                RobotListConfig.VirtualCameraHintTextFontName = RobotFactory.VirtualCameraImageFont.Name;
                RobotListConfig.VirtualCameraHintTextFontSize = RobotFactory.VirtualCameraImageFont.Size;

                RobotListConfig.RobotList = new VirtualRobotConfig[] { new VirtualRobotConfig() };
                RobotListConfig.RobotList[0].VirtualRobotId = "test1";
                RobotListConfig.RobotList[0].VirtualRobotName = "测试无人船1";
                RobotListConfig.RobotList[0].EnabledPosSensor = true;
                RobotListConfig.RobotList[0].EnabledCameraSensor = true;
                RobotListConfig.RobotList[0].EnabledMQTTControlTaskStartAndStop = false;
                RobotListConfig.RobotList[0].MQTTUser = "gs";
                RobotListConfig.RobotList[0].MQTTPassword = "gs_password";

                RobotListConfig.RobotList[0].DefaultQosLevels = new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE };
                RobotListConfig.RobotList[0].ListenSubject = "shore2boat";
                RobotListConfig.RobotList[0].CommandSendSubject = "boat2shore";
                RobotListConfig.RobotList[0].PictureSendSubject = "picture2shore";

                RobotListConfig.RobotList[0].DefaultLat = 26.2120;
                RobotListConfig.RobotList[0].DefaultLng = 129.4603;

                File.WriteAllText(xmlConfig, XmlSerializeTool.Serializer<VirtualRobotListConfig>(MainForm.RobotListConfig));
            }
            #endregion

            if (initWorker.IsBusy)
            {
                return;
            }

            initWorker.RunWorkerAsync();
        }

        void RobotFactory_OnUiActionEvent(object sender, RobotTasks.UIActionEventArgs args)
        {
            try
            {
                VirtualRobotConfig VRConfig = VirtualRobotConfigDict[args.User.UserCode];

                RobotTaskSocket taskSocket = null;
                if (VirtualRobotSocketDict.ContainsKey(args.User.UserCode))
                {
                    taskSocket = VirtualRobotSocketDict[args.User.UserCode];
                }

                if (args.ActionName.Equals(RobotFactory.UIAction_Move))
                {     
                     double lat = double.Parse(args.Objects["lat"].ToString());
                     double lng = double.Parse(args.Objects["lng"].ToString());

                     //Send Board GPS Position
                     if (taskSocket != null)
                     {
                         if (taskSocket.EnabledPosSensor)
                         {
                             //BOAT POS=23.227N,37.223E	船的位置为北纬23.227度，东经37.223度
                             taskSocket.PublishBoatPos(lat, lng);

                             //主电压
                             taskSocket.PublishBoatMainBattVol(18.7);

                             //航速
                             taskSocket.PublishBoatSpeed(2.2);

                             //航向
                             taskSocket.PublishBoatSailDir(22.5);
                         }

                         if (taskSocket.EnabledWindSensor)
                         {
                             //风速
                             taskSocket.PublishWindSpeed(2.7);
                             
                             //风向
                             taskSocket.PublishWindDir(22.5);
                         }

                         if (taskSocket.EnabledTempSensor)
                         {
                             //水温
                             taskSocket.PublishWaterTemp(33.3);
                             
                             //气温
                             taskSocket.PublishAirTemp(27);
                         }

                         if (taskSocket.EnabledCameraSensor)
                         {
                             //图片
                             Bitmap b12111 = (Bitmap)taskSocket.RobotUser.SupportedMonitor[taskSocket.DefaultCameraMonitorId].Process(new Command(CameraMonitor.Command_GetCameraImage, null)).Content;
                             taskSocket.PublishPicture(b12111);
                         }
                     }

                     ShowLogTextWithThread("无人船" + args.User.UserName + "(" + args.User.UserCode + ") 移动到坐标(" + lat + "," + lng + ")");
                }
            }
            catch (Exception ex)
            {
                ShowLogTextWithThread(ex.ToString());
                TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
            }
        }

        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            TaskSimulatorLib.SimulatorObject.logger.Info("无人船" + args.User.UserName + "的任务" + args.Task.TaskName + "完成！");
            ShowLogTextWithThread("无人船" + args.User.UserName + "的任务" + args.Task.TaskName + "完成！");

            if (cbIsAlwaysRunMoveTask.Checked)
            {
                VirtualRobotSocketDict[args.User.UserCode].RandomRectOrRoundTask();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //停止任务处理器
            RobotTasks.RobotFactory.Simulator.Stop();

            //关闭所有机器人
            CloseAllRobot();
        }

        /// <summary>
        /// 关闭所有机器人
        /// </summary>
        private void CloseAllRobot()
        {
            try
            {
                MQTTKeepConnectionWorker.CancelAsync();

                foreach (RobotTaskSocket taskSocket in VirtualRobotSocketDict.Values)
                {
                    taskSocket.Client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
            }
        }

        private void btnPlayAllTask_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RobotTaskSocket taskSocket in VirtualRobotSocketDict.Values)
                {
                    taskSocket.RandomRectOrRoundTask();
                }
            }
            catch (Exception ex)
            {
                ShowLogTextWithThread(ex.ToString());
                TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
            }
        }
    }

    [Serializable]
    public class VirtualRobotListConfig
    {
        /// <summary>
        /// 每秒走多少距离
        /// </summary>
        public double StepWithSecond { get; set; }

        /// <summary>
        /// 机器人在画方框时的边长或画圆形时的半径
        /// </summary>
        public double RobotMoveLimit { get; set; }

        /// <summary>
        /// 虚拟摄像头图片宽度
        /// </summary>
        public int VirtualCameraPictureWidth { get; set; }

        /// <summary>
        /// 虚拟摄像头图片高度
        /// </summary>
        public int VirtualCameraPictureHeight { get; set; }

        /// <summary>
        /// 虚拟摄像头图片字体
        /// </summary>
        public string VirtualCameraHintTextFontName { get; set; }

        /// <summary>
        /// 虚拟摄像头图片字体大小
        /// </summary>
        public float VirtualCameraHintTextFontSize { get; set; }

        /// <summary>
        /// 远程MQTT服务器IP
        /// </summary>
        public string MQTTServerIP { get; set; }

        /// <summary>
        /// 远程MQTT服务器端口
        /// </summary>
        public int MQTTServerPort { get; set; }

        /// <summary>
        /// 是否使用Tls模式登录MQTT
        /// </summary>
        public bool IsTlsModeLoginMQTT { get; set; }

        /// <summary>
        /// 虚拟摄像头数量
        /// </summary>
        public int VirtualCameraCount { get; set; }

        /// <summary>
        /// 机器人列表
        /// </summary>
        public VirtualRobotConfig[] RobotList { get; set; }
    }

    /// <summary>
    /// XML序列化工具
    /// </summary>
    public class XmlSerializeTool
    {
        //反序列化
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(xml);
            T obj = (T)xs.Deserialize(sr);
            sr.Close();
            sr.Dispose();
            return obj;
        }

        //序列化
        public static string Serializer<T>(T t)
        {
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
            xsn.Add(string.Empty, string.Empty);
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringWriter sw = new StringWriter();
            xs.Serialize(sw, t, xsn);
            string str = sw.ToString();
            sw.Close();
            sw.Dispose();
            return str;
        }
    }

    [Serializable]
    public class VirtualRobotConfig
    {
        /// <summary>
        /// 虚拟机器人ID
        /// </summary>
        public string VirtualRobotId { get; set; }

        /// <summary>
        /// 虚拟机器人名称
        /// </summary>
        public string VirtualRobotName { get; set; }

        /// <summary>
        /// 自动报告我的位置
        /// </summary>
        public bool EnabledPosSensor { get; set; }

        /// <summary>
        /// 自动上传我的摄像头图片
        /// </summary>
        public bool EnabledCameraSensor { get; set; }

        /// <summary>
        /// 在打开或关闭发动时允许启动或停止自动航行任务
        /// </summary>
        public bool EnabledMQTTControlTaskStartAndStop { get; set; }

        /// <summary>
        /// qos=1
        /// </summary>
        public byte[] DefaultQosLevels { get; set; }
        
        /// <summary>
        /// 监听主题
        /// </summary>
        public string ListenSubject { get; set; }

        /// <summary>
        /// 指令发送主题
        /// </summary>
        public string CommandSendSubject { get; set; }

        /// <summary>
        /// 图片发送主题
        /// </summary>
        public string PictureSendSubject { get; set; }

        /// <summary>
        /// MQTT用户
        /// </summary>
        public string MQTTUser { get; set; }

        /// <summary>
        /// MQTT密码
        /// </summary>
        public string MQTTPassword { get; set; }

        /// <summary>
        /// 初始纬度
        /// </summary>
        public double DefaultLat { get; set; }

        /// <summary>
        /// 初始经度
        /// </summary>
        public double DefaultLng { get; set; }
    }
}