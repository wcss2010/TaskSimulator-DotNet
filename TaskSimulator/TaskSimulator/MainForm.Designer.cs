namespace TaskSimulator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssl_stateinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnConfig = new System.Windows.Forms.ToolStripButton();
            this.tbtnStart = new System.Windows.Forms.ToolStripButton();
            this.tbtnMapMonitor = new System.Windows.Forms.ToolStripButton();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudMaxLogRowCount = new System.Windows.Forms.NumericUpDown();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tpStatusList = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvRobotList = new System.Windows.Forms.TreeView();
            this.gbBoatDetail = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbxSocketCommands = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvTaskStateList = new System.Windows.Forms.DataGridView();
            this.cTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTaskState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRoatWorkModeOnce = new System.Windows.Forms.RadioButton();
            this.rbRoatWorkModeAlways = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.trBoatStateUpdater = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLogRowCount)).BeginInit();
            this.tpStatusList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbBoatDetail.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskStateList)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_stateinfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 479);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1146, 25);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tssl_stateinfo
            // 
            this.tssl_stateinfo.Name = "tssl_stateinfo";
            this.tssl_stateinfo.Size = new System.Drawing.Size(195, 20);
            this.tssl_stateinfo.Text = "欢迎使用无人机模拟器 V1.1";
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnConfig,
            this.tbtnStart,
            this.tbtnMapMonitor});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1146, 59);
            this.toolStrip.TabIndex = 1;
            // 
            // tbtnConfig
            // 
            this.tbtnConfig.Image = ((System.Drawing.Image)(resources.GetObject("tbtnConfig.Image")));
            this.tbtnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnConfig.Name = "tbtnConfig";
            this.tbtnConfig.Size = new System.Drawing.Size(43, 56);
            this.tbtnConfig.Text = "配置";
            this.tbtnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtnConfig.Click += new System.EventHandler(this.tbtnConfig_Click);
            // 
            // tbtnStart
            // 
            this.tbtnStart.Image = ((System.Drawing.Image)(resources.GetObject("tbtnStart.Image")));
            this.tbtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStart.Name = "tbtnStart";
            this.tbtnStart.Size = new System.Drawing.Size(118, 56);
            this.tbtnStart.Text = "重启所有无人船";
            this.tbtnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtnStart.Click += new System.EventHandler(this.tbtnStart_Click);
            // 
            // tbtnMapMonitor
            // 
            this.tbtnMapMonitor.Image = ((System.Drawing.Image)(resources.GetObject("tbtnMapMonitor.Image")));
            this.tbtnMapMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnMapMonitor.Name = "tbtnMapMonitor";
            this.tbtnMapMonitor.Size = new System.Drawing.Size(163, 56);
            this.tbtnMapMonitor.Text = "打开无人船地图监视器";
            this.tbtnMapMonitor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtnMapMonitor.Click += new System.EventHandler(this.tbtnMapMonitor_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpLog);
            this.tcMain.Controls.Add(this.tpStatusList);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 59);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1146, 420);
            this.tcMain.TabIndex = 2;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.tbLogs);
            this.tpLog.Controls.Add(this.groupBox1);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(1138, 391);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "运行日志";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Location = new System.Drawing.Point(3, 55);
            this.tbLogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(1132, 333);
            this.tbLogs.TabIndex = 2;
            this.tbLogs.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudMaxLogRowCount);
            this.groupBox1.Controls.Add(this.btnClearLogs);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1132, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // nudMaxLogRowCount
            // 
            this.nudMaxLogRowCount.Location = new System.Drawing.Point(199, 19);
            this.nudMaxLogRowCount.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudMaxLogRowCount.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudMaxLogRowCount.Name = "nudMaxLogRowCount";
            this.nudMaxLogRowCount.Size = new System.Drawing.Size(120, 25);
            this.nudMaxLogRowCount.TabIndex = 1;
            this.nudMaxLogRowCount.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Location = new System.Drawing.Point(325, 19);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(131, 25);
            this.btnClearLogs.TabIndex = 2;
            this.btnClearLogs.Text = "清空日志";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "控制台日志最大显示行数：";
            // 
            // tpStatusList
            // 
            this.tpStatusList.Controls.Add(this.splitContainer1);
            this.tpStatusList.Location = new System.Drawing.Point(4, 25);
            this.tpStatusList.Name = "tpStatusList";
            this.tpStatusList.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatusList.Size = new System.Drawing.Size(1138, 391);
            this.tpStatusList.TabIndex = 1;
            this.tpStatusList.Text = "无人船状态列表";
            this.tpStatusList.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvRobotList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbBoatDetail);
            this.splitContainer1.Size = new System.Drawing.Size(1132, 385);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvRobotList
            // 
            this.tvRobotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRobotList.FullRowSelect = true;
            this.tvRobotList.HideSelection = false;
            this.tvRobotList.Location = new System.Drawing.Point(0, 0);
            this.tvRobotList.Name = "tvRobotList";
            this.tvRobotList.Size = new System.Drawing.Size(377, 385);
            this.tvRobotList.TabIndex = 0;
            this.tvRobotList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRobotList_AfterSelect);
            // 
            // gbBoatDetail
            // 
            this.gbBoatDetail.Controls.Add(this.panel4);
            this.gbBoatDetail.Controls.Add(this.panel3);
            this.gbBoatDetail.Controls.Add(this.panel2);
            this.gbBoatDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBoatDetail.Location = new System.Drawing.Point(0, 0);
            this.gbBoatDetail.Name = "gbBoatDetail";
            this.gbBoatDetail.Size = new System.Drawing.Size(751, 385);
            this.gbBoatDetail.TabIndex = 0;
            this.gbBoatDetail.TabStop = false;
            this.gbBoatDetail.Text = "无人船详细";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbxSocketCommands);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 348);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(745, 34);
            this.panel4.TabIndex = 4;
            // 
            // lbxSocketCommands
            // 
            this.lbxSocketCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSocketCommands.FormattingEnabled = true;
            this.lbxSocketCommands.ItemHeight = 15;
            this.lbxSocketCommands.Location = new System.Drawing.Point(97, 0);
            this.lbxSocketCommands.Name = "lbxSocketCommands";
            this.lbxSocketCommands.Size = new System.Drawing.Size(648, 34);
            this.lbxSocketCommands.TabIndex = 1;
            this.lbxSocketCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxSocketCommands_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 34);
            this.label3.TabIndex = 0;
            this.label3.Text = "所支持的控制台指令（双击即可运行）：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvTaskStateList);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(745, 294);
            this.panel3.TabIndex = 3;
            // 
            // dgvTaskStateList
            // 
            this.dgvTaskStateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaskStateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTaskName,
            this.cTaskState});
            this.dgvTaskStateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaskStateList.Location = new System.Drawing.Point(98, 0);
            this.dgvTaskStateList.MultiSelect = false;
            this.dgvTaskStateList.Name = "dgvTaskStateList";
            this.dgvTaskStateList.ReadOnly = true;
            this.dgvTaskStateList.RowHeadersVisible = false;
            this.dgvTaskStateList.RowTemplate.Height = 27;
            this.dgvTaskStateList.Size = new System.Drawing.Size(647, 294);
            this.dgvTaskStateList.TabIndex = 1;
            // 
            // cTaskName
            // 
            this.cTaskName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cTaskName.HeaderText = "任务名称";
            this.cTaskName.Name = "cTaskName";
            this.cTaskName.ReadOnly = true;
            // 
            // cTaskState
            // 
            this.cTaskState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cTaskState.HeaderText = "任务状态";
            this.cTaskState.Name = "cTaskState";
            this.cTaskState.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 294);
            this.label2.TabIndex = 0;
            this.label2.Text = "任务状态：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(745, 33);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRoatWorkModeOnce);
            this.panel1.Controls.Add(this.rbRoatWorkModeAlways);
            this.panel1.Location = new System.Drawing.Point(99, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 27);
            this.panel1.TabIndex = 1;
            // 
            // rbRoatWorkModeOnce
            // 
            this.rbRoatWorkModeOnce.AutoSize = true;
            this.rbRoatWorkModeOnce.Location = new System.Drawing.Point(184, 4);
            this.rbRoatWorkModeOnce.Name = "rbRoatWorkModeOnce";
            this.rbRoatWorkModeOnce.Size = new System.Drawing.Size(343, 19);
            this.rbRoatWorkModeOnce.TabIndex = 1;
            this.rbRoatWorkModeOnce.Text = "无人船航行任务停止后不需要再次自动规划行程";
            this.rbRoatWorkModeOnce.UseVisualStyleBackColor = true;
            this.rbRoatWorkModeOnce.CheckedChanged += new System.EventHandler(this.rbRoatWorkModeOnce_CheckedChanged);
            // 
            // rbRoatWorkModeAlways
            // 
            this.rbRoatWorkModeAlways.AutoSize = true;
            this.rbRoatWorkModeAlways.Checked = true;
            this.rbRoatWorkModeAlways.Location = new System.Drawing.Point(4, 4);
            this.rbRoatWorkModeAlways.Name = "rbRoatWorkModeAlways";
            this.rbRoatWorkModeAlways.Size = new System.Drawing.Size(163, 19);
            this.rbRoatWorkModeAlways.TabIndex = 0;
            this.rbRoatWorkModeAlways.TabStop = true;
            this.rbRoatWorkModeAlways.Text = "无人船需要连续航行";
            this.rbRoatWorkModeAlways.UseVisualStyleBackColor = true;
            this.rbRoatWorkModeAlways.CheckedChanged += new System.EventHandler(this.rbRoatWorkModeAlways_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "工作模式：";
            // 
            // trBoatStateUpdater
            // 
            this.trBoatStateUpdater.Enabled = true;
            this.trBoatStateUpdater.Interval = 1000;
            this.trBoatStateUpdater.Tick += new System.EventHandler(this.trBoatStateUpdater_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 504);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无人船模拟器 V1.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestMainForm_FormClosing);
            this.Load += new System.EventHandler(this.TestMainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxLogRowCount)).EndInit();
            this.tpStatusList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbBoatDetail.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskStateList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssl_stateinfo;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.TabPage tpStatusList;
        private System.Windows.Forms.ToolStripButton tbtnStart;
        private System.Windows.Forms.RichTextBox tbLogs;
        private System.Windows.Forms.ToolStripButton tbtnConfig;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvRobotList;
        private System.Windows.Forms.GroupBox gbBoatDetail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbRoatWorkModeAlways;
        private System.Windows.Forms.RadioButton rbRoatWorkModeOnce;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvTaskStateList;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxSocketCommands;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTaskState;
        private System.Windows.Forms.Timer trBoatStateUpdater;
        private System.Windows.Forms.ToolStripButton tbtnMapMonitor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.NumericUpDown nudMaxLogRowCount;
        private System.Windows.Forms.Label label4;
    }
}