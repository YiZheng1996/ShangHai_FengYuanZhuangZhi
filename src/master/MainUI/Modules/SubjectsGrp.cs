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
    public partial class SubjectsGrp : BaseModule
    {
        private const int Count = 6;
        public event ValueGroupHandler<double> SubjectsvalueGrpChaned;
        public SubjectsGrp()
        {
            this.Driver = Var.opcSubjects;
            InitializeComponent();
        }

        public SubjectsGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcSubjects;
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
                if (SubjectsvalueGrpChaned != null)
                {
                    SubjectsvalueGrpChaned(this, i, _TestList[i]);
                }
            }
        }
        /// <summary>
        /// 0:进气温度; 1:排气温度; 3:喷嘴差压1; 4:喷嘴差压2;
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
                    if (SubjectsvalueGrpChaned != null)
                        SubjectsvalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
