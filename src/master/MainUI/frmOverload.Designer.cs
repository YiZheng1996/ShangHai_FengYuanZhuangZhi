namespace MainUI
{
    partial class frmOverload
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
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.cboPS3 = new Sunny.UI.UIComboBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.cboPS2 = new Sunny.UI.UIComboBox();
            this.btnGet = new Sunny.UI.UIButton();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.cboOverLoad = new Sunny.UI.UIComboBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.cboTemp = new Sunny.UI.UIComboBox();
            this.SuspendLayout();
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(379, 40);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(153, 23);
            this.uiLabel2.TabIndex = 78;
            this.uiLabel2.Text = "吸附塔压力开关PS3";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboPS3
            // 
            this.cboPS3.DataSource = null;
            this.cboPS3.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboPS3.FillColor = System.Drawing.Color.White;
            this.cboPS3.FilterMaxCount = 50;
            this.cboPS3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboPS3.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboPS3.Location = new System.Drawing.Point(532, 37);
            this.cboPS3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboPS3.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboPS3.Name = "cboPS3";
            this.cboPS3.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboPS3.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboPS3.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboPS3.Size = new System.Drawing.Size(165, 29);
            this.cboPS3.TabIndex = 77;
            this.cboPS3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboPS3.Watermark = "请选择";
            this.cboPS3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(27, 40);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(153, 23);
            this.uiLabel1.TabIndex = 76;
            this.uiLabel1.Text = "吸附塔压力开关PS2";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboPS2
            // 
            this.cboPS2.DataSource = null;
            this.cboPS2.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboPS2.FillColor = System.Drawing.Color.White;
            this.cboPS2.FilterMaxCount = 50;
            this.cboPS2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboPS2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboPS2.Location = new System.Drawing.Point(180, 37);
            this.cboPS2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboPS2.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboPS2.Name = "cboPS2";
            this.cboPS2.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboPS2.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboPS2.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboPS2.Size = new System.Drawing.Size(165, 29);
            this.cboPS2.TabIndex = 75;
            this.cboPS2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboPS2.Watermark = "请选择";
            this.cboPS2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnGet
            // 
            this.btnGet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGet.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.btnGet.Location = new System.Drawing.Point(568, 145);
            this.btnGet.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(120, 46);
            this.btnGet.TabIndex = 388;
            this.btnGet.Text = "保存";
            this.btnGet.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGet.TipsText = "1";
            this.btnGet.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(379, 91);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(153, 23);
            this.uiLabel3.TabIndex = 392;
            this.uiLabel3.Text = "电机过载信号";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboOverLoad
            // 
            this.cboOverLoad.DataSource = null;
            this.cboOverLoad.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboOverLoad.FillColor = System.Drawing.Color.White;
            this.cboOverLoad.FilterMaxCount = 50;
            this.cboOverLoad.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboOverLoad.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboOverLoad.Location = new System.Drawing.Point(532, 88);
            this.cboOverLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboOverLoad.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboOverLoad.Name = "cboOverLoad";
            this.cboOverLoad.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboOverLoad.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboOverLoad.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboOverLoad.Size = new System.Drawing.Size(165, 29);
            this.cboOverLoad.TabIndex = 391;
            this.cboOverLoad.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboOverLoad.Watermark = "请选择";
            this.cboOverLoad.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(27, 91);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(153, 23);
            this.uiLabel4.TabIndex = 390;
            this.uiLabel4.Text = "高温反馈信号";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cboTemp
            // 
            this.cboTemp.DataSource = null;
            this.cboTemp.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cboTemp.FillColor = System.Drawing.Color.White;
            this.cboTemp.FilterMaxCount = 50;
            this.cboTemp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.cboTemp.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cboTemp.Location = new System.Drawing.Point(180, 88);
            this.cboTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboTemp.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboTemp.Name = "cboTemp";
            this.cboTemp.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboTemp.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.cboTemp.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom;
            this.cboTemp.Size = new System.Drawing.Size(165, 29);
            this.cboTemp.TabIndex = 389;
            this.cboTemp.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboTemp.Watermark = "请选择";
            this.cboTemp.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // frmOverload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(736, 210);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.cboOverLoad);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.cboTemp);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.cboPS3);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.cboPS2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOverload";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择测试点位";
            this.Load += new System.EventHandler(this.frmOverload_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIComboBox cboPS3;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cboPS2;
        private Sunny.UI.UIButton btnGet;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox cboOverLoad;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIComboBox cboTemp;
    }
}