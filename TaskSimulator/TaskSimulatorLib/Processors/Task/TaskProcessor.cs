using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
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
        ConcurrentDictionary<string, DeviceUser> userDict = new ConcurrentDictionary<string, DeviceUser>();
        /// <summary>
        /// 无人船用户字典
        /// </summary>
        public ConcurrentDictionary<string, DeviceUser> UserDict
        {
            get { return userDict; }
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
            
        }
    }
}