namespace MainUI
{
    partial class frmMainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainMenu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.picRunStatus = new System.Windows.Forms.PictureBox();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timerPLC = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPLC = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblYaLi = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblWenDu = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblShiDu = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnMetering = new Sunny.UI.UIImageButton();
            this.btnProc = new Sunny.UI.UIImageButton();
            this.btnChangePwd = new Sunny.UI.UIImageButton();
            this.btnExit = new Sunny.UI.UIImageButton();
            this.btnMainData = new Sunny.UI.UIImageButton();
            this.btnHardwareTest = new Sunny.UI.UIImageButton();
            this.btnReports = new Sunny.UI.UIImageButton();
            this.lblPenZui = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMetering)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangePwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHardwareTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReports)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.lblDateTime);
            this.panel1.Controls.Add(this.picRunStatus);
            this.panel1.Controls.Add(this.Logo);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 44);
            this.panel1.TabIndex = 0;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.lblDateTime.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDateTime.ForeColor = System.Drawing.Color.White;
            this.lblDateTime.Location = new System.Drawing.Point(1697, 14);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(159, 20);
            this.lblDateTime.TabIndex = 4;
            this.lblDateTime.Text = "2016-09-13 00:00:00";
            // 
            // picRunStatus
            // 
            this.picRunStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.picRunStatus.Image = ((System.Drawing.Image)(resources.GetObject("picRunStatus.Image")));
            this.picRunStatus.Location = new System.Drawing.Point(1874, -1);
            this.picRunStatus.Name = "picRunStatus";
            this.picRunStatus.Size = new System.Drawing.Size(43, 50);
            this.picRunStatus.TabIndex = 1;
            this.picRunStatus.TabStop = false;
            // 
            // Logo
            // 
            this.Logo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.Logo.Location = new System.Drawing.Point(-1, -1);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(134, 45);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 2;
            this.Logo.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(362, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1266, 44);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "试验台名称";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblTitle_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(1701, 1011);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "GJCS25024 V1.0.0.20251115";
            // 
            // timerPLC
            // 
            this.timerPLC.Interval = 1000;
            this.timerPLC.Tick += new System.EventHandler(this.timerPLC_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblUser,
            this.tslblPLC,
            this.lblYaLi,
            this.lblWenDu,
            this.lblShiDu,
            this.lblPenZui});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1004);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1920, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblUser
            // 
            this.tslblUser.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tslblUser.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.tslblUser.Name = "tslblUser";
            this.tslblUser.Size = new System.Drawing.Size(74, 21);
            this.tslblUser.Text = "用户名称";
            // 
            // tslblPLC
            // 
            this.tslblPLC.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.tslblPLC.ForeColor = System.Drawing.Color.White;
            this.tslblPLC.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.tslblPLC.Name = "tslblPLC";
            this.tslblPLC.Size = new System.Drawing.Size(71, 21);
            this.tslblPLC.Text = "PLC状态";
            // 
            // lblYaLi
            // 
            this.lblYaLi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.lblYaLi.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblYaLi.ForeColor = System.Drawing.Color.White;
            this.lblYaLi.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.lblYaLi.Name = "lblYaLi";
            this.lblYaLi.Size = new System.Drawing.Size(140, 21);
            this.lblYaLi.Text = "环境压力：0.0kPa";
            // 
            // lblWenDu
            // 
            this.lblWenDu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.lblWenDu.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblWenDu.ForeColor = System.Drawing.Color.White;
            this.lblWenDu.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.lblWenDu.Name = "lblWenDu";
            this.lblWenDu.Size = new System.Drawing.Size(128, 21);
            this.lblWenDu.Text = "环境温度：0.0℃";
            // 
            // lblShiDu
            // 
            this.lblShiDu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.lblShiDu.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblShiDu.ForeColor = System.Drawing.Color.White;
            this.lblShiDu.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.lblShiDu.Name = "lblShiDu";
            this.lblShiDu.Size = new System.Drawing.Size(148, 21);
            this.lblShiDu.Text = "环境湿度：0.0%RH";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 44);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnMetering);
            this.splitContainer1.Panel1.Controls.Add(this.btnProc);
            this.splitContainer1.Panel1.Controls.Add(this.btnChangePwd);
            this.splitContainer1.Panel1.Controls.Add(this.btnExit);
            this.splitContainer1.Panel1.Controls.Add(this.btnMainData);
            this.splitContainer1.Panel1.Controls.Add(this.btnHardwareTest);
            this.splitContainer1.Panel1.Controls.Add(this.btnReports);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.splitContainer1.Size = new System.Drawing.Size(1920, 960);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 5;
            // 
            // btnMetering
            // 
            this.btnMetering.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMetering.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnMetering.Image = ((System.Drawing.Image)(resources.GetObject("btnMetering.Image")));
            this.btnMetering.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnMetering.ImageHover")));
            this.btnMetering.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnMetering.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnMetering.ImagePress")));
            this.btnMetering.Location = new System.Drawing.Point(5, 395);
            this.btnMetering.Name = "btnMetering";
            this.btnMetering.Size = new System.Drawing.Size(99, 99);
            this.btnMetering.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMetering.TabIndex = 118;
            this.btnMetering.TabStop = false;
            this.btnMetering.Text = "计量管理";
            this.btnMetering.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMetering.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnMetering.Click += new System.EventHandler(this.btnMetering_Click);
            // 
            // btnProc
            // 
            this.btnProc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProc.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnProc.Image = ((System.Drawing.Image)(resources.GetObject("btnProc.Image")));
            this.btnProc.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnProc.ImageHover")));
            this.btnProc.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnProc.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnProc.ImagePress")));
            this.btnProc.Location = new System.Drawing.Point(5, 297);
            this.btnProc.Name = "btnProc";
            this.btnProc.Size = new System.Drawing.Size(99, 99);
            this.btnProc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnProc.TabIndex = 117;
            this.btnProc.TabStop = false;
            this.btnProc.Text = "测试逻辑";
            this.btnProc.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProc.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnProc.Click += new System.EventHandler(this.btnProc_Click);
            // 
            // btnChangePwd
            // 
            this.btnChangePwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePwd.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnChangePwd.Image = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.Image")));
            this.btnChangePwd.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.ImageHover")));
            this.btnChangePwd.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnChangePwd.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnChangePwd.ImagePress")));
            this.btnChangePwd.Location = new System.Drawing.Point(5, 760);
            this.btnChangePwd.Name = "btnChangePwd";
            this.btnChangePwd.Size = new System.Drawing.Size(99, 99);
            this.btnChangePwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnChangePwd.TabIndex = 116;
            this.btnChangePwd.TabStop = false;
            this.btnChangePwd.Text = "修改密码";
            this.btnChangePwd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChangePwd.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnChangePwd.Click += new System.EventHandler(this.btnChangePwd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageHover")));
            this.btnExit.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnExit.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnExit.ImagePress")));
            this.btnExit.Location = new System.Drawing.Point(5, 858);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(99, 99);
            this.btnExit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnExit.TabIndex = 115;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnMainData
            // 
            this.btnMainData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMainData.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnMainData.Image = ((System.Drawing.Image)(resources.GetObject("btnMainData.Image")));
            this.btnMainData.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnMainData.ImageHover")));
            this.btnMainData.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnMainData.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnMainData.ImagePress")));
            this.btnMainData.Location = new System.Drawing.Point(5, 199);
            this.btnMainData.Name = "btnMainData";
            this.btnMainData.Size = new System.Drawing.Size(99, 99);
            this.btnMainData.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMainData.TabIndex = 114;
            this.btnMainData.TabStop = false;
            this.btnMainData.Text = "数据管理";
            this.btnMainData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMainData.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnMainData.Click += new System.EventHandler(this.btnMainData_Click);
            // 
            // btnHardwareTest
            // 
            this.btnHardwareTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHardwareTest.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnHardwareTest.Image = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.Image")));
            this.btnHardwareTest.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.ImageHover")));
            this.btnHardwareTest.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnHardwareTest.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnHardwareTest.ImagePress")));
            this.btnHardwareTest.Location = new System.Drawing.Point(5, 101);
            this.btnHardwareTest.Name = "btnHardwareTest";
            this.btnHardwareTest.Size = new System.Drawing.Size(99, 99);
            this.btnHardwareTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnHardwareTest.TabIndex = 113;
            this.btnHardwareTest.TabStop = false;
            this.btnHardwareTest.Text = "硬件校准";
            this.btnHardwareTest.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHardwareTest.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnHardwareTest.Click += new System.EventHandler(this.btnHardwareTest_Click);
            // 
            // btnReports
            // 
            this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReports.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnReports.Image = ((System.Drawing.Image)(resources.GetObject("btnReports.Image")));
            this.btnReports.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnReports.ImageHover")));
            this.btnReports.ImageOffset = new System.Drawing.Point(12, 5);
            this.btnReports.ImagePress = ((System.Drawing.Image)(resources.GetObject("btnReports.ImagePress")));
            this.btnReports.Location = new System.Drawing.Point(5, 3);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(99, 99);
            this.btnReports.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnReports.TabIndex = 112;
            this.btnReports.TabStop = false;
            this.btnReports.Text = "数据查询";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReports.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // lblPenZui
            // 
            this.lblPenZui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.lblPenZui.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblPenZui.ForeColor = System.Drawing.Color.White;
            this.lblPenZui.Margin = new System.Windows.Forms.Padding(5, 3, 5, 2);
            this.lblPenZui.Name = "lblPenZui";
            this.lblPenZui.Size = new System.Drawing.Size(112, 21);
            this.lblPenZui.Text = "喷嘴系数：0.0";
            // 
            // frmMainMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1920, 1030);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMainMenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRunStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMetering)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnChangePwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMainData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnHardwareTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReports)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picRunStatus;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerPLC;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblUser;
        private System.Windows.Forms.ToolStripStatusLabel tslblPLC;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Sunny.UI.UIImageButton btnReports;
        private Sunny.UI.UIImageButton btnExit;
        private Sunny.UI.UIImageButton btnMainData;
        private Sunny.UI.UIImageButton btnHardwareTest;
        private Sunny.UI.UIImageButton btnChangePwd;
        public System.Windows.Forms.PictureBox Logo;
        private Sunny.UI.UIImageButton btnProc;
        private System.Windows.Forms.ToolStripStatusLabel lblYaLi;
        private System.Windows.Forms.ToolStripStatusLabel lblWenDu;
        private System.Windows.Forms.ToolStripStatusLabel lblShiDu;
        private Sunny.UI.UIImageButton btnMetering;
        private System.Windows.Forms.ToolStripStatusLabel lblPenZui;
    }
}