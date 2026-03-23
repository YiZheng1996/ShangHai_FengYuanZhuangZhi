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
    /// 启停试验
    /// </summary>
    public class NoiseTest : BaseTest
    {
        #region MyRegion

        #endregion
        //int Time = para.QTTime * 60; //试验时间
        int addup = 0;
        int ss = 0;
        int dy = 0, gy = 0; //低压时间、高压时间
        int qTt = 0, tTq = 0;//启动到停止时间、停止到启动时间
        int PS2DK = 0, PS2JT = 0, PS2HFSJ = 0;//压力开关PS2在停机后的断开、接通及恢复时间
        bool isB1 = true, isB2 = true, isB3 = false;//中间标记符
        int nSeconds = 0;//计时器
        int qtcs = 0;

        //public override bool Execute()
        //{
        //    try
        //    {
        //        TestStatus(true);
        //        Common.mResultAll.QiDongAir = "";
        //        Common.mResultAll.TingZhiAir = "";
        //        TxtTips("启停试验开始");
        //        Common.DOgrp[2] = true;
        //        //addup = Time / 6;
        //        //Common.AOgrp.ServoValve = para.PercentServeIn;
        //        DateTime T1 = DateTime.Now;
        //        //TxtTips("正在预充气中，请等待压力升至" + "\n" + para.QtDi + "KPa");
        //        //while (Common.AIgrp[3] <= para.QtDi - 10 && IsTesting && (DateTime.Now - T1).TotalSeconds < 600)
        //        {
        //            TxtTiming(600 - Convert.ToInt32((DateTime.Now - T1).TotalSeconds));
        //            Delay(1);
        //        }
        //        //if (Common.AIgrp[3] < para.QtDi - 10)
        //        //{
        //        //    MessageBox.Show("启停试验失败，原因：未能将气压升至【" + (para.QtDi - 10) + "】kPa，请检查气路是否安装错误或气路存在泄漏", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //    TestStatus(false);
        //        //    return false;
        //        //}
        //        Stopwatch sw = new Stopwatch();
        //        sw.Start();
        //        ss = 0;
        //        while (ss <= 6 && IsTesting)
        //        {
        //            TimeSpan TervalTime = sw.Elapsed;
        //            nSeconds = Convert.ToInt32(TervalTime.TotalSeconds);

        //            //判断是用压力还是压力开关判断启停
        //            //if (para.JiTiSwitch)//=0启停试验为气压控制，=1启停试验为机体压力开关控制
        //            //{
        //            //    //if (Common.DIgrp[para.JiTiPassage] == true && isB1)
        //            //    {
        //            //        Start();
        //            //        TxtTips("正在充气中，等待外部压力" + "\n" + "开关断开");
        //            //    }
        //            //    //if (Common.DIgrp[para.JiTiPassage] == false && isB2)
        //            //    {
        //            //        Stop();
        //            //        TxtTips("正在排气中，等待外部压力" + "\n" + "开关复位");
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    if (Common.AIgrp[3] <= para.QtDi && isB1)
        //            //    {
        //            //        Start();
        //            //        TxtTips("正在充气中,请等待排气压力" + "\n" + "升至" + para.QtGao + "KPa");
        //            //    }
        //            //    if (Common.AIgrp[3] >= para.QtGao && isB2)
        //            //    {
        //            //        Stop();
        //            //        TxtTips("正在排气中，请等待排气压力" + "\n" + "降至" + "\n" + para.QtDi + "KPa");
        //            //    }
        //            //}

        //            if (Common.DIgrp[5] == true && isB3 == true)
        //            {
        //                PS2JT = nSeconds;
        //                PS2HFSJ = PS2JT - PS2DK;
        //                isB3 = false;
        //            }
        //            if (PS2HFSJ > 15)
        //            {
        //                MessageBox.Show("机头开关复位时间超过14秒！请检查是否接错线或机头开关有异常！", "提示信息", MessageBoxButtons.OK,
        //                    MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }
        //            if (nSeconds >= addup * ss)
        //            {
        //                ss++;
        //            }
        //            if (isB1 == false && isB2 == false)
        //            {
        //                isB1 = true;
        //                isB2 = true;
        //                isB3 = false;
        //                int nTemp = ++qtcs;
        //                if (nTemp % 2 == 0)
        //                {
        //                    TxtTips("已完成【" + (nTemp / 2) + "】次启停试验");
        //                    Delay(2);
        //                }
        //                SetResult(ss - 1, qTt, tTq, PS2HFSJ, (nTemp / 2));
        //            }
        //            //TxtTiming(para.QTTime * 60 - nSeconds);
        //            Delay(0.5);
        //        }
        //        Common.DOgrp[2] = false;
        //        TestStatus(false);
        //        TxtTips("启停试验结束");
        //        return true;
        //        //return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("启停试验失败，原因：" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //return false;
        //        return false;
        //    }


        //}

        /// <summary>
        /// 启动空压机操作
        /// </summary>

        //private void Start()
        //{
        //    Common.mResultAll.QiDongAir = (Common.AIgrp[3]).ToString("f1");
        //    isB1 = false;
        //    //Common.AOgrp.ServoValve = para.PercentServeIn;
        //    Common.DOgrp[2] = true;
        //    dy = nSeconds;
        //    qTt = dy - gy;
        //    isB3 = false;
        //}
        ///// <summary>
        ///// 停止空压机操作
        ///// </summary>
        //private void Stop()
        //{
        //    Common.mResultAll.TingZhiAir = (Common.AIgrp[3]).ToString("f1");
        //    isB2 = false;
        //    //Common.AOgrp.ServoValve = para.PercentServeOut;
        //    Common.DOgrp[2] = false;
        //    gy = nSeconds;
        //    tTq = gy - dy;
        //    Thread.Sleep(2000);//最小1.5秒，因为机头压力开关断开也是需要时间的
        //    //判断是否采集压力开关恢复时间
        //    //if (para.JiTouSwitch)//=0启停试验不监控机头压力开关复位时间，=1启停试验监控机头压力开关复位时间
        //    {
        //        //if (Common[para.JiTouPassage] == true)
        //        //{
        //        //    MessageBox.Show("空压机压力开关PS2未正常断开！请注意！", "信息提示", MessageBoxButtons.OK,
        //        //  MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //        //}
        //        //while (Common.DIgrp[para.JiTouPassage] == false && isB3 == false)
        //        {
        //            PS2DK = nSeconds - 2;
        //            isB3 = true;
        //        }


        //    }

        //}

    }
}
