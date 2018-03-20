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
using TaskSimulator.BoatRobot;
using TaskSimulatorLib.Entitys;

namespace TaskSimulator.Forms
{
    /// <summary>
    /// 无人船轨迹跟踪
    /// </summary>
    public partial class BoatMapMonitor : MapMonitorBase
    {
        Dictionary<RobotUser, GMarkerGoogle> MarkerDict = new Dictionary<RobotUser, GMarkerGoogle>();

        public BoatMapMonitor()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RobotManager.OnUiActionEvent += RobotManager_OnUiActionEvent;

            //初始化地图
            InitMap(new GMap.NET.PointLatLng(24.2120, 135.4603), 6, 6, 8, false);
        }

        void RobotManager_OnUiActionEvent(object sender, UIActionEventArgs args)
        {

            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (args.ActionName.Equals(RobotManager.UIAction_Move))
                    {
                        try
                        {
                            LatAndLng pos = (LatAndLng)args.Objects["pos"];

                            GMarkerGoogle marker = null;
                            if (MarkerDict.ContainsKey(args.User))
                            {
                                marker = MarkerDict[args.User];
                                marker.Position = new GMap.NET.PointLatLng(pos.Lat, pos.Lng);
                            }
                            else
                            {
                                marker = new GMarkerGoogle(new GMap.NET.PointLatLng(pos.Lat, pos.Lng), new Bitmap(Bitmap.FromFile(System.IO.Path.Combine(Application.StartupPath, "ship.png"))));
                                marker.ToolTipMode = MarkerTooltipMode.Always;
                                marker.ToolTipText = args.User.UserName;
                                marker.Tag = args.Task;

                                MarkerDict.Add(args.User, marker);
                                DefaultOverlay.Markers.Add(marker);
                            }
                        }
                        catch (Exception ex)
                        {
                            TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
                        }
                    }
                }));
            }
        }
    }
}