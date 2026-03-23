using MainUI.BLL;
using MainUI.Config;
using Microsoft.Office.Interop.Excel;
using Sunny.UI;
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
    public partial class frmMetering : Form
    {
        private DateTimePicker dTimePicker = new DateTimePicker();
        public frmMetering()
        {
            InitializeComponent();

        }

        DateTimePicker dtp;
        MeasureBLL bll = new MeasureBLL(Var.Database, Var.ConnectionString, "Measure");
        //List<string> lstSignalType = new List<string>
        //{
        //    "4~20mA",
        //    "0~5V",
        //    "0~10V",
        //};
        private void frmTestItem_Load(object sender, EventArgs e)
        {
            dtp = new DateTimePicker();
            dtp.Leave += new EventHandler(dtp_Leave);
            dtp.VisibleChanged += new EventHandler(dtp_visiblechanged);
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "yyyy-MM-dd";
            dtp.Value = DateTime.Now;
            dtp.Hide();
            dtp.Parent = uiDataGridView1;
            //绑定表格下拉框
            //DataGridViewComboBoxColumn cbx1 = uiDataGridView1.Columns["SignalType"] as DataGridViewComboBoxColumn;
            //cbx1.DataSource = lstSignalType;
            System.Data.DataTable dt = bll.GetMeasure();
            uiDataGridView1.DataSource = dt;
        }
        private void uiDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (uiDataGridView1.CurrentCell.ColumnIndex == uiDataGridView1.Columns["MeasTime"].Index ||
                    uiDataGridView1.CurrentCell.ColumnIndex == uiDataGridView1.Columns["ValidTime"].Index)
                {
                    dtp.Location = uiDataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location;
                    dtp.Size = uiDataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Size;
                    if (uiDataGridView1[e.ColumnIndex, e.RowIndex].Value != DBNull.Value)
                    {
                        dtp.Value = Convert.ToDateTime(uiDataGridView1[e.ColumnIndex, e.RowIndex].Value);
                    }
                    dtp.Tag = uiDataGridView1.CurrentCell;
                    dtp.Show();
                    dtp.Focus();
                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);

            }
        }
        private void dtp_Leave(object sender, EventArgs e)
        {
            dtp.Hide();//焦点从日期控件离开时隐藏
        }


        private void dtp_visiblechanged(object sender, EventArgs e)
        {
            //改变日期时将日期赋值给单元格
            if (dtp.Tag != null)
            {
                DataGridViewCell cell = (DataGridViewCell)dtp.Tag;
                if (dtp.Value == dtp.MinDate)
                    cell.Value = DBNull.Value;
                else
                    cell.Value = dtp.Value.ToString("yyyy-MM-dd");
            }
            if (dtp.Visible == false)
                uiDataGridView1.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                MeasureModel mod = new MeasureModel();
                bll.Delete();
                for (int i = 0; i < uiDataGridView1.Rows.Count - 1; i++)
                {
                    mod.Number = uiDataGridView1.Rows[i].Cells["Number"].Value.ToString();
                    mod.SignalType = uiDataGridView1.Rows[i].Cells["SignalType"].Value.ToString();
                    mod.SensorType = uiDataGridView1.Rows[i].Cells["SensorType"].Value.ToString();
                    mod.SignalMin = uiDataGridView1.Rows[i].Cells["SignalMin"].Value.ToString();
                    mod.SignalMax = uiDataGridView1.Rows[i].Cells["SignalMax"].Value.ToString();
                    mod.MeasTime = uiDataGridView1.Rows[i].Cells["MeasTime"].Value.ToString();
                    mod.ValidTime = uiDataGridView1.Rows[i].Cells["ValidTime"].Value.ToString();
                    mod.MeasNumber = uiDataGridView1.Rows[i].Cells["MeasNumber"].Value.ToString();
                    if (!string.IsNullOrEmpty( mod.Number))
                    {
                        bll.Add(mod);
                    }
                }
                Var.MessageInfo("保存成功");
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void tsmAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int nSelectRow = uiDataGridView1.SelectedIndex;
                System.Data.DataTable dt = uiDataGridView1.DataSource as System.Data.DataTable;
                DataRow dr = dt.NewRow();
                dr["Number"] = "";
                dr["SignalType"] = "";
                dr["SensorType"] = "";
                dr["SignalMin"] = "";
                dr["SignalMax"] = "";
                dr["MeasTime"] = "";
                dr["ValidTime"] = "";
                dr["MeasNumber"] = "";
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
            try
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
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
    }
}
