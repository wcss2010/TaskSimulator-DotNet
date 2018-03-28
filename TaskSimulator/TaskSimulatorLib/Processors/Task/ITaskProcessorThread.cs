﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors.Task
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于根据监视器的数据生成指令添加到CommandProcessor中
     * 
     */
    public interface ITaskProcessorThread
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        WorkerThreadStateType WorkerThreadState { get; set; }

        /// <summary>
        /// 所属任务
        /// </summary>
        RobotTask Task { get; set; }

        /// <summary>
        /// 所属设备用户
        /// </summary>
        RobotUser User { get; set; }

        /// <summary>
        /// 处理一个命令
        /// </summary>
        /// <param name="commandObj"></param>
        /// <returns></returns>
        CommandResult Process(Entitys.Command commandObj);

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <returns></returns>
        ITaskProcessorThread Clone();

        /// <summary>
        /// 临时数据
        /// </summary>
        object Tag { get; set; }
    }

    /// <summary>
    /// 任务状态分为四种,准备运行，已开始，正在运行，已结束
    /// </summary>
    public enum WorkerThreadStateType
    {
        Ready, Started, Running, Ended
    }
}