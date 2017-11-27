using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskSimulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RobotTaskFactory.RobotFactory.OnUiActionEvent += RobotFactory_OnUiActionEvent;
            RobotTaskFactory.RobotFactory.Simulator.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
            RobotTaskFactory.RobotFactory.Simulator.Start();

            OpenAllRobot();
        }

        private void OpenAllRobot()
        {
            
        }

        void RobotFactory_OnUiActionEvent(object sender, RobotTaskFactory.UIActionEventArgs args)
        {
            
        }

        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //停止任务处理器
            RobotTaskFactory.RobotFactory.Simulator.Stop();

            //关闭所有机器人
            CloseAllRobot();
        }

        /// <summary>
        /// 关闭所有机器人
        /// </summary>
        private void CloseAllRobot()
        {
            
        }
    }
}