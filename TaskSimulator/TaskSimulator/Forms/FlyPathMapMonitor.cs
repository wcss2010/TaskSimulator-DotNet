using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskSimulator.Forms
{
    public partial class FlyPathMapMonitor : MapMonitorBase
    {
        /// <summary>
        /// 船标识
        /// </summary>
        public GMarkerGoogle BoatMarker { get; set; }

        /// <summary>
        /// 航行路径文本
        /// </summary>
        public string FlyPathText { get; set; }

        /// <summary>
        /// 船的初始位置
        /// </summary>
        public TaskSimulatorLib.Entitys.LatAndLng BoatDefaultPoint { get; set; }
        /// <summary>
        /// 线开始坐标（初始）
        /// </summary>
        private PointLatLng DefaultLineStartPoint { get; set; }

        /// <summary>
        /// 飞行线路
        /// </summary>
        private List<FlyPathLine> flyPathLines = new List<FlyPathLine>();

        /// <summary>
        /// 用于存储跟随鼠标而动的线
        /// </summary>
        private FlyPathLine TempFlyLine = null;

        /// <summary>
        /// 线开始坐标
        /// </summary>
        private PointLatLng LineStartPoint;

        /// <summary>
        /// 是否允许绘制航行路径
        /// </summary>
        private bool IsEnabledDrawFlyPath { get; set; }

        /// <summary>
        /// 每秒钟前进步数
        /// </summary>
        public double StepWithSecond { get; set; }

        public FlyPathMapMonitor()
        {
            InitializeComponent();

            //初始化地图
            InitMap(new GMap.NET.PointLatLng(24.2120, 135.4603), 6, 3, 21, false);

            MapControl.MouseDoubleClick += MapControl_MouseDoubleClick;
            MapControl.MouseClick += MapControl_MouseClick;
            MapControl.MouseDown += MapControl_MouseDown;
            MapControl.MouseUp += MapControl_MouseUp;
            MapControl.MouseMove += MapControl_MouseMove;
            MapControl.OnPolygonLeave += MapControl_OnPolygonLeave;
            MapControl.OnPolygonEnter += MapControl_OnPolygonEnter;
        }

        void MapControl_OnPolygonLeave(GMapPolygon item)
        {

        }

        void MapControl_OnPolygonEnter(GMapPolygon item)
        {

        }

        void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

            }
        }

        void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsEnabledDrawFlyPath)
            {
                if (TempFlyLine != null)
                {
                    DefaultOverlay.Routes.Remove(TempFlyLine.RouteObject);
                }

                TempFlyLine = new FlyPathLine(LineStartPoint, MapControl.FromLocalToLatLng(e.X, e.Y), Color.Red, 2);
                DefaultOverlay.Routes.Add(TempFlyLine.RouteObject);
            }
        }

        void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

            }
        }

        void MapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (TempFlyLine != null)
                {
                    LineStartPoint = TempFlyLine.EndPoints;
                    flyPathLines.Add(TempFlyLine);
                    TempFlyLine = null;
                }
            }
        }

        void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                StringBuilder sb = new StringBuilder();
                foreach (FlyPathLine fpl in flyPathLines)
                {
                    //sb.Append(fpl.StartPoints.Lat).Append(":").Append(fpl.StartPoints.Lng).Append("\n");
                    sb.Append(fpl.EndPoints.Lat).Append(":").Append(fpl.EndPoints.Lng).Append("\n");
                }

                FlyPathText = sb.ToString();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            DefaultLineStartPoint = new PointLatLng(BoatDefaultPoint.Lat, BoatDefaultPoint.Lng);
            LineStartPoint = new PointLatLng(BoatDefaultPoint.Lat, BoatDefaultPoint.Lng);

            //显示船
            BoatMarker = new GMarkerGoogle(LineStartPoint, new Bitmap(Bitmap.FromFile(System.IO.Path.Combine(Application.StartupPath, "ship.png"))));
            BoatMarker.ToolTipMode = MarkerTooltipMode.Always;
            BoatMarker.ToolTipText = "船";
            DefaultOverlay.Markers.Add(BoatMarker);

            if (string.IsNullOrEmpty(FlyPathText))
            {
                //还没有规划航行路线
                IsEnabledDrawFlyPath = true;
            }
            else
            {
                //已经规划航行路线
                IsEnabledDrawFlyPath = false;

                //加载已有的方案
                string[] flyPaths = FlyPathText.Split(new string[] { "\n" }, StringSplitOptions.None);
                if (flyPaths != null)
                {
                    //将Text文本转化成Point列表
                    List<PointLatLng> tempPoints = new List<PointLatLng>();
                    foreach (string fp in flyPaths)
                    {
                        string[] points = fp.Split(new string[] { ":" }, StringSplitOptions.None);
                        if (points != null && points.Length >= 2)
                        {
                            double lat = double.Parse(points[0]);
                            double lng = double.Parse(points[1]);

                            tempPoints.Add(new PointLatLng(lat, lng));
                        }
                    }

                    //绘制航行路线
                    PointLatLng firstPoint = DefaultLineStartPoint;
                    foreach (PointLatLng point in tempPoints)
                    {
                        FlyPathLine fpl = new FlyPathLine(firstPoint, point, Color.Red, 2);
                        DefaultOverlay.Routes.Add(fpl.RouteObject);

                        firstPoint = point;
                    }
                }
            }
        }

        private void btnDarwNewPath_Click(object sender, EventArgs e)
        {
            IsEnabledDrawFlyPath = true;
            flyPathLines.Clear();
            TempFlyLine = null;
            DefaultOverlay.Routes.Clear();

            LineStartPoint = DefaultLineStartPoint;
        }
    }

    /// <summary>
    /// 飞行线
    /// </summary>
    public class FlyPathLine
    {
        public FlyPathLine() { }

        public FlyPathLine(PointLatLng pointLatLng_S, PointLatLng pointLatLng_E, Color penColor, int width)
        {
            StartPoints = pointLatLng_S;
            EndPoints = pointLatLng_E;
            PenColor = penColor;

            RouteObject = new GMapRoute(new PointLatLng[] { StartPoints, EndPoints }, "");
            RouteObject.Stroke = new Pen(PenColor, width);
        }

        /// <summary>
        /// 线颜色
        /// </summary>
        public Color PenColor { get; set; }

        /// <summary>
        /// 线对象
        /// </summary>
        public GMapRoute RouteObject { get; set; }

        /// <summary>
        /// 开始点
        /// </summary>
        public PointLatLng StartPoints { get; set; }

        /// <summary>
        /// 结束点
        /// </summary>
        public PointLatLng EndPoints { get; set; }
    }
}