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
    public partial class AIGrp : BaseModule
    {
        public const int AIcount = 16;
        public Hardware[] hardwares = new Hardware[AIcount];
        private double[] _AiList = new double[AIcount];
        public double[] AIList
        {
            get { return _AiList; }
        }
        public double this[int index]
        {
            get { return AIList[index]; }
        }
        public event ValueGroupHandler<double> AIvalueGrpChanged;
        public AIGrp()
        {
            this.Driver = Var.opcAIGroup;
            InitializeComponent();
            for (int i = 0; i < hardwares.Length; i++)
            {
                hardwares[i] = new Hardware();
            }
        }
        public AIGrp(IContainer container)
            : base(container)
        {
            this.Driver = Var.opcAIGroup;
        }
        public void Fresh()
        {
            for (int i = 0; i < _AiList.Length; i++)
            {
                if (AIvalueGrpChanged != null)
                {
                    AIvalueGrpChanged(this, i, _AiList[i]);
                }
            }
        }
        public override void Init()
        {
            for (int i = 0; i < AIcount; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "AI.AI" + i.ToString().PadLeft(2, '0');

                this.Register<double>(opcTag, delegate (double value)
                {
                    _AiList[idx] = value;
                    if (AIvalueGrpChanged != null)
                        AIvalueGrpChanged(this, idx, value);
                });
            }
            base.Init();
        }
    }
}
