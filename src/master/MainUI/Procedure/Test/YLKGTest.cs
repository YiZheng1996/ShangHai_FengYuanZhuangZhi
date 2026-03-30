using System;
using System.Windows.Forms;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 压力开关试验
    ///
    /// 试验流程：
    ///   1. 提示操作员检查接线
    ///   2. 若气压 > 500kPa，先排气到 500kPa 以下
    ///   3. 校验6个DI点初始状态是否正确
    ///   4. 充气：气压上升，三组压力开关同时动作，记录每个点动作瞬间的气压值
    ///   5. 放气：气压下降，三组压力开关同时复位，记录每个点复位瞬间的气压值
    ///   6. 将结果写入报表
    ///
    /// 每组压力开关含两个互斥DI点：
    ///   常开(NO)：初始=false，充气动作后=true，放气复位后=false
    ///   常闭(NC)：初始=true， 充气动作后=false，放气复位后=true
    /// </summary>
    public class YLKGTest : BaseTest
    {
        // ══════════════════════════════════════════
        //  通道号配置
        // ══════════════════════════════════════════

        // PMC 压力开关
        private int DI_PMC_NO = 13;   // PMC-PS 常开 DI通道
        private int DI_PMC_NC = 14;   // PMC-PS 常闭 DI通道

        // LMRP 压力开关
        private int DI_LMRP_NO = 10;   // LMRP-PS 常开 DI通道
        private int DI_LMRP_NC = 11;   // LMRP-PS 常闭 DI通道

        // PCC 压力开关
        private int DI_PCC_NO = 15;   // PCC-PS 常开 DI通道
        private int DI_PCC_NC = 16;   // PCC-PS 常闭 DI通道

        private int AI_Pressure = 5;   // 排气压力 AI通道
        private int DO_Charge1 = 5;    // 充气控制    风源供电

        // ══════════════════════════════════════════
        //  超时时间配置
        // ══════════════════════════════════════════
        private int Timeout_Exhaust = 300;  // 排气超时：5分钟
        private int Timeout_Charge = 600;  // 充气超时：10分钟
        private int Timeout_Discharge = 600;  // 放气超时：10分钟

        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("压力开关试验开始");

                Common.DOgrp[9] = false; //关闭排气阀
                Common.DOgrp[17] = true; //110V允许
                Common.AOgrp.ServoValve = 0; // 关闭伺服阀

                // ──────────────────────────────────────────────────────────
                // 第一步：提示操作员检查接线和信号
                // ──────────────────────────────────────────────────────────
                ShowInfo(
                    "请确认以下事项后点击【确定】开始试验：\n\n" +
                    "1、检查三组压力开关（共6个DI点位）接线是否正确\n" +
                    "     · PMC-PS  ：常开 / 常闭\n" +
                    "     · LMRP-PS ：常开 / 常闭\n" +
                    "     · PCC-PS  ：常开 / 常闭\n\n" +
                    "2、初始状态应为：常开=断开(false)，常闭=接通(true)\n\n" +
                    "3、检查低压连接器输出电信号是否正常");

                // 如果操作员中途停止，直接退出
                if (!IsTesting) return false;

                // ──────────────────────────────────────────────────────────
                // 第二步：如果当前气压 > 500kPa，先排气
                // ──────────────────────────────────────────────────────────
                if (Common.AIgrp[AI_Pressure] > 500)
                {
                    TxtTips("当前气压超过500kPa，正在排气，请等待...");
                    Common.AOgrp.ServoValve = para.PercentServe; // 打开伺服阀排气
                    Common.DOgrp[DO_Charge1] = false;

                    DateTime t = DateTime.Now;
                    while (Common.AIgrp[AI_Pressure] > 500 && IsTesting)
                    {
                        // 超时检查
                        if ((DateTime.Now - t).TotalSeconds > Timeout_Exhaust)
                        {
                            Common.AOgrp.ServoValve = 0;
                            ShowInfo("排气超时！气压未能降至500kPa以下，请检查气路后重试。");
                            TestStatus(false);
                            return false;
                        }
                        // 显示倒计时
                        //TxtTiming(Timeout_Exhaust - Convert.ToInt32((DateTime.Now - t).TotalSeconds));
                        Delay(0.5);
                    }

                    Common.AOgrp.ServoValve = 10; // 关闭伺服阀
                    if (!IsTesting) return false;

                    TxtTips("排气完成，当前气压：" + Common.AIgrp[AI_Pressure].ToString("f1") + " kPa");
                    Delay(2); // 稳定2秒
                }


                // ──────────────────────────────────────────────────────────
                // 第三步：校验初始DI状态
                // 确保6个点都处于正确的初始位置，否则说明接线有问题
                // ──────────────────────────────────────────────────────────
                string initError = GetInitStateError();
                if (initError != "")
                {
                    ShowInfo("初始DI状态不正确，请检查接线：\n\n" + initError);
                    TestStatus(false);
                    return false;
                }
                TxtTips("初始状态正常，准备开始充气...");


                // ──────────────────────────────────────────────────────────
                // 第四步：充气，监测三组压力开关动作
                //
                // 期望：随着气压升高，每组压力开关先后动作：
                //   常开(NO)：false → true  → 记录该瞬间气压（接通气压）
                //   常闭(NC)：true  → false → 记录该瞬间气压（断开气压）
                //
                // 三组同时监测，全部记录完才结束
                // ──────────────────────────────────────────────────────────
                TxtTips("开始充气，监测三组压力开关动作...");
                Common.DOgrp[DO_Charge1] = true;           // 打开充气DO点

                // 充气阶段：记录6个点动作时的气压值
                double pmc_NO_OnPressure = 0;  // PMC 常开 接通气压
                double pmc_NC_OffPressure = 0;  // PMC 常闭 断开气压
                double lmrp_NO_OnPressure = 0;
                double lmrp_NC_OffPressure = 0;
                double pcc_NO_OnPressure = 0;
                double pcc_NC_OffPressure = 0;

                // 标记各点是否已经记录
                bool pmc_NO_Done = false;
                bool pmc_NC_Done = false;
                bool lmrp_NO_Done = false;
                bool lmrp_NC_Done = false;
                bool pcc_NO_Done = false;
                bool pcc_NC_Done = false;

                // 记录各点上一次的DI状态（用于检测跳变沿）
                bool last_pmc_NO = Common.DIgrp[DI_PMC_NO];
                bool last_pmc_NC = Common.DIgrp[DI_PMC_NC];
                bool last_lmrp_NO = Common.DIgrp[DI_LMRP_NO];
                bool last_lmrp_NC = Common.DIgrp[DI_LMRP_NC];
                bool last_pcc_NO = Common.DIgrp[DI_PCC_NO];
                bool last_pcc_NC = Common.DIgrp[DI_PCC_NC];

                DateTime tCharge = DateTime.Now;

                // 循环直到6个点全部记录完，或操作员停止
                while (IsTesting)
                {
                    // 六个点全部记录完，退出充气循环
                    if (pmc_NO_Done && pmc_NC_Done &&
                        lmrp_NO_Done && lmrp_NC_Done &&
                        pcc_NO_Done && pcc_NC_Done)
                        break;

                    // 充气超时处理
                    if ((DateTime.Now - tCharge).TotalSeconds > Timeout_Charge)
                    {
                        Common.DOgrp[DO_Charge1] = false;
                        Common.AOgrp.ServoValve = 0;
                        ShowInfo("充气超时！以下点位未动作：\n" + GetUndoneChargeMsg(
                            pmc_NO_Done, pmc_NC_Done,
                            lmrp_NO_Done, lmrp_NC_Done,
                            pcc_NO_Done, pcc_NC_Done));
                        TestStatus(false);
                        return false;
                    }

                    //TxtTiming(Timeout_Charge - Convert.ToInt32((DateTime.Now - tCharge).TotalSeconds));

                    // 读取当前气压和各DI状态
                    double curP = Common.AIgrp[AI_Pressure];

                    bool cur_pmc_NO = Common.DIgrp[DI_PMC_NO];
                    bool cur_pmc_NC = Common.DIgrp[DI_PMC_NC];
                    bool cur_lmrp_NO = Common.DIgrp[DI_LMRP_NO];
                    bool cur_lmrp_NC = Common.DIgrp[DI_LMRP_NC];
                    bool cur_pcc_NO = Common.DIgrp[DI_PCC_NO];
                    bool cur_pcc_NC = Common.DIgrp[DI_PCC_NC];

                    // PMC 常开：检测上升沿（false → true）
                    if (!pmc_NO_Done && cur_pmc_NO && !last_pmc_NO)
                    {
                        pmc_NO_OnPressure = curP;
                        pmc_NO_Done = true;
                        TxtTips("[PMC-PS常开] 动作接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PMC 常闭：检测下降沿（true → false）
                    if (!pmc_NC_Done && !cur_pmc_NC && last_pmc_NC)
                    {
                        pmc_NC_OffPressure = curP;
                        pmc_NC_Done = true;
                        TxtTips("[PMC-PS常闭] 动作断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // LMRP 常开：检测上升沿
                    if (!lmrp_NO_Done && cur_lmrp_NO && !last_lmrp_NO)
                    {
                        lmrp_NO_OnPressure = curP;
                        lmrp_NO_Done = true;
                        TxtTips("[LMRP-PS常开] 动作接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // LMRP 常闭：检测下降沿
                    if (!lmrp_NC_Done && !cur_lmrp_NC && last_lmrp_NC)
                    {
                        lmrp_NC_OffPressure = curP;
                        lmrp_NC_Done = true;
                        TxtTips("[LMRP-PS常闭] 动作断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PCC 常开：检测上升沿
                    if (!pcc_NO_Done && cur_pcc_NO && !last_pcc_NO)
                    {
                        pcc_NO_OnPressure = curP;
                        pcc_NO_Done = true;
                        TxtTips("[PCC-PS常开] 动作接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PCC 常闭：检测下降沿
                    if (!pcc_NC_Done && !cur_pcc_NC && last_pcc_NC)
                    {
                        pcc_NC_OffPressure = curP;
                        pcc_NC_Done = true;
                        TxtTips("[PCC-PS常闭] 动作断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // 更新上一次状态，供下一次循环判断边沿
                    last_pmc_NO = cur_pmc_NO;
                    last_pmc_NC = cur_pmc_NC;
                    last_lmrp_NO = cur_lmrp_NO;
                    last_lmrp_NC = cur_lmrp_NC;
                    last_pcc_NO = cur_pcc_NO;
                    last_pcc_NC = cur_pcc_NC;

                    Delay(0.1); // 每100ms轮询一次
                }

                if (!IsTesting) return false;

                TxtTips("充气阶段全部记录完成，稳定2秒后开始放气...");
                Delay(2);


                // ──────────────────────────────────────────────────────────
                // 第五步：放气，监测三组压力开关复位
                //
                // 期望：随着气压降低，每组压力开关先后复位：
                //   常开(NO)：true  → false → 记录该瞬间气压（断开复位气压）
                //   常闭(NC)：false → true  → 记录该瞬间气压（接通复位气压）
                //
                // 三组同时监测，全部记录完才结束
                // ──────────────────────────────────────────────────────────
                TxtTips("开始放气，监测三组压力开关复位...");
                Common.DOgrp[DO_Charge1] = false;          // 关闭充气DO点，开始放气
                Common.AOgrp.ServoValve = 50;   // 排气伺服开口50

                // 放气阶段：记录6个点复位时的气压值
                double pmc_NO_OffPressure = 0;  // PMC 常开 断开复位气压
                double pmc_NC_OnPressure = 0;  // PMC 常闭 接通复位气压
                double lmrp_NO_OffPressure = 0;
                double lmrp_NC_OnPressure = 0;
                double pcc_NO_OffPressure = 0;
                double pcc_NC_OnPressure = 0;

                // 标记各点复位是否已记录
                bool pmc_NO_OffDone = false;
                bool pmc_NC_OnDone = false;
                bool lmrp_NO_OffDone = false;
                bool lmrp_NC_OnDone = false;
                bool pcc_NO_OffDone = false;
                bool pcc_NC_OnDone = false;

                // 放气前重新读取当前状态作为边沿判断的起点
                last_pmc_NO = Common.DIgrp[DI_PMC_NO];
                last_pmc_NC = Common.DIgrp[DI_PMC_NC];
                last_lmrp_NO = Common.DIgrp[DI_LMRP_NO];
                last_lmrp_NC = Common.DIgrp[DI_LMRP_NC];
                last_pcc_NO = Common.DIgrp[DI_PCC_NO];
                last_pcc_NC = Common.DIgrp[DI_PCC_NC];

                DateTime tDischarge = DateTime.Now;

                // 循环直到6个点全部复位记录完，或操作员停止
                while (IsTesting)
                {
                    // 六个点全部记录完，退出放气循环
                    if (pmc_NO_OffDone && pmc_NC_OnDone &&
                        lmrp_NO_OffDone && lmrp_NC_OnDone &&
                        pcc_NO_OffDone && pcc_NC_OnDone)
                        break;

                    // 放气超时处理
                    if ((DateTime.Now - tDischarge).TotalSeconds > Timeout_Discharge)
                    {
                        Common.AOgrp.ServoValve = 0;
                        ShowInfo("放气超时！以下点位未复位：\n" + GetUndoneDischargeMsg(
                            pmc_NO_OffDone, pmc_NC_OnDone,
                            lmrp_NO_OffDone, lmrp_NC_OnDone,
                            pcc_NO_OffDone, pcc_NC_OnDone));
                        TestStatus(false);
                        return false;
                    }

                    //TxtTiming(Timeout_Discharge - Convert.ToInt32((DateTime.Now - tDischarge).TotalSeconds));

                    // 读取当前气压和各DI状态
                    double curP = Common.AIgrp[AI_Pressure];

                    bool cur_pmc_NO = Common.DIgrp[DI_PMC_NO];
                    bool cur_pmc_NC = Common.DIgrp[DI_PMC_NC];
                    bool cur_lmrp_NO = Common.DIgrp[DI_LMRP_NO];
                    bool cur_lmrp_NC = Common.DIgrp[DI_LMRP_NC];
                    bool cur_pcc_NO = Common.DIgrp[DI_PCC_NO];
                    bool cur_pcc_NC = Common.DIgrp[DI_PCC_NC];

                    // PMC 常开：检测下降沿（true → false）复位
                    if (!pmc_NO_OffDone && !cur_pmc_NO && last_pmc_NO)
                    {
                        pmc_NO_OffPressure = curP;
                        pmc_NO_OffDone = true;
                        TxtTips("[PMC-PS常开] 复位断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PMC 常闭：检测上升沿（false → true）复位
                    if (!pmc_NC_OnDone && cur_pmc_NC && !last_pmc_NC)
                    {
                        pmc_NC_OnPressure = curP;
                        pmc_NC_OnDone = true;
                        TxtTips("[PMC-PS常闭] 复位接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // LMRP 常开：检测下降沿复位
                    if (!lmrp_NO_OffDone && !cur_lmrp_NO && last_lmrp_NO)
                    {
                        lmrp_NO_OffPressure = curP;
                        lmrp_NO_OffDone = true;
                        TxtTips("[LMRP-PS常开] 复位断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // LMRP 常闭：检测上升沿复位
                    if (!lmrp_NC_OnDone && cur_lmrp_NC && !last_lmrp_NC)
                    {
                        lmrp_NC_OnPressure = curP;
                        lmrp_NC_OnDone = true;
                        TxtTips("[LMRP-PS常闭] 复位接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PCC 常开：检测下降沿复位
                    if (!pcc_NO_OffDone && !cur_pcc_NO && last_pcc_NO)
                    {
                        pcc_NO_OffPressure = curP;
                        pcc_NO_OffDone = true;
                        TxtTips("[PCC-PS常开] 复位断开，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // PCC 常闭：检测上升沿复位
                    if (!pcc_NC_OnDone && cur_pcc_NC && !last_pcc_NC)
                    {
                        pcc_NC_OnPressure = curP;
                        pcc_NC_OnDone = true;
                        TxtTips("[PCC-PS常闭] 复位接通，气压：" + curP.ToString("f1") + " kPa");
                    }

                    // 更新上一次状态
                    last_pmc_NO = cur_pmc_NO;
                    last_pmc_NC = cur_pmc_NC;
                    last_lmrp_NO = cur_lmrp_NO;
                    last_lmrp_NC = cur_lmrp_NC;
                    last_pcc_NO = cur_pcc_NO;
                    last_pcc_NC = cur_pcc_NC;

                    Delay(0.1); // 每100ms轮询一次
                }

                if (!IsTesting) return false;


                // ──────────────────────────────────────────────────────────
                // 第六步：关闭输出，写入报表结果
                // ──────────────────────────────────────────────────────────
                Common.DOgrp[DO_Charge1] = false;
                Common.AOgrp.ServoValve = 0;

                // PMC 组结果
                Common.mResultAll.dicReport["PMC02"] = pmc_NO_OffPressure.ToString("f1");  // 测试断开压力(NO)
                Common.mResultAll.dicReport["PMC01"] = pmc_NO_OnPressure.ToString("f1");   // 测试接通压力(NO)

                // LMRP 组结果
                Common.mResultAll.dicReport["LMRP01"] = lmrp_NC_OffPressure.ToString("f1"); // 断开压力(NC) → 充气时常闭断开
                Common.mResultAll.dicReport["LMRP02"] = lmrp_NC_OnPressure.ToString("f1");  // 接通压力(NC) → 放气时常闭复位接通
                Common.mResultAll.dicReport["LMRP03"] = lmrp_NO_OffPressure.ToString("f1"); // 断开压力(NO) → 放气时常开复位断开
                Common.mResultAll.dicReport["LMRP04"] = lmrp_NO_OnPressure.ToString("f1");  // 接通压力(NO) → 充气时常开接通

                // PCC 组结果
                Common.mResultAll.dicReport["PCC02"] = pcc_NO_OffPressure.ToString("f1");  // 断开压力(NO)
                Common.mResultAll.dicReport["PCC01"] = pcc_NO_OnPressure.ToString("f1");   // 接通压力(NO)

                TestStatus(false);
                TxtTips("压力开关试验结束");
                return true;
            }
            catch (Exception ex)
            {
                // 异常时确保关闭所有输出，避免设备持续充气
                Common.DOgrp[DO_Charge1] = false;
                Common.AOgrp.ServoValve = 0;
                TxtTips("压力开关试验异常，原因：" + ex.Message);
                TestStatus(false);
                return false;
            }
            finally
            {
                Common.DOgrp[17] = false; //110V允许
                Common.DOgrp[9] = true; // 打开排气阀
                // 无论正常结束、return false、还是异常，这里一定会执行
                TestStatus(false);
            }
        }

        /// <summary>
        /// 检查6个DI点的初始状态是否正确
        /// 正常：常开=false，常闭=true
        /// 返回错误描述字符串；全部正常则返回空字符串
        /// </summary>
        private string GetInitStateError()
        {
            string err = "";

            // 常开点 初始应为 false（断开）
            if (Common.DIgrp[DI_PMC_NO] != false) err += "· PMC-PS  常开 应为false，当前=" + Common.DIgrp[DI_PMC_NO] + "\n";
            if (Common.DIgrp[DI_LMRP_NO] != false) err += "· LMRP-PS 常开 应为false，当前=" + Common.DIgrp[DI_LMRP_NO] + "\n";
            if (Common.DIgrp[DI_PCC_NO] != false) err += "· PCC-PS  常开 应为false，当前=" + Common.DIgrp[DI_PCC_NO] + "\n";

            // 常闭点 初始应为 true（接通）
            if (Common.DIgrp[DI_PMC_NC] != true) err += "· PMC-PS  常闭 应为true， 当前=" + Common.DIgrp[DI_PMC_NC] + "\n";
            if (Common.DIgrp[DI_LMRP_NC] != true) err += "· LMRP-PS 常闭 应为true， 当前=" + Common.DIgrp[DI_LMRP_NC] + "\n";
            if (Common.DIgrp[DI_PCC_NC] != true) err += "· PCC-PS  常闭 应为true， 当前=" + Common.DIgrp[DI_PCC_NC] + "\n";

            return err;
        }

        /// <summary>
        /// 生成充气阶段超时提示：列出还未记录到动作的点位
        /// </summary>
        private string GetUndoneChargeMsg(
            bool pmc_NO, bool pmc_NC,
            bool lmrp_NO, bool lmrp_NC,
            bool pcc_NO, bool pcc_NC)
        {
            string msg = "";
            if (!pmc_NO) msg += "· PMC-PS  常开 未接通\n";
            if (!pmc_NC) msg += "· PMC-PS  常闭 未断开\n";
            if (!lmrp_NO) msg += "· LMRP-PS 常开 未接通\n";
            if (!lmrp_NC) msg += "· LMRP-PS 常闭 未断开\n";
            if (!pcc_NO) msg += "· PCC-PS  常开 未接通\n";
            if (!pcc_NC) msg += "· PCC-PS  常闭 未断开\n";
            return msg;
        }

        /// <summary>
        /// 生成放气阶段超时提示：列出还未复位的点位
        /// </summary>
        private string GetUndoneDischargeMsg(
            bool pmc_NO, bool pmc_NC,
            bool lmrp_NO, bool lmrp_NC,
            bool pcc_NO, bool pcc_NC)
        {
            string msg = "";
            if (!pmc_NO) msg += "· PMC-PS  常开 未复位断开\n";
            if (!pmc_NC) msg += "· PMC-PS  常闭 未复位接通\n";
            if (!lmrp_NO) msg += "· LMRP-PS 常开 未复位断开\n";
            if (!lmrp_NC) msg += "· LMRP-PS 常闭 未复位接通\n";
            if (!pcc_NO) msg += "· PCC-PS  常开 未复位断开\n";
            if (!pcc_NC) msg += "· PCC-PS  常闭 未复位接通\n";
            return msg;
        }
    }
}
