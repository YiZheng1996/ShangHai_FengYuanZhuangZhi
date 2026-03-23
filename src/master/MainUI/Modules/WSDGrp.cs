using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Modules;

namespace MainUI.Modules
{
    public partial class WSDGrp : BaseModule
    {
        private const int Count = 2;
        public event ValueGroupHandler<double> WSDvalueGrpChaned;
        public WSDGrp()
        {
            this.Driver = Var.opcWsd;
            InitializeComponent();
        }

        public WSDGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcWsd;
        }
        private double[] _TestList = new double[Count];
        public double[] TestList
        {
            get { return _TestList; }
        }
        public void Fresh()
        {
            for (int i = 0; i < _TestList.Length; i++)
            {
                if (WSDvalueGrpChaned != null)
                {
                    WSDvalueGrpChaned(this, i, _TestList[i]);
                }
            }
        }
        /// <summary>
        /// 0:温度; 1:湿度;
        /// </summary>
        public double this[int index]
        {
            get { return TestList[index]; }
            set { this.Write("CH" + index.ToString().PadLeft(1, '0'), value); }
        }

        public override void Init()
        {
            for (int i = 0; i < Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "CH" + i.ToString().PadLeft(1, '0');
                this.Register<double>(opcTag, delegate (double value)
                {
                    _TestList[idx] = value;
                    if (WSDvalueGrpChaned != null)
                        WSDvalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
