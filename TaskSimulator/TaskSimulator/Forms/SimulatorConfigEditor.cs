using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulatorLib.Entitys;

namespace TaskSimulator.Forms
{
    public partial class SimulatorConfigEditor : Form
    {
        private List<DynamicComponent> dynamicComponentList = new List<DynamicComponent>();

        public SimulatorConfigEditor()
        {
            InitializeComponent();

            if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig == null)
            {
                TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig = new TaskSimulatorLib.Entitys.RobotSimulatorConfig();
            }

            //加载动态组件列表
            dynamicComponentList.AddRange(TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.MonitorComponentMap.Values);
            dynamicComponentList.AddRange(TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.TaskComponentMap.Values);
            foreach (DynamicComponent dc in dynamicComponentList)
            {
                if (dc.ComponentType == DynamicComponentType.Monitor)
                {
                    //监视器
                }
                else
                {
                    //任务控制器
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnSelectSocketController_Click(object sender, EventArgs e)
        {
            if (ofdCSFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbSocketControllerFile.Text = ofdCSFile.FileName;
            }
        }

        private void btnCodeAdd_Click(object sender, EventArgs e)
        {
            gbComponentDetail.Enabled = true;
            ClearDynamicComponentEditor();
        }

        private void btnCodeSave_Click(object sender, EventArgs e)
        {
            gbComponentDetail.Enabled = false;



            ClearDynamicComponentEditor();
        }

        private void btnCodeDel_Click(object sender, EventArgs e)
        {

        }

        private void ClearDynamicComponentEditor()
        {
            tbComponentId.Text = "";
            tbComponentName.Text = "";
            tbComponentClassFile.Text = "";
            tbComponentClassFullName.Text = "";
            tbClassCode.Text = "";
            rbIsMonitor.Checked = true;
        }

        private void btnSelectComponentFile_Click(object sender, EventArgs e)
        {
            if (ofdCSFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbComponentClassFile.Text = ofdCSFile.FileName;
                tbClassCode.Text = System.IO.File.ReadAllText(tbComponentClassFile.Text.Trim());
            }
        }

        private void rbIsMonitor_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = ((RadioButton)sender);
            if (radioButton.Checked)
            {
                gbComponentDetail.Text = "动态" + radioButton.Text + "详细";
            }
        }

        private void rbIsTaskController_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = ((RadioButton)sender);
            if (radioButton.Checked)
            {
                gbComponentDetail.Text = "动态" + radioButton.Text + "详细";
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearDynamicComponentEditor();
            gbComponentDetail.Enabled = false;


        }
    }
}