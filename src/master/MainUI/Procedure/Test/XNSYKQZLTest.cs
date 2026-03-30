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
    /// 性能试验和空气质量测定
    /// </summary>
    public class XNSYKQZLTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("性能试验和空气质量测定开始");




                TestStatus(false);
                TxtTips("性能试验和空气质量测定结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("性能试验和空气质量测定失败，原因：" + ex.Message);
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