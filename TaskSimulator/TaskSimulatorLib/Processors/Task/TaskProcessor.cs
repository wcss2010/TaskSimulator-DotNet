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
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private BackgroundWorker workers = new BackgroundWorker();

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
                    Thread.Sleep(5);
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// 线程执行体
        /// </summary>
        private void DoWork()
        {
            if (Queues != null && Queues.Count > 0)
            {
                try
                {
                    //取一个对象
                    ProcessorQueueObject queueObject = new ProcessorQueueObject();
                    Queues.TryDequeue(out queueObject);

                    //开始处理任务
                    if (queueObject != null && queueObject.Task != null && queueObject.User != null && queueObject.Command != null)
                    {
                        if (queueObject.Task.TaskWorkerThread != null)
                        {
                            CommandResult cr = queueObject.Task.TaskWorkerThread.Process(queueObject.Command);
                            if (cr != null)
                            {
                                //打印处理结果
                                if (cr.IsOK)
                                {
                                    SuperObject.logger.Debug("设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")处理成功！");
                                }
                                else
                                {
                                    SuperObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")处理失败！原因：" + cr.Reason);
                                }

                                //检查这个任务是不是没有完成

                            }
                            else
                            {
                                SuperObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的任务处理线程(" + queueObject.Task.TaskCode + ")没有返回处理结果!");
                            }
                        }
                        else
                        {
                            SuperObject.logger.Error("对不起，设备(" + queueObject.User.UserCode + ")中没有任务处理线程(" + queueObject.Task.TaskCode + ")");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SuperObject.logger.Error(ex.ToString());
                }
            }
        }
    }
}