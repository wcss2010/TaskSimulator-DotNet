﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Monitors
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于查询GPS或Comera等设备的数据的接口
     * 
     */
    public interface IMonitor
    {
        /// <summary>
        /// 所属设备用户
        /// </summary>
        RobotUser User { get; set; }

        /// <summary>
        /// 处理一个指令
        /// </summary>
        /// <param name="commandObj"></param>
        /// <returns></returns>
        CommandResult Process(Command commandObj);

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <returns></returns>
        IMonitor Clone();

        /// <summary>
        /// 临时数据
        /// </summary>
        object Tag { get; set; }

        /// <summary>
        /// 监视器名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; set; }
    }
}