using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TaskSimulator.RobotTasks;

namespace TaskSimulator
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 机器人配置
        /// </summary>
        public static VirtualRobotListConfig RobotListConfig { get; set; }

        public MainForm()
        {
            InitializeComponent();
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
            tbLogs.AppendText(DateTime.Now.ToString() + ":" + txt);

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

                RobotListConfig.RobotList = new VirtualRobot[] { new VirtualRobot() };
                RobotListConfig.RobotList[0].VirtualRobotId = "test1";
                RobotListConfig.RobotList[0].VirtualRobotName = "测试无人船1";
                RobotListConfig.RobotList[0].EnabledAutoReportGpsLocation = true;
                RobotListConfig.RobotList[0].EnabledAutoUploadVirtualCameraPicture = true;
                RobotListConfig.RobotList[0].EnabledStartAndStopTaskWithOpenOrCloseEngine = false;
                RobotListConfig.RobotList[0].MQTTUser = "";
                RobotListConfig.RobotList[0].MQTTPassword = "";

                RobotListConfig.RobotList[0].DefaultLat = 26.2120;
                RobotListConfig.RobotList[0].DefaultLng = 129.4603;

                File.WriteAllText(xmlConfig, XmlSerializeTool.Serializer<VirtualRobotListConfig>(MainForm.RobotListConfig));
            }
            #endregion


        }

        void RobotFactory_OnUiActionEvent(object sender, RobotTasks.UIActionEventArgs args)
        {
            
        }

        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            
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
        public VirtualRobot[] RobotList { get; set; }
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
    public class VirtualRobot
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
        public bool EnabledAutoReportGpsLocation { get; set; }

        /// <summary>
        /// 自动上传我的摄像头图片
        /// </summary>
        public bool EnabledAutoUploadVirtualCameraPicture { get; set; }

        /// <summary>
        /// 在打开或关闭发动时允许启动或停止自动航行任务
        /// </summary>
        public bool EnabledStartAndStopTaskWithOpenOrCloseEngine { get; set; }

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