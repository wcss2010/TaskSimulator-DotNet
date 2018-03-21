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
    public partial class GPSPointSelectMapMonitor : MapMonitorBase
    {
        public GPSPointSelectMapMonitor()
        {
            InitializeComponent();

            //初始化地图
            InitMap(new GMap.NET.PointLatLng(24.2120, 135.4603), 6, 3, 21, false);

            MapControl.MouseDoubleClick += MapControl_MouseDoubleClick;
        }

        void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}