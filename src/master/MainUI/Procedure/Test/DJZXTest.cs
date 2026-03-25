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
    /// 额定转速检查
    /// </summary>
    public class EDZSTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("额定转速检查开始");


                ShowInputAndWrite("ZSJC", "额定转速（试验压力：900kPa±20kPa）：");
                Delay(1);

                TestStatus(false);
                TxtTips("额定转速检查结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("额定转速检查失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }

    }
}