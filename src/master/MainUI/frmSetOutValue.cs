using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmSetOutValue : Form
    {
        private double _OutValue = 0.0;
        /// <summary>
        /// 输出返回值
        /// </summary>
        [Description("返回设定值")]
        [DefaultValue(0)]
        public double OutValue
        {
            get { return _OutValue; }
            set { _OutValue = value; }
        }
        public frmSetOutValue()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 初始化模拟量输出窗体
        /// <para>origValue 原始值</para>
        /// <para>title 显示文本</para>
        /// <para>maxValue 最大值</para>
        /// </summary>
        public frmSetOutValue(double origValue, string title, double maxValue, int Decimal)
        {
            InitializeComponent();
            nudOutputValue.Maximum = maxValue;
            if (origValue > maxValue)
                origValue = maxValue;
            nudOutputValue.Text = origValue.ToString("f" + Decimal);
            this.Text = title + "[最大值 " + maxValue + "]";
            OutValue = origValue;
            nudOutputValue.Focus();
            nudOutputValue.SelectAll();
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nudOutputValue.Text))
            {
                nudOutputValue.Text = "0";
            }
            OutValue = Convert.ToDouble(nudOutputValue.Text);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudOutputValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                Btn_Ok_Click(null, null);

            }
        }

    }
}
