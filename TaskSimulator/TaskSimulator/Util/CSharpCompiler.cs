﻿using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskSimulator.Util
{
    /// <summary>
    /// C#动态编译器
    /// </summary>
    public class CSharpCompiler
    {
        /// <summary>
        /// 编译程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly Compile(string[] refDLL,string[] codeFiles)
        {
            //创建C#编译器实例
            CSharpCodeProvider comp = new CSharpCodeProvider();
            //编译器的传入参数     
            CompilerParameters cp = new CompilerParameters();  

            cp.ReferencedAssemblies.Add("system.dll");              //添加程序集 system.dll 的引用     
           
            cp.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "TaskSimulatorLib.dll")); //添加程序集 TaskSimulatorLib.dll 的引用

            //尝试加载第三方DLL
            if (refDLL != null)
            {
                foreach (string sss in refDLL)
                {
                    string destDllFiles = string.Empty;

                    if (string.IsNullOrEmpty(sss))
                    {
                        continue;
                    }
                    else
                    {
                        if (sss.StartsWith("./"))
                        {
                            destDllFiles = sss.Replace("./", System.IO.Path.Combine(System.IO.Path.Combine(Application.StartupPath, TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_COMPONENT_DIR), TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_DLLFILES_DIR));
                        }
                        else
                        {
                            destDllFiles = sss;
                        }
                    }

                    cp.ReferencedAssemblies.Add(destDllFiles);
                }
            }

            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;

            // 编译结果   
            CompilerResults cr = comp.CompileAssemblyFromFile(cp, codeFiles);

            if (cr.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("C#动态编译错误：").Append("\n");
                foreach (CompilerError err in cr.Errors)
                {
                    sb.Append(err.ToString()).Append("\n");
                }

                throw new Exception(sb.ToString());
            }
            else
            {
                return cr.CompiledAssembly;
            }
        }
    }
}