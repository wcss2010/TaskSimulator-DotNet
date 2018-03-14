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
        public SimulatorConfigEditor()
        {
            InitializeComponent();

            if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig == null)
            {
                TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig = new TaskSimulatorLib.Entitys.RobotSimulatorConfig();
            }

            InitConfig();
        }

        /// <summary>
        /// 初始化配置显示控件
        /// </summary>
        private void InitConfig()
        {
            //Socket控制器
            if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.SocketController != null)
            {
                tbSocketControllerFile.Text = TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.SocketController.ComponentClassFile;
                tbSocketControllerClassFullName.Text = TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.SocketController.ComponentClassFullName;
            }

            #region 显示动态组件列表
            List<DynamicComponent> dynamicComponentList = new List<DynamicComponent>();
            tvDynamicComponents.Nodes.Clear();

            //加载动态组件列表
            dynamicComponentList.AddRange(TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.MonitorComponentMap.Values);
            dynamicComponentList.AddRange(TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.TaskComponentMap.Values);
            
            foreach (DynamicComponent dc in dynamicComponentList)
            {
                TreeNode tnComponent = new TreeNode();
                tnComponent.Tag = dc;

                if (dc.ComponentType == DynamicComponentType.Monitor)
                {
                    //监视器
                    tnComponent.Text = dc.ComponentName + "(监视器)";
                }
                else
                {
                    //任务控制器
                    tnComponent.Text = dc.ComponentName + "(任务控制器)";
                }

                tvDynamicComponents.Nodes.Add(tnComponent);
            }
            #endregion
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
            tvDynamicComponents.SelectedNode = null;
        }

        private void btnCodeSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbComponentId.Text) || string.IsNullOrEmpty(tbComponentName.Text) || string.IsNullOrEmpty(tbComponentClassFullName.Text) || string.IsNullOrEmpty(tbComponentClassFile.Text))
            {
                MessageBox.Show("对不起，所填项目不能为空！");
                return;
            }

            gbComponentDetail.Enabled = false;

            TreeNode selected = tvDynamicComponents.SelectedNode != null ? tvDynamicComponents.SelectedNode : new TreeNode();
            DynamicComponent selectedComponent = selected.Tag != null ? (DynamicComponent)selected.Tag : new DynamicComponent();

            selectedComponent.ComponentId = tbComponentId.Text.Trim();
            selectedComponent.ComponentName = tbComponentName.Text.Trim();
            selectedComponent.ComponentClassFullName = tbComponentClassFullName.Text.Trim();
            selectedComponent.ComponentClassFile = tbComponentClassFile.Text.Trim();
            selectedComponent.ComponentType = this.rbIsMonitor.Checked ? DynamicComponentType.Monitor : DynamicComponentType.Task;

            selected.Text = tbComponentName.Text.Trim() + (this.rbIsMonitor.Checked ? "(监视器)" : "(任务控制器)");

            selected.Tag = selectedComponent;

            if (tvDynamicComponents.SelectedNode == null)
            {
                tvDynamicComponents.Nodes.Add(selected);
            }

            ClearDynamicComponentEditor();
        }

        private void btnCodeDel_Click(object sender, EventArgs e)
        {
            if (tvDynamicComponents.SelectedNode != null)
            {
                if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvDynamicComponents.SelectedNode.Remove();
                    ClearDynamicComponentEditor();
                }
            }
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
            gbComponentDetail.Enabled = true;

            if (tvDynamicComponents.SelectedNode != null && tvDynamicComponents.SelectedNode.Tag is DynamicComponent)
            {
                DynamicComponent dc = (DynamicComponent)tvDynamicComponents.SelectedNode.Tag;
                tbComponentId.Text = dc.ComponentId;
                tbComponentName.Text = dc.ComponentName;
                tbComponentClassFullName.Text = dc.ComponentClassFullName;
                tbComponentClassFile.Text = dc.ComponentClassFile;

                if (dc.ComponentType == DynamicComponentType.Monitor)
                {
                    rbIsMonitor.Checked = true;
                }
                else
                {
                    rbIsTaskController.Checked = true;
                }
            }
        }

        private void tbComponentClassFile_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbComponentClassFile.Text)) { return; }

            if (tbComponentClassFile.Text.Trim().StartsWith(@"./"))
            {
                tbClassCode.Text = System.IO.File.ReadAllText(System.IO.Path.Combine(System.IO.Path.Combine(Application.StartupPath, TaskSimulator.BoatRobot.RobotManager.ROBOT_DYNAMIC_COMPONENT_DIR), tbComponentClassFile.Text.Trim().Replace(@"./", "")));
            }
            else
            {
                tbClassCode.Text = System.IO.File.ReadAllText(tbComponentClassFile.Text.Trim());
            }
        }

        private void tvRobots_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Robot r = new Robot();
            
        }

        private void btnRobotAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnRobotSave_Click(object sender, EventArgs e)
        {

        }

        private void btnRobotDel_Click(object sender, EventArgs e)
        {

        }
    }
}