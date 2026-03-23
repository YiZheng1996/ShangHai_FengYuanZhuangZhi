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
    public partial class AOGrp : BaseModule
    {
        private const int AOcount = 4;
        public event ValueGroupHandler<double> AOvalueGrpChaned;
        public AOGrp()
        {
            this.Driver = Var.opcAOGroup;
            InitializeComponent();
        }

        public AOGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcAOGroup;
        }

        private double _cao02 = 0;
        /// <summary>
        /// 电压调节
        /// </summary>
        public double SignalVolt { get { return _cao02; } set { _cao02 = value; this.Write("AO.AO02", value); } }

        private double _cao03 = 0;
        /// <summary>
        /// 伺服阀
        /// </summary>
        public double ServoValve { get { return _cao03; } set { _cao03 = value; this.Write("AO.AO03", value * 16 / 100); } }


        public override void Init()
        {
            for (int i = 0; i < AOcount; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "AO.AO" + i.ToString().PadLeft(2, '0');
                this.Register<double>(opcTag, delegate (double value)
                {
                    if (idx == 2)
                    {
                        _cao02 = value;
                    }
                    if (idx == 3)
                    {
                        _cao03 = value / 16 * 100;
                    }
                    if (AOvalueGrpChaned != null)
                        AOvalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
