using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskSimulator.Forms
{
    public partial class FlyPathMapMonitor : MapMonitorBase
    {
        /// <summary>
        /// 航行路径文本
        /// </summary>
        public string FlyPathText { get; set; }

        public FlyPathMapMonitor()
        {
            InitializeComponent();

            //初始化地图
            InitMap(new GMap.NET.PointLatLng(24.2120, 135.4603), 6, 3, 21, false);

            MapControl.MouseDoubleClick += MapControl_MouseDoubleClick;
            MapControl.MouseClick += MapControl_MouseClick;
            MapControl.MouseDown += MapControl_MouseDown;
            MapControl.MouseUp += MapControl_MouseUp;
            MapControl.OnPolygonLeave += MapControl_OnPolygonLeave;
            MapControl.OnPolygonEnter += MapControl_OnPolygonEnter;
        }

        void MapControl_OnPolygonEnter(GMap.NET.WindowsForms.GMapPolygon item)
        {
            
        }

        void MapControl_OnPolygonLeave(GMap.NET.WindowsForms.GMapPolygon item)
        {
            
        }

        void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        void MapControl_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }


    }
}