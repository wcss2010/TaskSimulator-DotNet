using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Extends.Base;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulator.BoatRobot.Components
{
    public class BoatFlyTask : BaseTaskProcessorThread
    {
        /// <summary>
        /// 使用外部定义好的航行路径来播放
        /// </summary>
        public const string Command_FlyWithPath = "FlyWithPath";

        /// <summary>
        /// 从一个初始坐标点开始画方框(根据Limit)
        /// </summary>
        public const string Command_FlyWithRect = "FlyWithRect";

        /// <summary>
        /// 从一个初始坐标点开始画圆圈(根据Limit)
        /// </summary>
        public const string Command_FlyWithRound = "FlyWithRound";

        /// <summary>
        /// 外部航行路径
        /// </summary>
        public const string Property_Path = "Path";

        /// <summary>
        /// 圆圈半径或方框边长
        /// </summary>
        public const string Property_Radius = "Radius";

        private Queue<LatAndLng> FlyPosQueues = new Queue<LatAndLng>();
        private DateTime lastSendTime = DateTime.Now;

        public override CommandResult Process(Command commandObj)
        {
            TaskSimulatorLib.Entitys.CommandResult cr = new TaskSimulatorLib.Entitys.CommandResult();
            cr.IsOK = true;

            //设置状态为正在运行
            if (WorkerThreadState == WorkerThreadStateType.Started)
            {
                WorkerThreadState = WorkerThreadStateType.Running;
            }

            #region 尝试初始化队列
            if (FlyPosQueues.Count == 0)
            {
                if (commandObj.CommandText.Equals(Command_FlyWithPath))
                {
                    if (commandObj.Objects.ContainsKey(Property_Path))
                    {
                        //位置队列为空时初始化列表
                        List<LatAndLng> posList = (List<LatAndLng>)commandObj.Objects[Property_Path];
                        if (posList.Count > 0)
                        {
                            foreach (LatAndLng pos in posList)
                            {
                                FlyPosQueues.Enqueue(pos);
                            }
                        }
                    }
                }
                else if (commandObj.CommandText.Equals(Command_FlyWithRect))
                {
                    //画方框
                    if (commandObj.Objects.ContainsKey(Property_Radius))
                    {
                        double limit = (double)commandObj.Objects[Property_Radius];

                        commandObj.CommandText = GPSMonitor.Command_GetGPS;
                        CommandResult gps = User.SupportedMonitor[RobotManager.Monitor_GPS].Process(commandObj);
                        LatAndLng gpsCurrent = (LatAndLng)gps.Objects["pos"];

                        double lat = gpsCurrent.Lat;
                        double lng = gpsCurrent.Lng;

                        int stepCount = (int)(limit / StepWithSecond);
                        stepCount++;

                        //-
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lng = lng + StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }
                        //|
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat - StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }
                        //_
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lng = lng - StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }
                        //|
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat + StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }
                    }
                }
                else if (commandObj.CommandText.Equals(Command_FlyWithRound))
                {
                    //画圆圈
                    if (commandObj.Objects.ContainsKey(Property_Radius))
                    {
                        double limit = (double)commandObj.Objects[Property_Radius];

                        commandObj.CommandText = GPSMonitor.Command_GetGPS;
                        CommandResult gps = User.SupportedMonitor[RobotManager.Monitor_GPS].Process(commandObj);
                        LatAndLng gpsCurrent = (LatAndLng)gps.Objects["pos"];

                        double lat = gpsCurrent.Lat;
                        double lng = gpsCurrent.Lng;

                        int stepCount = (int)(limit / StepWithSecond);
                        if (stepCount % 2 > 0)
                        {
                            stepCount++;
                        }

                        //先向上走一段距离,到达圆的顶点
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat + StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }

                        //生成圆边的坐标点阵
                        int totalCount = stepCount * 2;
                        //生成右边的坐标点阵
                        for (int kkk = 1; kkk <= totalCount; kkk++)
                        {
                            if (kkk <= stepCount)
                            {
                                FlyPosQueues.Enqueue(new LatAndLng(lat - ((kkk - 1) * StepWithSecond), lng + (kkk * StepWithSecond)));
                            }
                            else
                            {
                                double newStep = stepCount * StepWithSecond;
                                newStep -= (kkk - stepCount) * StepWithSecond;
                                FlyPosQueues.Enqueue(new LatAndLng(lat - ((kkk - 1) * StepWithSecond), lng + newStep));
                            }
                        }

                        //生成左边的坐标点阵
                        int xxx = 0;
                        for (int kkk = totalCount; kkk >= 1; kkk--)
                        {
                            if (kkk >= stepCount)
                            {
                                xxx++;
                            }
                            else
                            {
                                xxx--;
                            }
                            FlyPosQueues.Enqueue(new LatAndLng(lat - (kkk * StepWithSecond), lng - (xxx * StepWithSecond)));
                        }

                        //向下走一段距离,回到圆心
                        for (int kkk = 0; kkk < stepCount; kkk++)
                        {
                            lat = lat - StepWithSecond;
                            FlyPosQueues.Enqueue(new LatAndLng(lat, lng));
                        }
                    }
                }
            }
            #endregion

            //指示CommandProcessor去播放队列中的位置点,1秒一条
            if ((DateTime.Now - lastSendTime).TotalSeconds >= 1)
            {
                lastSendTime = DateTime.Now;

                //从队列中取一个Position用来播放
                LatAndLng GPSCurrent = FlyPosQueues.Dequeue();

                //移动屏幕坐标点
                RobotManager.OnUiAction(RobotManager.UIAction_Move, User, Task, new KeyValuePair<string, object>[] { new KeyValuePair<string, object>("pos", GPSCurrent) });

                //向GPSMonitor报告自己的位置
                commandObj.CommandText = GPSMonitor.Command_ReportGPS;
                commandObj.Objects.Clear();
                commandObj.Objects.Add("pos", GPSCurrent);
                User.SupportedMonitor[RobotManager.Monitor_GPS].Process(commandObj);                
            }

            //队列中没有记录则结束任务
            if (FlyPosQueues.Count <= 0)
            {
                WorkerThreadState = WorkerThreadStateType.Ended;
            }

            return cr;
        }

        public double StepWithSecond { get; set; }
    }
}