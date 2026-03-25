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
    /// 电气测试
    /// </summary>
    public class DQCSTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("电气测试试验开始");

                ShowConfirmAndWrite("DQCS1", "绝缘电阻试验:\n主控制回路绝缘测试电压 DC2500V，是否大于10MΩ？");
                Delay(1);
                ShowConfirmAndWrite("DQCS2", "绝缘电阻试验:\n控制回路测试电压 DC500V，是否大于10MΩ？");
                Delay(1);

                ShowConfirmAndWrite("DQCS3", "绝缘耐压试验:\n主控制回路测试电压 AC1500V，是否1min无异常？");
                Delay(1);
                ShowConfirmAndWrite("DQCS4", "绝缘耐压试验:\n控制回路测试电压 AC1000V，是否1min无异常？");
                Delay(1);

                ShowConfirmAndWrite("DQCS5", "重复绝缘电阻试验:\n主控制回路绝缘测试电压 DC2500V，是否大于10MΩ？");
                Delay(1);
                ShowConfirmAndWrite("DQCS6", "重复绝缘电阻试验:\n控制回路测试电压 DC500V，是否大于10MΩ？");
                Delay(1);
                ShowConfirmAndWrite("DQCS7", "请确认:\n重复绝缘电阻试验阻值与绝缘电阻试验阻值的差值的绝对值应不大于绝缘电阻试验阻值的20%？");
                Delay(1);

                TestStatus(false);
                TxtTips("电气测试试验结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("电气测试试验失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}