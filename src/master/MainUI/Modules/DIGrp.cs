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
    public partial class DIGrp : BaseModule
    {
        public DIGrp()
        {
            this.Driver = Var.opcDIGroup;
            InitializeComponent();
        }

        public DIGrp(IContainer container)
                        : base(container)
        {
            this.Driver = Var.opcDIGroup;
        }
        private const int DIcnt = 32;
        private bool[] _diList = new bool[DIcnt];
        public bool[] DIList
        {
            get { return _diList; }
        }
        public bool this[int index]
        {
            get { return DIList[index]; }
        }
        public void Fresh()
        {
            for (int i = 0; i < _diList.Length; i++)
            {
                if (DIGroupChanged != null)
                {
                    DIGroupChanged(this, i, _diList[i]);
                }
            }
        }
        public event ValueGroupHandler<bool> DIGroupChanged;
        public override void Init()
        {
            for (int i = 0; i < DIcnt; i++)
            {
                int idx = i; // 循环中的i需要用临时变量存储。
                string opcTag = "DI.DI" + i.ToString().PadLeft(2, '0');
                this.Register<bool>(opcTag, delegate (bool value)
                {
                    //数组赋值
                    _diList[idx] = value;
                    if (DIGroupChanged != null)
                        DIGroupChanged(this, idx, value);

                });
            }
            base.Init();
        }
    }
}
