using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 干燥塔与外泵风试验
    /// </summary>
    public class DryingDowerTest : BaseTest
    {
        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        TxtTips("外泵风试验开始");
        //        if (Common.mTestViewModel.ModelName.Contains("F04") || Common.mTestViewModel.ModelName.Contains("F03"))
        //        {
        //            //bool WGF = para.WGFXHxz;
        //            //if (WGF == false)
        //            {
        //                MessageBox.Show("请打开外泵风手阀后点击确定", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
        //                  MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //                Delay(1);
        //                Common.mResultAll.WaiBengFenON = "ON";
        //                Common.mResultAll.WaiBengFenOFF = "OFF";

        //                if (Common.DIgrp[6] == true)
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "合格";
        //                }
        //                else
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "不合格";
        //                }
        //                MessageBox.Show("请关闭外泵风手阀后点击确定", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
        //                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        //                if (Common.DIgrp[6] == false)//关阀也得判定【如果开关状态就只为true不会改变了呢？】
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "合格";
        //                }
        //                else
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "不合格";
        //                }



        //            }

        //            //if (WGF == true)
        //            {

        //                MessageBox.Show("请打开外泵风手阀后点击确定", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
        //                  MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //                Delay(1);
        //                Common.mResultAll.WaiBengFenON = "OFF";
        //                Common.mResultAll.WaiBengFenOFF = "ON";

        //                if (Common.DIgrp[6] == false)
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "合格";
        //                }
        //                else
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "不合格";
        //                }
        //                MessageBox.Show("请关闭外泵风手阀后点击确定", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,
        //                    MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //                if (Common.DIgrp[6] == true)
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "合格";
        //                }
        //                else
        //                {
        //                    Common.mResultAll.WaiBengFenRes = "不合格";
        //                }
        //            }
        //        }

        //        else
        //        {
        //            Common.DOgrp[6] = true; //有些型号没有手阀需要给强泵风信号
        //            Delay(1);
        //            if (Common.DIgrp[6] == true)
        //            {
        //                Common.mResultAll.WaiBengFenRes = "合格";
        //            }
        //            else
        //            {
        //                Common.mResultAll.WaiBengFenRes = "不合格";
        //            }
        //        }
        //        TxtTips("干燥塔试验开始");
        //        int QH = 0;
        //        Common.DOgrp[7] = true; //干燥器启动信号
        //        Delay(1);
        //        bool temp1 = Common.DIgrp[7];
        //        bool temp2 = Common.DIgrp[8];
        //        if (temp1 == false && temp2 == false)
        //        {
        //            MessageBox.Show("干燥器的双塔未能检测到信号，请确认干燥是否上电或已损坏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return false;
        //        }
        //        Stopwatch sw = new Stopwatch();
        //        TxtTips("等待干燥塔第一次切换");
        //        while (temp1 == Common.DIgrp[7] && temp2 == Common.DIgrp[8])
        //        {
        //            QH += 1;
        //            TxtTiming(QH);
        //            Delay(1);
        //        }
        //        sw.Start();
        //        TxtTips("等待干燥塔第二次切换");
              
        //        while (temp1 != Common.DIgrp[7] && temp2 != Common.DIgrp[8])
        //        {
        //            TxtTiming((int)sw.Elapsed.TotalSeconds);
        //            Delay(0.1);
        //        }
        //        TimeSpan ts1 = sw.Elapsed;
        //        sw.Restart();

        //        TxtTips("等待干燥塔第三次切换");
        //        while (temp1 == Common.DIgrp[7] && temp2 == Common.DIgrp[8])
        //        {
        //            TxtTiming((int)sw.Elapsed.TotalSeconds);
        //            Delay(0.1);
        //        }
        //        sw.Stop();
        //        TimeSpan ts2 = sw.Elapsed;
        //        Common.mResultAll.Ta1 = ts1.TotalSeconds;
        //        Common.mResultAll.Ta2 = ts2.TotalSeconds;
        //        Common.DOgrp[7] = false; //干燥器启动信号

        //        TestStatus(false);
        //        TxtTips("干燥塔试验结束");
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        TxtTips("启停试验失败，原因：" + ex.Message);
        //        return false;
        //    }
        //}
    }
}
