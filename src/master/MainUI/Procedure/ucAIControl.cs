using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    public partial class ucAIControl : UserControl
    {
        public ucAIControl()
        {
            InitializeComponent();
        }
        private string _labelText;
        /// <summary>
        /// 控件名称
        /// </summary>
        public string LabelText
        {
            get { return _labelText; }
            set { _labelText = value; uiLabel7.Text = value; }
        }

        private double _value;
        /// <summary>
        /// 控件值
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { _value = value; txtType.Text = value.ToString("f" + Decimal); }
        }
        private int _Index;
        /// <summary>
        /// 控件对应的下标
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }
        /// <summary>
        /// 保留小数位
        /// </summary>
        public int Decimal { get; set; } = 2;



    }
}
