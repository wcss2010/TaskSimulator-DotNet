﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Engines;
using TaskSimulatorLib.Processors;

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
        /// 任务处理器(用于生成Command)
        /// </summary>
        public ITaskWorkerThread TaskProcessorUnit { get; set; }

        ConcurrentDictionary<string, ICommandWorkerThread> commandWorkerDict = new ConcurrentDictionary<string, ICommandWorkerThread>();
        /// <summary>
        /// 任务的指令执行器字典
        /// </summary>
        public ConcurrentDictionary<string, ICommandWorkerThread> CommandWorkerDict
        {
            get { return commandWorkerDict; }
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
}