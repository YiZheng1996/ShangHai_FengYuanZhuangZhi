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
    /// 泄漏试验
    /// </summary>
    public class XLSYTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("泄漏试验开始");




                TestStatus(false);
                TxtTips("泄漏试验结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("泄漏试验失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}