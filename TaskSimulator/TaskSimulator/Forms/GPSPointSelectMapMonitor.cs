using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulatorLib.Entitys;

namespace TaskSimulator.Forms
{
    public partial class GPSPointSelectMapMonitor : MapMonitorBase
    {
        /// <summary>
        /// 当前船的位置
        /// </summary>
        public LatAndLng BoatDefaultPoint { get; set; }

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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            Marker = new GMarkerGoogle(new GMap.NET.PointLatLng(BoatDefaultPoint.Lat, BoatDefaultPoint.Lng), new Bitmap(Bitmap.FromFile(System.IO.Path.Combine(Application.StartupPath, "ship.png"))));
            Marker.ToolTipMode = MarkerTooltipMode.Always;
            Marker.ToolTipText = "船";

            DefaultOverlay.Markers.Add(Marker);
        }

        /// <summary>
        /// 船的标记
        /// </summary>
        public GMarkerGoogle Marker { get; set; }
    }
}