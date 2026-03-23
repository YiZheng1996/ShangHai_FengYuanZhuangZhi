using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 超负荷试验
    /// </summary>
    public class AirTest : BaseTest
    {
        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        TxtTips("超负荷试验开始");
        //        Common.DOgrp[2] = true;
        //        //int time = (para.ProvingCTime * 60) / 5;
        //        //double cfhYL = para.Overload;
        //        //Common.AOgrp.ServoValve = para.PercentServeIn;
        //        DateTime T1 = DateTime.Now;
        //        //TxtTips("超负荷正在充气中，请等待排" + "\n" +" 气压力升至" + cfhYL + "KPa或10分钟后" + "\n" + "超时报警");
        //        //while (Common.AIgrp[3] < cfhYL && IsTesting && (DateTime.Now - T1).TotalSeconds < 600)
        //        //{
        //        //    TxtTiming(600 - Convert.ToInt32((DateTime.Now - T1).TotalSeconds));
        //        //    Delay(1);
        //        //}
        //        //if (Common.AIgrp[3] < cfhYL)
        //        //{
        //        //    MessageBox.Show("超负荷试验失败，原因：未能将气压升至【" + para.Overload + "】kPa，请检查气路是否安装错误或气路存在泄漏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //    return false;
        //        //}
        //        //Common.AOgrp.ServoValve = para.PercentServeOut;
        //        int sj = 0;
        //        T1 = DateTime.Now;
        //        double T2 = 0;
        //        int nTmep = 0;
        //        //TxtTips("超负荷试验正在试验中，请手" + "\n" + " 动调整伺服阀开口大小，稳定" + "\n" + " 排气压力在"+ cfhYL + "±10KPa内");
        //        //for (int i = 0; i < 5 && IsTesting; i++)
        //        //{
        //        //    do
        //        //    {
        //        //        sj++;
        //        //        nTmep++;
        //        //        TxtTiming(para.ProvingCTime * 60 - nTmep);
        //        //        Delay(1);
        //        //    }
        //        //    while (sj < time && IsTesting);
        //        //    T2 = sj / 60;
        //        //    sj = 0;
        //        //    SetResult(i, T2);
        //        //}
        //        double Chaotime = Convert.ToDouble(nTmep.ToString());
        //        Common.mResultAll.ChaoTime = Convert.ToString(Chaotime / 60);
        //        Common.DOgrp[2] = false;
        //        TestStatus(false);
        //        TxtTips("超负荷试验结束");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("超负荷试验失败，原因：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return false;
        //    }
        //}
   
    }
}
