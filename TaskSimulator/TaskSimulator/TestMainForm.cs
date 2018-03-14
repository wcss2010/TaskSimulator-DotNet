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

            //初始化对象
            TaskSimulator.BoatRobot.RobotManager.Init();

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
            TaskSimulatorLib.SimulatorObject.Simulator.Stop();
        }

        private void tbtnConfig_Click(object sender, EventArgs e)
        {
            SimulatorConfigEditor editor = new SimulatorConfigEditor();
            editor.ShowDialog();
        }
    }
}
