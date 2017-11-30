using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.RobotTasks;
using TaskSimulatorLib.Entitys;

namespace TaskSimulator
{
    public partial class GisMonitor : Form
    {
        Dictionary<RobotUser, GMarkerGoogle> MarkerDict = new Dictionary<RobotUser, GMarkerGoogle>();
        /// <summary>
        /// 地图上的图层，用户显示船只
        /// </summary>
        private GMapOverlay objects = null;

        public GisMonitor()
        {
            InitializeComponent();
        }

        private void GisMonitor_Load(object sender, EventArgs e)
        {
            RobotFactory.OnUiActionEvent += new UIActionDelegate(RobotFactory_OnShipMoveEvent);

            mapControl.CacheLocation = Environment.CurrentDirectory + "\\GMapCache\\"; //缓存位置
            mapControl.MaxZoom = 8;
            mapControl.MinZoom = 6;
            mapControl.Zoom = 6;
            mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = System.Windows.Forms.MouseButtons.Left; //左键拖拽地图
            mapControl.MapProvider = GMapProviders.GoogleChinaMap;

            //检查是否需要导入地图到缓存
            //if (!File.Exists(Path.Combine(Application.StartupPath, @"GMapCache\TileDBv5\en\Data.gmdb")))
            //{
            if (mapControl.Manager.ImportFromGMDB(Path.Combine(Application.StartupPath, "SeaMap201711192033.gmdb")))
            {
                mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
            }
            //}

            try
            {
                //1.美国空军嘉手纳空军基地
                //位置： 日本冲绳县中头郡    
                mapControl.Position = new GMap.NET.PointLatLng(24.2120, 135.4603);
            }
            catch (Exception ex) { }

            //添加图层
            objects = new GMapOverlay("objects");
            mapControl.Overlays.Add(this.objects);
        }

        void RobotFactory_OnShipMoveEvent(object sender, UIActionEventArgs args)
        {
            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (args.ActionName.Equals(RobotFactory.UIAction_Move))
                    {
                        try
                        {
                            double lat = double.Parse(args.Objects["lat"].ToString());
                            double lng = double.Parse(args.Objects["lng"].ToString());

                            GMarkerGoogle marker = null;
                            if (MarkerDict.ContainsKey(args.User))
                            {
                                marker = MarkerDict[args.User];
                                marker.Position = new GMap.NET.PointLatLng(lat, lng);
                            }
                            else
                            {
                                marker = new GMarkerGoogle(new GMap.NET.PointLatLng(lat, lng), new Bitmap(Bitmap.FromFile(Path.Combine(Application.StartupPath, "ship.png"))));
                                marker.ToolTipMode = MarkerTooltipMode.Always;
                                marker.ToolTipText = args.User.UserName;
                                marker.Tag = args.Task;

                                MarkerDict.Add(args.User, marker);
                                objects.Markers.Add(marker);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.ToString());
                        }
                    }
                }));
            }
        }

        private void mapControl_Click(object sender, EventArgs e)
        {

        }

        private void mapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PointLatLng latLng = mapControl.FromLocalToLatLng(e.X, e.Y);
                var current = new PointLatLng(Math.Abs(latLng.Lat), latLng.Lng);
                this.Text = this.Tag + "(" + current.Lat + "," + current.Lng + ")(已经复制到剪切板)";
                Clipboard.SetData(DataFormats.Text, "(" + current.Lat + "," + current.Lng + ")");
            }
        }
    }
}