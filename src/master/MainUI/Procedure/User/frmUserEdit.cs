using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RW.UI.Model;
using RW.UI.BLL;

namespace MainUI.Procedure.User
{
    public partial class frmUserEdit : Form
    {
        public frmUserEdit()
        {
            InitializeComponent();
            InitRadioButton();
        }

        private UserInfo User;
        public frmUserEdit(UserInfo user)
        {
            InitializeComponent();
            InitRadioButton();
            this.User = user;
            this.txtUserName.Text = user.Username;
            this.txtPassword.Text = user.Password;

            string level = user.Permission;
            if (level != null)
                this.comboBox1.SelectedValue = level;
        }

        void InitRadioButton()
        {
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "ID";
            this.comboBox1.DataSource = RW.UI.RWUser.Permissions;

        }
        UserBLL userBll = new UserBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (User == null)
                {
                    User = new UserInfo();
                }
                if (comboBox1.Text == "")
                {
                    Var.MessageInfo("请选择权限");
                    return;
                }
                User.Username = this.txtUserName.Text;
                User.Password = this.txtPassword.Text;
                User.Permission = this.comboBox1.Text;

                int count = userBll.Save(User);

                if (count > 0)
                {
                    Var.MessageInfo("保存成功！");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    Var.MessageInfo("保存失败！");
                }
            }
            catch (Exception ex)
            {
                Var.MessageError("检测到非法字符，请正确录入信息");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}