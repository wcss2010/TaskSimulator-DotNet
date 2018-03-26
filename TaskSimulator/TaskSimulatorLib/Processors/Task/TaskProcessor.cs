using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors.Task
{
    public delegate void TaskCompleteDelegate(object sender, TaskCompleteArgs args);
    public class TaskCompleteArgs : EventArgs
    {
        public RobotUser User { get; set; }
        public Entitys.RobotTask Task { get; set; }
    }

    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于自主产生任务以及对任务进行分解
     * 
     */
    public class TaskProcessor
    {
        private ConcurrentQueue<ProcessorQueueObject> queues = new ConcurrentQueue<ProcessorQueueObject>();
        /// <summary>
        /// Task处理队列
        /// </summary>
        public ConcurrentQueue<ProcessorQueueObject> Queues
        {
            get { return queues; }
            set { queues = value; }
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private BackgroundWorker workers = new BackgroundWorker();

        /// <summary>
        /// 任务完成事件
        /// </summary>
        public event TaskCompleteDelegate OnTaskCompleteEvent;

        public TaskProcessor()
        {
            workers.WorkerSupportsCancellation = true;
            workers.DoWork += workers_DoWork;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            if (workers.IsBusy)
            {
                return;
            }

            workers.RunWorkerAsync();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            workers.CancelAsync();
        }

        void workers_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!workers.CancellationPending)
            {
                DoWork();

                try
                {
                    Thread.Sleep(2);
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// 投递任务完成事件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="task"></param>
        protected void OnTaskComplete(RobotUser user, Entitys.RobotTask task)
        {
            if (OnTaskCompleteEvent != null)
            {
                TaskCompleteArgs tca = new TaskCompleteArgs();
                tca.User = user;
                tca.Task = task;

                OnTaskCompleteEvent(this, tca);
            }
        }

        /// <summary>
        /// 线程执行体
        /// </summary>
        private void DoWork()
        {
            if (Queues != null && Queues.Count > 0)
            {
                ProcessorQueueObject queueObject = null;

                try
                {
                    //取一个对象
                    queueObject = new ProcessorQueueObject();
                    Queues.TryDequeue(out queueObject);
                }
                catch (Exception ex)
                {
                    SimulatorObject.logger.Error(ex.ToString());
                }

                //调用Command工作线程去处理
                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                    {
                        try
                        {
                            //开始处理任务
                            if (queueObject != null && queueObject.Task != null && queueObject.User != null && queueObject.Command != null)
                            {
                                if (queueObject.Task.TaskWorkerThread != null)
                                {
                                    //检查这个任务是否要用
                                    if (queueObject.Task.Enabled)
                                    {
                                        CommandResult cr = queueObject.Task.TaskWorkerThread.Process(queueObject.Command);
                                        if (cr != null)
                                        {
                                            //打印处理结果
                                            if (cr.IsOK)
                                            {
                                                //SimulatorObject.logger.Debug("设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")处理成功！");
                                            }
                                            else
                                            {
                                                SimulatorObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")处理失败！原因：" + cr.ErrorReason);
                                            }
                                        }
                                        else
                                        {
                                            SimulatorObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")没有返回处理结果!");
                                        }

                                        //检查这个任务是不是没有完成
                                        if (queueObject.Task.TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Ended)
                                        {
                                            //投递任务完成事件
                                            OnTaskComplete(queueObject.User, queueObject.Task);
                                        }
                                        else
                                        {
                                            //还在运行的任务需要再入队列
                                            Queues.Enqueue(queueObject);
                                        }
                                    }
                                    else
                                    {
                                        SimulatorObject.logger.Error("对不起，设备(" + queueObject.User.UserCode + ")不支持任务(" + queueObject.Task.TaskCode + ")");
                                    }
                                }
                                else
                                {
                                    SimulatorObject.logger.Error("对不起，设备(" + queueObject.User.UserCode + ")中没有任务处理线程(" + queueObject.Task.TaskCode + ")");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SimulatorObject.logger.Error(ex.ToString());

                            //出错的任务，为了保险起见，仍然继续执行，并打印错误

                            //检查这个任务是不是没有完成
                            if (queueObject.Task.TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Ended)
                            {
                                //投递任务完成事件
                                OnTaskComplete(queueObject.User, queueObject.Task);
                            }
                            else
                            {
                                //还在运行的任务需要再入队列
                                Queues.Enqueue(queueObject);
                            }
                        }
                    }));
            }
        }
    }
}