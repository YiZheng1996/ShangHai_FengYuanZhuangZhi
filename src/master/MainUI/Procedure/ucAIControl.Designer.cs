namespace MainUI.Procedure
{
    partial class ucAIControl
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
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.txtType = new Sunny.UI.UITextBox();
            this.SuspendLayout();
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.Transparent;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel7.Location = new System.Drawing.Point(0, 0);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(145, 30);
            this.uiLabel7.StyleCustomMode = true;
            this.uiLabel7.TabIndex = 109;
            this.uiLabel7.Text = "控制电源控制(kPa)";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // txtType
            // 
            this.txtType.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtType.DoubleValue = 385.12D;
            this.txtType.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtType.FillReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtType.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.txtType.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtType.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.txtType.Location = new System.Drawing.Point(152, 0);
            this.txtType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtType.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtType.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.txtType.ShowText = false;
            this.txtType.Size = new System.Drawing.Size(89, 32);
            this.txtType.Style = Sunny.UI.UIStyle.Custom;
            this.txtType.StyleCustomMode = true;
            this.txtType.TabIndex = 110;
            this.txtType.Text = "385.12";
            this.txtType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtType.Type = Sunny.UI.UITextBox.UIEditType.Double;
            this.txtType.Watermark = "";
            this.txtType.WatermarkColor = System.Drawing.Color.Black;
            this.txtType.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucAIControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.uiLabel7);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucAIControl";
            this.Size = new System.Drawing.Size(245, 32);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UITextBox txtType;
    }
}
