using System;
using System.Collections.Generic;
using System.Linq;

namespace MainUI.Procedure.Test
{
    /// <summary>
    /// 性能试验
    /// </summary>
    public class XNSYTest : BaseTest
    {
        /// <summary>排气压力 AI通道</summary>
        private int AI_Pressure = 5;

        public override bool Execute()
        {
            try
            {
                TestStatus(true);
                TxtTips("性能试验开始");

                double targetPressure = para.Pressure;
                int testTime = para.ProvingMTime * 60; // 总试验时间（秒）

                #region 第一阶段：充气

                // 启动信号 
                Common.DOgrp[9] = false; //关闭排气阀
                Common.DOgrp[5] = true;  // 风源供电
                Common.DOgrp[7] = true;  // 110V输出
                Common.DOgrp[17] = true; // 110V允许
                Common.AOgrp.ServoValve = 5;  // 充气固定伺服阀开5%，快速升压到试验气压值暂定900±20


                DateTime T1 = DateTime.Now;
                TxtTips("性能试验正在充气中，请等待排气压力升至" + targetPressure + "KPa");

                while (Common.AIgrp[AI_Pressure] <= targetPressure && IsTesting && (DateTime.Now - T1).TotalSeconds < 600)
                {
                    //TxtTiming(600 - Convert.ToInt32((DateTime.Now - T1).TotalSeconds));

                    // 实时监控DI点
                    //if (!CheckMonitorDI()) return false;

                    Delay(0.5);
                }

                if (!IsTesting) return false;

                if (Common.AIgrp[AI_Pressure] < targetPressure)
                {
                    ShowInfo("性能试验失败，原因：未能将气压升至【" + targetPressure + "】kPa，请检查气路是否安装错误或气路存在泄漏");
                    TestStatus(false);
                    return false;
                }

                #endregion

                #region 第二阶段：人工检查

                Common.AOgrp.ServoValve = para.PercentServe;  // 将伺服阀打开到参数设定的开度，使气压稳定在设定工作压力±20


                ShowInputAndWrite("ZSJC", "额定转速（1387r/min～1533r/min）：");
                Delay(1);

                ShowConfirmAndWrite("SFLY", "泄漏试验\n在供风装置所有的油路连接处用干净的纸擦拭，目测检查是否有漏油现象");
                Delay(1);

                ShowConfirmAndWrite("SFLQ", "泄漏试验\n在供风装置所有的气路连接处涂抹检漏液，目测检查是否有漏气现象");
                Delay(1);

                #endregion

                #region 第三阶段：稳定运行采集数据

                List<double> listFlow = new List<double>();     // 流量采集
                int elapsed = 0;

                while (elapsed < testTime && IsTesting)
                {
                    elapsed++;
                    //TxtTiming(testTime - elapsed);
                    TxtTips("性能试验中，请手动调整伺服阀开口大小，稳定排气压力在" + targetPressure + "±20KPa内，剩余时间：" + (testTime - elapsed) + " 秒");

                    // 实时监控DI点
                    //if (!CheckMonitorDI()) return false;

                    // 排气压力在目标值±20范围内时采集流量
                    if (Common.AIgrp[AI_Pressure] > targetPressure - 20 && Common.AIgrp[AI_Pressure] < targetPressure + 20)
                    {
                        listFlow.Add(Common.liuliangRst);
                    }

                    Delay(1);
                }

                if (!IsTesting) return false;

                ShowConfirmAndWrite("WGZD", "性能试验\n观察干燥器控制器无故障信号(L13指示灯亮)");
                Delay(1);

                ShowInputAndWrite("JCJRQ", "性能试验\n手动将干燥器内的温度开关短接，检查加热器是否接通。测试辅助电路的吸收电流：");
                //ShowConfirmAndWrite("JCJRQ", "性能试验\n手动将干燥器内的温度开关短接，检查加热器是否接通。测试辅助电路的吸收电流，判定标准：I＞0.9A");
                Delay(1);

                // 露点测试手动
                ShowInputAndWrite("CSLD", "请手动输入测试的露点值：");

                #endregion

                #region 第四阶段：记录结果

                double avgFlow = listFlow.Count > 0 ? listFlow.Average() : 0;

                // 写入 dicReport（key 与 Excel 模板命名单元格对应）
                Common.mResultAll.dicReport["HJSD"] = Common.WSDgrp[1].ToString("f1");          // 环境湿度
                Common.mResultAll.dicReport["DQYL"] = Common.AIgrp[0].ToString("f1");           // 大气压力
                Common.mResultAll.dicReport["HJWD"] = Common.WSDgrp[0].ToString("f1");          // 环境温度
                Common.mResultAll.dicReport["PQCWD"] = Common.Subjectsgrp[0].ToString("f1");    // 排气侧温度(排气温度)
                Common.mResultAll.dicReport["CSLL"] = avgFlow.ToString("f2");                   // 测试流量（流量均值）

                Common.mResultAll.dicReport["GLDY01"] = Common.powerReadgrp[4].ToString("f1");     // A相相电压
                Common.mResultAll.dicReport["GLDY02"] = Common.powerReadgrp[5].ToString("f1");     // B相相电压
                Common.mResultAll.dicReport["GLDY03"] = Common.powerReadgrp[6].ToString("f1");    // C相相电压
                Common.mResultAll.dicReport["GLDL01"] = Common.powerReadgrp[0].ToString("f1");     // A相电流
                Common.mResultAll.dicReport["GLDL02"] = Common.powerReadgrp[1].ToString("f1");     // B相电流
                Common.mResultAll.dicReport["GLDL03"] = Common.powerReadgrp[2].ToString("f1");     // C相电流
                Common.mResultAll.dicReport["CSGL01"] = Common.powerReadgrp[15].ToString("f1");     // A有功功率
                Common.mResultAll.dicReport["CSGL02"] = Common.powerReadgrp[15].ToString("f1");     // B有功功率
                Common.mResultAll.dicReport["CSGL03"] = Common.powerReadgrp[15].ToString("f1");     // C有功功率

                #endregion



                TestStatus(false);
                TxtTips("性能试验结束");
                return true;
            }
            catch (Exception ex)
            {
                Common.DOgrp[5] = false;  // 风源供电
                Common.DOgrp[17] = false; // 110V允许
                TxtTips("性能试验失败，原因：" + ex.Message);
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


        /// <summary>
        /// 监控DI的描述（用于提示）
        /// </summary>
        private string monitorDIName = "被试品故障信号(L13)";

        /// <summary>
        /// 实时监控DI点，信号断开返回false（试验终止）
        /// </summary>
        private bool CheckMonitorDI()
        {
            // DI信号为false时认为被试品异常
            if (Common.DIgrp[17] == false)
            {
                Common.DOgrp[2] = false;
                Common.AOgrp.ServoValve = 0;
                ShowInfo("【" + monitorDIName + "】信号断开，被试品可能存在故障，试验终止！\n请检查被试品状态。");
                TxtTips("性能试验异常终止：" + monitorDIName + "信号断开");
                TestStatus(false);
                return false;
            }
            return true;
        }
    }
}