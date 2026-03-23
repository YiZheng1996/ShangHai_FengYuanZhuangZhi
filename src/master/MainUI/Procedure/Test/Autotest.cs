using MainUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 自动测试逻辑
    /// </summary>
    public class Autotest : BaseTest
    {
        /// <summary>
        /// 执行动作行
        /// </summary>
        public DataRow dtProc { get; set; }
        /// <summary>
        /// 记录上一步动作
        /// </summary>
        private string UpStep { get; set; }
        /// <summary>
        /// 检测的返回值
        /// </summary>
        private object oValue { get; set; }
        /// <summary>
        /// 记录AI还是DI
        /// </summary>
        private bool isAI { get; set; }
        /// <summary>
        /// 一键采集
        /// </summary>
        public bool isGather { get; set; }

        public override string Execute()
        {
            try
            {
                TestStatus(true);
                int nSerial = dtProc["Serial"].ToInt();
                string sOperate = dtProc["Operate"].ToString();
                string sPoint = dtProc["Point"].ToString();
                string sDecide = dtProc["Decide"].ToString();
                object oTestValue = dtProc["TestValue"];
                switch (sOperate)
                {
                    case "等待":
                        DengDai(sPoint, oTestValue.ToDouble());
                        break;
                    case "调整":
                        TiaoZheng(sPoint, oTestValue.ToDouble());
                        break;
                    case "打开":
                        DaKai(sPoint);
                        break;
                    case "关闭":
                        GuanBi(sPoint);
                        break;
                    case "检测":
                        oValue = JianCe(sPoint, sDecide, oTestValue.ToDouble());
                        UpStep = "检测";
                        break;
                    case "提示弹窗":
                        TiShiTanChuan(sPoint, oTestValue.ToString());
                        break;
                    case "确认弹窗":
                        oValue = QueRenTianChuan(sPoint, oTestValue.ToString());
                        UpStep = "确认弹窗";
                        break;
                    case "输入弹窗":
                        oValue = ShuRuTianChuan(sPoint, oTestValue.ToString());
                        UpStep = "输入弹窗";
                        break;
                    case "提示":
                        TiShi(sPoint, oTestValue.ToString());
                        break;
                    case "计时开始":
                        isJieShu = false;
                        TimekeepStart(sOperate, oTestValue.ToString());
                        break;
                    case "计时结束":
                        TimekeepStop(sOperate, oTestValue.ToString());
                        UpStep = "计时结束";
                        break;
                    case "采值":
                        if (UpStep == "检测")
                        {
                            if (isAI)
                            {
                                TestStatus(false);
                                return oValue.ToDouble().ToString("f2");
                            }
                            else
                            {
                                TestStatus(false);
                                return oValue.ToString();
                            }

                        }
                        else if (UpStep == "确认弹窗")
                        {
                            TestStatus(false);
                            return oValue.ToString();
                        }
                        else if (UpStep == "输入弹窗")
                        {
                            TestStatus(false);
                            return oValue.ToString();
                        }
                        else if (UpStep == "计时结束")
                        {
                            //等待计时器的值赋予
                            while (!isJieShu)
                            {
                                Thread.Sleep(1);
                            }
                            TestStatus(false);
                            double dTemp = nMilliseconds / 1000;
                            return dTemp.ToString("f1");
                        }
                        break;
                    default:
                        break;
                }

                TestStatus(false);
                return "";
            }
            catch (Exception ex)
            {
                TestStatus(false);
                Var.MessageError(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 等待
        /// </summary>
        private void DengDai(string key, double value)
        {
            isGather = true;
            WaitCallback wait = delegate () { return isGather; };
            if (key == "时间")
            {
                Delay(value, 100, wait);
            }
            else
            {
                throw new ArgumentException("等待", "操作【等待】只允许定义时间");
            }

        }
        /// <summary>
        /// 提示弹窗
        /// </summary>
        private void TiShiTanChuan(string key, string value)
        {
            Var.MessageInfo(value);
        }
        /// <summary>
        /// 确认弹窗
        /// </summary>
        private bool QueRenTianChuan(string key, string value)
        {
            return Var.MessageOKCancel(value);
        }
        /// <summary>
        /// 输入弹窗
        /// </summary>
        private int ShuRuTianChuan(string key, string value)
        {
            return Var.MessageInput(value);
        }
        /// <summary>
        /// 提示
        /// </summary>
        private void TiShi(string key, string value)
        {
            TxtTips(value);
        }

        bool isJiShi_BZ;
        /// <summary>
        /// 用于判断是否已经将值赋予
        /// </summary>
        bool isJieShu = false;
        int nMilliseconds = 0;
        /// <summary>
        /// 计时器开始
        /// </summary>
        private void TimekeepStart(string key, string value)
        {

            isJiShi_BZ = true;
            Thread th = new Thread(new ThreadStart(Timekeeping));
            th.Start();
        }
        /// <summary>
        /// 计时器结束
        /// </summary>
        private void TimekeepStop(string key, string value)
        {
            isJiShi_BZ = false;
        }
        /// <summary>
        /// 计时
        /// </summary>
        private void Timekeeping()
        {
            TimeSpan ts = new TimeSpan();
            DateTime dt = DateTime.Now;
            while (isJiShi_BZ)
            {
                Thread.Sleep(100);
            }
            ts = DateTime.Now - dt;
            nMilliseconds = Convert.ToInt32(ts.TotalMilliseconds);
            isJieShu = true;
        }

        /// <summary>
        /// 调整
        /// </summary>
        private void TiaoZheng(string key, double value)
        {
            switch (key)
            {
                case "主电源电压":
                    Common.powerControlgrp.SetVolt = value;
                    break;
                case "主电源频率":
                    Common.powerControlgrp.SetFrequency = value;
                    break;
                case "信号电源电压":
                    Common.AOgrp.SignalVolt = value;
                    break;
                case "伺服阀开度":
                    Common.AOgrp.ServoValve = value;
                    break;
                default:
                    throw new ArgumentException("调整", "操作【调整】点位异常");
            }
        }

        /// <summary>
        /// 打开
        /// </summary>
        private void DaKai(string key)
        {
            bool isOK = false;
            int index = 0;
            foreach (var item in para.listDOPoint)
            {
                if (item.isStartUsing == true && item.ProductName == key)
                {
                    isOK = true;
                    index = item.Index;
                }
            }
            if (key == "风源供电")
            {
                Common.DOgrp[5] = true;
                return;
            }
            if (!isOK)
            {
                throw new ArgumentException("打开", "操作【打开】点位异常，或未启用");
            }
            Common.DOgrp[index] = true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void GuanBi(string key)
        {
            bool isOK = false;
            int index = 0;
            foreach (var item in para.listDOPoint)
            {
                if (item.isStartUsing == true && item.ProductName == key)
                {
                    isOK = true;
                    index = item.Index;
                }
            }
            if (key == "风源供电")
            {
                Common.DOgrp[5] = false;
                return;
            }
            if (!isOK)
            {
                throw new ArgumentException("打开", "操作【打开】点位异常，或未启用");
            }
            Common.DOgrp[index] = false;
        }
        /// <summary>
        /// 检测
        /// </summary>
        private object JianCe(string key, string Decide, double value)
        {
            //需区分四种情况，
            //1、等于AIDI返回值存变量用于写报表；
            //2、小于大于AI用于等待AI值小于大于某个值
            //3、上升下降沿AI用于等待AI值翻转，并返回翻转时的值
            //4、上升下降沿DI用于等待DI值翻转
            double dTemp = TeShuAIJianCe(key);
            if (dTemp != -1)
            {
                return dTemp;
            }
            //首先判断点位Key是否在DIAI集合中，并启用
            bool isOK = false;
            int index = 0;
            isAI = true;
            foreach (var item in para.listAIPoint)
            {
                if (item.isStartUsing == true && item.ProductName == key)
                {
                    isOK = true;
                    index = item.Index;
                    isAI = true;
                }
            }
            foreach (var item in para.listDIPoint)
            {
                if (item.isStartUsing == true && item.ProductName == key)
                {
                    isOK = true;
                    index = item.Index;
                    isAI = false;
                }
            }
            if (!isOK)
            {
                throw new ArgumentException("检测1", "操作【检测】点位异常，或未启用");
            }
            //再判断判断符号
            switch (Decide)
            {
                case "等于":
                    if (isAI)
                    {
                        return Common.AIgrp[index];
                    }
                    else
                    {
                        return Common.DIgrp[index];
                    }
                case "大于":
                    while (Common.AIgrp[index] <= value)
                    {
                        Delay(0.01);
                    }
                    break;
                case "小于":
                    while (Common.AIgrp[index] >= value)
                    {
                        Delay(0.01);
                    }
                    break;
                case "上升沿":
                    if (isAI)
                    {
                        double nTemp = Common.AIgrp[index];
                        Delay(1);
                        while (Common.AIgrp[index] <= nTemp)
                        {
                            Thread.Sleep(1);
                            nTemp = Common.AIgrp[index];
                        }
                        return nTemp;
                    }
                    else
                    {
                        if (!Common.DIgrp[index])
                        {
                            while (Common.DIgrp[index] == false)
                            {
                                Thread.Sleep(1);
                            }

                        }
                        return Common.DIgrp[index];

                        //bool isTemp = Common.DIgrp[index];
                        //Delay(0.1);
                        //while (Common.DIgrp[index] == isTemp)
                        //{
                        //    Thread.Sleep(1);
                        //}
                        //return isTemp;
                    }
                case "下降沿":
                    if (isAI)
                    {
                        if (Common.DIgrp[index])
                        {
                            while (Common.DIgrp[index] == true)
                            {
                                Thread.Sleep(1);
                            }

                        }
                        return Common.DIgrp[index];
                        //double nTemp = Common.AIgrp[index];
                        //Delay(1);
                        //while (Common.AIgrp[index] >= nTemp)
                        //{
                        //    Thread.Sleep(1);
                        //    nTemp = Common.AIgrp[index];
                        //}
                        //return nTemp;
                    }
                    else
                    {
                        bool isTemp = Common.DIgrp[index];
                        Delay(0.1);
                        while (Common.DIgrp[index] == isTemp)
                        {
                            Thread.Sleep(1);
                        }
                        return isTemp;
                    }
                default:
                    throw new ArgumentException("检测2", "操作【检测】判断符号为空");
            }
            return -1;
        }

        private double TeShuAIJianCe(string key)
        {
            switch (key)
            {
                case "喷嘴流量":
                    return Common.liuliangRst;
                case "吸气温度":
                    return Common.Subjectsgrp[0];
                case "排气温度":
                    return Common.Subjectsgrp[1];
                case "环境温度":
                    return Common.WSDgrp[0];
                case "平均线电压":
                    return Common.powerReadgrp[7];
                case "平均相电压":
                    return Common.powerReadgrp[11];
                case "平均电流":
                    return Common.powerReadgrp[3];
                case "频率":
                    return Common.powerReadgrp[28];
                case "总有功功率":
                    return Common.powerReadgrp[15];
                case "总无功功率":
                    return Common.powerReadgrp[19];
                case "总视在功率":
                    return Common.powerReadgrp[23];
                case "功率因数":
                    return Common.powerReadgrp[27];
                case "轴功率":
                    return Common.powerReadgrp.ZPower;
                default:
                    return -1;
            }
        }


    }
}
