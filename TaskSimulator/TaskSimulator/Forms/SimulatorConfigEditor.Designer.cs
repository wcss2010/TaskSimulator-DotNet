﻿namespace TaskSimulator.Forms
{
    partial class SimulatorConfigEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBase = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvDynamicComponents = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCodeAdd = new System.Windows.Forms.Button();
            this.btnCodeSave = new System.Windows.Forms.Button();
            this.btnCodeDel = new System.Windows.Forms.Button();
            this.gbComponentDetail = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbIsTaskController = new System.Windows.Forms.RadioButton();
            this.rbIsMonitor = new System.Windows.Forms.RadioButton();
            this.tbClassCode = new System.Windows.Forms.RichTextBox();
            this.btnSelectComponentFile = new System.Windows.Forms.Button();
            this.tbComponentClassFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbComponentClassFullName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbComponentName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbComponentId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectSocketController = new System.Windows.Forms.Button();
            this.tbSocketControllerFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpRobotList = new System.Windows.Forms.TabPage();
            this.ofdCSFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tvRobots = new System.Windows.Forms.TreeView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRobotAdd = new System.Windows.Forms.Button();
            this.btnRobotSave = new System.Windows.Forms.Button();
            this.btnRobotDel = new System.Windows.Forms.Button();
            this.gbRobotDetail = new System.Windows.Forms.GroupBox();
            this.tbSocketControllerClassFullName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.plRobotConfigList = new System.Windows.Forms.Panel();
            this.gbConnections = new System.Windows.Forms.GroupBox();
            this.gbCameras = new System.Windows.Forms.GroupBox();
            this.gbComponents = new System.Windows.Forms.GroupBox();
            this.gbFlyPaths = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpBase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbComponentDetail.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tpRobotList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbRobotDetail.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.plRobotConfigList.SuspendLayout();
            this.gbConnections.SuspendLayout();
            this.gbFlyPaths.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 526);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1131, 51);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(953, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 51);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(1042, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 51);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBase);
            this.tabControl1.Controls.Add(this.tpRobotList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1131, 526);
            this.tabControl1.TabIndex = 1;
            // 
            // tpBase
            // 
            this.tpBase.Controls.Add(this.tbSocketControllerClassFullName);
            this.tpBase.Controls.Add(this.tbSocketControllerFile);
            this.tpBase.Controls.Add(this.label6);
            this.tpBase.Controls.Add(this.groupBox1);
            this.tpBase.Controls.Add(this.btnSelectSocketController);
            this.tpBase.Controls.Add(this.label1);
            this.tpBase.Location = new System.Drawing.Point(4, 25);
            this.tpBase.Name = "tpBase";
            this.tpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tpBase.Size = new System.Drawing.Size(1123, 497);
            this.tpBase.TabIndex = 0;
            this.tpBase.Text = "基础配置";
            this.tpBase.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1117, 424);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "监视器与任务控制器脚本";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 21);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvDynamicComponents);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbComponentDetail);
            this.splitContainer1.Size = new System.Drawing.Size(1111, 400);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvDynamicComponents
            // 
            this.tvDynamicComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDynamicComponents.HideSelection = false;
            this.tvDynamicComponents.Location = new System.Drawing.Point(0, 0);
            this.tvDynamicComponents.Name = "tvDynamicComponents";
            this.tvDynamicComponents.Size = new System.Drawing.Size(328, 344);
            this.tvDynamicComponents.TabIndex = 0;
            this.tvDynamicComponents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCodeAdd);
            this.panel2.Controls.Add(this.btnCodeSave);
            this.panel2.Controls.Add(this.btnCodeDel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(328, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCodeAdd
            // 
            this.btnCodeAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeAdd.Location = new System.Drawing.Point(103, 0);
            this.btnCodeAdd.Name = "btnCodeAdd";
            this.btnCodeAdd.Size = new System.Drawing.Size(75, 56);
            this.btnCodeAdd.TabIndex = 2;
            this.btnCodeAdd.Text = "新增";
            this.btnCodeAdd.UseVisualStyleBackColor = true;
            this.btnCodeAdd.Click += new System.EventHandler(this.btnCodeAdd_Click);
            // 
            // btnCodeSave
            // 
            this.btnCodeSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeSave.Location = new System.Drawing.Point(178, 0);
            this.btnCodeSave.Name = "btnCodeSave";
            this.btnCodeSave.Size = new System.Drawing.Size(75, 56);
            this.btnCodeSave.TabIndex = 1;
            this.btnCodeSave.Text = "保存";
            this.btnCodeSave.UseVisualStyleBackColor = true;
            this.btnCodeSave.Click += new System.EventHandler(this.btnCodeSave_Click);
            // 
            // btnCodeDel
            // 
            this.btnCodeDel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeDel.Location = new System.Drawing.Point(253, 0);
            this.btnCodeDel.Name = "btnCodeDel";
            this.btnCodeDel.Size = new System.Drawing.Size(75, 56);
            this.btnCodeDel.TabIndex = 0;
            this.btnCodeDel.Text = "删除";
            this.btnCodeDel.UseVisualStyleBackColor = true;
            this.btnCodeDel.Click += new System.EventHandler(this.btnCodeDel_Click);
            // 
            // gbComponentDetail
            // 
            this.gbComponentDetail.Controls.Add(this.groupBox2);
            this.gbComponentDetail.Controls.Add(this.tbClassCode);
            this.gbComponentDetail.Controls.Add(this.btnSelectComponentFile);
            this.gbComponentDetail.Controls.Add(this.tbComponentClassFile);
            this.gbComponentDetail.Controls.Add(this.label5);
            this.gbComponentDetail.Controls.Add(this.tbComponentClassFullName);
            this.gbComponentDetail.Controls.Add(this.label4);
            this.gbComponentDetail.Controls.Add(this.tbComponentName);
            this.gbComponentDetail.Controls.Add(this.label3);
            this.gbComponentDetail.Controls.Add(this.tbComponentId);
            this.gbComponentDetail.Controls.Add(this.label2);
            this.gbComponentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbComponentDetail.Enabled = false;
            this.gbComponentDetail.Location = new System.Drawing.Point(0, 0);
            this.gbComponentDetail.Name = "gbComponentDetail";
            this.gbComponentDetail.Size = new System.Drawing.Size(779, 400);
            this.gbComponentDetail.TabIndex = 0;
            this.gbComponentDetail.TabStop = false;
            this.gbComponentDetail.Text = "动态监视器详细";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbIsTaskController);
            this.groupBox2.Controls.Add(this.rbIsMonitor);
            this.groupBox2.Location = new System.Drawing.Point(438, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(140, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "类型";
            // 
            // rbIsTaskController
            // 
            this.rbIsTaskController.AutoSize = true;
            this.rbIsTaskController.Location = new System.Drawing.Point(6, 58);
            this.rbIsTaskController.Name = "rbIsTaskController";
            this.rbIsTaskController.Size = new System.Drawing.Size(103, 19);
            this.rbIsTaskController.TabIndex = 1;
            this.rbIsTaskController.Text = "任务控制器";
            this.rbIsTaskController.UseVisualStyleBackColor = true;
            this.rbIsTaskController.CheckedChanged += new System.EventHandler(this.rbIsTaskController_CheckedChanged);
            // 
            // rbIsMonitor
            // 
            this.rbIsMonitor.AutoSize = true;
            this.rbIsMonitor.Checked = true;
            this.rbIsMonitor.Location = new System.Drawing.Point(6, 24);
            this.rbIsMonitor.Name = "rbIsMonitor";
            this.rbIsMonitor.Size = new System.Drawing.Size(73, 19);
            this.rbIsMonitor.TabIndex = 0;
            this.rbIsMonitor.TabStop = true;
            this.rbIsMonitor.Text = "监视器";
            this.rbIsMonitor.UseVisualStyleBackColor = true;
            this.rbIsMonitor.CheckedChanged += new System.EventHandler(this.rbIsMonitor_CheckedChanged);
            // 
            // tbClassCode
            // 
            this.tbClassCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbClassCode.Location = new System.Drawing.Point(3, 202);
            this.tbClassCode.Name = "tbClassCode";
            this.tbClassCode.ReadOnly = true;
            this.tbClassCode.Size = new System.Drawing.Size(773, 195);
            this.tbClassCode.TabIndex = 7;
            this.tbClassCode.Text = "";
            // 
            // btnSelectComponentFile
            // 
            this.btnSelectComponentFile.Location = new System.Drawing.Point(438, 158);
            this.btnSelectComponentFile.Name = "btnSelectComponentFile";
            this.btnSelectComponentFile.Size = new System.Drawing.Size(75, 25);
            this.btnSelectComponentFile.TabIndex = 6;
            this.btnSelectComponentFile.Text = "选";
            this.btnSelectComponentFile.UseVisualStyleBackColor = true;
            this.btnSelectComponentFile.Click += new System.EventHandler(this.btnSelectComponentFile_Click);
            // 
            // tbComponentClassFile
            // 
            this.tbComponentClassFile.Location = new System.Drawing.Point(113, 158);
            this.tbComponentClassFile.Name = "tbComponentClassFile";
            this.tbComponentClassFile.Size = new System.Drawing.Size(319, 25);
            this.tbComponentClassFile.TabIndex = 5;
            this.tbComponentClassFile.TextChanged += new System.EventHandler(this.tbComponentClassFile_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "组件类文件:";
            // 
            // tbComponentClassFullName
            // 
            this.tbComponentClassFullName.Location = new System.Drawing.Point(113, 113);
            this.tbComponentClassFullName.Name = "tbComponentClassFullName";
            this.tbComponentClassFullName.Size = new System.Drawing.Size(319, 25);
            this.tbComponentClassFullName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "组件类全名:";
            // 
            // tbComponentName
            // 
            this.tbComponentName.Location = new System.Drawing.Point(78, 70);
            this.tbComponentName.Name = "tbComponentName";
            this.tbComponentName.Size = new System.Drawing.Size(354, 25);
            this.tbComponentName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "组件名:";
            // 
            // tbComponentId
            // 
            this.tbComponentId.Location = new System.Drawing.Point(78, 28);
            this.tbComponentId.Name = "tbComponentId";
            this.tbComponentId.Size = new System.Drawing.Size(354, 25);
            this.tbComponentId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "组件ID:";
            // 
            // btnSelectSocketController
            // 
            this.btnSelectSocketController.Location = new System.Drawing.Point(455, 21);
            this.btnSelectSocketController.Name = "btnSelectSocketController";
            this.btnSelectSocketController.Size = new System.Drawing.Size(75, 25);
            this.btnSelectSocketController.TabIndex = 2;
            this.btnSelectSocketController.Text = "选";
            this.btnSelectSocketController.UseVisualStyleBackColor = true;
            this.btnSelectSocketController.Click += new System.EventHandler(this.btnSelectSocketController_Click);
            // 
            // tbSocketControllerFile
            // 
            this.tbSocketControllerFile.Location = new System.Drawing.Point(159, 21);
            this.tbSocketControllerFile.Name = "tbSocketControllerFile";
            this.tbSocketControllerFile.Size = new System.Drawing.Size(290, 25);
            this.tbSocketControllerFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Socket控制器脚本：";
            // 
            // tpRobotList
            // 
            this.tpRobotList.Controls.Add(this.splitContainer2);
            this.tpRobotList.Location = new System.Drawing.Point(4, 25);
            this.tpRobotList.Name = "tpRobotList";
            this.tpRobotList.Padding = new System.Windows.Forms.Padding(3);
            this.tpRobotList.Size = new System.Drawing.Size(1123, 497);
            this.tpRobotList.TabIndex = 1;
            this.tpRobotList.Text = "机器人列表";
            this.tpRobotList.UseVisualStyleBackColor = true;
            // 
            // ofdCSFile
            // 
            this.ofdCSFile.Filter = "C#动态脚本|*.cs";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tvRobots);
            this.splitContainer2.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gbRobotDetail);
            this.splitContainer2.Size = new System.Drawing.Size(1117, 491);
            this.splitContainer2.SplitterDistance = 349;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvRobots
            // 
            this.tvRobots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRobots.HideSelection = false;
            this.tvRobots.Location = new System.Drawing.Point(0, 0);
            this.tvRobots.Name = "tvRobots";
            this.tvRobots.Size = new System.Drawing.Size(349, 435);
            this.tvRobots.TabIndex = 2;
            this.tvRobots.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvRobots_AfterSelect);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnRobotAdd);
            this.panel3.Controls.Add(this.btnRobotSave);
            this.panel3.Controls.Add(this.btnRobotDel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 435);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 56);
            this.panel3.TabIndex = 3;
            // 
            // btnRobotAdd
            // 
            this.btnRobotAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRobotAdd.Location = new System.Drawing.Point(124, 0);
            this.btnRobotAdd.Name = "btnRobotAdd";
            this.btnRobotAdd.Size = new System.Drawing.Size(75, 56);
            this.btnRobotAdd.TabIndex = 2;
            this.btnRobotAdd.Text = "新增";
            this.btnRobotAdd.UseVisualStyleBackColor = true;
            this.btnRobotAdd.Click += new System.EventHandler(this.btnRobotAdd_Click);
            // 
            // btnRobotSave
            // 
            this.btnRobotSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRobotSave.Location = new System.Drawing.Point(199, 0);
            this.btnRobotSave.Name = "btnRobotSave";
            this.btnRobotSave.Size = new System.Drawing.Size(75, 56);
            this.btnRobotSave.TabIndex = 1;
            this.btnRobotSave.Text = "保存";
            this.btnRobotSave.UseVisualStyleBackColor = true;
            this.btnRobotSave.Click += new System.EventHandler(this.btnRobotSave_Click);
            // 
            // btnRobotDel
            // 
            this.btnRobotDel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRobotDel.Location = new System.Drawing.Point(274, 0);
            this.btnRobotDel.Name = "btnRobotDel";
            this.btnRobotDel.Size = new System.Drawing.Size(75, 56);
            this.btnRobotDel.TabIndex = 0;
            this.btnRobotDel.Text = "删除";
            this.btnRobotDel.UseVisualStyleBackColor = true;
            this.btnRobotDel.Click += new System.EventHandler(this.btnRobotDel_Click);
            // 
            // gbRobotDetail
            // 
            this.gbRobotDetail.Controls.Add(this.flowLayoutPanel1);
            this.gbRobotDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRobotDetail.Location = new System.Drawing.Point(0, 0);
            this.gbRobotDetail.Name = "gbRobotDetail";
            this.gbRobotDetail.Size = new System.Drawing.Size(764, 491);
            this.gbRobotDetail.TabIndex = 0;
            this.gbRobotDetail.TabStop = false;
            this.gbRobotDetail.Text = "机器人详细";
            // 
            // tbSocketControllerClassFullName
            // 
            this.tbSocketControllerClassFullName.Location = new System.Drawing.Point(705, 21);
            this.tbSocketControllerClassFullName.Name = "tbSocketControllerClassFullName";
            this.tbSocketControllerClassFullName.Size = new System.Drawing.Size(275, 25);
            this.tbSocketControllerClassFullName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(549, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Socket控制器类全名：";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panel5);
            this.flowLayoutPanel1.Controls.Add(this.panel6);
            this.flowLayoutPanel1.Controls.Add(this.panel7);
            this.flowLayoutPanel1.Controls.Add(this.panel8);
            this.flowLayoutPanel1.Controls.Add(this.panel9);
            this.flowLayoutPanel1.Controls.Add(this.plRobotConfigList);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(758, 467);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 15);
            this.label11.TabIndex = 0;
            this.label11.Text = "默认位置：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "半径：";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(84, 1);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(90, 25);
            this.textBox6.TabIndex = 1;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(226, 1);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(90, 25);
            this.textBox5.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "步长：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(264, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(279, 15);
            this.label12.TabIndex = 3;
            this.label12.Text = "/秒 (表示机器人每秒行进的经纬度数值)";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(84, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(174, 25);
            this.textBox2.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(264, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(293, 15);
            this.label13.TabIndex = 4;
            this.label13.Text = "(表示机器人在围绕默认位置画圈时的半径)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "机器人名：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(179, 4);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(47, 15);
            this.label14.TabIndex = 5;
            this.label14.Text = "Lng :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "机器人ID：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(322, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 15);
            this.label15.TabIndex = 6;
            this.label15.Text = "Lat";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 25);
            this.textBox1.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(84, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(174, 25);
            this.textBox3.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(84, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(174, 25);
            this.textBox4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.textBox1);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(642, 30);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textBox2);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Location = new System.Drawing.Point(3, 39);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(642, 30);
            this.panel6.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.textBox3);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Location = new System.Drawing.Point(3, 75);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(642, 30);
            this.panel7.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.textBox4);
            this.panel8.Controls.Add(this.label13);
            this.panel8.Controls.Add(this.label10);
            this.panel8.Location = new System.Drawing.Point(3, 111);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(642, 30);
            this.panel8.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.textBox6);
            this.panel9.Controls.Add(this.textBox5);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.label14);
            this.panel9.Controls.Add(this.label15);
            this.panel9.Location = new System.Drawing.Point(3, 147);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(642, 30);
            this.panel9.TabIndex = 1;
            // 
            // plRobotConfigList
            // 
            this.plRobotConfigList.Controls.Add(this.gbFlyPaths);
            this.plRobotConfigList.Controls.Add(this.gbComponents);
            this.plRobotConfigList.Controls.Add(this.gbCameras);
            this.plRobotConfigList.Controls.Add(this.gbConnections);
            this.plRobotConfigList.Location = new System.Drawing.Point(3, 183);
            this.plRobotConfigList.Name = "plRobotConfigList";
            this.plRobotConfigList.Size = new System.Drawing.Size(642, 480);
            this.plRobotConfigList.TabIndex = 4;
            // 
            // gbConnections
            // 
            this.gbConnections.Controls.Add(this.richTextBox1);
            this.gbConnections.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbConnections.Location = new System.Drawing.Point(0, 0);
            this.gbConnections.Name = "gbConnections";
            this.gbConnections.Size = new System.Drawing.Size(642, 120);
            this.gbConnections.TabIndex = 0;
            this.gbConnections.TabStop = false;
            this.gbConnections.Text = "连接配置(写法：参数名称=参数值，例如：RemoteIP=127.0.0.1,一行只能写一个键值对！)：";
            // 
            // gbCameras
            // 
            this.gbCameras.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCameras.Location = new System.Drawing.Point(0, 120);
            this.gbCameras.Name = "gbCameras";
            this.gbCameras.Size = new System.Drawing.Size(642, 120);
            this.gbCameras.TabIndex = 1;
            this.gbCameras.TabStop = false;
            this.gbCameras.Text = "虚拟摄像头配置：";
            // 
            // gbComponents
            // 
            this.gbComponents.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbComponents.Location = new System.Drawing.Point(0, 240);
            this.gbComponents.Name = "gbComponents";
            this.gbComponents.Size = new System.Drawing.Size(642, 120);
            this.gbComponents.TabIndex = 2;
            this.gbComponents.TabStop = false;
            this.gbComponents.Text = "监视器与任务控制器配置：";
            // 
            // gbFlyPaths
            // 
            this.gbFlyPaths.Controls.Add(this.richTextBox2);
            this.gbFlyPaths.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFlyPaths.Location = new System.Drawing.Point(0, 360);
            this.gbFlyPaths.Name = "gbFlyPaths";
            this.gbFlyPaths.Size = new System.Drawing.Size(642, 120);
            this.gbFlyPaths.TabIndex = 3;
            this.gbFlyPaths.TabStop = false;
            this.gbFlyPaths.Text = "自定义飞行路径(Lat=纬度，Lng=经度，写法：Lat,Lng，一行只能写一个经纬度！)：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(636, 96);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(3, 21);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(636, 96);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // SimulatorConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 577);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SimulatorConfigEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模拟器配置编辑器";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpBase.ResumeLayout(false);
            this.tpBase.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbComponentDetail.ResumeLayout(false);
            this.gbComponentDetail.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tpRobotList.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbRobotDetail.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.plRobotConfigList.ResumeLayout(false);
            this.gbConnections.ResumeLayout(false);
            this.gbFlyPaths.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpBase;
        private System.Windows.Forms.TabPage tpRobotList;
        private System.Windows.Forms.TextBox tbSocketControllerFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofdCSFile;
        private System.Windows.Forms.Button btnSelectSocketController;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvDynamicComponents;
        private System.Windows.Forms.GroupBox gbComponentDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCodeAdd;
        private System.Windows.Forms.Button btnCodeSave;
        private System.Windows.Forms.Button btnCodeDel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbComponentId;
        private System.Windows.Forms.TextBox tbComponentName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbComponentClassFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbComponentClassFullName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectComponentFile;
        private System.Windows.Forms.RichTextBox tbClassCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbIsTaskController;
        private System.Windows.Forms.RadioButton rbIsMonitor;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView tvRobots;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRobotAdd;
        private System.Windows.Forms.Button btnRobotSave;
        private System.Windows.Forms.Button btnRobotDel;
        private System.Windows.Forms.GroupBox gbRobotDetail;
        private System.Windows.Forms.TextBox tbSocketControllerClassFullName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel plRobotConfigList;
        private System.Windows.Forms.GroupBox gbFlyPaths;
        private System.Windows.Forms.GroupBox gbComponents;
        private System.Windows.Forms.GroupBox gbCameras;
        private System.Windows.Forms.GroupBox gbConnections;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}