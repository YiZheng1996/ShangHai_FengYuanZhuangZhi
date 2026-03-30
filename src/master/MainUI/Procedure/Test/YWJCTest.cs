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
    /// 油位检查
    /// </summary>
    public class YWJCTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("油位检查开始");

                ShowConfirmAndWrite("YWJC", "油位检查:\n检查油位是否达到要求的高度？");
                Delay(1);


                TestStatus(false);
                TxtTips("油位检查结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("油位检查失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
            finally
            {
                // 无论正常结束、return false、还是异常，这里一定会执行
                TestStatus(false);
            }
        }

    }
}