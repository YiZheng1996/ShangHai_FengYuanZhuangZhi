using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 气密性试验
    /// </summary>
    public class LeakageTest : BaseTest
    {
        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        TxtTips("气密性试验开始");
        //        Common.DOgrp[2] = true;
        //        Common.AOgrp.ServoValve = 0;
        //        DateTime T1 = DateTime.Now;
        //        TxtTips("正在测试充气时间..");
        //        //if (para.WenYaTime == 0 || para.BaoYaTime == 0)
        //        {
        //            MessageBox.Show("气密性试验失败，原因：试验参数未设置");
        //            TestStatus(false);
        //            return false;
        //        }
        //        while (Common.AIgrp[3] < 1000 && IsTesting && (DateTime.Now - T1).TotalSeconds < 600)
        //        {
        //            TxtTiming(600 - Convert.ToInt32((DateTime.Now - T1).TotalSeconds));
        //            Delay(0.5);
        //        }
        //        if (Common.AIgrp[3] < 1000)
        //        {
        //            MessageBox.Show("气密性试验失败，原因：未能将气压升至【1000】kPa，请检查气路是否安装错误或气路存在泄漏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            TestStatus(false);
        //            return false;
        //        }
        //        Common.DOgrp[2] = false;

        //        TxtTips("正在稳压中..");
        //        int nTmep = 0;
        //        //while (IsTesting && para.WenYaTime > nTmep)
        //        //{
        //        //    nTmep++;
        //        //    TxtTiming(para.WenYaTime - nTmep);
        //        //    Delay(1);
        //        //}
        //        if (Common.AIgrp[3] < 900)
        //        {
        //            MessageBox.Show("气密性试验失败，原因：稳压过程中气压已经泄漏100kPa气压，泄漏量异常大，请检查气路是否安装错误或气路存在泄漏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            TestStatus(false);
        //            return false;
        //        }
        //        TxtTips("正在保压中," + "\n" + "请用检漏液检查空压机的" + "\n" + "各气路接口应无气泡");
        //        double InitialAir = Common.AIgrp[3];
        //        nTmep = 0;
        //        //while (IsTesting && para.BaoYaTime > nTmep)
        //        //{
        //        //    nTmep++;
        //        //    TxtTiming(para.BaoYaTime - nTmep);
        //        //    Delay(1);
        //        //}
        //        double CompletedAir = Common.AIgrp[3];
        //        Common.mResultAll.Leakage = InitialAir - CompletedAir;

        //        TestStatus(false);
        //        TxtTips("气密性试验结束");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("满负荷试验失败，原因：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //}

    }
}
