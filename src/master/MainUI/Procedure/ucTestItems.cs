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
    public partial class ucTestItems : ucBaseManagerUI
    {
        TestItemsBLL bll = new TestItemsBLL(Var.Database, Var.ConnectionString, "TestItems");
        public ucTestItems()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSerial.Text.ToInt() <= 0)
            {
                Var.MessageInfo("请输入序号");
                return;
            }
            if (string.IsNullOrEmpty(txtItemName.Text.Trim()))
            {
                Var.MessageInfo("请输入测试名称");
                return;
            }
            if (string.IsNullOrEmpty(txtSavePath.Text.Trim()))
            {
                Var.MessageInfo("请输入数据存放位置");
                return;
            }
            if (string.IsNullOrEmpty(txtModel.Text.Trim()))
            {
                Var.MessageInfo("请选择产品型号");
                return;
            }

            if (bll.Add(txtSerial.Text.ToInt(), txtItemName.Text.Trim(), txtSavePath.Text.Trim(), Common.mTestViewModel.ModelID))
            {
                Var.MessageInfo("新增成功");
                LoadModel();
            }
            else
            {
                Var.MessageInfo("新增失败");
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            LoadModel();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null)
                {
                    Var.MessageInfo("请选择需要删除的行");
                    return;
                }
                string sTemp = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ItemName"].Value.ToString();
                if (!Var.MessageOKCancel("请确认，是否删除测试项点【" + sTemp + "】"))
                {
                    return;
                }
                if (nID < 0)
                    return;

                if (bll.Delete(nID))
                {
                    Var.MessageInfo("删除成功");
                    LoadModel();
                }
                else
                {
                    Var.MessageInfo("删除失败");
                    LoadModel();

                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        int nID = -1;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string id = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id"].Value.ToString();
                string Serial = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["Serial"].Value.ToString();
                string ItemName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ItemName"].Value.ToString();
                string SavePath = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["SavePath"].Value.ToString();
                nID = id.ToInt();
                txtSerial.Text = Serial;
                txtItemName.Text = ItemName;
                txtSavePath.Text = SavePath;
            }
            catch (Exception ex)
            {
                //Var.MessageError(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSerial.Text.ToInt() <= 0)
                {
                    Var.MessageInfo("请输入序号");
                    return;
                }
                if (string.IsNullOrEmpty(txtItemName.Text.Trim()))
                {
                    Var.MessageInfo("请输入测试名称");
                    return;
                }
                if (string.IsNullOrEmpty(txtSavePath.Text.Trim()))
                {
                    Var.MessageInfo("请输入数据存放位置");
                    return;
                }
                if (string.IsNullOrEmpty(txtModel.Text.Trim()))
                {
                    Var.MessageInfo("请选择产品型号");
                    return;
                }
                if (bll.Update(nID, txtSerial.Text.ToInt(), txtItemName.Text.Trim(), txtSavePath.Text.Trim(), Common.mTestViewModel.ModelID))
                {

                    Var.MessageInfo("修改成功");

                    LoadModel();
                }
                else
                {
                    Var.MessageInfo("修改失败");

                }
            }
            catch (Exception)
            {
                Var.MessageError("发生错误，新增失败");
            }
        }

        private void LoadModel()
        {
            try
            {
                DataTable dt = bll.GetTestItems(Common.mTestViewModel.ModelID);
                //if (dt.Rows.Count == 0)
                //{
                //    return;
                //}
                dataGridView1.DataSource = dt;
                dataGridView1.Sort(dataGridView1.Columns["Serial"], ListSortDirection.Ascending);
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        DataTable dtTemp = new DataTable();
        private void btnCopy_Click(object sender, EventArgs e)
        {

            DataColumn dc = new DataColumn("Serial");
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("ItemName");
            dtTemp.Columns.Add(dc);
            dc = new DataColumn("SavePath");
            dtTemp.Columns.Add(dc);

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string Serial = dataGridView1.Rows[i].Cells["Serial"].Value.ToString();
                string ItemName = dataGridView1.Rows[i].Cells["ItemName"].Value.ToString();
                string SavePath = dataGridView1.Rows[i].Cells["SavePath"].Value.ToString();
                DataRow dr = dtTemp.NewRow();
                dr["Serial"] = Serial;
                dr["ItemName"] = ItemName;
                dr["SavePath"] = SavePath;
                dtTemp.Rows.Add(dr);
            }
            Var.MessageInfo("复制成功");
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                bll.Add(dtTemp.Rows[i]["Serial"].ToInt(), dtTemp.Rows[i]["ItemName"].ToString(), dtTemp.Rows[i]["SavePath"].ToString(), Common.mTestViewModel.ModelID);

            }
            Var.MessageInfo("粘贴成功");
            LoadModel();

        }
    }
}
