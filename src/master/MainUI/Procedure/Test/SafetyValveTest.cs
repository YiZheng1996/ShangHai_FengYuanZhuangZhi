using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    /// <summary>
    /// 安全阀试验
    /// </summary>
    public class SafetyValveTest : BaseTest
    {
        double[,] AirTightness = new double[3, 4];
        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        TxtTips("安全阀试验试验开始,请等待" + "\n" + "安全阀完全打开或排气压力值超" + "\n" + "过1100KPa");
        //        Common.DOgrp[2] = true;
        //        Common.AOgrp.ServoValve = 5;
        //        Common.DOgrp[7] = false;
        //        Delay(10);
        //        bool IsStatus = true;
        //        while (IsStatus)
        //        {
        //            if (Common.AIgrp[3] > 1200)
        //            {
        //                IsStatus = false;
        //            }
        //            double dTemp = Common.AIgrp[3]; //1080
        //            Delay(1);
        //            if (dTemp > Common.AIgrp[3]) //1070
        //            {
        //                IsStatus = false;
        //                TxtTips("安全阀已被触发，等待测" + "\n" + "量关阀气压,开启压力为" + "\n" + dTemp.ToString("f1") + "KPa");
        //                Debug.WriteLine("安全阀已被触发");
        //            }
        //            else
        //            {
        //                Common.mResultAll.OpenAir = dTemp;
        //                Debug.WriteLine(Common.mResultAll.OpenAir);
        //            }
        //        }
        //        IsStatus = true;
        //        while (IsStatus)
        //        {
        //            double dTemp = Common.AIgrp[3];
        //            Delay(1);
        //            if (dTemp < Common.AIgrp[3])
        //            {
        //                IsStatus = false;
        //                Debug.WriteLine("安全阀恢复");
        //            }
        //            else
        //            {
        //                Common.mResultAll.CloseAir = dTemp;
        //                TxtTips("安全阀关阀气压为" + "\n" + dTemp.ToString("f1") + "KPa");
        //                Debug.WriteLine(Common.mResultAll.CloseAir);
        //            }
        //        }
        //        Common.DOgrp[2] = false;
        //        Common.DOgrp[11] = true;
        //        Common.DOgrp[4] = true;//伺服阀供电
        //        Common.DOgrp[7] = true;
        //        Common.AOgrp.ServoValve = 65.0;//伺服阀开口65%
        //        while (Common.AIgrp[3] > 50)
        //        {
        //            Delay(0.1);
        //        }
        //        Common.DOgrp[11] = false;
        //        Common.AOgrp.ServoValve = 0.0;
        //        TxtTips("安全阀试验结束");
        //        TestStatus(false);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        TxtTips("安全阀试验失败，原因：" + ex.Message);
        //        return false;
        //    }
        //}
    }
}
