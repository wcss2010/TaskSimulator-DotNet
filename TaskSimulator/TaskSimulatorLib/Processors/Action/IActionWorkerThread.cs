using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors.Action
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于执行TaskProcessor生成的Command
     * 
     */
    public interface IActionWorkerThread
    {
        /// <summary>
        /// 所支持的指令
        /// </summary>
        string Cmd { get; set; }

        /// <summary>
        /// 所属任务
        /// </summary>
        Entitys.RobotTask Task { get; set; }

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
        IActionWorkerThread Clone();

        /// <summary>
        /// 临时数据
        /// </summary>
        object Tag { get; set; }
    }
}