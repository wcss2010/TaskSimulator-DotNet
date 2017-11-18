using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSimulatorLib.Engines
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于执行TaskProcessor生成的Command
     * 
     */
    public interface ICommandWorker
    {
        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}