﻿namespace TaskSimulator
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
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpStatusList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_stateinfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 458);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(967, 25);
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
            this.toolStrip.Size = new System.Drawing.Size(967, 43);
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
            this.tcMain.Size = new System.Drawing.Size(967, 415);
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
            this.tpStatusList.Size = new System.Drawing.Size(959, 386);
            this.tpStatusList.TabIndex = 1;
            this.tpStatusList.Text = "无人机状态列表";
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
            this.splitContainer1.Size = new System.Drawing.Size(953, 380);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvRobotList
            // 
            this.tvRobotList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRobotList.Location = new System.Drawing.Point(0, 0);
            this.tvRobotList.Name = "tvRobotList";
            this.tvRobotList.Size = new System.Drawing.Size(317, 380);
            this.tvRobotList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(632, 380);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "无人船状态:";
            // 
            // TestMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 483);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Name = "TestMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无人机模拟器 V1.1";
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
    }
}