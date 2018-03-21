using GMap.NET;
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

namespace TaskSimulator.Forms
{
    public partial class FlyPathMapMonitor : MapMonitorBase
    {
        /// <summary>
        /// 航行路径文本
        /// </summary>
        public string FlyPathText { get; set; }

        /// <summary>
        /// 船的初始位置
        /// </summary>
        public TaskSimulatorLib.Entitys.LatAndLng BoatDefaultPoint { get; set; }

        /// <summary>
        /// 多边形的点集
        /// </summary>
        private List<PointLatLng> drawingPoints = new List<PointLatLng>();
        
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
       
        /// <summary>
        /// 画出两点直接的直线
        /// </summary>
        /// <param name="pointLatLng_S"></param>
        /// <param name="pointLatLng_E"></param>
        private void DrawLineBetweenTwoPoint(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(pointLatLng_S);
            points.Add(pointLatLng_E);
            GMapRoute r = new GMapRoute(points, "");
            r.Stroke = new Pen(Color.Green, 1);
            DefaultOverlay.Routes.Add(r);
        }

        void MapControl_OnPolygonLeave(GMapPolygon item)
        {

        }

        void MapControl_OnPolygonEnter(GMapPolygon item)
        {

        }

        void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                drawingPoints.Add(MapControl.FromLocalToLatLng(e.X, e.Y));

                if (drawingPoints.Count >= 2)
                {
                    PointLatLng endPoint = drawingPoints[drawingPoints.Count - 1];
                    PointLatLng startPoint = drawingPoints[drawingPoints.Count - 2];

                    DrawLineBetweenTwoPoint(startPoint, endPoint);
                }
            }
        }

        void MapControl_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}