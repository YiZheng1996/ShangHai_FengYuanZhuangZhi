using MainUI.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Model
{
    public class SetReport
    {
        frmReport report = null;
        ParaConfig para = null;
        public SetReport(frmReport report, ParaConfig para)
        {
            this.report = report;
            this.para = para;
        }
        public void SetRep()
        {
            try
            {

                report.Write("GFMK", Common.mTestViewModel.TestNO);     //供风模块
                report.Write("STGZQ", Common.mTestViewModel.ElecNO);     //双塔干燥器
                report.Write("KYJ", Common.mTestViewModel.HeadNO);     //空压机
                report.Write("SYRQ", DateTime.Now.ToString("yyyy-MM-dd HH:mm"));     //试验日期
                report.Write("Username", RW.UI.RWUser.User.Username);     //试验者名
                report.Write("XH", Common.mTestViewModel.ModelName);     //项目名称
                report.Write("BZ", Common.mTestViewModel.Mark);          //备注

                // 新模式遍历
                foreach (var item in Common.mResultAll.dicReport)
                {
                    report.Write(item.Key, item.Value);
                }

                foreach (var item in Common.mResultAll.dic)
                {
                    //拆key，如果需要填写多个单元格将以分号隔开
                    string[] Array = item.Key.Split(';');
                    for (int i = 0; i < Array.Length - 1; i++)
                    {
                        if (i <= item.Value.Count - 1)
                        {
                            report.Write(Array[i], item.Value[i]);
                        }
                    }
                }
                if (Common.mResultAll.listTime.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listTime.Count; i++)
                    {
                        report.Write(2, "A" + (1 + i), Common.mResultAll.listTime[i]);
                    }
                }
                if (Common.mResultAll.listPS2.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listPS2.Count; i++)
                    {
                        report.Write(2, "B" + (1 + i), Convert.ToInt64(Common.mResultAll.listPS2[i]));
                    }
                }
                if (Common.mResultAll.listPS3.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listPS3.Count; i++)
                    {
                        report.Write(2, "C" + (1 + i), Convert.ToInt64(Common.mResultAll.listPS3[i]));
                    }
                }
                if (Common.mResultAll.listPS2_3.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listPS2_3.Count; i++)
                    {
                        report.Write(2, "D" + (1 + i), Convert.ToInt64(Common.mResultAll.listPS2_3[i]));
                    }
                }
                if (Common.mResultAll.listIsTemp.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listIsTemp.Count; i++)
                    {
                        report.Write(2, "E" + (1 + i), Convert.ToInt64(Common.mResultAll.listIsTemp[i]));
                    }
                }
                if (Common.mResultAll.listIsOverload.Count >= 1)
                {
                    for (int i = 1; i < Common.mResultAll.listIsOverload.Count; i++)
                    {
                        report.Write(2, "F" + (1 + i), Convert.ToInt64(Common.mResultAll.listIsOverload[i]));
                    }
                }


            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        double CalculateAverage(double[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("数组不能有空值");
            }

            double sum = 0;
            foreach (double num in array)
            {
                sum += num;
            }

            return sum / array.Length;
        }
    }
}