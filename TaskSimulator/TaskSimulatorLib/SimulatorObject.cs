using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Processors.Task;
using TaskSimulatorLib.Util;

namespace TaskSimulatorLib
{
    /// <summary>
    /// 控制台日志显示接口
    /// </summary>
    public interface IConsoleWindowLogger
    {
        /// <summary>
        /// 显示一段提示文字(UI操作)
        /// </summary>
        /// <param name="txt"></param>
        void ShowLogTextWithUI(string txt);

        /// <summary>
        /// 显示一段提示文字(线程中)
        /// </summary>
        /// <param name="txt"></param>
        void ShowLogTextWithThread(string txt);
    }

    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类是整个类库的入口
     * 
     */
    public class SimulatorObject
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ROBOT_CONFIG_FILENAME = "config.xml";

        public static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SimulatorObject));
        /// <summary>
        /// 生成一个日志对象
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static log4net.ILog GetLogger(Type t)
        {
            return log4net.LogManager.GetLogger(t);
        }

        /// <summary>
        /// 在主窗体上的一个接口用于显示一些提示性的文字
        /// </summary>
        public static IConsoleWindowLogger ConsoleLoggerWindow { get; set; }

        private static SimulatorObject simulator = new SimulatorObject();
        /// <summary>
        /// 机器人模拟器实例
        /// </summary>
        public static SimulatorObject Simulator
        {
            get { return SimulatorObject.simulator; }
            set { SimulatorObject.simulator = value; }
        }

        ConcurrentDictionary<string, RobotUser> userDict = new ConcurrentDictionary<string, RobotUser>();
        /// <summary>
        /// 无人船用户字典
        /// </summary>
        public ConcurrentDictionary<string, RobotUser> UserDict
        {
            get { return userDict; }
        }

        private RobotSimulatorConfig simulatorConfig = null;
        /// <summary>
        /// 机器人配置文件
        /// </summary>
        public RobotSimulatorConfig SimulatorConfig
        {
            get { return simulatorConfig; }
            set { simulatorConfig = value; }
        }

        private TaskProcessor taskProcessor = new TaskProcessor();
        /// <summary>
        /// 任务处理器
        /// </summary>
        public TaskProcessor TaskProcessor
        {
            get { return taskProcessor; }
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            TaskProcessor.Start();
            //ActionProcessor.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            TaskProcessor.Stop();
            //ActionProcessor.Stop();
        }

        /// <summary>
        /// 任务入队
        /// </summary>
        /// <param name="pqo"></param>
        public void TaskEnqueue(ProcessorQueueObject pqo)
        {
            bool enabledAdd = true;
            foreach (ProcessorQueueObject queue in taskProcessor.Queues)
            {
                if (queue.User.Equals(pqo.User) && queue.Task.Equals(pqo.Task))
                {
                    enabledAdd = false;
                    break;
                }
            }
            if (enabledAdd)
            {
                pqo.Task.TaskWorkerThread.WorkerThreadState = WorkerThreadStateType.Started;
                taskProcessor.Queues.Enqueue(pqo);
            }
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="userCode"></param>
        public void TaskStartup(string userCode, string taskCode, Command taskCommand)
        {
            if (UserDict.ContainsKey(userCode))
            {
                //取出用户
                RobotUser selectedUser = UserDict[userCode];

                //如果不存在这个任务，则不添加新任务
                if (selectedUser == null || !selectedUser.SupportedTask.ContainsKey(taskCode))
                {
                    return;
                }

                //如果正在运行，则不添加新任务
                if (selectedUser.SupportedTask[taskCode].TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Running)
                {
                    return;
                }

                //重置工作线程状态为Ready
                selectedUser.SupportedTask[taskCode].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

                //添加到任务队列
                TaskEnqueue(new ProcessorQueueObject(selectedUser, selectedUser.SupportedTask[taskCode], taskCommand));
            }
        }

        /// <summary>
        /// 载入配置文件
        /// </summary>
        public void LoadConfig()
        {
            SimulatorConfig = XmlSerializeTool.Deserialize<RobotSimulatorConfig>(File.ReadAllText(Path.Combine(Application.StartupPath, ROBOT_CONFIG_FILENAME)));
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        public void SaveConfig()
        {
            if (SimulatorConfig == null)
            {
                return;
            }

            string xml = XmlSerializeTool.Serializer<RobotSimulatorConfig>(SimulatorConfig);
            if (string.IsNullOrEmpty(xml))
            {
                return;
            }
            else
            {
                File.WriteAllText(Path.Combine(Application.StartupPath, ROBOT_CONFIG_FILENAME), xml);
            }
        }
    }
}