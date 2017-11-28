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
            this.tbLogs = new System.Windows.Forms.RichTextBox();
            this.btnPlayAllTask = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPlayAllTask);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(940, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbLogs
            // 
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Location = new System.Drawing.Point(0, 66);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(940, 459);
            this.tbLogs.TabIndex = 1;
            this.tbLogs.Text = "";
            // 
            // btnPlayAllTask
            // 
            this.btnPlayAllTask.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPlayAllTask.Location = new System.Drawing.Point(3, 17);
            this.btnPlayAllTask.Name = "btnPlayAllTask";
            this.btnPlayAllTask.Size = new System.Drawing.Size(203, 46);
            this.btnPlayAllTask.TabIndex = 0;
            this.btnPlayAllTask.Text = "让所有没有启动的无人船画圈圈！";
            this.btnPlayAllTask.UseVisualStyleBackColor = true;
            this.btnPlayAllTask.Click += new System.EventHandler(this.btnPlayAllTask_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 525);
            this.Controls.Add(this.tbLogs);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无人船任务模拟器 V1.0";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox tbLogs;
        private System.Windows.Forms.Button btnPlayAllTask;
    }
}