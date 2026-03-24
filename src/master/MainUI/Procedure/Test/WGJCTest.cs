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
    /// 外观检查
    /// </summary>
    public class WGJCTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("启停试验开始");




                TestStatus(false);
                TxtTips("启停试验结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("外观检查试验失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}