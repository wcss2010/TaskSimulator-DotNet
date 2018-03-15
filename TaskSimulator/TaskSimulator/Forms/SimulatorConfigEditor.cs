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
            ClearComponentDetail();
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

            ClearComponentDetail();
        }

        private void btnCodeDel_Click(object sender, EventArgs e)
        {
            if (tvDynamicComponents.SelectedNode != null)
            {
                if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvDynamicComponents.SelectedNode.Remove();
                    ClearComponentDetail();
                    gbComponentDetail.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 清理动态组件详细
        /// </summary>
        private void ClearComponentDetail()
        {
            tbComponentId.Text = "";
            tbComponentName.Text = "";
            tbComponentClassFile.Text = "";
            tbComponentClassFullName.Text = "";
            tbClassCode.Text = "";
            rbIsMonitor.Checked = true;
        }

        /// <summary>
        /// 清理机器人详细
        /// </summary>
        private void ClearRobotDetail()
        {
            tbRobotId.Text = "";
            tbRobotName.Text = "";
            tbRobotRadius.Text = "" + (0.15 * 15);
            tbRobotStepWithSecond.Text = "0.15";
            tbRobotFlyPaths.Text = "IP=127.0.0.1\nPort=8788\nUserName=admin\nPassword=123123";
            tbRobotConnectionInfos.Text = "";
            tbRobotCameraNames.Text = "一号前视摄像头\n二号后视摄像头\n三号左侧摄像头\n四号右侧摄像头";
            tbRobotCameraImages.Text = "";
            tbRobotCameraWidth.Text = "200";
            tbRobotCameraHeight.Text = "180";
            btnRobotFonts.Text = "选择字体及字号";
            dgvRobotComponents.Rows.Clear();

            btnRobotFonts.Tag = new Font("宋体", 16);
            btnRobotFonts.Text = ((Font)btnRobotFonts.Tag).ToString();

            foreach (TreeNode componentNode in tvDynamicComponents.Nodes)
            {
                if (componentNode.Tag != null && componentNode.Tag is DynamicComponent)
                {
                    DynamicComponent dc = (DynamicComponent)componentNode.Tag;

                    int rowIndex = dgvRobotComponents.Rows.Add();

                    dgvRobotComponents.Rows[rowIndex].Cells[0].Value = dc.ComponentName;
                    dgvRobotComponents.Rows[rowIndex].Cells[1].Value = dc.ComponentType == DynamicComponentType.Monitor ? "监视器" : "任务控制器";
                    dgvRobotComponents.Rows[rowIndex].Cells[2].Value = false;
                    dgvRobotComponents.Rows[rowIndex].Tag = dc;
                }
            }
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
            ClearComponentDetail();
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
            Robot robotData = (Robot)tvRobots.SelectedNode.Tag;
            plRobotDetail.Enabled = true;
            ClearRobotDetail();

            tbRobotId.Text = robotData.RobotId;
            tbRobotName.Text = robotData.RobotName;
            tbRobotRadius.Text = robotData.Radius + "";
            tbRobotStepWithSecond.Text = robotData.StepWithSecond + "";
            tbRobotCameraWidth.Text = robotData.CameraPictureWidth + "";
            tbRobotCameraHeight.Text = robotData.CameraPictureHeight + "";

            //连接参数
            StringBuilder connectionString = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in robotData.ConnectionMap)
            {
                connectionString.Append(kvp.Key).Append("=").Append(kvp.Value).Append("\n");
            }
            tbRobotConnectionInfos.Text = connectionString.ToString();
            
            //飞行路径
            StringBuilder flyPathString = new StringBuilder();
            if (robotData.VoyageRoutes != null)
            {
                foreach (LatAndLng lal in robotData.VoyageRoutes)
                {
                    flyPathString.Append(lal.Lat).Append(":").Append(lal.Lng).Append("\n");
                }
            }
            tbRobotFlyPaths.Text = flyPathString.ToString();

            //组件状态
            foreach (DataGridViewRow dRow in dgvRobotComponents.Rows)
            {
                if (dRow.Tag != null && dRow.Tag is DynamicComponent)
                {
                    DynamicComponent dc = (DynamicComponent)dRow.Tag;

                    if (robotData.MonitorStateMap.ContainsKey(dc.ComponentId))
                    {
                        dRow.Cells[2].Value = robotData.MonitorStateMap[dc.ComponentId];
                    }
                    else if (robotData.TaskStateMap.ContainsKey(dc.ComponentId))
                    {
                        dRow.Cells[2].Value = robotData.TaskStateMap[dc.ComponentId];
                    }
                }
            }

            //摄像头名称及背景
            if (robotData.CameraNames != null)
            {
                StringBuilder cameraNameBuilder = new StringBuilder();
                foreach (string cName in robotData.CameraNames)
                {
                    cameraNameBuilder.Append(cName).Append("\n");
                }
                tbRobotCameraNames.Text = cameraNameBuilder.ToString();
            }
            if (robotData.CameraBackgrounds != null)
            {
                StringBuilder cameraImageBuilder = new StringBuilder();
                foreach (string cImage in robotData.CameraBackgrounds)
                {
                    cameraImageBuilder.Append(cImage).Append("\n");
                }
                tbRobotCameraImages.Text = cameraImageBuilder.ToString();
            }
        }

        private void btnRobotAdd_Click(object sender, EventArgs e)
        {
            plRobotDetail.Enabled = true;
            ClearRobotDetail();
            tvRobots.SelectedNode = null;
        }

        private void btnRobotSave_Click(object sender, EventArgs e)
        {

        }

        private void btnRobotDel_Click(object sender, EventArgs e)
        {
            if (tvRobots.SelectedNode != null)
            {
                if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvRobots.SelectedNode.Remove();
                    ClearRobotDetail();
                    plRobotDetail.Enabled = false;
                }
            }
        }
    }
}