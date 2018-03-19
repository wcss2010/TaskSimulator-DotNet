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
        /// 消息接收
        /// </summary>
        /// <param name="msg"></param>
        void Receive(object source,byte[] msg);

        /// <summary>
        /// 是否已连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 机器人启动
        /// </summary>
        void RobotStart(object args);

        /// <summary>
        /// 机器人停止
        /// </summary>
        void RobotStop();
    }
}