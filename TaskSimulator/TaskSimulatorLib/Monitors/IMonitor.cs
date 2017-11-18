using System;
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
        DeviceUser User { get; set; }

        /// <summary>
        /// 查询或改变监视器数据
        /// </summary>
        /// <returns></returns>
        MonitorQueryResult ExecuteOrQuery(string command, MonitorQueryParameter parameter);
    }
}