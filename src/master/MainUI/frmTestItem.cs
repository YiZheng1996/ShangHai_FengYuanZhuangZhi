using MainUI.BLL;
using MainUI.Config;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmTestItem : Form
    {
        public frmTestItem()
        {
            InitializeComponent();
        }
        TestItemsBLL Itembll = new TestItemsBLL(Var.Database, Var.ConnectionString, "TestItems");
        TestProcBLL procbll = new TestProcBLL(Var.Database, Var.ConnectionString, "TestProc");


        //调整：用于AO;检测：用于AI或DI;打开、关闭：用于DO
        List<string> lstCaoZuo = new List<string>
        {
            "等待",
            "调整",
            "打开",
            "关闭",
            "检测",
            "确认弹窗",
            "提示弹窗",
            "输入弹窗",
            "提示",
            "计时开始",
            "计时结束",
            "采值"
        };
        List<string> lstPanDuan = new List<string>
        {
            "等于",
            "大于",
            "小于",
            "上升沿",
            "下降沿"

        };
        List<string> lstDianWei = new List<string>();
        System.Data.DataTable dtItems = new System.Data.DataTable();
        private void btnGet_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog(this);
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            //根据选择的型号查询该型号的测试项点
            dtItems = Itembll.GetTestItems(Common.mTestViewModel.ModelID);
            uiTreeView1.Nodes.Clear();
            foreach (DataRow item in dtItems.Rows)
            {
                TreeNode node = new TreeNode(item["ItemName"].ToString());
                node.Tag = item["ID"];
                uiTreeView1.Nodes.Add(node);
            }
            //绑定表格下拉框
            DataGridViewComboBoxColumn cbx1 = uiDataGridView1.Columns["Operate"] as DataGridViewComboBoxColumn;
            cbx1.DataSource = lstCaoZuo;
            DataGridViewComboBoxColumn cbx2 = uiDataGridView1.Columns["Decide"] as DataGridViewComboBoxColumn;
            cbx2.DataSource = lstPanDuan;
            ParaConfig paraconfig = new ParaConfig();
            paraconfig.SetSectionName(Common.mTestViewModel.ModelID.ToString());
            paraconfig.Load();
            lstDianWei.Clear();
            lstDianWei.Add("无点位");
            lstDianWei.Add("时间");
            lstDianWei.Add("风源供电");
            lstDianWei.Add("伺服阀开度");
            lstDianWei.Add("喷嘴流量");
            lstDianWei.Add("吸气温度");
            lstDianWei.Add("排气温度");
            lstDianWei.Add("环境温度");
            foreach (var item in paraconfig.listAIPoint)
            {
                if (item.isStartUsing == true)
                {
                    lstDianWei.Add(item.ProductName);
                }
            }
            foreach (var item in paraconfig.listDIPoint)
            {
                if (item.isStartUsing == true)
                {
                    lstDianWei.Add(item.ProductName);
                }
            }
            foreach (var item in paraconfig.listDOPoint)
            {
                if (item.isStartUsing == true)
                {
                    lstDianWei.Add(item.ProductName);
                }
            }
            lstDianWei.Add("平均线电压");
            lstDianWei.Add("平均相电压");
            lstDianWei.Add("平均电流");
            lstDianWei.Add("频率");
            lstDianWei.Add("总有功功率");
            lstDianWei.Add("总无功功率");
            lstDianWei.Add("总视在功率");
            lstDianWei.Add("功率因数");
            lstDianWei.Add("轴功率");
            DataGridViewComboBoxColumn cbx3 = uiDataGridView1.Columns["Point"] as DataGridViewComboBoxColumn;
            cbx3.DataSource = lstDianWei;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int TestItemId = uiTreeView1.SelectedNode.Tag.ToInt();
                procbll.DeleteByTestItemId(TestItemId);
                int Serial = 0;
                string Operate = "";
                string Point = "";
                string Decide = "";
                string TestValue = "";
                for (int i = 0; i < uiDataGridView1.Rows.Count - 1; i++)
                {
                    Serial = uiDataGridView1.Rows[i].Cells["Serial"].Value.ToInt();
                    Operate = uiDataGridView1.Rows[i].Cells["Operate"].Value.ToString();
                    Point = uiDataGridView1.Rows[i].Cells["Point"].Value.ToString();
                    Decide = uiDataGridView1.Rows[i].Cells["Decide"].Value.ToString();
                    TestValue = uiDataGridView1.Rows[i].Cells["TestValue"].Value.ToString();
                    if (Point != null)
                    {
                        procbll.Add(TestItemId, Serial, Operate, Point, Decide, TestValue);
                    }
                }
                Var.MessageInfo("保存成功");
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        System.Data.DataTable dtProc = new System.Data.DataTable();
        private void uiTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode tr = uiTreeView1.SelectedNode;
                dtProc = procbll.GetTestProcAll(tr.Tag.ToInt());
                uiDataGridView1.DataSource = dtProc;
            }
            catch (Exception ex)
            {

                Var.MessageError(ex.Message);
            }
        }

        private void frmTestItem_Load(object sender, EventArgs e)
        {

        }

        private void tsmAdd_Click(object sender, EventArgs e)
        {
            try
            {

                int nSelectRow = uiDataGridView1.SelectedIndex;
                System.Data.DataTable dt = uiDataGridView1.DataSource as System.Data.DataTable;
                DataRow dr = dt.NewRow();
                dr["TestItemId"] = dt.Rows[nSelectRow]["TestItemId"].ToInt();
                dr["Serial"] = 0;
                dr["Operate"] = "";
                dr["Point"] = "";
                dr["Decide"] = "";
                dr["TestValue"] = "";
                dt.Rows.InsertAt(dr, nSelectRow);
                uiDataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void tsmDel_Click(object sender, EventArgs e)
        {
            if (!Var.MessageOKCancel("请确认是否删除该行"))
            {
                return;
            }
            int nSelectRow = uiDataGridView1.SelectedIndex;
            System.Data.DataTable dt = uiDataGridView1.DataSource as System.Data.DataTable;
            dt.Rows.RemoveAt(nSelectRow);
            uiDataGridView1.DataSource = dt;
        }
        List<RowItem> lstRowItemAsc = new List<RowItem>();

        private void tsmCopy_Click(object sender, EventArgs e)
        {
            try
            {
                lstRowItemAsc.Clear();
                List<RowItem> lstRowItem = new List<RowItem>();
                foreach (DataGridViewRow item in uiDataGridView1.SelectedRows)
                {
                    if (!item.IsNewRow)
                    {
                        RowItem ri = new RowItem();
                        ri.Serial = item.Cells["Serial"].Value?.ToString() ?? "";
                        ri.Operate = item.Cells["Operate"].Value?.ToString() ?? "";
                        ri.Point = item.Cells["Point"].Value?.ToString() ?? "";
                        ri.Decide = item.Cells["Decide"].Value?.ToString() ?? "";
                        ri.TestValue = item.Cells["TestValue"].Value?.ToString() ?? "";
                        lstRowItem.Add(ri);
                    }
                }
                lstRowItemAsc = lstRowItem.OrderByDescending(x => x.Serial).ToList();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void tsmStickup_Click(object sender, EventArgs e)
        {
            try
            {
                int nSelectRow = uiDataGridView1.SelectedIndex;
                if (nSelectRow == -1)
                {
                    nSelectRow = 0;
                }
                System.Data.DataTable dt = uiDataGridView1.DataSource as System.Data.DataTable;
                for (int i = 0; i < lstRowItemAsc.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    // dr["TestItemId"] = dt.Rows[nSelectRow]["TestItemId"].ToInt();
                    dr["Serial"] = lstRowItemAsc[i].Serial;
                    dr["Operate"] = lstRowItemAsc[i].Operate;
                    dr["Point"] = lstRowItemAsc[i].Point;
                    dr["Decide"] = lstRowItemAsc[i].Decide;
                    dr["TestValue"] = lstRowItemAsc[i].TestValue;
                    dt.Rows.InsertAt(dr, nSelectRow);
                }
                uiDataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// Excel导入
        /// </summary>
        private void btnToLead_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel文件 (*.xls;*.xlsx)|*.xls;*.xlsx";
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            System.Data.DataTable dt = ExcelToDataTable(openFileDialog.FileName, null, true);
                            System.Data.DataTable dt2 = uiDataGridView1.DataSource as System.Data.DataTable;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow dr = dt2.NewRow();
                                dr["Serial"] = dt.Rows[i][0];
                                dr["Operate"] = dt.Rows[i][1];
                                dr["Point"] = dt.Rows[i][2];
                                dr["Decide"] = dt.Rows[i][3];
                                dr["TestValue"] = dt.Rows[i][4];
                                dt2.Rows.Add(dr);
                            }

                            uiDataGridView1.DataSource = dt2;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"导入失败：{ex.Message}", "错误");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// 使用OleDb导入Excel到DataTable
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="sheetName">工作表名称（如"Sheet1"），传null则读取第一个工作表</param>
        /// <param name="hasHeader">是否包含表头</param>
        /// <returns>DataTable</returns>
        public static System.Data.DataTable ExcelToDataTable(string filePath, string sheetName = null, bool hasHeader = true)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel文件不存在", filePath);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            string connectionString = string.Empty;
            string fileExt = Path.GetExtension(filePath).ToLower();

            // 根据文件格式构建连接字符串
            if (fileExt == ".xlsx")
            {
                // Office 2007+ 格式，需要安装Access Database Engine
                connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR={(hasHeader ? "YES" : "NO")}'";
            }
            else if (fileExt == ".xls")
            {
                // Office 2003 格式
                connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR={(hasHeader ? "YES" : "NO")}'";
            }
            else
            {
                throw new ArgumentException("仅支持.xls和.xlsx格式的Excel文件");
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 如果未指定工作表名称，获取第一个工作表
                    if (string.IsNullOrEmpty(sheetName))
                    {
                        System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString();
                    }

                    // 读取数据
                    string sql = $"SELECT * FROM [{sheetName}]";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"读取Excel失败：{ex.Message}，请检查是否安装Microsoft Access Database Engine", ex);
                }
            }

            return dt;
        }
        /// <summary>
        /// Excel导出
        /// </summary>
        private void btnToOut_Click(object sender, EventArgs e)
        {
            if (uiDataGridView1.Rows.Count == 0)
            {
                Var.MessageInfo("DataGridView中没有数据可导出！");
                return;
            }

            // 创建Excel应用程序对象
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook workbook = null;
            Worksheet worksheet = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = false; // 不显示Excel窗口
                workbook = excelApp.Workbooks.Add();
                worksheet = workbook.Worksheets[1] as Worksheet;
                worksheet.Name = "Sheet1";

                // 写入表头
                for (int i = 0; i < uiDataGridView1.Columns.Count; i++)
                {
                    if (i < 2)
                    {
                        continue;
                    }
                    worksheet.Cells[1, i - 1] = uiDataGridView1.Columns[i].HeaderText;
                    worksheet.Cells[1, i - 1].Font.Bold = true;
                }

                // 写入数据
                for (int row = 0; row < uiDataGridView1.Rows.Count; row++)
                {
                    if (uiDataGridView1.Rows[row].IsNewRow) continue;

                    for (int col = 0; col < uiDataGridView1.Columns.Count; col++)
                    {
                        if (col < 2)
                        {
                            continue;
                        }
                        worksheet.Cells[row + 2, col - 1] = uiDataGridView1.Rows[row].Cells[col].Value?.ToString() ?? "";
                    }
                }

                // 自动调整列宽
                worksheet.UsedRange.EntireColumn.AutoFit();

                // 保存文件
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel文件 (*.xlsx)|*.xlsx",
                    Title = "保存Excel文件",
                    FileName = uiTreeView1.SelectedNode.Text
                };

                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    Var.MessageInfo("数据导出成功！");
                }
            }
            catch (Exception ex)
            {
                Var.MessageError($"导出失败：{ex.Message}");
            }
            finally
            {
                // 释放COM对象，防止Excel进程残留
                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
                worksheet = null;
                workbook = null;
                excelApp = null;
            }

        }
    }
    public class RowItem
    {

        public string Serial { get; set; }
        public string Operate { get; set; }
        public string Point { get; set; }
        public string Decide { get; set; }
        public string TestValue { get; set; }
    }


}
