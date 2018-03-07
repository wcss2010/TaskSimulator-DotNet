using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulator.BoatRobot.Entitys;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;

namespace TaskSimulator.BoatRobot.Components
{
    /// <summary>
    /// GPS监视器(主要用于保存现在的GPS地址)
    /// </summary>
    public class GPSMonitor : BaseMonitor
    {
        /// <summary>
        /// 报告GPS位置命令
        /// </summary>
        public const string Command_ReportGPS = "ReportGPS";

        /// <summary>
        /// 获得GPS位置命令
        /// </summary>
        public const string Command_GetGPS = "GetGPS";

        public override CommandResult Process(Command commandObj)
        {
            TaskSimulatorLib.Entitys.CommandResult cr = new TaskSimulatorLib.Entitys.CommandResult();
            cr.CommandText = commandObj.CommandText;
            cr.IsOK = true;

            switch (commandObj.CommandText)
            {
                case Command_ReportGPS:
                    if (commandObj.Objects.ContainsKey("pos"))
                    {
                        GPSCurrent = (LatAndLng)commandObj.Objects["pos"];
                    }
                    break;
                case Command_GetGPS:
                    cr.Objects.Add("pos", GPSCurrent);
                    break;
            }

            return cr;
        }

        public LatAndLng GPSCurrent { get; set; }
    }
}