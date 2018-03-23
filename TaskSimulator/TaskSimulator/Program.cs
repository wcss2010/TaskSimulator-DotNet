using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TaskSimulator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //创建动态代码目录
            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.IO.Path.Combine(Application.StartupPath, TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_COMPONENT_DIR),TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_DLLFILES_DIR));
            }
            catch (Exception ex) { }

            try
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_RESFILES_DIR));
            }
            catch (Exception ex) { }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}