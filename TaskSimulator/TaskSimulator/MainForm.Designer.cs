﻿namespace TaskSimulator
{
    partial class MainForm
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
            this.mapControl = new GMap.NET.WindowsForms.GMapControl();
            this.groupBox = new System.Windows.Forms.Panel();
            this.pbCamera4 = new System.Windows.Forms.PictureBox();
            this.pbCamera3 = new System.Windows.Forms.PictureBox();
            this.pbCamera2 = new System.Windows.Forms.PictureBox();
            this.pbCamera1 = new System.Windows.Forms.PictureBox();
            this.rtbSelectedMarker = new System.Windows.Forms.RichTextBox();
            this.btnAddShip = new System.Windows.Forms.Button();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera1)).BeginInit();
            this.SuspendLayout();
            // 
            // mapControl
            // 
            this.mapControl.Bearing = 0F;
            this.mapControl.CanDragMap = true;
            this.mapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.mapControl.GrayScaleMode = false;
            this.mapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mapControl.LevelsKeepInMemmory = 5;
            this.mapControl.Location = new System.Drawing.Point(0, 0);
            this.mapControl.MarkersEnabled = true;
            this.mapControl.MaxZoom = 2;
            this.mapControl.MinZoom = 2;
            this.mapControl.MouseWheelZoomEnabled = true;
            this.mapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mapControl.Name = "mapControl";
            this.mapControl.NegativeMode = false;
            this.mapControl.PolygonsEnabled = true;
            this.mapControl.RetryLoadTile = 0;
            this.mapControl.RoutesEnabled = true;
            this.mapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mapControl.ShowTileGridLines = false;
            this.mapControl.Size = new System.Drawing.Size(589, 687);
            this.mapControl.TabIndex = 0;
            this.mapControl.Zoom = 0D;
            this.mapControl.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.mapControl_OnMarkerClick);
            // 
            // groupBox
            // 
            this.groupBox.AutoScroll = true;
            this.groupBox.Controls.Add(this.pbCamera4);
            this.groupBox.Controls.Add(this.pbCamera3);
            this.groupBox.Controls.Add(this.pbCamera2);
            this.groupBox.Controls.Add(this.pbCamera1);
            this.groupBox.Controls.Add(this.rtbSelectedMarker);
            this.groupBox.Controls.Add(this.btnAddShip);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox.Location = new System.Drawing.Point(589, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(428, 687);
            this.groupBox.TabIndex = 1;
            // 
            // pbCamera4
            // 
            this.pbCamera4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCamera4.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbCamera4.Location = new System.Drawing.Point(0, 807);
            this.pbCamera4.Name = "pbCamera4";
            this.pbCamera4.Size = new System.Drawing.Size(412, 235);
            this.pbCamera4.TabIndex = 5;
            this.pbCamera4.TabStop = false;
            // 
            // pbCamera3
            // 
            this.pbCamera3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCamera3.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbCamera3.Location = new System.Drawing.Point(0, 572);
            this.pbCamera3.Name = "pbCamera3";
            this.pbCamera3.Size = new System.Drawing.Size(412, 235);
            this.pbCamera3.TabIndex = 4;
            this.pbCamera3.TabStop = false;
            // 
            // pbCamera2
            // 
            this.pbCamera2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCamera2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbCamera2.Location = new System.Drawing.Point(0, 337);
            this.pbCamera2.Name = "pbCamera2";
            this.pbCamera2.Size = new System.Drawing.Size(412, 235);
            this.pbCamera2.TabIndex = 3;
            this.pbCamera2.TabStop = false;
            // 
            // pbCamera1
            // 
            this.pbCamera1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbCamera1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbCamera1.Location = new System.Drawing.Point(0, 102);
            this.pbCamera1.Name = "pbCamera1";
            this.pbCamera1.Size = new System.Drawing.Size(412, 235);
            this.pbCamera1.TabIndex = 2;
            this.pbCamera1.TabStop = false;
            // 
            // rtbSelectedMarker
            // 
            this.rtbSelectedMarker.Dock = System.Windows.Forms.DockStyle.Top;
            this.rtbSelectedMarker.Location = new System.Drawing.Point(0, 36);
            this.rtbSelectedMarker.Name = "rtbSelectedMarker";
            this.rtbSelectedMarker.Size = new System.Drawing.Size(412, 66);
            this.rtbSelectedMarker.TabIndex = 1;
            this.rtbSelectedMarker.Text = "";
            // 
            // btnAddShip
            // 
            this.btnAddShip.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddShip.Location = new System.Drawing.Point(0, 0);
            this.btnAddShip.Name = "btnAddShip";
            this.btnAddShip.Size = new System.Drawing.Size(412, 36);
            this.btnAddShip.TabIndex = 0;
            this.btnAddShip.Text = "跑船";
            this.btnAddShip.UseVisualStyleBackColor = true;
            this.btnAddShip.Click += new System.EventHandler(this.btnAddShip_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 687);
            this.Controls.Add(this.mapControl);
            this.Controls.Add(this.groupBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCamera1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl mapControl;
        private System.Windows.Forms.Panel groupBox;
        private System.Windows.Forms.Button btnAddShip;
        private System.Windows.Forms.RichTextBox rtbSelectedMarker;
        private System.Windows.Forms.PictureBox pbCamera1;
        private System.Windows.Forms.PictureBox pbCamera4;
        private System.Windows.Forms.PictureBox pbCamera3;
        private System.Windows.Forms.PictureBox pbCamera2;
    }
}

