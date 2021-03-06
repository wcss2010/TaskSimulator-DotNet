﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.Forms;
using TaskSimulatorLib;

namespace TaskSimulator
{
    public partial class MainForm : Form, IConsoleWindowLogger
    {
        public MainForm()
        {
            InitializeComponent();

            UpdateVersionInfo();
            TaskSimulatorLib.SimulatorObject.ConsoleLoggerWindow = this;
            TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.OnTaskCompleteEvent += TaskProcessor_OnTaskCompleteEvent;
        }

        /// <summary>
        /// 更新版本信息
        /// </summary>
        private void UpdateVersionInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            this.Text = this.Tag + " V" + version.ToString();
            tssl_stateinfo.Text = "欢迎使用" + this.Tag + " V" + version.ToString();
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

            //继续出自主航行任务
            if (args.User != null && (string.IsNullOrEmpty(args.User.WorkMode) || args.User.WorkMode == TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ALWAYS))
            {
                if (args.User.SocketController!= null)
                {
                    //机器人停止
                    args.User.SocketController.RobotStop();

                    //机器人启动
                    args.User.SocketController.RobotStart(null);

                    ShowLogTextWithThread("无人船" + args.User.UserName + "的自主航行任务，正在执行中...");
                }
            }
        }

        /// <summary>
        /// 显示一段提示文字(UI操作)
        /// </summary>
        /// <param name="txt"></param>
        public void ShowLogTextWithUI(string txt)
        {
            if (tbLogs.Lines != null && tbLogs.Lines.Length >= nudMaxLogRowCount.Value)
            {
                tbLogs.Clear();
                tbLogs.Lines = new string[0];
            }

            tbLogs.AppendText(DateTime.Now.ToString() + ":" + txt + System.Environment.NewLine);

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
        public void ShowLogTextWithThread(string txt)
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
                    if (ru.SocketController != null)
                    {
                        try
                        {
                            ru.SocketController.Stop();
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
                try
                {
                    TaskSimulatorLib.SimulatorObject.logger.Info("正在进行重启操作，请稍等...");
                    ShowLogTextWithThread("正在进行重启操作，请稍等...");

                    foreach (TaskSimulatorLib.Entitys.RobotUser ru in TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Values)
                    {
                        //设置工作模式
                        ru.WorkMode = TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ONCE;

                        if (ru.SocketController != null)
                        {
                            //停止机器人
                            ru.SocketController.RobotStop();

                            //停止Socket
                            ru.SocketController.Stop();
                        }
                    }

                    //检查当前是不是已经一份配置的运行了
                    TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Clear();

                    foreach (TaskSimulatorLib.Processors.ProcessorQueueObject pqo in TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.Queues)
                    {
                        if (pqo.Task != null && pqo.Task.TaskProcessorThread != null)
                        {
                            pqo.Task.TaskProcessorThread.WorkerThreadState = TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ended;
                        }
                    }
                    TaskSimulatorLib.SimulatorObject.Simulator.TaskProcessor.Queues = new System.Collections.Concurrent.ConcurrentQueue<TaskSimulatorLib.Processors.ProcessorQueueObject>();

                    TaskSimulatorLib.SimulatorObject.logger.Info("运行缓存清理完毕...");
                    ShowLogTextWithThread("运行缓存清理完毕...");

                    TaskSimulatorLib.SimulatorObject.logger.Info("正在初始化无人船相关数据...");
                    ShowLogTextWithThread("正在初始化无人船相关数据...");

                    //准备初始化无人船对象
                    TaskSimulator.BoatRobot.RobotManager.Init();

                    TaskSimulatorLib.SimulatorObject.logger.Info("无人船相关数据初始化完毕...");
                    ShowLogTextWithThread("无人船相关数据初始化完毕...");

                    //无人船启动
                    foreach (TaskSimulatorLib.Entitys.RobotUser ru in TaskSimulatorLib.SimulatorObject.Simulator.UserDict.Values)
                    {
                        //打印日志
                        TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + ru.UserName + "正在启动...");
                        ShowLogTextWithThread("无人船" + ru.UserName + "正在启动...");

                        if (ru.SocketController != null)
                        {
                            //启动Socket
                            try
                            {
                                ru.SocketController.Start();
                            }
                            catch (Exception ex)
                            {
                                SimulatorObject.logger.Error(ex.ToString());
                                ShowLogTextWithThread(ex.ToString());
                            }

                            //启动无人船
                            ru.SocketController.RobotStart(null);
                        }

                        //打印日志
                        TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + ru.UserName + "已启动！");
                        ShowLogTextWithThread("无人船" + ru.UserName + "已启动！");
                    }

                    //初始化无人船状态列表
                    InitBoatStateList();

                    TaskSimulatorLib.SimulatorObject.logger.Info("无人船模拟器重启完毕.");
                    ShowLogTextWithThread("无人船模拟器重启完毕.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！错误:" + ex.ToString());
                }
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
                try
                {
                    TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;
                    TaskSimulatorLib.Sockets.ConsoleCommand robotCommand = (TaskSimulatorLib.Sockets.ConsoleCommand)lbxSocketCommands.SelectedItem;

                    if (ru.SocketController != null)
                    {
                        //运行相关指令
                        ru.SocketController.ProcessConsoleCommand(robotCommand.Code, new object[] { });

                        TaskSimulatorLib.SimulatorObject.logger.Debug("无人船" + ru.UserName + "执行了" + robotCommand + "指令！");
                        ShowLogTextWithThread("无人船" + ru.UserName + "执行了" + robotCommand + "指令！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("操作失败！错误:" + ex.ToString());
                }
            }
        }

        private void rbRoatWorkModeAlways_CheckedChanged(object sender, EventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;

                if (rbRoatWorkModeAlways.Checked)
                {
                    if (ru.WorkMode != TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ALWAYS)
                    {
                        if (ru.SocketController != null)
                        {
                            ru.SocketController.RobotStart(null);
                        }
                    }

                    ru.WorkMode = TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ALWAYS;
                }
            }
        }

        private void rbRoatWorkModeOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;

                if (rbRoatWorkModeOnce.Checked)
                {
                    ru.WorkMode = TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ONCE;
                }
            }
        }

        private void tvRobotList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tvRobotList.SelectedNode != null)
            {
                TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;

                //显示状态信息
                trBoatStateUpdater_Tick(this, new EventArgs());

                //工作状态
                if (ru.WorkMode == TaskSimulatorLib.Entitys.RobotUser.WORKMODE_ALWAYS || string.IsNullOrEmpty(ru.WorkMode))
                {
                    rbRoatWorkModeAlways.Checked = true;
                }
                else
                {
                    rbRoatWorkModeOnce.Checked = true;
                }

                gbBoatDetail.Text = "无人船详细(ID:" + ru.UserCode + ",名称:" + ru.UserName + ",Socket状态:" + (ru.SocketController != null ? (ru.SocketController.IsConnected() ? "在线" : "离线") : ("离线")) + ")";

                //Socket指令
                lbxSocketCommands.Items.Clear();
                if (ru.SocketController != null)
                {
                    TaskSimulatorLib.Sockets.ConsoleCommand[] robotCmdTeams = ru.SocketController.GetSupportedConsoleCommands();
                    if (robotCmdTeams != null)
                    {
                        foreach (TaskSimulatorLib.Sockets.ConsoleCommand cmd in robotCmdTeams)
                        {
                            lbxSocketCommands.Items.Add(cmd);
                        }
                    }
                }
            }
        }

        private void trBoatStateUpdater_Tick(object sender, EventArgs e)
        {
            try
            {
                if (tvRobotList.SelectedNode != null)
                {
                    TaskSimulatorLib.Entitys.RobotUser ru = (TaskSimulatorLib.Entitys.RobotUser)tvRobotList.SelectedNode.Tag;

                    //显示一段状态说明文本
                    if (ru.SocketController != null)
                    {
                        tbSocketInfo.Text = ru.SocketController.GetStatusInfo();
                    }

                    #region 显示任务状态列表
                    dgvTaskStateList.Rows.Clear();
                    foreach (TaskSimulatorLib.Entitys.RobotTask rt in ru.SupportedTask.Values)
                    {
                        List<object> cells = new List<object>();
                        cells.Add(rt.TaskName + "(" + rt.TaskCode + ")");
                        switch (rt.TaskProcessorThread.WorkerThreadState)
                        {
                            case TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ready:
                                cells.Add("准备");
                                break;
                            case TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Started:
                                cells.Add("已开始");
                                break;
                            case TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Running:
                                cells.Add("运行中");
                                break;
                            case TaskSimulatorLib.Processors.Task.WorkerThreadStateType.Ended:
                                cells.Add("已结束");
                                break;
                        }
                        int rowIndex = dgvTaskStateList.Rows.Add(cells.ToArray());
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                TaskSimulatorLib.SimulatorObject.logger.Error(ex.ToString());
            }
        }

        private void tbtnMapMonitor_Click(object sender, EventArgs e)
        {
            BoatMapMonitor mapMonitor = new BoatMapMonitor();
            mapMonitor.Show();
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            tbLogs.Text = "";
            tbLogs.Clear();
            tbLogs.Lines = new string[0];
        }
    }
}