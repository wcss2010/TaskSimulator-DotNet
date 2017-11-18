using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

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
        /// 所支持的指令
        /// </summary>
        string Cmd { get; set; }

        /// <summary>
        /// 所属任务
        /// </summary>
        Task Task { get; set; }

        /// <summary>
        /// 所属设备用户
        /// </summary>
        DeviceUser User { get; set; }
        
        /// <summary>
        /// 处理一个命令
        /// </summary>
        /// <param name="commandObj"></param>
        /// <returns></returns>
        CommandResult Process(Command commandObj);

        /// <summary>
        /// 创建一个新对象
        /// </summary>
        /// <returns></returns>
        ICommandWorker Clone();

        /// <summary>
        /// 临时数据
        /// </summary>
        object Tag { get; set; }
    }
}