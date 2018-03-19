namespace TaskSimulator
{
    partial class TestMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestMainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tssl_stateinfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnConfig = new System.Windows.Forms.ToolStripButton();
            this.tbtnStart = new System.Windows.Forms.ToolStripButton();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.tpStatusList = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvRobotList = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbRoatWorkModeAlways = new System.Windows.Forms.RadioButton();
            this.rbRoatWorkModeOnce = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvTaskStateList = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxSocketCommands = new System.Windows.Forms.ListBox();
            this.cTaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTaskState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trBoatStateUpdater = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpStatusList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskStateList)).BeginInit();
            this.panel4.SuspendLayout();
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
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnConfig,
            this.tbtnStart});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1146, 43);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tbtnConfig
            // 
            this.tbtnConfig.Image = ((System.Drawing.Image)(resources.GetObject("tbtnConfig.Image")));
            this.tbtnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnConfig.Name = "tbtnConfig";
            this.tbtnConfig.Size = new System.Drawing.Size(43, 40);
            this.tbtnConfig.Text = "配置";
            this.tbtnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtnConfig.Click += new System.EventHandler(this.tbtnConfig_Click);
            // 
            // tbtnStart
            // 
            this.tbtnStart.Image = ((System.Drawing.Image)(resources.GetObject("tbtnStart.Image")));
            this.tbtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStart.Name = "tbtnStart";
            this.tbtnStart.Size = new System.Drawing.Size(43, 40);
            this.tbtnStart.Text = "重启";
            this.tbtnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tbtnStart.Click += new System.EventHandler(this.tbtnStart_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpLog);
            this.tcMain.Controls.Add(this.tpStatusList);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 43);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1146, 436);
            this.tcMain.TabIndex = 2;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.tbLogs);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(959, 386);
            this.tpLog.TabIndex = 0;
            this.tpLog.Text = "运行日志";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Location = new System.Drawing.Point(3, 3);
            this.tbLogs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(953, 380);
            this.tbLogs.TabIndex = 2;
            this.tbLogs.Text = "";
            // 
            // tpStatusList
            // 
            this.tpStatusList.Controls.Add(this.splitContainer1);
            this.tpStatusList.Location = new System.Drawing.Point(4, 25);
            this.tpStatusList.Name = "tpStatusList";
            this.tpStatusList.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatusList.Size = new System.Drawing.Size(1138, 407);
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
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1132, 401);
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
            this.tvRobotList.Size = new System.Drawing.Size(377, 401);
            this.tvRobotList.TabIndex = 0;
            this.tvRobotList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRobotList_AfterSelect);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(751, 401);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "无人船详细";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.rbRoatWorkModeOnce);
            this.panel1.Controls.Add(this.rbRoatWorkModeAlways);
            this.panel1.Location = new System.Drawing.Point(99, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 27);
            this.panel1.TabIndex = 1;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvTaskStateList);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(745, 208);
            this.panel3.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 208);
            this.label2.TabIndex = 0;
            this.label2.Text = "任务状态：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.dgvTaskStateList.Size = new System.Drawing.Size(647, 208);
            this.dgvTaskStateList.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbxSocketCommands);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 262);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(745, 136);
            this.panel4.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 136);
            this.label3.TabIndex = 0;
            this.label3.Text = "当前";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbxSocketCommands
            // 
            this.lbxSocketCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxSocketCommands.FormattingEnabled = true;
            this.lbxSocketCommands.ItemHeight = 15;
            this.lbxSocketCommands.Location = new System.Drawing.Point(97, 0);
            this.lbxSocketCommands.Name = "lbxSocketCommands";
            this.lbxSocketCommands.Size = new System.Drawing.Size(648, 136);
            this.lbxSocketCommands.TabIndex = 1;
            this.lbxSocketCommands.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxSocketCommands_MouseDoubleClick);
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
            // trBoatStateUpdater
            // 
            this.trBoatStateUpdater.Enabled = true;
            this.trBoatStateUpdater.Interval = 1000;
            this.trBoatStateUpdater.Tick += new System.EventHandler(this.trBoatStateUpdater_Tick);
            // 
            // TestMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 504);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "TestMainForm";
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
            this.tpStatusList.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaskStateList)).EndInit();
            this.panel4.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
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
    }
}