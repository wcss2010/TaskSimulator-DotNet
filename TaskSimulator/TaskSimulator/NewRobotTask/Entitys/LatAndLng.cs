using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSimulator.NewRobotTask.Entitys
{
    /// <summary>
    /// 坐标点
    /// </summary>
    [Serializable]
    public class LatAndLng
    {
        public LatAndLng() { }

        public LatAndLng(double lat, double lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }
    }
}
