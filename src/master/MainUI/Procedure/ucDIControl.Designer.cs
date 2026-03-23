namespace MainUI.Procedure
{
    partial class ucDIControl
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
            this.uiLedBulb1 = new Sunny.UI.UILedBulb();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.SuspendLayout();
            // 
            // uiLedBulb1
            // 
            this.uiLedBulb1.BackColor = System.Drawing.Color.Transparent;
            this.uiLedBulb1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.uiLedBulb1.Location = new System.Drawing.Point(0, 0);
            this.uiLedBulb1.Name = "uiLedBulb1";
            this.uiLedBulb1.On = false;
            this.uiLedBulb1.Size = new System.Drawing.Size(32, 32);
            this.uiLedBulb1.TabIndex = 108;
            this.uiLedBulb1.Tag = "1";
            this.uiLedBulb1.Text = "uiLedBulb1";
            this.uiLedBulb1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(38, 1);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(120, 30);
            this.uiLabel7.StyleCustomMode = true;
            this.uiLabel7.TabIndex = 109;
            this.uiLabel7.Text = "啊啊啊啊啊啊";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucDIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.uiLedBulb1);
            this.Controls.Add(this.uiLabel7);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDIControl";
            this.Size = new System.Drawing.Size(160, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILedBulb uiLedBulb1;
        private Sunny.UI.UILabel uiLabel7;
    }
}
