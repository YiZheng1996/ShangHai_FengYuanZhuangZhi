using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 电机转向检查
    /// </summary>
    public class DJZXTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("电机转向检查开始");




                TestStatus(false);
                TxtTips("电机转向检查结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("电机转向检查失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}