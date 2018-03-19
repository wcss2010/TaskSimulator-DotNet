using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskSimulator.BoatRobot;
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

            //编译配置
            if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.RefDLL != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.RefDLL)
                {
                    sb.Append(s).Append("\n");
                }
                tbCompileRefDLLPaths.Text = sb.ToString();
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

            #region 显示机器人列表
            if (TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.Robots != null)
            {
                tvRobots.Nodes.Clear();

                foreach (Robot robot in TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig.Robots)
                {
                    TreeNode item = new TreeNode();
                    item.Text = robot.RobotName;
                    item.Tag = robot;

                    tvRobots.Nodes.Add(item);
                }
            }
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RobotSimulatorConfig rsc = new RobotSimulatorConfig();
            
            //Socket控制器
            rsc.SocketController.ComponentClassFile = tbSocketControllerFile.Text;
            rsc.SocketController.ComponentClassFullName = tbSocketControllerClassFullName.Text;

            //编译配置
            string[] compileTeams = tbCompileRefDLLPaths.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
            List<string> compileRefDlls = new List<string>();

            if (compileTeams != null)
            {
                foreach (string s in compileTeams)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    else
                    {
                        compileRefDlls.Add(s.Trim());
                    }
                }
            }

            rsc.RefDLL = compileRefDlls.ToArray();

            //监视器
            foreach (TreeNode tn in tvDynamicComponents.Nodes)
            {
                DynamicComponent dc = (DynamicComponent)tn.Tag;
                if (dc.ComponentType == DynamicComponentType.Monitor)
                {
                    rsc.MonitorComponentMap.Add(dc.ComponentId, dc);
                }
                else
                {
                    rsc.TaskComponentMap.Add(dc.ComponentId, dc);
                }
            }

            //机器人
            List<Robot> robotList = new List<Robot>();
            foreach (TreeNode tn in tvRobots.Nodes)
            {
                Robot robot = (Robot)tn.Tag;
                robotList.Add(robot);
            }
            rsc.Robots = robotList.ToArray();

            TaskSimulatorLib.SimulatorObject.Simulator.SimulatorConfig = rsc;
            TaskSimulatorLib.SimulatorObject.Simulator.SaveConfig();

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
            tvDynamicComponents.SelectedNode = null;
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
            tbRobotFlyPaths.Text = "";
            tbRobotConnectionInfos.Text = "IP=127.0.0.1\nPort=8788\nUserName=admin\nPassword=123123"; 
            tbRobotCameraNames.Text = "一号前视摄像头\n二号后视摄像头\n三号左侧摄像头\n四号右侧摄像头";
            tbRobotCameraImages.Text = "";
            tbRobotCameraWidth.Text = "200";
            tbRobotCameraHeight.Text = "180";
            btnRobotFonts.Text = "选择字体及字号";
            rbRobotDefaultLat.Text = "0.0";
            rbRobotDefaultLng.Text = "0.0";
            dgvRobotComponents.Rows.Clear();

            btnRobotFonts.Tag = new Font("宋体", 16);
            btnRobotFonts.Text = ((Font)btnRobotFonts.Tag).ToString();

            //GPS监视器(默认)
            DynamicComponent gpsMonitor = new DynamicComponent();
            gpsMonitor.ComponentId = RobotManager.Monitor_GPS;
            gpsMonitor.ComponentName = "GPS监视器";
            gpsMonitor.ComponentType = DynamicComponentType.Monitor;
            int rowIndexs = dgvRobotComponents.Rows.Add();
            dgvRobotComponents.Rows[rowIndexs].Cells[0].Value = gpsMonitor.ComponentName;
            dgvRobotComponents.Rows[rowIndexs].Cells[1].Value = gpsMonitor.ComponentType == DynamicComponentType.Monitor ? "监视器" : "任务控制器";
            dgvRobotComponents.Rows[rowIndexs].Cells[2].Value = true;
            dgvRobotComponents.Rows[rowIndexs].ReadOnly = true;
            dgvRobotComponents.Rows[rowIndexs].Tag = gpsMonitor;

            //自主飞行控制器
            DynamicComponent boatFly = new DynamicComponent();
            boatFly.ComponentId = RobotManager.Task_BoatFly;
            boatFly.ComponentName = "自主航行任务";
            boatFly.ComponentType = DynamicComponentType.Task;
            rowIndexs = dgvRobotComponents.Rows.Add();
            dgvRobotComponents.Rows[rowIndexs].Cells[0].Value = boatFly.ComponentName;
            dgvRobotComponents.Rows[rowIndexs].Cells[1].Value = boatFly.ComponentType == DynamicComponentType.Monitor ? "监视器" : "任务控制器";
            dgvRobotComponents.Rows[rowIndexs].Cells[2].Value = true;
            dgvRobotComponents.Rows[rowIndexs].ReadOnly = true;
            dgvRobotComponents.Rows[rowIndexs].Tag = boatFly;

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
            try
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
            catch (Exception ex) { }
        }

        private void tvRobots_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Robot robotData = (Robot)tvRobots.SelectedNode.Tag;
            gbRobotDetail.Enabled = true;
            ClearRobotDetail();

            tbRobotId.Text = robotData.RobotId;
            tbRobotName.Text = robotData.RobotName;
            tbRobotRadius.Text = robotData.Radius + "";
            tbRobotStepWithSecond.Text = robotData.StepWithSecond + "";
            tbRobotCameraWidth.Text = robotData.CameraPictureWidth + "";
            tbRobotCameraHeight.Text = robotData.CameraPictureHeight + "";

            if (robotData.DefaultGpsPos != null)
            {
                rbRobotDefaultLat.Text = robotData.DefaultGpsPos.Lat + "";
                rbRobotDefaultLng.Text = robotData.DefaultGpsPos.Lng + "";
            }

            if (!string.IsNullOrEmpty(robotData.CameraHintTextFontName))
            {
                btnRobotFonts.Tag = new Font(robotData.CameraHintTextFontName, robotData.CameraHintTextFontSize);
                btnRobotFonts.Text = btnRobotFonts.Tag.ToString();
            }

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
            gbRobotDetail.Enabled = true;
            ClearRobotDetail();
            tvRobots.SelectedNode = null;
        }

        private void btnRobotSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbRobotId.Text) || string.IsNullOrEmpty(tbRobotName.Text) || string.IsNullOrEmpty(tbRobotRadius.Text) || string.IsNullOrEmpty(tbRobotStepWithSecond.Text) || string.IsNullOrEmpty(tbRobotCameraWidth.Text) || string.IsNullOrEmpty(tbRobotCameraHeight.Text))
            {
                MessageBox.Show("对不起，所填项目不能为空！");
                return;
            }
            string[] flyPaths = tbRobotFlyPaths.Text.Trim().Split(new string[] { "\n" }, StringSplitOptions.None);
            if (flyPaths != null && flyPaths.Length > 1 && flyPaths.Length < 4)
            {
                MessageBox.Show("对不起，如果要规划飞行路径必须写三个以上的经纬度坐标点！");
                return;
            }
            string[] nameTeams = tbRobotCameraNames.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
            string[] imageTeams = tbRobotCameraImages.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
            if (nameTeams == null || nameTeams.Length < 1)
            {
                MessageBox.Show("对不起，必须设置虚拟摄像头的名称！");
                return;
            }
            if (imageTeams != null && imageTeams.Length < 2)
            {
                MessageBox.Show("对不起，必须设置两张以上的背景图！");
                return;
            }
            if (btnRobotFonts.Tag == null)
            {
                MessageBox.Show("对不起，必须设置字体！");
                return;
            }

            try
            {
                gbRobotDetail.Enabled = false;

                TreeNode selected = tvRobots.SelectedNode != null ? tvRobots.SelectedNode : new TreeNode();
                Robot robot = selected.Tag != null ? (Robot)selected.Tag : new Robot();

                //填充数据
                robot.RobotId = tbRobotId.Text;
                robot.RobotName = tbRobotName.Text;
                robot.StepWithSecond = double.Parse(tbRobotStepWithSecond.Text);
                robot.Radius = double.Parse(tbRobotRadius.Text);
                robot.DefaultGpsPos = new LatAndLng(double.Parse(rbRobotDefaultLat.Text), double.Parse(rbRobotDefaultLng.Text));
                robot.CameraPictureWidth = int.Parse(tbRobotCameraWidth.Text);
                robot.CameraPictureHeight = int.Parse(tbRobotCameraHeight.Text);
                robot.CameraHintTextFontName = ((Font)btnRobotFonts.Tag).Name;
                robot.CameraHintTextFontSize = ((Font)btnRobotFonts.Tag).Size;

                //连接配置
                string[] connectionTeams = tbRobotConnectionInfos.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                if (connectionTeams != null)
                {
                    foreach (string conn in connectionTeams)
                    {
                        if (string.IsNullOrEmpty(conn) || conn.IndexOf("=") < 0)
                        {
                            continue;
                        }
                        else
                        {
                            string[] kvp = conn.Split(new string[] { "=" }, StringSplitOptions.None);
                            if (kvp != null && kvp.Length >= 2)
                            {
                                robot.ConnectionMap[kvp[0].Trim()] = kvp[1].Trim();
                            }
                        }
                    }
                }
                //行动路径
                string[] flyPathTeams = tbRobotFlyPaths.Text.Split(new string[] { "\n" }, StringSplitOptions.None);
                if (flyPathTeams != null)
                {
                    List<LatAndLng> posList = new List<LatAndLng>();

                    foreach (string flyPos in flyPathTeams)
                    {
                        if (string.IsNullOrEmpty(flyPos) || flyPos.IndexOf(":") < 0)
                        {
                            continue;
                        }
                        else
                        {
                            string[] kvp = flyPos.Split(new string[] { ":" }, StringSplitOptions.None);
                            if (kvp != null && kvp.Length >= 2)
                            {
                                posList.Add(new LatAndLng(double.Parse(kvp[0]), double.Parse(kvp[1])));
                            }
                        }
                    }

                    robot.VoyageRoutes = posList.ToArray();
                }

                //组件状态
                foreach (DataGridViewRow dRow in dgvRobotComponents.Rows)
                {
                    if (dRow.Tag != null && dRow.Tag is DynamicComponent)
                    {
                        DynamicComponent dc = (DynamicComponent)dRow.Tag;
                        bool checkResult = (bool)dRow.Cells[2].Value;

                        if (dc.ComponentType == DynamicComponentType.Monitor)
                        {
                            robot.MonitorStateMap[dc.ComponentId] = checkResult;
                        }
                        else
                        {
                            robot.TaskStateMap[dc.ComponentId] = checkResult;
                        }
                    }
                }

                //摄像头名称及背景图
                List<string> nameResults = new List<string>();
                List<string> imageResults = new List<string>();
                foreach (string s in nameTeams)
                {
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    else
                    {
                        nameResults.Add(s);
                    }
                }
                if (imageTeams != null)
                {
                    foreach (string s in imageTeams)
                    {
                        if (string.IsNullOrEmpty(s))
                        {
                            continue;
                        }
                        else
                        {
                            imageResults.Add(s);
                        }
                    }
                }
                robot.CameraNames = nameResults.ToArray();
                robot.CameraBackgrounds = imageResults.ToArray();

                selected.Tag = robot;
                selected.Text = robot.RobotName;
                if (tvRobots.SelectedNode == null)
                {
                    tvRobots.Nodes.Add(selected);
                }

                ClearRobotDetail();
                tvRobots.SelectedNode = null;
            }
            catch (Exception ex)
            {
                gbRobotDetail.Enabled = true;
            }
        }

        private void btnRobotDel_Click(object sender, EventArgs e)
        {
            if (tvRobots.SelectedNode != null)
            {
                if (MessageBox.Show("真的要删除吗？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    tvRobots.SelectedNode.Remove();
                    ClearRobotDetail();
                    gbRobotDetail.Enabled = false;
                }
            }
        }

        private void btnRobotFonts_Click(object sender, EventArgs e)
        {
            fdImageFont.Font = (Font)btnRobotFonts.Tag;
            if (fdImageFont.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                btnRobotFonts.Tag = fdImageFont.Font;
                btnRobotFonts.Text = btnRobotFonts.Tag.ToString();
            }
        }

        private void btnRobotoSelectDefaultPos_Click(object sender, EventArgs e)
        {

        }

        private void btnRobotSelectAllComponents_Click(object sender, EventArgs e)
        {
            for (int k = 2; k < dgvRobotComponents.Rows.Count; k++)
            {
                bool val = (bool)dgvRobotComponents.Rows[k].Cells[2].Value;

                dgvRobotComponents.Rows[k].Cells[2].Value = !val;
            }
        }

        private void btnRobotEditFlyPathInMap_Click(object sender, EventArgs e)
        {

        }
    }
}