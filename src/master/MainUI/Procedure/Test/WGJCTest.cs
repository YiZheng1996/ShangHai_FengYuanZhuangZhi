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

                ShowConfirmAndWrite("WGJC", "外观检查，请确认:\n装置外观检查是否合格？");
                Delay(1);

                ShowConfirmAndWrite("CCJC1", "尺寸检查，请确认:\n装置电气接口尺寸是否正确？");
                Delay(1);

                ShowConfirmAndWrite("CCJC2", "尺寸检查，请确认:\n装置机械接口尺寸是否正确？");
                Delay(1);

                ShowConfirmAndWrite("CCJC3", "尺寸检查，请确认:\n装置气动接口尺寸是否正确？");
                Delay(1);


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
            finally
            {
                // 无论正常结束、return false、还是异常，这里一定会执行
                TestStatus(false);
            }
        }

    }
}