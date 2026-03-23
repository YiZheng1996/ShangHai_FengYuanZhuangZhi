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

namespace MainUI
{
    public partial class frmChangePwd : Form
    {
        public frmChangePwd()
        {
            InitializeComponent();
        }


        private void frmChangePwd_Load(object sender, EventArgs e)
        {
            this.txtUsername.Text = RW.UI.RWUser.User.Username;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string password = this.txtPassword.Text;
            string newPwd1 = this.txtNewPwd1.Text;
            string newPwd2 = this.txtNewPwd2.Text;


            if (string.IsNullOrEmpty(password))
            {
                Var.MessageInfo("密码不能为空，请重新输入！");
                this.txtPassword.Focus();
                return;
            }
            if (string.IsNullOrEmpty(newPwd1))
            {
                Var.MessageInfo("新密码不能为空，请重新输入");
                this.txtNewPwd1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(newPwd2))
            {
                Var.MessageInfo("确认密码不能为空，请重新输入！");
                this.txtNewPwd2.Focus();
                return;
            }
            if (newPwd1 != newPwd2)
            {
                Var.MessageInfo("两次输入的密码不正确，请重新输入！");
                this.txtNewPwd1.Focus();
                return;
            }
            if (password != RW.UI.RWUser.User.Password)
            {
                Var.MessageInfo("原始密码不正确，请重新输入！");
                this.txtPassword.Focus();
                return;
            }

            UserBLL bllUser = new UserBLL();
            if (bllUser.ChangePwd(RW.UI.RWUser.User.Username, newPwd1))
            {
                Var.MessageInfo("密码修改成功，重新登录后即可生效！");
                RW.UI.RWUser.User.Password = newPwd1;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Var.MessageInfo("修改失败！");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}