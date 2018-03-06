using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Processors;
using TaskSimulatorLib.Processors.Action;
using TaskSimulatorLib.Processors.Task;

namespace TaskSimulatorLib
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类是整个类库的入口
     * 
     */
    public class SimulatorObject
    {
       public static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SimulatorObject));       
       /// <summary>
       /// 生成一个日志对象
       /// </summary>
       /// <param name="t"></param>
       /// <returns></returns>
       public static log4net.ILog GetLogger(Type t)
       {
           return log4net.LogManager.GetLogger(t);
       }
       
       ConcurrentDictionary<string, RobotUser> userDict = new ConcurrentDictionary<string, RobotUser>();
       /// <summary>
       /// 无人船用户字典
       /// </summary>
       public ConcurrentDictionary<string, RobotUser> UserDict
       {
           get { return userDict; }
       }

       private TaskProcessor taskProcessor = new TaskProcessor();
       /// <summary>
       /// 任务处理器
       /// </summary>
       public TaskProcessor TaskProcessor
       {
           get { return taskProcessor; }
       }

       private ActionProcessor actionProcessor = new ActionProcessor();
       /// <summary>
       /// 动作处理器
       /// </summary>
       public ActionProcessor ActionProcessor
       {
           get { return actionProcessor; }
       }
       
       /// <summary>
       /// 开始
       /// </summary>
       public void Start()
       {
           TaskProcessor.Start();
           ActionProcessor.Start();
       }
       
       /// <summary>
       /// 停止
       /// </summary>
       public void Stop()
       {
           TaskProcessor.Stop();
           ActionProcessor.Stop();
       }

       /// <summary>
       /// 添加一个任务到任务队列中准备执行
       /// </summary>
       /// <param name="pqo"></param>
       public void AddTaskToRunningQueue(ProcessorQueueObject pqo)
       {
           bool enabledAdd = true;
           foreach (ProcessorQueueObject queue in taskProcessor.Queues)
           {
               if (queue.User.Equals(pqo.User) && queue.Task.Equals(pqo.Task))
               {
                   enabledAdd = false;
                   break;
               }
           }
           if (enabledAdd)
           {
               pqo.Task.TaskWorkerThread.WorkerThreadState = WorkerThreadStateType.Started;
               taskProcessor.Queues.Enqueue(pqo);
           }
       }

       /// <summary>
       /// 启动一个任务
       /// </summary>
       /// <param name="userCode"></param>
       public void StartTask(string userCode, string taskCode,Command taskCommand)
       {
           if (UserDict.ContainsKey(userCode))
           {
               //取出用户
               RobotUser selectedUser = UserDict[userCode];

               //如果不存在这个任务，则不添加新任务
               if (selectedUser == null || !selectedUser.SupportedTask.ContainsKey(taskCode))
               {
                   return;
               }

               //如果正在运行，则不添加新任务
               if (selectedUser.SupportedTask[taskCode].TaskWorkerThread.WorkerThreadState == WorkerThreadStateType.Running)
               {
                   return;
               }

               //重置工作线程状态为Ready
               selectedUser.SupportedTask[taskCode].TaskWorkerThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready;

               //添加到任务队列
               AddTaskToRunningQueue(new ProcessorQueueObject(selectedUser, selectedUser.SupportedTask[taskCode], taskCommand));
           }
       }
    }
}