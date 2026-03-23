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
    public partial class NozzleGrp : BaseModule
    {
        private const int Count = 6;
        public event ValueGroupHandler<double> NozzlevalueGrpChaned;
        public NozzleGrp()
        {
            this.Driver = Var.opcNozzle ;
            InitializeComponent();
        }

        public NozzleGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcNozzle;
        }
        private double[] _TestList = new double[Count];
        public double[] TestList
        {
            get { return _TestList; }
        }
        /// <summary>
        /// 0:测温点1; 1:测温点2; 2:测温点3; 4:测压点1; 5:测压点2;
        /// </summary>
        public double this[int index]
        {
            get { return TestList[index]; }
            set { this.Write("CH" + index.ToString().PadLeft(1, '0'), value); }
        }
        public void Fresh()
        {
            for (int i = 0; i < _TestList.Length; i++)
            {
                if (NozzlevalueGrpChaned != null)
                {
                    NozzlevalueGrpChaned(this, i, _TestList[i]);
                }
            }
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
                    if (NozzlevalueGrpChaned != null)
                        NozzlevalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
