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
     *  本类主要用于监视器数据查询结果
     * 
     */
    public class ObjectResult
    {
        /// <summary>
        /// 指令
        /// </summary>
        public string Cmd { get; set; }

        private SortedList<string, object> objects = new SortedList<string, object>();
        /// <summary>
        /// 结果对象(key=参数名，Value=参数值)
        /// </summary>
        public SortedList<string, object> Objects
        {
            get { return objects; }
        }

        /// <summary>
        /// 临时数据
        /// </summary>
        public object Tag { get; set; }
    }
}