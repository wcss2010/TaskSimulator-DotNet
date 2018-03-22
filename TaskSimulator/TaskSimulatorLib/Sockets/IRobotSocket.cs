using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib.Sockets
{
    /**
     * 机器人的Socket接口，用于适配MQTT,TCP,UCP等通信方式
     * 
     */
    public interface IRobotSocket :ICloneable
    {
        /// <summary>
        /// 所属设备用户
        /// </summary>
        RobotUser User { get; set; }

        /// <summary>
        /// 初始化Socket
        /// </summary>
        /// <param name="robot">机器人信息</param>
        void Init(Robot robot);

        /// <summary>
        /// 开始Socket
        /// </summary>
        void Start();

        /// <summary>
        /// 结束Socket
        /// </summary>
        void Stop();

        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="msg"></param>
        void Publish(object dest, byte[] msg);

        /// <summary>
        /// 消息发布
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="msg"></param>
        void Publish(object dest, object[] msg);

        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="msg"></param>
        void Receive(object source,byte[] msg);

        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="msg"></param>
        void Receive(object source, object[] msg);

        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 机器人启动
        /// </summary>
        void RobotStart(object[] args);

        /// <summary>
        /// 机器人停止
        /// </summary>
        void RobotStop();

        /// <summary>
        /// 获得所支持的控制台指令列表
        /// </summary>
        /// <returns></returns>
        ConsoleCommand[] GetSupportedConsoleCommands();

        /// <summary>
        /// 执行相关指令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        object ProcessConsoleCommand(string code, object[] args);
    }

    /// <summary>
    /// 机器人支持的控制台指令
    /// </summary>
    public class ConsoleCommand
    {
        public ConsoleCommand() { }

        public ConsoleCommand(string code, string name)
        {
            this.Code = code;
            this.Name = name;
        }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public object Tag { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(Name))
            {
                return base.ToString();
            }
            else
            {
                return Name + "(" + Code + ")";
            }
        }
    }
}