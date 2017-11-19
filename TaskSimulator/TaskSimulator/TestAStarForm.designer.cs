namespace TaskSimulator
{
    partial class TestAStarForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnSetStart = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSetEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlButtom = new System.Windows.Forms.Panel();
            this.chkThroughCorner = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAPath = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlButtom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.ContextMenuStrip = this.contextMenuStrip1;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(501, 501);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            this.pnlMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseClick);
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMain_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetStart,
            this.btnSetEnd});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // btnSetStart
            // 
            this.btnSetStart.Name = "btnSetStart";
            this.btnSetStart.Size = new System.Drawing.Size(124, 22);
            this.btnSetStart.Text = "设为起点";
            this.btnSetStart.Click += new System.EventHandler(this.btnSetStart_Click);
            // 
            // btnSetEnd
            // 
            this.btnSetEnd.Name = "btnSetEnd";
            this.btnSetEnd.Size = new System.Drawing.Size(124, 22);
            this.btnSetEnd.Text = "设为终点";
            this.btnSetEnd.Click += new System.EventHandler(this.btnSetEnd_Click);
            // 
            // pnlButtom
            // 
            this.pnlButtom.Controls.Add(this.chkThroughCorner);
            this.pnlButtom.Controls.Add(this.label1);
            this.pnlButtom.Controls.Add(this.btnAPath);
            this.pnlButtom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtom.Location = new System.Drawing.Point(0, 501);
            this.pnlButtom.Name = "pnlButtom";
            this.pnlButtom.Size = new System.Drawing.Size(501, 32);
            this.pnlButtom.TabIndex = 0;
            // 
            // chkThroughCorner
            // 
            this.chkThroughCorner.AutoSize = true;
            this.chkThroughCorner.Location = new System.Drawing.Point(324, 8);
            this.chkThroughCorner.Name = "chkThroughCorner";
            this.chkThroughCorner.Size = new System.Drawing.Size(96, 16);
            this.chkThroughCorner.TabIndex = 2;
            this.chkThroughCorner.Text = "是否穿越拐角";
            this.chkThroughCorner.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "蓝色：起始地点    绿色：结束地点    黑色：阻挡区域";
            // 
            // btnAPath
            // 
            this.btnAPath.Location = new System.Drawing.Point(433, 5);
            this.btnAPath.Name = "btnAPath";
            this.btnAPath.Size = new System.Drawing.Size(54, 23);
            this.btnAPath.TabIndex = 0;
            this.btnAPath.Text = "寻路";
            this.btnAPath.UseVisualStyleBackColor = true;
            this.btnAPath.Click += new System.EventHandler(this.btnAPath_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 533);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A*算法演示";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlButtom.ResumeLayout(false);
            this.pnlButtom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlButtom;
        private System.Windows.Forms.Button btnAPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkThroughCorner;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnSetStart;
        private System.Windows.Forms.ToolStripMenuItem btnSetEnd;
    }
}

