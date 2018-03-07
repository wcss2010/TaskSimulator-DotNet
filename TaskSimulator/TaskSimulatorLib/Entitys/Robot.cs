using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib.Entitys
{
    /// <summary>
    ///  机器人配置
    /// </summary>
    [Serializable]
    public class Robot :ICloneable
    {
        public Robot()
        {
            this.RobotId = string.Empty;
            this.RobotName = string.Empty;
            this.CameraHintTextFontName = "宋体";
            this.DefaultGpsPos = new LatAndLng();
            this.VoyageRoutes = new LatAndLng[0];
            this.MonitorStateMap = new SerializableDictionary<string, bool>();
            this.TaskStateMap = new SerializableDictionary<string, bool>();
            this.ConnectionMap = new SerializableDictionary<string, string>();
            this.CameraNames = new string[] { };
            this.CameraBackgrounds = new string[] { };
        }

        /// <summary>
        /// 虚拟机器人ID
        /// </summary>
        public string RobotId { get; set; }

        /// <summary>
        /// 虚拟机器人名称
        /// </summary>
        public string RobotName { get; set; }

        /// <summary>
        /// 每秒走多少距离
        /// </summary>
        public double StepWithSecond { get; set; }

        /// <summary>
        /// 机器人在画方框时的边长或画圆形时的半径
        /// </summary>
        public double Radius { get; set; }
        
        /// <summary>
        /// 初始的虚拟船位置
        /// </summary>
        public LatAndLng DefaultGpsPos { get; set; }

        /// <summary>
        /// 航行路线(如果规划了航行路线就按照它走，如果没有默认就是围着初始位置画圈圈)
        /// </summary>
        public LatAndLng[] VoyageRoutes { get; set; }

        /// <summary>
        /// 虚拟摄像头图片宽度
        /// </summary>
        public int CameraPictureWidth { get; set; }

        /// <summary>
        /// 虚拟摄像头图片高度
        /// </summary>
        public int CameraPictureHeight { get; set; }

        /// <summary>
        /// 虚拟摄像头图片字体
        /// </summary>
        public string CameraHintTextFontName { get; set; }

        /// <summary>
        /// 虚拟摄像头图片字体大小
        /// </summary>
        public float CameraHintTextFontSize { get; set; }

        /// <summary>
        /// 虚拟摄像头名称
        /// </summary>
        public string[] CameraNames { get; set; }

        /// <summary>
        /// 虚拟摄像头背景文件列表
        /// </summary>
        public string[] CameraBackgrounds { get; set; }

        /// <summary>
        /// 无人船监视器状态(Key=MonitorId,Value=当前是否可用)
        /// </summary>
        public SerializableDictionary<string, bool> MonitorStateMap { get; set; }

        /// <summary>
        /// 无人船任务状态(Key=TaskId,Value=当前是否启用)
        /// </summary>
        public SerializableDictionary<string, bool> TaskStateMap { get; set; }

        /// <summary>
        /// 连接配置字典
        /// </summary>
        public SerializableDictionary<string, string> ConnectionMap { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
