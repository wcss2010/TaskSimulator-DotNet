using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.Forms;

namespace TaskSimulator
{
    public partial class TestMainForm : Form
    {
        /// <summary>
        /// 最大日志显示行
        /// </summary>
        public const int Max_Log_Line_Count = 20;

        public TestMainForm()
        {
            InitializeComponent();

            TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
        }

        /// <summary>
        /// 任务完成时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void TaskProcessor_OnTaskCompleteEvent(object sender, TaskSimulatorLib.Processors.Task.TaskCompleteArgs args)
        {
            TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + args.User.UserName + "的任务" + args.Task.TaskName + "完成！");
            ShowLogTextWithThread("无人船" + args.User.UserName + "的任务" + args.Task.TaskName + "完成！");

        }

        /// <summary>
        /// 显示一段提示文字(UI操作)
        /// </summary>
        /// <param name="txt"></param>
        protected void ShowLogTextWithUI(string txt)
        {
            if (tbLogs.Lines != null && tbLogs.Lines.Length >= Max_Log_Line_Count)
            {
                tbLogs.Clear();
            }

            tbLogs.AppendText(DateTime.Now.ToString() + ":" + txt + "\n");

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

        private void TestMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                TaskSimulatorLib.SimulatorObject.Simulator.LoadConfig();
                if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig == null)
                {
                    MessageBox.Show("对不起，没有找到配置文件！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("对不起，配置文件加载失败！Ex:" + ex.ToString());
            }

            //开启模拟器
            TaskSimulatorLib.SimulatorObject.Simulator.Start();

            //初始化
            InitSimulator();
        }

        private void InitSimulator()
        {

        }

        private void TestMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //停止模拟器
            TaskSimulatorLib.SimulatorObject.Simulator.Stop();

            //停止无人船的Socket
            if (TaskSimulatorLib.SimulatorObject.Simulator.UserDict != null) 
            {
                foreach (TaskSimulatorLib.Entitys.RobotUser ru in TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Values)
                {
                    if (ru.RobotSocket != null)
                    {
                        try
                        {
                            ru.RobotSocket.Stop();
                        }
                        catch (Exception ex) { }
                    }
                }
            }
        }

        private void tbtnConfig_Click(object sender, EventArgs e)
        {
            SimulatorConfigEditor editor = new SimulatorConfigEditor();
            if (editor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbtnStart.PerformClick();
            }
        }

        private void tbtnStart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("真的要重启所有无人船吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                //检查当前是不是已经一份配置的运行了
                TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Clear();
                TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.Queues = new System.Collections.Concurrent.ConcurrentQueue<TaskSimulatorLib.Processors.ProcessorQueueObject>();


                //准备初始化无人船对象
                TaskSimulator.BoatRobot.RobotManager.Init();
                TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.Queues = new System.Collections.Concurrent.ConcurrentQueue<TaskSimulatorLib.Processors.ProcessorQueueObject>();

                //无人船启动
                foreach (TaskSimulatorLib.Entitys.RobotUser ru in TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Values)
                {
                    if (ru.RobotSocket != null)
                    {
                        //启动Socket
                        ru.RobotSocket.Start();

                        //启动无人船
                        ru.RobotSocket.RobotStart(null);

                        //打印日志
                        TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + ru.UserName + "已启动！");
                        ShowLogTextWithThread("无人船" + ru.UserName + "已启动！");
                    }
                }

                //初始化无人船状态列表
                InitBoatStateList();
            }
        }

        /// <summary>
        /// 初始化无人船状态
        /// </summary>
        private void InitBoatStateList()
        {
            tvRobotList.Nodes.Clear();
            foreach (TaskSimulatorLib.Entitys.RobotUser ru in TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Values)
            {
                TreeNode tn = new TreeNode();
                tn.Text = ru.UserName + "(" + ru.UserCode + ")";
                tn.Tag = ru;

                tvRobotList.Nodes.Add(tn);
            }
        }

        private void lbxSocketCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tvRobotList.SelectedNode != null && lbxSocketCommands.SelectedItem != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;
                TaskSimulatorLib.Sockets.RobotCommand robotCommand = (TaskSimulatorLib.Sockets.RobotCommand)lbxSocketCommands.SelectedItem;

                if (ru.RobotSocket != null)
                {
                    //运行相关指令
                    ru.RobotSocket.ProcessRobotCommand(robotCommand.Code, new object[] { });

                    TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + ru.UserName + "执行了" + robotCommand + "指令！");
                    ShowLogTextWithThread("无人船" + ru.UserName + "执行了" + robotCommand + "指令！");
                }
            }
        }

        private void rbRoatWorkModeAlways_CheckedChanged(object sender, EventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;
                ru.WorkMode = "always";
            }
        }

        private void rbRoatWorkModeOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;
                ru.WorkMode = "once";
            }
        }

        private void tvRobotList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;

                //工作状态
                if (ru.WorkMode == "always")
                {
                    rbRoatWorkModeAlways.Checked = true;
                }
                else
                {
                    rbRoatWorkModeOnce.Checked = true;
                }

                //Socket指令
                lbxSocketCommands.Items.Clear();
                if (ru.RobotSocket != null)
                {
                    TaskSimulatorLib.Sockets.RobotCommand[] robotCmdTeams = ru.RobotSocket.GetSupportedRobotCommands();
                    if (robotCmdTeams != null)
                    {
                        foreach (TaskSimulatorLib.Sockets.RobotCommand cmd in robotCmdTeams)
                        {
                            lbxSocketCommands.Items.Add(cmd);
                        }
                    }
                }
            }
        }

        private void trBoatStateUpdater_Tick(object sender, EventArgs e)
        {

        }
    }
}