using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Processors.Command;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulatorLib.Entitys
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于描述无人船的自主任务
     * 
     */
    public class Task
    {
        /// <summary>
        /// 任务代码
        /// </summary>
        public string TaskCode { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// 任务的当前状态
        /// </summary>
        public StateType TaskState { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int TaskPriority { get; set; }

        /// <summary>
        /// 任务处理器(用于生成Command)
        /// </summary>
        public ITaskWorkerThread TaskWorkerThread { get; set; }

        ConcurrentDictionary<string, ICommandWorkerThread> commandWorkerDict = new ConcurrentDictionary<string, ICommandWorkerThread>();
        /// <summary>
        /// 任务的指令执行器字典
        /// </summary>
        public ConcurrentDictionary<string, ICommandWorkerThread> CommandWorkerDict
        {
            get { return commandWorkerDict; }
        }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 参数对象(key=参数名，Value=参数值)
        /// </summary>
        public Dictionary<string, object> Objects
        {
            get { return objects; }
        }

        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }

    /// <summary>
    /// 任务类型分为以下几种：
    /// 实时执行的(NowTask)，随机执行的(RandomTask)，指定时间执行的(OnlyTimeTask)，每隔一段时间执行的(TimeAfterTask)
    /// </summary>
    public enum TaskType
    {
       NowTask,RandomTask,OnlyTimeTask,TimeAfterTask
    }

    /// <summary>
    /// 任务状态分为四种,准备运行，已开始，正在运行，已结束
    /// </summary>
    public enum StateType
    {
       Ready,Started,Running,Ended
    }
}