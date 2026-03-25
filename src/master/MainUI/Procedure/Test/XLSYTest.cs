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

                ShowInputAndWrite("SFLY", "泄漏试验\n在供风装置所有的油路连接处用干净的纸擦拭，目测检查是否有漏油现象");
                Delay(1);

                ShowInputAndWrite("SFLQ", "泄漏试验\n在供风装置所有的气路连接处涂抹检漏液，目测检查是否有漏气现象");
                Delay(1);
                
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