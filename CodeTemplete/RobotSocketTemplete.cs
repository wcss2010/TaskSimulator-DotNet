using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib;
using TaskSimulatorLib.Sockets;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;
using TaskSimulatorLib.Monitors;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Extends;
using TaskSimulatorLib.Processors.Task;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib.Templete
{
    public class RobotSocketTemplete : IRobotSocket
    {
        string robotName = "";

        /// <summary>
        /// 获得支持的控制台指令(用于在界面中双击使用)
        /// </summary>
        /// <returns></returns>
        public ConsoleCommand[] GetSupportedConsoleCommands()
        {
            List<ConsoleCommand> consoleList = new List<ConsoleCommand>();
            return consoleList.ToArray();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="robot"></param>
        public void Init(Robot robot)
        {
            robotName = robot.RobotName;
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread("初始化机器人" + robotName);
        }

        /// <summary>
        /// 是否已经连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return true;
        }

        /// <summary>
        /// 执行控制台指令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object ProcessConsoleCommand(string code, object[] args)
        {
            return null;
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="msg"></param>
        public void Publish(object dest, byte[] msg)
        {
            
        }

        /// <summary>
        /// 解析消息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="msg"></param>
        public void Receive(object source, byte[] msg)
        {
            
        }

        /// <summary>
        /// 机器人启动
        /// </summary>
        /// <param name="args"></param>
        public void RobotStart(object[] args)
        {
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(robotName + "机器人启动");
        }

        /// <summary>
        /// 机器人停止
        /// </summary>
        public void RobotStop()
        {
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(robotName + "机器人停止");
        }

        /// <summary>
        /// Socket启动
        /// </summary>
        public void Start()
        {
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(robotName + "Socket启动");
        }

        /// <summary>
        /// Socket停止
        /// </summary>
        public void Stop()
        {
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow.ShowLogTextWithThread(robotName + "Socket停止");
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Class1();
        }
    }
}