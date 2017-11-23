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
     *  本类主要用于描述指令执行结果
     * 
     */
    public class CommandResult
    {
        public CommandResult() { }

        public CommandResult(string command, bool isok, string errorReason, object content, KeyValuePair<string, object>[] paramList)
        {
            this.CommandText = command;
            this.IsOK = isok;
            this.ErrorReason = errorReason;
            this.Content = content;

            if (paramList != null)
            {
                foreach (KeyValuePair<string, object> kvp in paramList)
                {
                    Objects.Add(kvp.Key, kvp.Value);
                }
            }
        }

        /// <summary>
        /// 已执行的指令
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsOK { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string ErrorReason { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }

        private Dictionary<string, object> objects = new Dictionary<string, object>();
        /// <summary>
        /// 结果对象(key=参数名，Value=参数值)
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