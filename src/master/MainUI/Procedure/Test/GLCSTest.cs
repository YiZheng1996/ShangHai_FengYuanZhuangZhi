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
    /// 功率测试
    /// </summary>
    public class GLCSTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("功率测试开始");




                TestStatus(false);
                TxtTips("功率测试结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("功率测试失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}