﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSimulator.BoatRobot.Entitys
{
    /// <summary>
    /// 用于定义机器人动态组件
    /// </summary>
    public class DynamicComponent
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ComponentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ComponentName { get; set; }

        /// <summary>
        /// 类全名
        /// </summary>
        public string ComponentClassFullName { get; set; }

        /// <summary>
        /// 类文件
        /// </summary>
        public string ComponentClassFile { get; set; }
    }
}