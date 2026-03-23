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
    public partial class PowerReadGrp : BaseModule
    {
        private const int Count = 29;
        public event ValueGroupHandler<double> PowerReadvalueGrpChaned;
        public PowerReadGrp()
        {
            this.Driver = Var.opcPowerRead;
            InitializeComponent();
        }

        public PowerReadGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcPowerRead;
        }
        private double[] _TestList = new double[Count];
        public double[] TestList
        {
            get { return _TestList; }
        }
    
        public double this[int index]
        {
            get { return TestList[index]; }
            set { this.Write("CH" + index.ToString().PadLeft(2, '0'), value); }
        }

        public void Fresh()
        {
            for (int i = 0; i < _TestList.Length; i++)
            {
                if (PowerReadvalueGrpChaned != null)
                {
                    PowerReadvalueGrpChaned(this, i, _TestList[i]);
                }
            }
        }
        /// <summary>
        /// 轴功率
        /// </summary>
        public double ZPower
        {
            get
            {
                return TestList[15] * 0.886 * 0.98;
            }
        }
     

        public override void Init()
        {
            for (int i = 0; i < Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "CH" + i.ToString().PadLeft(2, '0');
                this.Register<double>(opcTag, delegate (double value)
                {
                    _TestList[idx] = value;
                    if (PowerReadvalueGrpChaned != null)
                        PowerReadvalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
