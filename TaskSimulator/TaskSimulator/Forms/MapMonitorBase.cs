using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulatorLib.Entitys;

namespace TaskSimulator.Forms
{
    /// <summary>
    /// 基于GMap.Net实现的地图基础窗体
    /// 
    /// System.IO.Path.Combine(Application.StartupPath, "GMapCache")
    /// </summary>
    public partial class MapMonitorBase : Form
    {
        /// <summary>
        /// 默认图层可以用于显示船只等
        /// </summary>
        public GMapOverlay DefaultOverlay { get; set; }

        /// <summary>
        /// 地图控件
        /// </summary>
        public GMapControl MapControl
        {
            get
            {
                return mapControl;
            }
        }

        /// <summary>
        /// 当前鼠标点击的经纬度
        /// </summary>
        public PointLatLng PointFromMouseClick { get; set; }

        public MapMonitorBase()
        {
            InitializeComponent();

            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, "GMapCache"));
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public virtual void InitMap(PointLatLng defaultPosition,int currentZoom, int minZoom, int maxZoom, bool isUseCacheOnly)
        {
            mapControl.CacheLocation = System.IO.Path.Combine(Application.StartupPath, "GMapCache"); //缓存位置
            mapControl.MaxZoom = maxZoom;
            mapControl.MinZoom = minZoom;
            mapControl.Zoom = currentZoom;

            //选择缓存模式
            if (isUseCacheOnly)
            {
                mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
            }
            else
            {
                if (PingIpOrDomainName("www.baidu.com"))
                {
                    mapControl.Manager.Mode = GMap.NET.AccessMode.ServerAndCache;
                }
                else
                {
                    mapControl.Manager.Mode = GMap.NET.AccessMode.CacheOnly;
                }
            }

            //如果使用离线模式则尝试加载默认离线地图
            if (mapControl.Manager.Mode == AccessMode.CacheOnly)
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(Application.StartupPath, "offline.gmdb")))
                {
                    bool result = mapControl.Manager.ImportFromGMDB(System.IO.Path.Combine(Application.StartupPath, "offline.gmdb"));
                    if (result)
                    {
                        //地图载入完成
                    }
                }
            }

            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = System.Windows.Forms.MouseButtons.Left; //左键拖拽地图
            mapControl.MapProvider = GMapProviders.GoogleChinaMap;

            try
            {
                //1.美国空军嘉手纳空军基地
                //位置： 日本冲绳县中头郡    
                mapControl.Position = defaultPosition;
            }
            catch (Exception ex) { }

            //添加图层
            DefaultOverlay = new GMapOverlay("objects");
            mapControl.Overlays.Add(this.DefaultOverlay);
        }
        
        /// <summary>
        /// 用于检查IP地址或域名是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败 
        /// </summary>
        /// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();
                PingOptions objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                string data = "";
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                int intTimeout = 120;
                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                string strInfo = objPinReply.Status.ToString();
                if (strInfo == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void mapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PointLatLng latLng = mapControl.FromLocalToLatLng(e.X, e.Y);
                PointFromMouseClick = new PointLatLng(Math.Abs(latLng.Lat), latLng.Lng);
            }
        }

        private void mapControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PointLatLng latLng = mapControl.FromLocalToLatLng(e.X, e.Y);
                PointFromMouseClick = new PointLatLng(Math.Abs(latLng.Lat), latLng.Lng);
            }
        }
    }
}