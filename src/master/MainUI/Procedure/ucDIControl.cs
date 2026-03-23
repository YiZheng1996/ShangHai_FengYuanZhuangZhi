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
    public partial class ucDIControl : UserControl
    {
        public ucDIControl()
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

        private bool _value;
        /// <summary>
        /// 控件值
        /// </summary>
        public bool Value
        {
            get { return _value; }
            set { _value = value; uiLedBulb1.On = value; }
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
    }
}
