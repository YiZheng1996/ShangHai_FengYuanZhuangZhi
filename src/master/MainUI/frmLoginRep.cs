using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.Driver;
using RW.UI.Manager.User;
using RW.UI.BLL;
using System.IO;

namespace MainUI
{
    public partial class frmLoginRep : Form
    {
        public frmLoginRep()
        {
            InitializeComponent();
        }


        private void frmChangePwd_Load(object sender, EventArgs e)
        {
            try
            {
                // 目标路径（替换为你的路径，支持绝对路径/相对路径）
                string targetPath = Application.StartupPath + "\\save";

                // 校验路径是否存在
                if (!Directory.Exists(targetPath))
                {
                    Var.MessageInfo($"路径不存在：{targetPath}");
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Report");
                dt.Columns.Add("filePath");
                // 筛选.xlsx和.xls格式（可追加其他Excel扩展名）
                string[] excelExtensions = { "*.xlsx", "*.xls", "*.xlsm" };
                foreach (var ext in excelExtensions)
                {
                    string[] excelFiles = Directory.GetFiles(targetPath, ext);
                    foreach (string filePath in excelFiles)
                    {

                        // 输出文件名称
                        string filename = filePath.Substring(filePath.IndexOf("save") + 5);
                        if (string.IsNullOrEmpty(Common.mTestViewModel.ModelName))
                        {
                            Var.MessageInfo("请选择型号后再载入");
                            this.Close();
                            return;
                        }
                        if (filename.IndexOf(Common.mTestViewModel.ModelName) >= 0)
                        {
                            if (string.IsNullOrEmpty(Common.mTestViewModel.TestNO))
                            {
                                Var.MessageInfo("请输入编号后再载入");
                                this.Close();
                                return;
                            }
                            if (filename.IndexOf(Common.mTestViewModel.TestNO) >= 0)
                            {
                                // 输出文件完整路径
                                DataRow dr = dt.NewRow();
                                dr["Report"] = filename;
                                dr["filePath"] = filePath;
                                dt.Rows.Add(dr);
                            }

                        }
                    }
                }
                uiDataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            sPath = Var.rootRptSave + "\\" + Common.mTestViewModel.ModelName + "_" + Common.mTestViewModel.TestNO + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".xls";
            this.Close();
        }

        public string sPath { get; set; }
        /// <summary>
        /// 载入
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            sPath = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["filePath"].Value.ToString();
            this.Close();
        }

        private void uiDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            sPath = uiDataGridView1.Rows[uiDataGridView1.CurrentRow.Index].Cells["filePath"].Value.ToString();
            frmDispReport frm = new frmDispReport(sPath);
            frm.ShowDialog();
        }
    }
}