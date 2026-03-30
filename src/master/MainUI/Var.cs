using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Modules;
using RW.Driver;
using RW.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using RW.Data;
using MainUI.Config;
using Sunny.UI;

namespace MainUI
{
    public static class Var
    {
        public static string SoftName = "AGTU-0.9风源装置试验台";
        public static OPCDriver opcDOGroup = new OPCDriver();
        public static OPCDriver opcDIGroup = new OPCDriver();
        public static OPCDriver opcAOGroup = new OPCDriver();
        public static OPCDriver opcAIGroup = new OPCDriver();
        public static OPCDriver opcStatus = new OPCDriver();
        public static OPCDriver opcNozzle = new OPCDriver();
        public static OPCDriver opcSubjects = new OPCDriver();
        public static OPCDriver opcWsd = new OPCDriver();
        public static OPCDriver opcPowerControl = new OPCDriver();
        public static OPCDriver opcPowerRead = new OPCDriver();
        public static string rootRptSave = Application.StartupPath + "\\save";  //报表保存路径
        public static string ConnectionString = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\db.mdb;jet oledb:database password=ok";  //数据库连接字符串
        public static OleDB Database = new OleDB();
        /// <summary>
        /// 创建OPC连接地址
        /// </summary>
        static Var()
        {
            string kepServerName = "KEPware.KEPServerEx.V4";

            opcDOGroup.ServerName = kepServerName;
            opcDOGroup.Prefix = "PLC.S7.";

            opcDIGroup.ServerName = kepServerName;
            opcDIGroup.Prefix = "PLC.S7.";

            opcAIGroup.ServerName = kepServerName;
            opcAIGroup.Prefix = "PLC.S7.";

            opcAOGroup.ServerName = kepServerName;
            opcAOGroup.Prefix = "PLC.S7.";

            opcStatus.ServerName = kepServerName;
            opcStatus.Prefix = "PLC.S7.";

            opcPowerControl.ServerName = kepServerName;
            opcPowerControl.Prefix = "temp1.PowerControl.";

            opcPowerRead.ServerName = kepServerName;
            opcPowerRead.Prefix = "temp1.PowerRead.";

            opcSubjects.ServerName = kepServerName;
            opcSubjects.Prefix = "temp2.subjects.";

            opcWsd.ServerName = kepServerName;
            opcWsd.Prefix = "temp2.wsd.";

            opcNozzle.ServerName = kepServerName;
            opcNozzle.Prefix = "temp4.nozzle.";

        }
        /// <summary>
        /// OPC打开
        /// </summary>
        public static void Connect()
        {
            opcDOGroup.Connect();
            opcDIGroup.Connect();
            opcAIGroup.Connect();
            opcAOGroup.Connect();
            opcStatus.Connect();
            opcNozzle.Connect();
            opcSubjects.Connect();
            opcWsd.Connect();
            opcPowerControl.Connect();
            opcPowerRead.Connect();

        }
        /// <summary>
        /// OPC关闭
        /// </summary>
        public static void Close()
        {
            opcDOGroup.Close();
            opcDIGroup.Close();
            opcAIGroup.Close();
            opcAOGroup.Close();
            opcStatus.Close();
            opcNozzle.Close();
            opcSubjects.Close();
            opcWsd.Close();
            opcPowerControl.Close();
            opcPowerRead.Close();
        }
        //c流量系数，D喷嘴直径（m），tx吸气温度（k），tp喷嘴前温度（k），pb大气压力（kpa）,pp喷嘴压差（pa）
        /// <summary>
        /// 流量计算
        /// </summary>
        /// <param name="c">流量系数</param>
        /// <param name="zj">喷嘴直径（m）</param>
        /// <param name="tx">吸气温度（℃）</param>
        /// <param name="tp">tp喷嘴前温度（℃）</param>
        /// <param name="pb">pb大气压力（kPa）</param>
        /// <param name="pp">pp喷嘴压差（Pa）</param>
        /// <returns></returns>
        public static double Flux(double c, double zj, double tx, double tp, double pb, double pp)
        {
            double liuliangRst;
            liuliangRst = 18.82 * c * 60.0 * (zj / 1000.00) * (zj / 1000.00) * (273.15 + tx) * Math.Sqrt((pp * 1.00) / ((273.15 + tp) * pb * 1000.00));
            liuliangRst = Math.Round(liuliangRst, 4);
            return liuliangRst;
        }
        /// <summary>
        /// 量程转换
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputL">4</param>
        /// <param name="inputH">20</param>
        /// <param name="outL">0</param>
        /// <param name="outH">1000</param>
        /// <returns></returns>
        public static double AIAO_Convert(double input, double inputL, double inputH, double outL, double outH)
        {
            double rst = (outH - outL) * (input - inputL) / (inputH - inputL) + outL;
            rst = Math.Round(rst, 3);
            return rst;
        }

        public static bool ToBool(this object obj, bool ret = false)
        {
            Boolean.TryParse(obj + "", out ret);
            return ret;
        }
        public static int ToInt(this object obj, int ret = 0)
        {
            int.TryParse(obj + "", out ret);
            return ret;
        }
        public static double ToDouble(this object obj, double ret = 0)
        {
            double.TryParse(obj + "", out ret);
            return ret;
        }
        public static decimal ToDecimal(this object obj, decimal ret = 0)
        {
            decimal.TryParse(obj + "", out ret);
            return ret;
        }
        static UIPage uiPage = new UIPage();
        public static void MessageInfo(string sTemp)
        {
            uiPage.ShowInfoDialog("提示", sTemp, UIStyle.Blue, true);
        }
        public static bool MessageOKCancel(string sTemp)
        {
            bool temp = false;
            if (sTemp.IndexOf("\\n") >= 0)
            {
                string[] array = sTemp.Split('\\');
                sTemp = "";
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = array[i].Replace('n', ' ');
                    sTemp += array[i] + Environment.NewLine;
                }
            }
            temp = uiPage.ShowAskDialog("提示", sTemp, UIStyle.Blue, true);
            return temp;
        }
        public static void MessageError(string sTemp)
        {
            uiPage.ShowInfoDialog("提示", sTemp, UIStyle.Red, true);
        }
        public static int MessageInput(string sTemp)
        {
            frmSetOutValue frmsetAir = new frmSetOutValue(0, sTemp, 99999, 0);
            DialogResult diaRst = frmsetAir.ShowDialog();
            if (diaRst == DialogResult.OK)
            {

                return Convert.ToInt32(frmsetAir.OutValue);

            }
            else
            {
                return 0;
            }
        }
    }
}
