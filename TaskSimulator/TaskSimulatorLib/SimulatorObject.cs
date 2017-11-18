using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskSimulatorLib.Entitys;
using TaskSimulatorLib.Processors.Command;
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
       
       ConcurrentDictionary<string, DeviceUser> userDict = new ConcurrentDictionary<string, DeviceUser>();
       /// <summary>
       /// 无人船用户字典
       /// </summary>
       public ConcurrentDictionary<string, DeviceUser> UserDict
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

       private CommandProcessor commandProcessor = new CommandProcessor();
       /// <summary>
       /// 指令处理器
       /// </summary>
       public CommandProcessor CommandProcessor
       {
           get { return commandProcessor; }
       }
       
       /// <summary>
       /// 开始
       /// </summary>
       public void Start()
       {
           TaskProcessor.Start();
           CommandProcessor.Start();
       }
       
       /// <summary>
       /// 停止
       /// </summary>
       public void Stop()
       {
           TaskProcessor.Stop();
           CommandProcessor.Stop();
       }
    }
}