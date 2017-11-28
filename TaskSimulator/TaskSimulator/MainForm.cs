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

            RobotTasks.RobotFactory.OnUiActionEvent += RobotFactory_OnUiActionEvent;
            RobotTasks.RobotFactory.Simulator.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
            RobotTasks.RobotFactory.Simulator.Start();

            OpenAllRobot();
        }

        /// <summary>
        /// 显示一段提示文字(UI操作)
        /// </summary>
        /// <param name="txt"></param>
        protected void ShowLogTextWithUI(string txt)
        {
            tbLogs.AppendText(DateTime.Now.ToString() + ":" + txt);

            if (tbLogs.Text.Length > 0)
            {
                tbLogs.SelectionStart = tbLogs.Text.Length; //Set the current caret position at the end
                tbLogs.ScrollToCaret(); //Now scroll it automatically
            }
        }

        /// <summary>
        /// 显示一段提示文字(线程中)
        /// </summary>
        /// <param name="txt"></param>
        protected void ShowLogTextWithThread(string txt)
        {
            if (IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(delegate()
                    {
                        ShowLogTextWithUI(txt);
                    }));
            }
        }

        private void OpenAllRobot()
        {
            
        }

        void RobotFactory_OnUiActionEvent(object sender, RobotTasks.UIActionEventArgs args)
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