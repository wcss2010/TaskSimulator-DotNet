namespace TaskSimulator.Forms
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBase = new System.Windows.Forms.TabPage();
            this.tpRobotList = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSocketControllerFile = new System.Windows.Forms.TextBox();
            this.ofdCSFile = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectSocketController = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView = new System.Windows.Forms.TreeView();
            this.gbComponentDetail = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCodeDel = new System.Windows.Forms.Button();
            this.btnCodeSave = new System.Windows.Forms.Button();
            this.btnCodeAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbComponentId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbComponentName = new System.Windows.Forms.TextBox();
            this.tbComponentClassFullName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbComponentClassFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectComponentFile = new System.Windows.Forms.Button();
            this.tbClassCode = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbIsMonitor = new System.Windows.Forms.RadioButton();
            this.rbIsTaskController = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpBase.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbComponentDetail.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 515);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 51);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(952, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 51);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Location = new System.Drawing.Point(863, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(89, 51);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBase);
            this.tabControl1.Controls.Add(this.tpRobotList);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1041, 515);
            this.tabControl1.TabIndex = 1;
            // 
            // tpBase
            // 
            this.tpBase.Controls.Add(this.groupBox1);
            this.tpBase.Controls.Add(this.btnSelectSocketController);
            this.tpBase.Controls.Add(this.tbSocketControllerFile);
            this.tpBase.Controls.Add(this.label1);
            this.tpBase.Location = new System.Drawing.Point(4, 25);
            this.tpBase.Name = "tpBase";
            this.tpBase.Padding = new System.Windows.Forms.Padding(3);
            this.tpBase.Size = new System.Drawing.Size(1033, 486);
            this.tpBase.TabIndex = 0;
            this.tpBase.Text = "基础配置";
            this.tpBase.UseVisualStyleBackColor = true;
            // 
            // tpRobotList
            // 
            this.tpRobotList.Location = new System.Drawing.Point(4, 25);
            this.tpRobotList.Name = "tpRobotList";
            this.tpRobotList.Padding = new System.Windows.Forms.Padding(3);
            this.tpRobotList.Size = new System.Drawing.Size(893, 486);
            this.tpRobotList.TabIndex = 1;
            this.tpRobotList.Text = "机器人列表";
            this.tpRobotList.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Socket控制器脚本：";
            // 
            // tbSocketControllerFile
            // 
            this.tbSocketControllerFile.Location = new System.Drawing.Point(159, 15);
            this.tbSocketControllerFile.Name = "tbSocketControllerFile";
            this.tbSocketControllerFile.Size = new System.Drawing.Size(290, 25);
            this.tbSocketControllerFile.TabIndex = 1;
            // 
            // ofdCSFile
            // 
            this.ofdCSFile.Filter = "C#动态脚本|*.cs";
            // 
            // btnSelectSocketController
            // 
            this.btnSelectSocketController.Location = new System.Drawing.Point(455, 15);
            this.btnSelectSocketController.Name = "btnSelectSocketController";
            this.btnSelectSocketController.Size = new System.Drawing.Size(75, 25);
            this.btnSelectSocketController.TabIndex = 2;
            this.btnSelectSocketController.Text = "选";
            this.btnSelectSocketController.UseVisualStyleBackColor = true;
            this.btnSelectSocketController.Click += new System.EventHandler(this.btnSelectSocketController_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1027, 424);
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
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gbComponentDetail);
            this.splitContainer1.Size = new System.Drawing.Size(1021, 400);
            this.splitContainer1.SplitterDistance = 302;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(302, 344);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
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
            this.gbComponentDetail.Size = new System.Drawing.Size(715, 400);
            this.gbComponentDetail.TabIndex = 0;
            this.gbComponentDetail.TabStop = false;
            this.gbComponentDetail.Text = "动态监视器详细";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCodeAdd);
            this.panel2.Controls.Add(this.btnCodeSave);
            this.panel2.Controls.Add(this.btnCodeDel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 344);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(302, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCodeDel
            // 
            this.btnCodeDel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeDel.Location = new System.Drawing.Point(227, 0);
            this.btnCodeDel.Name = "btnCodeDel";
            this.btnCodeDel.Size = new System.Drawing.Size(75, 56);
            this.btnCodeDel.TabIndex = 0;
            this.btnCodeDel.Text = "删除";
            this.btnCodeDel.UseVisualStyleBackColor = true;
            this.btnCodeDel.Click += new System.EventHandler(this.btnCodeDel_Click);
            // 
            // btnCodeSave
            // 
            this.btnCodeSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeSave.Location = new System.Drawing.Point(152, 0);
            this.btnCodeSave.Name = "btnCodeSave";
            this.btnCodeSave.Size = new System.Drawing.Size(75, 56);
            this.btnCodeSave.TabIndex = 1;
            this.btnCodeSave.Text = "保存";
            this.btnCodeSave.UseVisualStyleBackColor = true;
            this.btnCodeSave.Click += new System.EventHandler(this.btnCodeSave_Click);
            // 
            // btnCodeAdd
            // 
            this.btnCodeAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCodeAdd.Location = new System.Drawing.Point(77, 0);
            this.btnCodeAdd.Name = "btnCodeAdd";
            this.btnCodeAdd.Size = new System.Drawing.Size(75, 56);
            this.btnCodeAdd.TabIndex = 2;
            this.btnCodeAdd.Text = "新增";
            this.btnCodeAdd.UseVisualStyleBackColor = true;
            this.btnCodeAdd.Click += new System.EventHandler(this.btnCodeAdd_Click);
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
            // tbComponentId
            // 
            this.tbComponentId.Location = new System.Drawing.Point(78, 28);
            this.tbComponentId.Name = "tbComponentId";
            this.tbComponentId.Size = new System.Drawing.Size(354, 25);
            this.tbComponentId.TabIndex = 1;
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
            // tbComponentName
            // 
            this.tbComponentName.Location = new System.Drawing.Point(78, 70);
            this.tbComponentName.Name = "tbComponentName";
            this.tbComponentName.Size = new System.Drawing.Size(354, 25);
            this.tbComponentName.TabIndex = 1;
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
            // tbClassCode
            // 
            this.tbClassCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbClassCode.Location = new System.Drawing.Point(3, 202);
            this.tbClassCode.Name = "tbClassCode";
            this.tbClassCode.ReadOnly = true;
            this.tbClassCode.Size = new System.Drawing.Size(709, 195);
            this.tbClassCode.TabIndex = 7;
            this.tbClassCode.Text = "";
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
            // SimulatorConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 566);
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
            this.gbComponentDetail.ResumeLayout(false);
            this.gbComponentDetail.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TreeView treeView;
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
    }
}