namespace MainUI
{
    partial class frmHardWare
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpAI = new Sunny.UI.UIGroupBox();
            this.ucCalibration12 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration13 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration14 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration15 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration9 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration10 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration11 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration6 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration7 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration8 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration4 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration3 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration2 = new MainUI.Procedure.UCCalibration();
            this.grpAO = new Sunny.UI.UIGroupBox();
            this.ucCalibration5 = new MainUI.Procedure.UCCalibration();
            this.ucCalibration1 = new MainUI.Procedure.UCCalibration();
            this.panel1.SuspendLayout();
            this.grpAI.SuspendLayout();
            this.grpAO.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(279, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 38);
            this.panel1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(192, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 27);
            this.label2.TabIndex = 7;
            this.label2.Text = "增益值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(12, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "测定值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(106, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "零点值";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Cursor = System.Windows.Forms.Cursors.Default;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.ForeColor = System.Drawing.Color.DarkRed;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(176, 881);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(433, 27);
            this.label12.TabIndex = 54;
            this.label12.Text = "计算公式：测定值 = 工程值 * 增益值 - 零点值  ";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // grpAI
            // 
            this.grpAI.Controls.Add(this.ucCalibration12);
            this.grpAI.Controls.Add(this.ucCalibration13);
            this.grpAI.Controls.Add(this.ucCalibration14);
            this.grpAI.Controls.Add(this.ucCalibration15);
            this.grpAI.Controls.Add(this.ucCalibration9);
            this.grpAI.Controls.Add(this.ucCalibration10);
            this.grpAI.Controls.Add(this.ucCalibration11);
            this.grpAI.Controls.Add(this.ucCalibration6);
            this.grpAI.Controls.Add(this.ucCalibration7);
            this.grpAI.Controls.Add(this.ucCalibration8);
            this.grpAI.Controls.Add(this.ucCalibration4);
            this.grpAI.Controls.Add(this.ucCalibration3);
            this.grpAI.Controls.Add(this.ucCalibration2);
            this.grpAI.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpAI.Location = new System.Drawing.Point(49, 54);
            this.grpAI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAI.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpAI.Name = "grpAI";
            this.grpAI.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpAI.Size = new System.Drawing.Size(690, 661);
            this.grpAI.TabIndex = 382;
            this.grpAI.Text = "输入检测";
            this.grpAI.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpAI.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucCalibration12
            // 
            this.ucCalibration12.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration12.GainValue = 0D;
            this.ucCalibration12.Index = 10;
            this.ucCalibration12.Location = new System.Drawing.Point(41, 507);
            this.ucCalibration12.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration12.Name = "ucCalibration12";
            this.ucCalibration12.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration12.TabIndex = 16;
            this.ucCalibration12.Text = "可调电源电压检测";
            this.ucCalibration12.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration13
            // 
            this.ucCalibration13.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration13.GainValue = 0D;
            this.ucCalibration13.Index = 13;
            this.ucCalibration13.Location = new System.Drawing.Point(41, 601);
            this.ucCalibration13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration13.Name = "ucCalibration13";
            this.ucCalibration13.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration13.TabIndex = 18;
            this.ucCalibration13.Text = "喷嘴差压检测(Px2)";
            this.ucCalibration13.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration14
            // 
            this.ucCalibration14.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration14.GainValue = 0D;
            this.ucCalibration14.Index = 12;
            this.ucCalibration14.Location = new System.Drawing.Point(41, 554);
            this.ucCalibration14.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration14.Name = "ucCalibration14";
            this.ucCalibration14.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration14.TabIndex = 17;
            this.ucCalibration14.Text = "喷嘴差压检测(Px1)";
            this.ucCalibration14.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration15
            // 
            this.ucCalibration15.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration15.GainValue = 0D;
            this.ucCalibration15.Index = 9;
            this.ucCalibration15.Location = new System.Drawing.Point(41, 460);
            this.ucCalibration15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration15.Name = "ucCalibration15";
            this.ucCalibration15.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration15.TabIndex = 15;
            this.ucCalibration15.Text = "DC110V被试品温度检测";
            this.ucCalibration15.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration9
            // 
            this.ucCalibration9.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration9.GainValue = 0D;
            this.ucCalibration9.Index = 8;
            this.ucCalibration9.Location = new System.Drawing.Point(41, 413);
            this.ucCalibration9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration9.Name = "ucCalibration9";
            this.ucCalibration9.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration9.TabIndex = 14;
            this.ucCalibration9.Text = "DC24V被试品温度检测";
            this.ucCalibration9.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration10
            // 
            this.ucCalibration10.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration10.GainValue = 0D;
            this.ucCalibration10.Index = 7;
            this.ucCalibration10.Location = new System.Drawing.Point(41, 366);
            this.ucCalibration10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration10.Name = "ucCalibration10";
            this.ucCalibration10.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration10.TabIndex = 13;
            this.ucCalibration10.Text = "PE08压力检测";
            this.ucCalibration10.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration11
            // 
            this.ucCalibration11.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration11.GainValue = 0D;
            this.ucCalibration11.Index = 6;
            this.ucCalibration11.Location = new System.Drawing.Point(41, 319);
            this.ucCalibration11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration11.Name = "ucCalibration11";
            this.ucCalibration11.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration11.TabIndex = 12;
            this.ucCalibration11.Text = "PE07压力检测";
            this.ucCalibration11.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration6
            // 
            this.ucCalibration6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration6.GainValue = 0D;
            this.ucCalibration6.Index = 5;
            this.ucCalibration6.Location = new System.Drawing.Point(41, 272);
            this.ucCalibration6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration6.Name = "ucCalibration6";
            this.ucCalibration6.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration6.TabIndex = 11;
            this.ucCalibration6.Text = "PE06压力检测";
            this.ucCalibration6.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration7
            // 
            this.ucCalibration7.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration7.GainValue = 0D;
            this.ucCalibration7.Index = 4;
            this.ucCalibration7.Location = new System.Drawing.Point(41, 225);
            this.ucCalibration7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration7.Name = "ucCalibration7";
            this.ucCalibration7.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration7.TabIndex = 10;
            this.ucCalibration7.Text = "PE05压力检测";
            this.ucCalibration7.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration8
            // 
            this.ucCalibration8.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration8.GainValue = 0D;
            this.ucCalibration8.Index = 3;
            this.ucCalibration8.Location = new System.Drawing.Point(41, 178);
            this.ucCalibration8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration8.Name = "ucCalibration8";
            this.ucCalibration8.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration8.TabIndex = 9;
            this.ucCalibration8.Text = "PE04压力检测";
            this.ucCalibration8.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration4
            // 
            this.ucCalibration4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration4.GainValue = 0D;
            this.ucCalibration4.Index = 2;
            this.ucCalibration4.Location = new System.Drawing.Point(41, 131);
            this.ucCalibration4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration4.Name = "ucCalibration4";
            this.ucCalibration4.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration4.TabIndex = 8;
            this.ucCalibration4.Text = "PE03压力检测";
            this.ucCalibration4.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration3
            // 
            this.ucCalibration3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration3.GainValue = 0D;
            this.ucCalibration3.Index = 1;
            this.ucCalibration3.Location = new System.Drawing.Point(41, 84);
            this.ucCalibration3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration3.Name = "ucCalibration3";
            this.ucCalibration3.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration3.TabIndex = 7;
            this.ucCalibration3.Text = "PE02压力检测";
            this.ucCalibration3.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // ucCalibration2
            // 
            this.ucCalibration2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration2.GainValue = 0D;
            this.ucCalibration2.Location = new System.Drawing.Point(41, 37);
            this.ucCalibration2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration2.Name = "ucCalibration2";
            this.ucCalibration2.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration2.TabIndex = 6;
            this.ucCalibration2.Text = "环境压力检测";
            this.ucCalibration2.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AI_Submited);
            // 
            // grpAO
            // 
            this.grpAO.Controls.Add(this.ucCalibration5);
            this.grpAO.Controls.Add(this.ucCalibration1);
            this.grpAO.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.grpAO.Location = new System.Drawing.Point(49, 725);
            this.grpAO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpAO.MinimumSize = new System.Drawing.Size(1, 1);
            this.grpAO.Name = "grpAO";
            this.grpAO.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.grpAO.Size = new System.Drawing.Size(690, 138);
            this.grpAO.TabIndex = 383;
            this.grpAO.Text = "输出检测";
            this.grpAO.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grpAO.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ucCalibration5
            // 
            this.ucCalibration5.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration5.GainValue = 0D;
            this.ucCalibration5.Index = 2;
            this.ucCalibration5.Location = new System.Drawing.Point(41, 37);
            this.ucCalibration5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration5.Name = "ucCalibration5";
            this.ucCalibration5.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration5.TabIndex = 19;
            this.ucCalibration5.Text = "可调电源输出电压";
            this.ucCalibration5.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AO_Submited);
            // 
            // ucCalibration1
            // 
            this.ucCalibration1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ucCalibration1.GainValue = 0D;
            this.ucCalibration1.Index = 3;
            this.ucCalibration1.Location = new System.Drawing.Point(41, 80);
            this.ucCalibration1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCalibration1.Name = "ucCalibration1";
            this.ucCalibration1.Size = new System.Drawing.Size(620, 37);
            this.ucCalibration1.TabIndex = 20;
            this.ucCalibration1.Text = "伺服阀";
            this.ucCalibration1.Submited += new System.EventHandler<MainUI.Procedure.SubmitArgs>(this.ucCalibration_AO_Submited);
            // 
            // frmHardWare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(822, 917);
            this.Controls.Add(this.grpAO);
            this.Controls.Add(this.grpAI);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHardWare";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "硬件校准";
            this.Load += new System.EventHandler(this.frmHardWare_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpAI.ResumeLayout(false);
            this.grpAI.PerformLayout();
            this.grpAO.ResumeLayout(false);
            this.grpAO.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.Timer timer1;
        private Sunny.UI.UIGroupBox grpAI;
        private Sunny.UI.UIGroupBox grpAO;
        private Procedure.UCCalibration ucCalibration2;
        private Procedure.UCCalibration ucCalibration4;
        private Procedure.UCCalibration ucCalibration3;
        private Procedure.UCCalibration ucCalibration1;
        private Procedure.UCCalibration ucCalibration13;
        private Procedure.UCCalibration ucCalibration14;
        private Procedure.UCCalibration ucCalibration15;
        private Procedure.UCCalibration ucCalibration9;
        private Procedure.UCCalibration ucCalibration10;
        private Procedure.UCCalibration ucCalibration11;
        private Procedure.UCCalibration ucCalibration6;
        private Procedure.UCCalibration ucCalibration7;
        private Procedure.UCCalibration ucCalibration8;
        private Procedure.UCCalibration ucCalibration5;
        private Procedure.UCCalibration ucCalibration12;
    }
}