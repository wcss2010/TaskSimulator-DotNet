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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnShowGisMonitor = new System.Windows.Forms.Button();
            this.btnClearLogs = new System.Windows.Forms.Button();
            this.cbIsAlwaysRunMoveTask = new System.Windows.Forms.CheckBox();
            this.btnPlayAllTask = new System.Windows.Forms.Button();
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnShowGisMonitor);
            this.groupBox1.Controls.Add(this.btnClearLogs);
            this.groupBox1.Controls.Add(this.cbIsAlwaysRunMoveTask);
            this.groupBox1.Controls.Add(this.btnPlayAllTask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1236, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(763, 28);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(165, 64);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnShowGisMonitor
            // 
            this.btnShowGisMonitor.Location = new System.Drawing.Point(536, 28);
            this.btnShowGisMonitor.Name = "btnShowGisMonitor";
            this.btnShowGisMonitor.Size = new System.Drawing.Size(221, 64);
            this.btnShowGisMonitor.TabIndex = 2;
            this.btnShowGisMonitor.Text = "显示地图监视器";
            this.btnShowGisMonitor.UseVisualStyleBackColor = true;
            this.btnShowGisMonitor.Click += new System.EventHandler(this.btnShowGisMonitor_Click);
            // 
            // btnClearLogs
            // 
            this.btnClearLogs.Location = new System.Drawing.Point(315, 28);
            this.btnClearLogs.Name = "btnClearLogs";
            this.btnClearLogs.Size = new System.Drawing.Size(215, 64);
            this.btnClearLogs.TabIndex = 2;
            this.btnClearLogs.Text = "清理日志显示";
            this.btnClearLogs.UseVisualStyleBackColor = true;
            this.btnClearLogs.Click += new System.EventHandler(this.btnClearLogs_Click);
            // 
            // cbIsAlwaysRunMoveTask
            // 
            this.cbIsAlwaysRunMoveTask.AutoSize = true;
            this.cbIsAlwaysRunMoveTask.Location = new System.Drawing.Point(934, 50);
            this.cbIsAlwaysRunMoveTask.Name = "cbIsAlwaysRunMoveTask";
            this.cbIsAlwaysRunMoveTask.Size = new System.Drawing.Size(268, 22);
            this.cbIsAlwaysRunMoveTask.TabIndex = 1;
            this.cbIsAlwaysRunMoveTask.Text = "无人船需要一直进行航行任务";
            this.cbIsAlwaysRunMoveTask.UseVisualStyleBackColor = true;
            // 
            // btnPlayAllTask
            // 
            this.btnPlayAllTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPlayAllTask.Location = new System.Drawing.Point(4, 25);
            this.btnPlayAllTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnPlayAllTask.Name = "btnPlayAllTask";
            this.btnPlayAllTask.Size = new System.Drawing.Size(304, 70);
            this.btnPlayAllTask.TabIndex = 0;
            this.btnPlayAllTask.Text = "让所有没有启动的无人船画圈圈！";
            this.btnPlayAllTask.UseVisualStyleBackColor = true;
            this.btnPlayAllTask.Click += new System.EventHandler(this.btnPlayAllTask_Click);
            // 
            // tbLogs
            // 
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Location = new System.Drawing.Point(0, 99);
            this.tbLogs.Margin = new System.Windows.Forms.Padding(4);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(1236, 689);
            this.tbLogs.TabIndex = 1;
            this.tbLogs.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 788);
            this.ControlBox = false;
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无人船任务模拟器 V1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbLogs;
        private System.Windows.Forms.Button btnPlayAllTask;
        private System.Windows.Forms.CheckBox cbIsAlwaysRunMoveTask;
        private System.Windows.Forms.Button btnClearLogs;
        private System.Windows.Forms.Button btnShowGisMonitor;
        private System.Windows.Forms.Button btnExit;
    }
}