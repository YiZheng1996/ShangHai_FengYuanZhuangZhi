using MainUI.BLL;
using MainUI.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainUI
{
    public partial class frmOverload : Form
    {
        ParaConfig para;
        public int PS2 { get; set; }
        public int PS3 { get; set; }
        public int Over { get; set; }
        public int Temp { get; set; }
        public frmOverload()
        {
            InitializeComponent();
        }

        public frmOverload(ParaConfig paraconfig)
        {
            InitializeComponent();
            this.para = paraconfig;
        }
        List<DIPoint> list = new List<DIPoint>();
        private void frmOverload_Load(object sender, EventArgs e)
        {
            foreach (var item in para.listDIPoint)
            {
                if (item.isStartUsing)
                {
                    list.Add(item);
                }
            }
            cboPS2.DataSource = list;
            cboPS2.ValueMember = "Index";
            cboPS2.DisplayMember = "ProductName";
            cboPS3.DataSource = list;
            cboPS3.ValueMember = "Index";
            cboPS3.DisplayMember = "ProductName";
            cboOverLoad.DataSource = list;
            cboOverLoad.ValueMember = "Index";
            cboOverLoad.DisplayMember = "ProductName";
            cboTemp.DataSource = list;
            cboTemp.ValueMember = "Index";
            cboTemp.DisplayMember = "ProductName";
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            PS2 = cboPS2.SelectedValue.ToInt();
            PS3 = cboPS3.SelectedValue.ToInt();
            Over = cboOverLoad.SelectedValue.ToInt();
            Temp = cboTemp.SelectedValue.ToInt();
            this.DialogResult = DialogResult.OK;
            this.Close();


        }
    }
}
