using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RW.UI.Manager;
using MainUI.Model;
using MainUI.BLL;
using MainUI.Config;
using MainUI.Properties;

namespace MainUI.Procedure
{
    public partial class ucTestParams : ucBaseManagerUI
    {
        ParaConfig paraconfig = new ParaConfig();
        public string ModelID { get; set; }
        public ucTestParams()
        {
            InitializeComponent();
        }
        private void ucTestParams_Load(object sender, EventArgs e)
        {
            //加载喷嘴直径参数
            double[] DiaGrp = new double[] { 3.18, 4.76, 6.35, 9.52, 12.7, 19.05, 25.4, 34.92, 50.80 };
            cboNozzleDiameter.Items.Clear();
            for (int i = 0; i < DiaGrp.Length; i++)
            {
                cboNozzleDiameter.Items.Add(DiaGrp[i]);
            }
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                paraconfig = new ParaConfig();
                paraconfig.SetSectionName(ModelID);
                paraconfig.Load();
                if (chkRetain.Checked)
                {
                    return;
                }
                txtRpt.Text = paraconfig.RptFile;
                nudNozzleXS.Text = paraconfig.Nozzle.ToString();
                cboNozzleDiameter.Text = paraconfig.NozzleDiameter.ToString();
                nudRevise.Text = paraconfig.Revise.ToString();
                nudVolt.Text = paraconfig.Volt.ToString();
                nudFrequency.Text = paraconfig.Frequency.ToString();
                if (paraconfig.isVolt)
                {
                    rbn24.Checked = true;
                }
                else
                {
                    rbn110.Checked = true;
                }

                nudpressure.Text = paraconfig.Pressure.ToString();
                nudMtime.Text = paraconfig.ProvingMTime.ToString();
                nudPercentServeIn.Text = paraconfig.PercentServe.ToString();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// 确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ModelID))
                {
                    Var.MessageInfo("请选择产品后保存");
                    return;
                }
                paraconfig.SetSectionName(ModelID);
                paraconfig.RptFile = txtRpt.Text;
                paraconfig.Nozzle = Convert.ToDouble(nudNozzleXS.Text);
                paraconfig.NozzleDiameter = Convert.ToDouble(cboNozzleDiameter.Text);
                paraconfig.Revise = Convert.ToDouble(nudRevise.Text);
                paraconfig.isVolt = rbn24.Checked;
                paraconfig.Volt = nudVolt.Text.ToInt();
                paraconfig.Frequency = nudFrequency.Text.ToInt();

                paraconfig.Pressure = nudpressure.Text.ToInt();
                paraconfig.ProvingMTime = nudMtime.Text.ToInt();
                paraconfig.PercentServe = nudPercentServeIn.Text.ToDouble();

                paraconfig.Save();
                Var.MessageInfo("保存成功。");
            }
            catch (Exception ex)
            {
                Var.MessageError("保存失败。" + ex.Message);
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ModelID))
                LoadConfig();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Application.StartupPath + "\\reports\\";
            openFile.Filter = "Excel2003|*.xls|Excel2007+|*.xlsx";
            openFile.ShowDialog();
            if (string.IsNullOrEmpty(openFile.FileName) == false)
            {
                string path = openFile.FileName;
                string[] str = path.Split('\\');
                txtRpt.Text = str[str.Length - 1];
            }
        }

        //产品选择
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            ModelID = Common.mTestViewModel.ModelID.ToString();
            LoadConfig();

        }

        private void swhJiTouSwitch_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void swhJiTiSwitch_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
