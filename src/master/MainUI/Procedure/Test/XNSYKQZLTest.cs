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
    /// 压力开关试验
    /// </summary>
    public class YLKGTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("压力开关试验开始");




                TestStatus(false);
                TxtTips("压力开关试验结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("压力开关试验失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}