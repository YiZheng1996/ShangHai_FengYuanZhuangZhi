using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 满负荷试验
    /// </summary>
    public class LeakCheckTest : BaseTest
    {
        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        TxtTips("满负荷试验开始");
        //        Common.DOgrp[2] = true;
        //        //int time = (para.ProvingMTime * 60) / 5;
        //        //double mfhYL = para.Pressure +0;
        //        //Common.AOgrp.ServoValve = para.PercentServeIn;
        //        DateTime T1 = DateTime.Now;
        //        //TxtTips("满负荷试验正在充气中，请等待排" + "\n" + " 气压力升至" + mfhYL + "KPa或10分钟后" + "\n" + "超时报警");
        //        //while (Common.AIgrp[3] < mfhYL && IsTesting && (DateTime.Now - T1).TotalSeconds < 600)
        //        //{
        //        //    TxtTiming(600 - Convert.ToInt32((DateTime.Now - T1).TotalSeconds));
        //        //    Delay(0.5);
        //        //}
        //        //if (Common.AIgrp[3] < mfhYL)
        //        //{
        //        //    MessageBox.Show("满负荷试验失败，原因：未能将气压升至【" + para.Pressure + "】kPa，请检查气路是否安装错误或气路存在泄漏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //    TestStatus(false);
        //        //    return false;
        //        //}
        //        //Common.AOgrp.ServoValve = para.PercentServeOut;
        //        MessageBox.Show("请检查空压机油路接头、机头端盖、螺堵等部位应无润滑油渗出，并用检漏液检查各气路接口应无气泡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        T1 = DateTime.Now;
        //        //TxtTips("满负荷试验正在试验中，请手" + "\n" + " 动调整伺服阀开口大小，稳定" + "\n" + " 排气压力在" + mfhYL + "±10KPa内");
        //        int sj = 0;
        //        int nTmep = 0;
        //        List<double> list = new List<double>();
        //        //for (int i = 0; i < 5 && IsTesting; i++)
        //        //{
        //        //    do
        //        //    {
        //        //        sj++;
        //        //        nTmep++;
        //        //        //TxtTiming(para.ProvingMTime * 60 - nTmep);
        //        //        //Delay(1);
        //        //        //if (Common.AIgrp[3] > para.Pressure - 5 && Common.AIgrp[3] < para.Pressure + 5)
        //        //        //{
        //        //        //    list.Add(Common.liuliangRst);
        //        //        //}
        //        //    }
        //        //    //while (sj < time && IsTesting);
        //        //    //sj = 0;
        //        //    SetResult(i,list);
        //        //}
        //        double Mantime = Convert.ToDouble(nTmep.ToString());
        //        Common.mResultAll.ManTime = Convert.ToString(Mantime/60);
        //        Common.DOgrp[2] = false;
        //        TestStatus(false);
        //        TxtTips("满负荷试验结束");
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
