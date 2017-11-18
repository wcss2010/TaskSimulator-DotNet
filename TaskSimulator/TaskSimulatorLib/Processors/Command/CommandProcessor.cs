using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using TaskSimulatorLib.Entitys;

namespace TaskSimulatorLib.Processors.Command
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于对TaskProcessor分配给它的Command使用线程池来运行
     * 
     */
    public class CommandProcessor
    {
        private ConcurrentQueue<CommandProcessorQueueObject> queues = new ConcurrentQueue<CommandProcessorQueueObject>();
        /// <summary>
        /// Command处理队列
        /// </summary>
        public ConcurrentQueue<CommandProcessorQueueObject> Queues
        {
            get { return queues; }
        }

        /// <summary>
        /// 工作线程
        /// </summary>
        private BackgroundWorker workers = new BackgroundWorker();

        public CommandProcessor()
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
            
        }
    }

    /// <summary>
    /// 处理队列对象
    /// </summary>
    public class CommandProcessorQueueObject
    {
        /// <summary>
        /// 所属任务
        /// </summary>
        Entitys.Task Task { get; set; }

        /// <summary>
        /// 所属设备用户
        /// </summary>
        DeviceUser User { get; set; }

        /// <summary>
        /// 要执行的指令参数
        /// </summary>
        Entitys.Command Command { get; set; }
    }
}