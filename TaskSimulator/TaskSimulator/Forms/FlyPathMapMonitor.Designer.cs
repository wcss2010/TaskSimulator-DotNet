namespace TaskSimulator.Forms
{
    partial class FlyPathMapMonitor
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
            this.gbNeedDraw = new System.Windows.Forms.GroupBox();
            this.btnDarwNewPath = new System.Windows.Forms.Button();
            this.gbNeedDraw.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbNeedDraw
            // 
            this.gbNeedDraw.BackColor = System.Drawing.Color.Transparent;
            this.gbNeedDraw.Controls.Add(this.btnDarwNewPath);
            this.gbNeedDraw.Location = new System.Drawing.Point(0, 0);
            this.gbNeedDraw.Name = "gbNeedDraw";
            this.gbNeedDraw.Size = new System.Drawing.Size(156, 53);
            this.gbNeedDraw.TabIndex = 3;
            this.gbNeedDraw.TabStop = false;
            // 
            // btnDarwNewPath
            // 
            this.btnDarwNewPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDarwNewPath.Location = new System.Drawing.Point(3, 21);
            this.btnDarwNewPath.Name = "btnDarwNewPath";
            this.btnDarwNewPath.Size = new System.Drawing.Size(150, 29);
            this.btnDarwNewPath.TabIndex = 0;
            this.btnDarwNewPath.Text = "重新创建航行路径";
            this.btnDarwNewPath.UseVisualStyleBackColor = true;
            this.btnDarwNewPath.Click += new System.EventHandler(this.btnDarwNewPath_Click);
            // 
            // FlyPathMapMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 454);
            this.Controls.Add(this.gbNeedDraw);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FlyPathMapMonitor";
            this.Text = "自定义航行路径(单击右键确定行进路线，双击左键保存退出)";
            this.Controls.SetChildIndex(this.gbNeedDraw, 0);
            this.gbNeedDraw.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbNeedDraw;
        private System.Windows.Forms.Button btnDarwNewPath;
    }
}