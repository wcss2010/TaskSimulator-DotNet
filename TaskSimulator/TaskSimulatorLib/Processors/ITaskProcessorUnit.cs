using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSimulatorLib.Processors
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于根据监视器的数据生成指令添加到CommandQueueWorker中
     * 
     */
    public class ITaskProcessorUnit
    {
        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}