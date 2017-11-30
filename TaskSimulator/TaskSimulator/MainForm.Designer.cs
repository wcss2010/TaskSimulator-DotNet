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
            this.btnPlayAllTask = new System.Windows.Forms.Button();
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.cbIsAlwaysRunMoveTask = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbIsAlwaysRunMoveTask);
            this.groupBox1.Controls.Add(this.btnPlayAllTask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(1410, 99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnPlayAllTask
            // 
            this.btnPlayAllTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPlayAllTask.Location = new System.Drawing.Point(4, 25);
            this.btnPlayAllTask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.tbLogs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(1410, 689);
            this.tbLogs.TabIndex = 1;
            this.tbLogs.Text = "";
            // 
            // cbIsAlwaysRunMoveTask
            // 
            this.cbIsAlwaysRunMoveTask.AutoSize = true;
            this.cbIsAlwaysRunMoveTask.Location = new System.Drawing.Point(1130, 38);
            this.cbIsAlwaysRunMoveTask.Name = "cbIsAlwaysRunMoveTask";
            this.cbIsAlwaysRunMoveTask.Size = new System.Drawing.Size(268, 22);
            this.cbIsAlwaysRunMoveTask.TabIndex = 1;
            this.cbIsAlwaysRunMoveTask.Text = "无人船需要一直进行航行任务";
            this.cbIsAlwaysRunMoveTask.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1410, 788);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}