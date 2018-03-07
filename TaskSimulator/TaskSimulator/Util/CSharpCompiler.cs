using Microsoft.CSharp;
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
        /// <param name="csharpCodes"></param>
        /// <returns></returns>
        public static Assembly Compile(string[] csharpCodes)
        {
            //创建C#编译器实例
            CSharpCodeProvider comp = new CSharpCodeProvider();
            //编译器的传入参数     
            CompilerParameters cp = new CompilerParameters();  

            cp.ReferencedAssemblies.Add("system.dll");              //添加程序集 system.dll 的引用     
            cp.ReferencedAssemblies.Add("system.data.dll");         //添加程序集 system.data.dll 的引用     
            cp.ReferencedAssemblies.Add("system.xml.dll");          //添加程序集 system.xml.dll 的引用     
            cp.ReferencedAssemblies.Add("system.windows.forms.dll");  //添加程序集 system.windows.forms.dll 的引用
            cp.ReferencedAssemblies.Add("system.core.dll");              //添加程序集 system.core.dll 的引用
            cp.ReferencedAssemblies.Add("system.design.dll");              //添加程序集 system.design.dll 的引用
            cp.ReferencedAssemblies.Add("system.drawing.dll");              //添加程序集 system.drawing.dll 的引用
            cp.ReferencedAssemblies.Add("system.net.dll");              //添加程序集 system.net.dll 的引用
            cp.ReferencedAssemblies.Add("system.web.dll");              //添加程序集 system.web.dll 的引用
            
            cp.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "TaskSimulatorLib.dll")); //添加程序集 TaskSimulatorLib.dll 的引用

            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;

            // 编译结果   
            CompilerResults cr = comp.CompileAssemblyFromSource(cp, csharpCodes);

            if (cr.Errors.HasErrors)
            {
                throw new Exception(" CSharpCompiler 编译出错!");
            }
            else
            {
                return cr.CompiledAssembly;
            }
        }
    }
}