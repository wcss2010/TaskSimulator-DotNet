using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors.Action
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于对TaskProcessor分配给它的Command使用线程池来运行
     * 
     */
    public class ActionProcessor
    {
        private ConcurrentQueue<ProcessorQueueObject> queues = new ConcurrentQueue<ProcessorQueueObject>();
        /// <summary>
        /// Command处理队列
        /// </summary>
        public ConcurrentQueue<ProcessorQueueObject> Queues
        {
            get { return queues; }
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private BackgroundWorker workers = new BackgroundWorker();

        public ActionProcessor()
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

                    //调用Command工作线程去处理
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object o)
                        {
                            try
                            {
                                if (queueObject != null && queueObject.Task != null && queueObject.Command != null && queueObject.User != null)
                                {
                                    if (queueObject.User.SupportedAction.ContainsKey(queueObject.Command.CommandText))
                                    {
                                        CommandResult cr = queueObject.User.SupportedAction[queueObject.Command.CommandText].Process(queueObject.Command);
                                        if (cr != null)
                                        {
                                            if (cr.IsOK)                                        
                                            {
                                                SimulatorObject.logger.Debug("设备(" + queueObject.User.UserCode + ")中的指令处理线程(" + queueObject.Command.CommandText + ")执行成功！");
                                            }
                                            else
                                            {
                                                SimulatorObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的指令处理线程(" + queueObject.Command.CommandText + ")执行失败！原因：" + cr.ErrorReason);
                                            }
                                        }
                                        else
                                        {
                                            SimulatorObject.logger.Warn("对不起，设备(" + queueObject.User.UserCode + ")中的指令处理线程(" + queueObject.Command.CommandText + ")没有返回处理结果！");
                                        }
                                    }
                                    else
                                    {
                                        SimulatorObject.logger.Error("对不起，设备(" + queueObject.User.UserCode + ")中没有指令处理线程(" + queueObject.Command.CommandText + ")");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                SimulatorObject.logger.Error(ex.ToString());
                            }
                        }));
                }
                catch (Exception ex)
                {
                    SimulatorObject.logger.Error(ex.ToString());
                }
            }
        }
    }
}