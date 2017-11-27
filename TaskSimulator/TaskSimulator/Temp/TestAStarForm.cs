using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TaskSimulatorLib.Util;

namespace TaskSimulator
{
    /// <summary>
    /// A*自动导路算法测试
    /// </summary>
    public partial class TestAStarForm : Form
    {
        TstDoPlan tdp = new TstDoPlan();

        /// <summary>
        /// 开始节点
        /// </summary>
        Point start = new Point(0, 0);
        /// <summary>
        /// 结束节点
        /// </summary>
        Point end = new Point(22, 22);
        /// <summary>
        /// 阻挡物节点
        /// </summary>
        public static List<Point> collPoints = new List<Point>();

        public TestAStarForm()
        {
            InitializeComponent();
            collPoints.Add(new Point(5, 2));
            collPoints.Add(new Point(5, 3));
            collPoints.Add(new Point(5, 4));
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FlashPnlMain();
            DrawStartEndPoint();
            DrawColl();
        }

        /// <summary>
        /// 画开始和结束点
        /// </summary>
        private void DrawStartEndPoint()
        {
            Brush brush = new SolidBrush(Color.Blue);
            Rectangle tabRect = Rectangle.FromLTRB((start.X + 1) * 20 + 1, (start.Y + 1) * 20 + 1, (start.X + 2) * 20, (start.Y + 2) * 20);
            Graphics g = pnlMain.CreateGraphics();
            g.FillRectangle(brush, tabRect);


            Brush brushEnd = new SolidBrush(Color.Green);
            Rectangle tabRectEnd = Rectangle.FromLTRB((end.X + 1) * 20 + 1, (end.Y + 1) * 20 + 1, (end.X + 2) * 20, (end.Y + 2) * 20);
            g.FillRectangle(brushEnd, tabRectEnd);
        }

        /// <summary>
        /// 画阻隔
        /// </summary>
        private void DrawColl()
        {
            int count = collPoints.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    Point colPoint = collPoints[i];

                    Brush brush = new SolidBrush(Color.Black);
                    Rectangle tabRect = Rectangle.FromLTRB((colPoint.X + 1) * 20 + 1, (colPoint.Y + 1) * 20 + 1, (colPoint.X + 2) * 20, (colPoint.Y + 2) * 20);
                    Graphics g = pnlMain.CreateGraphics();
                    g.FillRectangle(brush, tabRect);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 移出阻挡
        /// </summary>
        /// <param name="point">节点</param>
        private void RemoveColl(Point point)
        {
            Brush brush = new SolidBrush(Color.White);
            Rectangle tabRect = Rectangle.FromLTRB((point.X + 1) * 20 + 1, (point.Y + 1) * 20 + 1, (point.X + 2) * 20, (point.Y + 2) * 20);
            Graphics g = pnlMain.CreateGraphics();
            g.FillRectangle(brush, tabRect);
        }
        /// <summary>
        /// 移出阻挡
        /// </summary>
        /// <param name="point">节点对象</param>
        /// <param name="color">节点颜色</param>
        private void RemoveColl(Point point, Color color)
        {
            Brush brush = new SolidBrush(color);
            Rectangle tabRect = Rectangle.FromLTRB((point.X + 1) * 20 + 1, (point.Y + 1) * 20 + 1, (point.X + 2) * 20, (point.Y + 2) * 20);
            Graphics g = pnlMain.CreateGraphics();
            g.FillRectangle(brush, tabRect);
        }

        /// <summary>
        /// 画节点
        /// </summary>
        private void FlashPnlMain()
        {
            int xCount = pnlMain.Width / 20;
            int yCount = pnlMain.Height / 20;

            Font font = new Font(new FontFamily("宋体"), (float)9.0, FontStyle.Regular);
            Brush fontColor = new SolidBrush(Color.Black);
            for (int x = 0; x <= xCount - 1; x++)
            {
                for (int y = 0; y <= yCount - 1; y++)
                {
                    Brush brush = new SolidBrush(Color.Red);
                    Rectangle tabRect = Rectangle.FromLTRB(x * 20, y * 20, (x + 1) * 20, (y + 1) * 20);
                    Graphics g = pnlMain.CreateGraphics();
                    Pen pen = new Pen(brush);
                    g.DrawRectangle(pen, tabRect);
                    if (x == 0 && y != 0)
                    {
                        g.DrawString((y - 1).ToString(), font, fontColor, x * 20 + 4, y * 20 + 4);
                    }
                    else if (y == 0 && x != 0)
                    {
                        g.DrawString((x - 1).ToString(), font, fontColor, x * 20 + 4, y * 20 + 4);
                    }
                }
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            FlashPnlMain();
            DrawStartEndPoint();
            DrawColl();
        }

        private void pnlMain_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = GetPointByXY(e.X, e.Y);

            if (point == start || point == end)
            {
                return;
            }

            bool flag = false;
            for (int i = 0; i < collPoints.Count; i++)
            {
                if (collPoints[i] == point)
                {
                    flag = true;
                }
            }
            if (!flag)
            {
                collPoints.Add(point);
                DrawColl();
            }
            else
            {
                collPoints.Remove(point);
                RemoveColl(point);
            }
        }

        /// <summary>
        /// 清空节点中数据
        /// </summary>
        private void RefreshPoint()
        {
            int xCount = pnlMain.Width / 20;
            int yCount = pnlMain.Height / 20;

            for (int x = 0; x <= xCount - 1; x++)
            {
                for (int y = 0; y <= yCount - 1; y++)
                {
                    Point point = new Point(x, y);
                    if (point == start || point == end)
                    {
                        DrawStartEndPoint();
                    }
                    else if (!collPoints.Contains(point))
                    {
                        RemoveColl(point);
                    }
                }
            }
        }

        /// <summary>
        /// 开始寻路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAPath_Click(object sender, EventArgs e)
        {
            RefreshPoint();

            //清空开启、关闭列表
            tdp.OpenPoints.Clear();
            tdp.ClosePoints.Clear();

            AStarPoint startPoint = new AStarPoint(start, null, 0, 0);
            AStarPoint endPoint = new AStarPoint(end, null, 0, 0);

            //1、把起始格添加到开启列表
            tdp.OpenPoints.Add(startPoint);

            IList<Point> route = tdp.Start(endPoint);
            if (route == null)
            {
                MessageBox.Show("路径不通！");
                return;
            }
            Font font = new Font(new FontFamily("宋体"), (float)9.0, FontStyle.Regular);
            Brush fontColor = new SolidBrush(Color.Red);
            for (int i = 0; i < route.Count; i++)
            {
                Point tmpPoint = route[i];
                Graphics g = pnlMain.CreateGraphics();
                g.DrawString((i + 1).ToString(), font, fontColor, (tmpPoint.X + 1) * 20 + 4, (tmpPoint.Y + 1) * 20 + 4);

                //if (tmpPoint != start && tmpPoint != end)
                //{
                //    RemoveColl(tmpPoint, Color.Red);
                //}

                //Brush brush = new SolidBrush(Color.Green);
                //Rectangle tabRect = Rectangle.FromLTRB((tmpPoint.X + 1) * 20 + 1, (tmpPoint.Y + 1) * 20 + 1, (tmpPoint.X + 2) * 20, (tmpPoint.Y + 2) * 20);
                //Graphics g = pnlMain.CreateGraphics();
                ////bool s = RecMap.Contains(tabRect);
                //g.FillRectangle(brush, tabRect);

                //Brush brush = new SolidBrush(Color.Red);
                //Rectangle tabRect = Rectangle.FromLTRB(x * 20, y * 20, (x + 1) * 20, (y + 1) * 20);
                //Graphics g = pnlMain.CreateGraphics();
                //Pen pen = new Pen(brush);
                //g.DrawRectangle(pen, tabRect);
            }
        }

        /// <summary>
        /// 获取节点位置
        /// </summary>
        /// <param name="X">相对窗体中X坐标</param>
        /// <param name="Y">相对窗体中Y坐标</param>
        /// <returns></returns>
        private Point GetPointByXY(int X, int Y)
        {
            int x = X / 20;
            int y = Y / 20;
            if (X / 20 != 0)
            {
                x = x - 1;
            }

            if (Y / 20 != 0)
            {
                y = y - 1;
            }
            return new Point(x, y);
        }

        private void btnSetStart_Click(object sender, EventArgs e)
        {
            if (RightClickXY.IsEmpty == false && RightClickXY != end && !collPoints.Contains(RightClickXY))
            {
                RemoveColl(start);
                start = RightClickXY;
                DrawStartEndPoint();
            }
        }

        private void btnSetEnd_Click(object sender, EventArgs e)
        {
            if (RightClickXY.IsEmpty == false && RightClickXY != start && !collPoints.Contains(RightClickXY))
            {
                RemoveColl(end);
                end = RightClickXY;
                DrawStartEndPoint();
            }
        }

        Point RightClickXY = Point.Empty;
        private void pnlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RightClickXY = GetPointByXY(e.X, e.Y);
            }
        }
    }

    public class TstDoPlan : BaseDoPlan
    {
        protected override bool IsBar(Point point)
        {
            return this.IsInList(point, TestAStarForm.collPoints);
        }
    }
}