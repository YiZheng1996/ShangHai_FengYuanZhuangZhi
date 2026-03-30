using System;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 电机转向检查
    /// </summary>
    public class DJZXTest : BaseTest
    {
        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("电机转向检查开始");

                Common.DOgrp[9] = false; //关闭排气阀
                Common.DOgrp[5] = true;  // 风源供电
                Common.DOgrp[7] = true;  // 110V输出
                Common.DOgrp[17] = true; // 110V允许
                Delay(2);
                Common.DOgrp[5] = false;  // 风源供电

                // 转向检查，如何转向不对需要停止试验，更换线路
                bool yes = ShowConfirm("电机转向检查:\n电机旋转方向是否正确？", "系统提示");
                Common.mResultAll.dicReport["DJZXJC"] = yes ? "OK" : "NG";
                if (!yes)
                {
                    TestStatus(false);
                    return false;
                }

                TestStatus(false);
                TxtTips("电机转向检查结束");
                return true;
            }
            catch (Exception ex)
            {
                TxtTips("电机转向检查失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
            finally
            {
                // 试验完成后关闭输出点
                Common.DOgrp[5] = false;  // 风源供电
                Common.DOgrp[17] = false; // 110V允许
                Common.DOgrp[9] = true; // 打开排气阀

                // 无论正常结束、return false、还是异常，这里一定会执行
                TestStatus(false);
            }
        }

    }
}