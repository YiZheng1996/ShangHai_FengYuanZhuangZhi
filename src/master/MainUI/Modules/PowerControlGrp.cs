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
    public partial class PowerControlGrp : BaseModule
    {
        private const int Count = 7;
        public event ValueGroupHandler<double> PowerControlvalueGrpChaned;
        public PowerControlGrp()
        {
            this.Driver = Var.opcPowerControl;
            InitializeComponent();
        }

        public PowerControlGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcPowerControl;
        }

        private double _ch02 = 0;
        /// <summary>
        /// 设置频率
        /// </summary>
        public double SetFrequency { get { return _ch02; } set { _ch02 = value; this.Write("CH02", value * 10); } }

        private double _ch03;
        /// <summary>
        /// 设置电压
        /// </summary>
        public double SetVolt { get { return _ch03; } set { _ch03 = value; this.Write("CH03", value * 10 / 1.730); } }

        private int _ch00;
        /// <summary>
        /// 控制模式切换 2为远程，3为本地
        /// </summary>
        public int SetModel { get { return _ch00; } set { _ch00 = value; this.Write("CH00", value); } }

        private int _ch01;
        /// <summary>
        /// 高低档切换（1为低档 2位高档）
        /// </summary>
        public int SetGears { get { return _ch01; } set { _ch01 = value; this.Write("CH01", value); } }

        private int _ch04;
        /// <summary>
        /// 启停寄存器（0=停止，1=启动）
        /// </summary>
        public int SetStartUp { get { return _ch04; } set { _ch04 = value; this.Write("CH04", value); } }


        public override void Init()
        {
            for (int i = 0; i < Count; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "CH" + i.ToString().PadLeft(2, '0');
                this.Register<double>(opcTag, delegate (double value)
                {
                    if (PowerControlvalueGrpChaned != null)
                        PowerControlvalueGrpChaned(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
