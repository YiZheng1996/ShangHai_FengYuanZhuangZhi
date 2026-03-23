namespace MainUI.Procedure
{
    partial class ucDOControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiSwitch1 = new Sunny.UI.UISwitch();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // uiSwitch1
            // 
            this.uiSwitch1.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.uiSwitch1.BackColor = System.Drawing.Color.Transparent;
            this.uiSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiSwitch1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiSwitch1.Location = new System.Drawing.Point(0, 0);
            this.uiSwitch1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiSwitch1.Name = "uiSwitch1";
            this.uiSwitch1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiSwitch1.Size = new System.Drawing.Size(60, 32);
            this.uiSwitch1.Style = Sunny.UI.UIStyle.Custom;
            this.uiSwitch1.StyleCustomMode = true;
            this.uiSwitch1.SwitchShape = Sunny.UI.UISwitch.UISwitchShape.Square;
            this.uiSwitch1.TabIndex = 117;
            this.uiSwitch1.Tag = "2";
            this.uiSwitch1.Text = "uiSwitch1";
            this.uiSwitch1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiSwitch1.Click += new System.EventHandler(this.uiSwitch1_Click);
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(60, 1);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(106, 30);
            this.uiLabel7.StyleCustomMode = true;
            this.uiLabel7.TabIndex = 118;
            this.uiLabel7.Text = "控制电源控制";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucDOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.uiLabel7);
            this.Controls.Add(this.uiSwitch1);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDOControl";
            this.Size = new System.Drawing.Size(160, 32);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UISwitch uiSwitch1;
        private Sunny.UI.UILabel uiLabel7;
    }
}
