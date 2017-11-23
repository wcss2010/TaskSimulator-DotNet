using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSimulatorLib.Entitys
{
    /**
     *  无人船自主任务模拟器 V1.0
     * 
     *  作者：李文龙
     * 
     *  本类主要用于描述一个指令
     * 
     */
    public class Command
    {
        public Command() { }

        public Command(string command, KeyValuePair<string, object>[] paramList)
        {
            this.Command = command;

            if (paramList != null)
            {
                foreach (KeyValuePair<string, object> kvp in paramList)
                {
                    Objects.Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// 要执行的指令
        /// </summary>
        public string Command { get; set; }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 参数对象(key=参数名，Value=参数值)
        /// </summary>
        public Dictionary<string, object> Objects
        {
            get { return objects; }
        }

        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}