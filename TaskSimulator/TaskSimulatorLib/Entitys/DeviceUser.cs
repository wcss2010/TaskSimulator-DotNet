using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSimulatorLib.Entitys
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于这艘无人船的基本用户信息(包括支持什么样的任务，起始坐标等)
     * 
     */
    public class DeviceUser
    {
        ConcurrentDictionary<string, Task> supportedTask = new ConcurrentDictionary<string, Task>();
        /// <summary>
        /// 支持的任务
        /// </summary>
        public ConcurrentDictionary<string, Task> SupportedTask
        {
            get { return supportedTask; }
        }

    }
}