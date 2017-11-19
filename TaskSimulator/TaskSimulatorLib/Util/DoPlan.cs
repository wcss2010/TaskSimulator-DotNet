using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TaskSimulatorLib.Util
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类是A*自动寻路算法
     * 
     */
    public abstract class DoPlan
    {
        /// <summary>
        /// 前面或后退一步的大小
        /// </summary>
        public int StepLength = 1;

        /// <summary>
        /// 开放节点
        /// </summary>
        protected List<AStarPoint> openPoints = new List<AStarPoint>();
        /// <summary>
        /// 关闭列表
        /// </summary>
        protected List<AStarPoint> closePoints = new List<AStarPoint>();

        /// <summary>
        /// 地图区域
        /// </summary>
        public Rectangle RecMap = new Rectangle(0, 0, 24, 24);

        /// <summary>
        /// 路径规划
        /// </summary>
        /// <param name="end">结束节点</param>
        /// <returns></returns>
        public IList<Point> Start(AStarPoint end)
        {
            //2、重复如下的工作：
            //a) 寻找开启列表中F值最低的格子。我们称它为当前格。
            AStarPoint minCostFPoint = this.GetMinCostFPoint(openPoints);
            if (minCostFPoint == null) //表明从起点到终点之间没有任何通路。
            {
                return null;
            }
            AStarPoint start = minCostFPoint;
            //b) 把它切换到关闭列表。
            RemovePoint(minCostFPoint, openPoints);
            closePoints.Add(start);

            //c) 对相邻的8格中的每一个
            //检查所有相邻格子
            IList<CompassDirections> allDirections = GetAllDirections();
            foreach (CompassDirections direction in allDirections)
            {
                Point point = GetAdjacentPoint(start.CurrentPoint, direction);

                //相邻点已经在地图之外
                if (!RecMap.Contains(point))
                {
                    continue;
                }

                //如果它不可通过或者已经在关闭列表中，略过它。反之如下。 
                if (IsBar(point) || IsInList(point, closePoints) || IsCorner(start.CurrentPoint, direction))
                {
                    continue;
                }

                int costG = start.G + GetCost(direction);
                int costH = GetCostH(point, end.CurrentPoint);
                //如果它已经在开启列表中，用G值为参考检查新的路径是否更好。更低的G值意味着更好的路径。如果是这样，就把这一格的父节点改成当前格，并且重新计算这一格的G和F值
                //如果你保持你的开启列表按F值排序，改变之后你可能需要重新对开启列表排序。 
                AStarPoint existPoint;
                if (IsInList(point, openPoints, out existPoint))
                {
                    if (existPoint.G > costG)
                    {
                        existPoint.ResetParentPoint(start, costG);
                    }
                }
                //如果它不在开启列表中，把它添加进去。把当前格作为这一格的父节点。记录这一格的F,G,和H值。 
                else
                {
                    AStarPoint newPoint = new AStarPoint(point, start, costG, costH);
                    openPoints.Add(newPoint);
                }
            }
            //d) 停止，当你 
            //把目标格添加进了开启列表，这时候路径被找到，或者 
            AStarPoint endPoint;
            if (IsInList(end.CurrentPoint, openPoints, out endPoint))
            {
                //3、保存路径。从目标格开始，沿着每一格的父节点移动直到回到起始格。这就是你的路径。
                IList<Point> route = new List<Point>();
                route.Add(endPoint.CurrentPoint);
                //route.Insert(0, currenNode.Location);
                AStarPoint tempPoint = endPoint;
                while (tempPoint.ParentPoint != null)
                {
                    route.Insert(0, tempPoint.ParentPoint.CurrentPoint);
                    tempPoint = tempPoint.ParentPoint;
                }

                return route;
            }
            //没有找到目标格，开启列表已经空了。这时候，路径不存在。 
            if (openPoints.Count == 0)
            {
                return null;
            }
            return this.Start(end);
        }

        /// <summary>
        /// 路径规划
        /// </summary>
        /// <param name="start">开始节点</param>
        /// <param name="end">结束节点</param>
        /// <returns></returns>
        public IList<Point> Start(AStarPoint start, AStarPoint end)
        {
            //2、重复如下的工作：
            //a) 寻找开启列表中F值最低的格子。我们称它为当前格。
            //AddOpenList(openPoints, start);
            //b) 把它切换到关闭列表。
            //openPoints.Remove(start);
            //closePoints.Add(start);

            //c) 对相邻的8格中的每一个
            //如果它不可通过或者已经在关闭列表中，略过它。反之如下。 
            //如果它不在开启列表中，把它添加进去。把当前格作为这一格的父节点。记录这一格的F,G,和H值。 
            //如果它已经在开启列表中，用G值为参考检查新的路径是否更好。更低的G值意味着更好的路径。如果是这样，就把这一格的父节点改成当前格，并且重新计算这一格的G和F值。如果你保持你的开启列表按F值排序，改变之后你可能需要重新对开启列表排序。 
            //d) 停止，当你 
            //把目标格添加进了开启列表，这时候路径被找到，或者 
            //没有找到目标格，开启列表已经空了。这时候，路径不存在。 


            //检查所有相邻格子
            IList<CompassDirections> allDirections = GetAllDirections();
            foreach (CompassDirections direction in allDirections)
            {
                Point point = GetAdjacentPoint(start.CurrentPoint, direction);

                //相邻点已经在地图之外
                if (!RecMap.Contains(point))
                {
                    continue;
                }

                //跳过那些已经在关闭列表中的或者不可通过的(有墙，水的地形，或者其他无法通过的地形)，把他们添加进开启列表
                if (IsBar(point) || IsInList(point, closePoints))
                {
                    continue;
                }

                int costG = GetCost(direction);
                int costH = GetCostH(point, end.CurrentPoint);
                //如果某个相邻格已经在开启列表里了，检查现在的这条路径是否更好。换句话说，检查如果我们用新的路径到达它的话，G值是否会更低一些。如果不是，那就什么都不做。
                //如果新的G值更低，那就把相邻方格的父节点改为目前选中的方格（在上面的图表中，把箭头的方向改为指向这个方格）。最后，重新计算F和G的值。
                AStarPoint existPoint;
                if (IsInList(point, openPoints, out existPoint))
                {
                    if (existPoint.G > costG)
                    {
                        existPoint.ResetParentPoint(start, costG);
                        //return this.DoPlan(existPoint, end);
                    }
                }
                //如果他们还不在里面的话。把选中的方格作为新的方格的父节点。
                else
                {
                    AStarPoint newPoint = new AStarPoint(point, start, costG, costH);
                    openPoints.Add(newPoint);
                }
            }

            //开启列表中删除，然后添加到关闭列表中
            //openPoints.Remove(start);
            RemovePoint(start, openPoints);
            closePoints.Add(start);

            //d) 停止，当你 
            //把目标格添加进了开启列表，这时候路径被找到，或者 
            //没有找到目标格，开启列表已经空了。这时候，路径不存在。 
            AStarPoint endPoint;
            if (IsInList(end.CurrentPoint, openPoints, out endPoint))
            {
                IList<Point> route = new List<Point>();
                route.Add(endPoint.CurrentPoint);
                //route.Insert(0, currenNode.Location);
                AStarPoint tempPoint = endPoint;
                while (tempPoint.ParentPoint != null)
                {
                    route.Insert(0, tempPoint.ParentPoint.CurrentPoint);
                    tempPoint = tempPoint.ParentPoint;
                }

                return route;
            }

            //3、保存路径。从目标格开始，沿着每一格的父节点移动直到回到起始格。这就是你的路径。 
            AStarPoint minCostFPoint = this.GetMinCostFPoint(openPoints);
            if (minCostFPoint == null) //表明从起点到终点之间没有任何通路。
            {
                return null;
            }

            return this.Start(minCostFPoint, end);


            //AStarPoint pointEast = GetAdjacentPoint(start, CompassDirections.East);
            //if (IsInList(pointEast, closePoints) || !IsInList(pointEast, collPoints))
            //{
            //    if (IsInList(pointEast, openPoints))
            //    {

            //    }
            //}
            //AStarPoint pointWest = GetAdjacentPoint(start, CompassDirections.West);
            //AStarPoint pointSouth = GetAdjacentPoint(start, CompassDirections.South);
            //AStarPoint pointNorth = GetAdjacentPoint(start, CompassDirections.North);

            //AStarPoint pointSouthEast = GetAdjacentPoint(start, CompassDirections.SouthEast);
            //AStarPoint pointSouthWest = GetAdjacentPoint(start, CompassDirections.SouthWest);
            //AStarPoint pointNorthEast = GetAdjacentPoint(start, CompassDirections.NorthEast);
            //AStarPoint pointNorthWest = GetAdjacentPoint(start, CompassDirections.NorthWest);
        }

        /// <summary>
        /// 移除List对象的第一个匹配项
        /// </summary>
        /// <param name="point">节点</param>
        /// <param name="pointList">节点列表</param>
        protected void RemovePoint(AStarPoint point, List<AStarPoint> pointList)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i].CurrentPoint == point.CurrentPoint)
                {
                    pointList.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 水平代价
        /// </summary>
        protected int CostH = 10;
        /// <summary>
        /// 斜角代价
        /// </summary>
        protected int CostX = 14;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentPoint"></param>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        protected int GetCostG(AStarPoint parentPoint, AStarPoint currentPoint)
        {
            if (parentPoint.X != currentPoint.X || parentPoint.Y != currentPoint.Y)
            {
                return currentPoint.G + CostX;
            }
            else
            {
                return currentPoint.G + CostH;
            }
        }

        /// <summary>
        /// 获取此节点到结束节点的代价H
        /// </summary>
        /// <param name="currentPoint">当前节点</param>
        /// <param name="endPoint">结束节点</param>
        /// <returns>代价H</returns>
        protected int GetCostH(Point currentPoint, Point endPoint)
        {
            return Math.Abs(currentPoint.X - endPoint.X) + Math.Abs(currentPoint.Y - endPoint.Y);
            //return (Math.Abs(currentPoint.X - endPoint.X) + Math.Abs(currentPoint.Y - endPoint.Y)) * CostH;
        }

        /// <summary>
        /// 获取移动一次所需要的代价G
        /// </summary>
        /// <param name="moveDirection">移动方向</param>
        /// <returns></returns>
        protected int GetCost(CompassDirections moveDirection)
        {
            if (moveDirection == CompassDirections.NotSet)
            {
                return 0;
            }

            if (moveDirection == CompassDirections.East || moveDirection == CompassDirections.West
                || moveDirection == CompassDirections.South || moveDirection == CompassDirections.North)
            {
                return CostH;
            }
            return CostX;
        }

        /// <summary>
        /// 获取节点是否在列表中
        /// </summary>
        /// <param name="point">节点</param>
        /// <param name="pointList">节点列表</param>
        /// <returns></returns>
        protected bool IsInList(Point point, List<AStarPoint> pointList)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i].CurrentPoint == point)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取节点是否在列表中
        /// </summary>
        /// <param name="point">节点</param>
        /// <param name="pointList">节点列表</param>
        /// <param name="aPoint">节点对象</param>
        /// <returns></returns>
        protected bool IsInList(Point point, List<AStarPoint> pointList, out AStarPoint aPoint)
        {
            aPoint = null;
            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i].CurrentPoint == point)
                {
                    aPoint = pointList[i];
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取节点是否在列表中
        /// </summary>
        /// <param name="point">节点</param>
        /// <param name="pointList">节点列表</param>
        /// <returns></returns>
        protected bool IsInList(Point point, List<Point> pointList)
        {
            for (int i = 0; i < pointList.Count; i++)
            {
                if (pointList[i] == point)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取节点的所有移动方向
        /// </summary>
        /// <returns></returns>
        protected IList<CompassDirections> GetAllDirections()
        {
            IList<CompassDirections> allDirections = new List<CompassDirections>();
            allDirections.Add(CompassDirections.East);
            allDirections.Add(CompassDirections.West);
            allDirections.Add(CompassDirections.South);
            allDirections.Add(CompassDirections.North);

            allDirections.Add(CompassDirections.SouthEast);
            allDirections.Add(CompassDirections.SouthWest);
            allDirections.Add(CompassDirections.NorthEast);
            allDirections.Add(CompassDirections.NorthWest);
            return allDirections;

            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.East));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.West));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.South));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.North));

            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.SouthEast));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.SouthWest));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.NorthEast));
            //openList.Add(GetAdjacentPoint(currentPoint, CompassDirections.NorthWest));
        }

        /// <summary>
        /// 获取节点的对角线移动方向
        /// </summary>
        /// <returns></returns>
        protected IList<CompassDirections> GetDiagonalDirections()
        {
            IList<CompassDirections> diagonalDirections = new List<CompassDirections>();

            diagonalDirections.Add(CompassDirections.SouthEast);
            diagonalDirections.Add(CompassDirections.SouthWest);
            diagonalDirections.Add(CompassDirections.NorthEast);
            diagonalDirections.Add(CompassDirections.NorthWest);
            return diagonalDirections;
        }

        /// <summary>
        /// 获取移动当前节点后的节点对象
        /// </summary>
        /// <param name="current">当前节点</param>
        /// <param name="direction">移动方向</param>
        /// <returns></returns>
        protected virtual Point GetAdjacentPoint(Point current, CompassDirections direction)
        {
            switch (direction)
            {
                case CompassDirections.North: return new Point(current.X, current.Y - StepLength);
                case CompassDirections.South: return new Point(current.X, current.Y + StepLength);
                case CompassDirections.East: return new Point(current.X + StepLength, current.Y);
                case CompassDirections.West: return new Point(current.X - StepLength, current.Y);
                case CompassDirections.NorthEast: return new Point(current.X + StepLength, current.Y - StepLength);
                case CompassDirections.NorthWest: return new Point(current.X - StepLength, current.Y - StepLength);
                case CompassDirections.SouthEast: return new Point(current.X + StepLength, current.Y + StepLength);
                case CompassDirections.SouthWest: return new Point(current.X - StepLength, current.Y + StepLength);
                default: return current;
            }
        }

        /// <summary>
        /// GetNodeOnLocation 目标位置location是否已存在于开放列表或关闭列表中
        /// </summary>       
        protected AStarPoint GetNodeOnLocation(AStarPoint point, List<AStarPoint> openList, List<AStarPoint> closeOpenList)
        {
            foreach (AStarPoint temp in openList)
            {
                if (temp == point)
                {
                    return temp;
                }
            }

            foreach (AStarPoint temp in closeOpenList)
            {
                if (temp == point)
                {
                    return temp;
                }
            }
            return null;
        }

        /// <summary>
        /// 从开放列表中获取代价F最小的节点，以启动下一次递归
        /// </summary>
        /// <param name="openList">开放列表</param>
        /// <returns></returns>
        protected AStarPoint GetMinCostFPoint(IList<AStarPoint> openList)
        {
            if (openList.Count == 0)
            {
                return null;
            }

            AStarPoint minPoint = openList[0];
            foreach (AStarPoint point in openList)
            {
                if (point.F < minPoint.F)
                {
                    minPoint = point;
                }
            }

            return minPoint;
        }

        protected AStarPoint GetNearPoint(AStarPoint current, AStarPoint end)
        {
            int F = 0;
            int G = 0;

            int H = 0;
            F = G + H;
            return current;
        }

        /// <summary>
        /// 是否可以穿越拐角
        /// </summary>
        public bool CanThroughCorner { get; set; }

        /// <summary>
        /// 此节点移动到下一个节点时，是否穿越了拐角
        /// </summary>
        /// <param name="point">待移动的节点</param>
        /// <param name="collList">阻挡物节点列表</param>
        /// <param name="dir">移动方向</param>
        /// <returns></returns>
        protected virtual bool IsCorner(Point point, CompassDirections dir)
        {
            if (CanThroughCorner)
            {
                return false;
            }
            {
                Point nearPointStepLength = new Point(0, 0), nearPoint2 = new Point(0, 0);
                switch (dir)
                {
                    case CompassDirections.NorthEast:
                        nearPointStepLength = new Point(point.X + StepLength, point.Y);
                        nearPoint2 = new Point(point.X, point.Y - StepLength);
                        break;
                    case CompassDirections.NorthWest:
                        nearPointStepLength = new Point(point.X - StepLength, point.Y);
                        nearPoint2 = new Point(point.X, point.Y - StepLength);
                        break;
                    case CompassDirections.SouthEast:
                        nearPointStepLength = new Point(point.X + StepLength, point.Y);
                        nearPoint2 = new Point(point.X, point.Y + StepLength);
                        break;
                    case CompassDirections.SouthWest:
                        nearPointStepLength = new Point(point.X - StepLength, point.Y);
                        nearPoint2 = new Point(point.X, point.Y + StepLength);
                        break;
                    default: return false;
                }
                if (IsBar(nearPointStepLength) || IsBar(nearPoint2))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 判断一个点是否为障碍物
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected abstract bool IsBar(Point point);
    }

    /// <summary>
    /// 节点移动方向
    /// </summary>
    public enum CompassDirections
    {
        /// <summary>
        /// 未设置
        /// </summary>
        NotSet = 0,
        /// <summary>
        /// 上移（北）
        /// </summary>
        North = 1,
        /// <summary>
        /// 右上移（东北）
        /// </summary>
        NorthEast = 2,
        /// <summary>
        /// 右移（东）
        /// </summary>
        East = 3,
        /// <summary>
        /// 右下移（东南）
        /// </summary>
        SouthEast = 4,
        /// <summary>
        /// 下移（南）
        /// </summary>
        South = 5,
        /// <summary>
        /// 左下移（西南）
        /// </summary>
        SouthWest = 6,
        /// <summary>
        /// 左移（西）
        /// </summary>
        West = 7,
        /// <summary>
        /// 左上移（西南）
        /// </summary>
        NorthWest = 8
    }

    /// <summary>
    /// A*算法，节点对象
    /// </summary>
    public class AStarPoint
    {
        /// <summary>
        /// 从起点，沿着产生的路径，移动到网格上指定方格的移动耗费。 
        /// </summary>
        public int G = 0;
        /// <summary>
        /// 从网格上那个方格移动到终点B的预估移动耗费。
        /// </summary>
        public int H = 0;

        /// <summary>
        /// X 坐标
        /// </summary>
        public int X
        {
            get { return CurrentPoint.X; }
            set { CurrentPoint.X = value; }
        }
        /// <summary>
        /// Y 坐标
        /// </summary>
        public int Y
        {
            get { return CurrentPoint.Y; }
            set { CurrentPoint.Y = value; }
        }

        /// <summary>
        /// 初始化当前节点的坐标
        /// </summary>
        /// <param name="_x">X坐标</param>
        /// <param name="_y">Y坐标</param>
        public AStarPoint(int _x, int _y)
        {
            CurrentPoint = new Point(_x, _y);
        }

        /// <summary>
        /// 初始化当前节点对象
        /// </summary>
        /// <param name="currentPoint">当前节点</param>
        /// <param name="parentPoint">父节点</param>
        /// <param name="costG">代价G</param>
        /// <param name="costH">代价H</param>
        public AStarPoint(Point currentPoint, AStarPoint parentPoint, int costG, int costH)
        {
            this.CurrentPoint = currentPoint;
            this.ParentPoint = parentPoint;
            this.G = costG;
            this.H = costH;
        }

        /// <summary>
        /// 当前节点
        /// </summary>
        public Point CurrentPoint = new Point(0, 0);
        /// <summary>
        /// 父节点
        /// </summary>
        public AStarPoint ParentPoint = null;

        /// <summary>
        /// F = G + H
        /// </summary>
        public int F
        {
            get
            {
                return this.G + this.H;
            }
        }

        /// <summary>
        /// 重置当前节点的父节点和代价G
        /// </summary>
        /// <param name="parentPoint">父节点</param>
        /// <param name="costG">代价G</param>
        public void ResetParentPoint(AStarPoint parentPoint, int costG)
        {
            this.ParentPoint = parentPoint;
            this.G = costG;
        }
    }
}