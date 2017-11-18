using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSimulatorLib
{
    public class SuperObject
    {
       public static log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SuperObject));

       public static log4net.ILog GetLogger(Type t)
       {
           return log4net.LogManager.GetLogger(t);
       }
    }
}