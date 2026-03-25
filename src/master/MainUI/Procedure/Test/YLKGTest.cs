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
    /// 压力开关试验
    /// </summary>
    public class YLKGTest : BaseTest
    {
        #region DI通道配置（根据实际接线修改）

        /// <summary>PMC压力开关 DI通道</summary>
        private int DI_PMC = 1;
        /// <summary>LMRP压力开关 DI通道</summary>
        private int DI_LMRP = 2;
        /// <summary>PCC压力开关 DI通道</summary>
        private int DI_PCC = 3;

        /// <summary>排气压力 AI通道</summary>
        private int AI_Pressure = 5;

        #endregion

        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("压力开关试验开始");

                #region 第一步：提示检查接线

                ShowInfo("请确认以下事项：\n1、检查PMC、LMRP、PCC三组压力开关接线是否正确\n2、检查低压连接器的输出电信号是否正常\n\n确认无误后点击【确定】开始试验");

                if (!IsTesting) return false;

                #endregion

                #region 第二步：气压超过500kPa则先排气

                if (Common.AIgrp[AI_Pressure] > 500)
                {
                    TxtTips("当前气压超过500kPa，正在排气...");
                    Common.AOgrp.ServoValve = para.PercentServe;

                    DateTime tExhaust = DateTime.Now;
                    while (Common.AIgrp[AI_Pressure] > 500 && IsTesting && (DateTime.Now - tExhaust).TotalSeconds < 300)
                    {
                        TxtTiming(300 - Convert.ToInt32((DateTime.Now - tExhaust).TotalSeconds));
                        Delay(0.5);
                    }

                    if (!IsTesting) return false;

                    if (Common.AIgrp[AI_Pressure] > 500)
                    {
                        ShowInfo("排气超时，气压未能降至500kPa以下，请检查气路");
                        TestStatus(false);
                        return false;
                    }

                    TxtTips("排气完成，当前气压：" + Common.AIgrp[AI_Pressure].ToString("f1") + "kPa");
                    Common.AOgrp.ServoValve = 0;
                    Delay(2);
                }

                #endregion

                #region 第三步：充气 - 记录3个压力开关接通（亮）的气压值

                TxtTips("开始充气，监测压力开关接通...");
                Common.DOgrp[2] = true;
                Common.AOgrp.ServoValve = para.PercentServe;

                // 记录上一次的DI状态（用于检测边沿）
                bool lastPMC = Common.DIgrp[DI_PMC];
                bool lastLMRP = Common.DIgrp[DI_LMRP];
                bool lastPCC = Common.DIgrp[DI_PCC];

                // 接通气压值
                double pmcOnPressure = 0;
                double lmrpOnPressure = 0;
                double pccOnPressure = 0;

                // 是否已记录接通
                bool pmcOnRecorded = false;
                bool lmrpOnRecorded = false;
                bool pccOnRecorded = false;

                DateTime tCharge = DateTime.Now;
                int chargeTimeout = 600; // 充气超时10分钟

                while (!(pmcOnRecorded && lmrpOnRecorded && pccOnRecorded) && IsTesting)
                {
                    // 超时保护
                    if ((DateTime.Now - tCharge).TotalSeconds > chargeTimeout)
                    {
                        ShowInfo("充气超时，以下压力开关未接通：\n"
                            + (!pmcOnRecorded ? "· PMC\n" : "")
                            + (!lmrpOnRecorded ? "· LMRP\n" : "")
                            + (!pccOnRecorded ? "· PCC\n" : "")
                            + "请检查接线和气路");
                        TestStatus(false);
                        return false;
                    }

                    TxtTiming(chargeTimeout - Convert.ToInt32((DateTime.Now - tCharge).TotalSeconds));

                    bool curPMC = Common.DIgrp[DI_PMC];
                    bool curLMRP = Common.DIgrp[DI_LMRP];
                    bool curPCC = Common.DIgrp[DI_PCC];
                    double curPressure = Common.AIgrp[AI_Pressure];

                    // PMC：从灭→亮（上升沿）
                    if (!pmcOnRecorded && curPMC && !lastPMC)
                    {
                        pmcOnPressure = curPressure;
                        pmcOnRecorded = true;
                        TxtTips("PMC压力开关已接通，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    // LMRP：从灭→亮
                    if (!lmrpOnRecorded && curLMRP && !lastLMRP)
                    {
                        lmrpOnPressure = curPressure;
                        lmrpOnRecorded = true;
                        TxtTips("LMRP压力开关已接通，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    // PCC：从灭→亮
                    if (!pccOnRecorded && curPCC && !lastPCC)
                    {
                        pccOnPressure = curPressure;
                        pccOnRecorded = true;
                        TxtTips("PCC压力开关已接通，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    lastPMC = curPMC;
                    lastLMRP = curLMRP;
                    lastPCC = curPCC;

                    Delay(0.1); // 100ms轮询，保证捕捉瞬间
                }

                if (!IsTesting) return false;

                TxtTips("三组压力开关全部接通，准备放气...");
                Delay(2);

                #endregion

                #region 第四步：放气 - 记录3个压力开关断开（灭）的气压值

                TxtTips("开始放气，监测压力开关断开...");
                Common.DOgrp[2] = false;
                Common.AOgrp.ServoValve = para.PercentServe;

                // 重新取当前状态
                lastPMC = Common.DIgrp[DI_PMC];
                lastLMRP = Common.DIgrp[DI_LMRP];
                lastPCC = Common.DIgrp[DI_PCC];

                // 断开气压值
                double pmcOffPressure = 0;
                double lmrpOffPressure = 0;
                double pccOffPressure = 0;

                // 是否已记录断开
                bool pmcOffRecorded = false;
                bool lmrpOffRecorded = false;
                bool pccOffRecorded = false;

                DateTime tDischarge = DateTime.Now;
                int dischargeTimeout = 600;

                while (!(pmcOffRecorded && lmrpOffRecorded && pccOffRecorded) && IsTesting)
                {
                    if ((DateTime.Now - tDischarge).TotalSeconds > dischargeTimeout)
                    {
                        ShowInfo("放气超时，以下压力开关未断开：\n"
                            + (!pmcOffRecorded ? "· PMC\n" : "")
                            + (!lmrpOffRecorded ? "· LMRP\n" : "")
                            + (!pccOffRecorded ? "· PCC\n" : "")
                            + "请检查开关是否卡死");
                        TestStatus(false);
                        return false;
                    }

                    TxtTiming(dischargeTimeout - Convert.ToInt32((DateTime.Now - tDischarge).TotalSeconds));

                    bool curPMC = Common.DIgrp[DI_PMC];
                    bool curLMRP = Common.DIgrp[DI_LMRP];
                    bool curPCC = Common.DIgrp[DI_PCC];
                    double curPressure = Common.AIgrp[AI_Pressure];

                    // PMC：从亮→灭（下降沿）
                    if (!pmcOffRecorded && !curPMC && lastPMC)
                    {
                        pmcOffPressure = curPressure;
                        pmcOffRecorded = true;
                        TxtTips("PMC压力开关已断开，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    // LMRP：从亮→灭
                    if (!lmrpOffRecorded && !curLMRP && lastLMRP)
                    {
                        lmrpOffPressure = curPressure;
                        lmrpOffRecorded = true;
                        TxtTips("LMRP压力开关已断开，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    // PCC：从亮→灭
                    if (!pccOffRecorded && !curPCC && lastPCC)
                    {
                        pccOffPressure = curPressure;
                        pccOffRecorded = true;
                        TxtTips("PCC压力开关已断开，气压：" + curPressure.ToString("f1") + "kPa");
                    }

                    lastPMC = curPMC;
                    lastLMRP = curLMRP;
                    lastPCC = curPCC;

                    Delay(0.1);
                }

                if (!IsTesting) return false;

                #endregion

                #region 第五步：写入结果

                Common.AOgrp.ServoValve = 0;

                // 接通气压（充气时亮的瞬间）
                Common.mResultAll.dicReport["PMC_ON"] = pmcOnPressure.ToString("f1");
                Common.mResultAll.dicReport["LMRP_ON"] = lmrpOnPressure.ToString("f1");
                Common.mResultAll.dicReport["PCC_ON"] = pccOnPressure.ToString("f1");

                // 断开气压（放气时灭的瞬间）
                Common.mResultAll.dicReport["PMC_OFF"] = pmcOffPressure.ToString("f1");
                Common.mResultAll.dicReport["LMRP_OFF"] = lmrpOffPressure.ToString("f1");
                Common.mResultAll.dicReport["PCC_OFF"] = pccOffPressure.ToString("f1");

                // 回差值 = 接通气压 - 断开气压
                Common.mResultAll.dicReport["PMC_DIFF"] = (pmcOnPressure - pmcOffPressure).ToString("f1");
                Common.mResultAll.dicReport["LMRP_DIFF"] = (lmrpOnPressure - lmrpOffPressure).ToString("f1");
                Common.mResultAll.dicReport["PCC_DIFF"] = (pccOnPressure - pccOffPressure).ToString("f1");

                #endregion

                TestStatus(false);
                TxtTips("压力开关试验结束");
                return true;
            }
            catch (Exception ex)
            {
                Common.DOgrp[2] = false;
                Common.AOgrp.ServoValve = 0;
                TxtTips("压力开关试验失败，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
        }
    }
}