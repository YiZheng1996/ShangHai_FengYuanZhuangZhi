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
using Sunny.UI;

namespace MainUI.Procedure
{
    public partial class ucTestPoint : ucBaseManagerUI
    {

        ParaConfig paraconfig = new ParaConfig();


        public ucTestPoint()
        {
            InitializeComponent();
        }

        public string ModelID { get; set; }

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
                paraconfig.listAIPoint.Clear();
                paraconfig.listDIPoint.Clear();
                paraconfig.listDOPoint.Clear();
                //DI
                for (int i = 1; i <= 21; i++)
                {
                    DIPoint dipoint = new DIPoint();
                    foreach (var item in tabDI24.Controls)
                    {
                        if (item is UITextBox)
                        {
                            UITextBox txt = item as UITextBox;
                            if (txt.Tag.ToInt() == i)
                            {
                                dipoint.Index = txt.Tag.ToInt();
                                dipoint.ProductName = txt.Text;
                            }
                        }
                        if (item is UILabel)
                        {
                            UILabel lab = item as UILabel;
                            if (lab.Tag.ToInt() == i)
                            {
                                dipoint.TestbedName = lab.Text;
                            }
                        }
                        if (item is UICheckBox)
                        {
                            UICheckBox chk = item as UICheckBox;
                            if (chk.Tag.ToInt() == i)
                            {
                                dipoint.isStartUsing = chk.Checked;
                            }
                        }
                    }
                    foreach (var item in tabDI110.Controls)
                    {
                        if (item is UITextBox)
                        {
                            UITextBox txt = item as UITextBox;
                            if (txt.Tag.ToInt() == i)
                            {
                                dipoint.Index = txt.Tag.ToInt();
                                dipoint.ProductName = txt.Text;
                            }
                        }
                        if (item is UILabel)
                        {
                            UILabel lab = item as UILabel;
                            if (lab.Tag.ToInt() == i)
                            {
                                dipoint.TestbedName = lab.Text;
                            }
                        }
                        if (item is UICheckBox)
                        {
                            UICheckBox chk = item as UICheckBox;
                            if (chk.Tag.ToInt() == i)
                            {
                                dipoint.isStartUsing = chk.Checked;
                            }
                        }
                    }
                    paraconfig.listDIPoint.Add(dipoint);
                }
                //DO
                for (int i = 12; i <= 24; i++)
                {
                    DOPoint dopoint = new DOPoint();
                    foreach (var item in tabDO.Controls)
                    {
                        if (item is UITextBox)
                        {
                            UITextBox txt = item as UITextBox;
                            if (txt.Tag.ToInt() == i)
                            {
                                dopoint.Index = txt.Tag.ToInt();
                                dopoint.ProductName = txt.Text;
                            }
                        }
                        if (item is UILabel)
                        {
                            UILabel lab = item as UILabel;
                            if (lab.Tag.ToInt() == i)
                            {
                                dopoint.TestbedName = lab.Text;
                            }
                        }
                        if (item is UICheckBox)
                        {
                            UICheckBox chk = item as UICheckBox;
                            if (chk.Tag.ToInt() == i)
                            {
                                dopoint.isStartUsing = chk.Checked;
                            }
                        }
                    }
                    paraconfig.listDOPoint.Add(dopoint);
                }
                for (int i = 1; i <= 9; i++)
                {
                    AIPoint aipoint = new AIPoint();
                    foreach (var item in tabAI.Controls)
                    {
                        if (item is UITextBox)
                        {
                            UITextBox txt = item as UITextBox;
                            if (txt.Tag.ToInt() == i)
                            {
                                aipoint.Index = txt.Tag.ToInt();
                                aipoint.ProductName = txt.Text;
                            }
                        }
                        if (item is UILabel)
                        {
                            UILabel lab = item as UILabel;
                            if (lab.Tag.ToInt() == i)
                            {
                                aipoint.TestbedName = lab.Text;
                            }
                        }
                        if (item is UICheckBox)
                        {
                            UICheckBox chk = item as UICheckBox;
                            if (chk.Tag.ToInt() == i)
                            {
                                aipoint.isStartUsing = chk.Checked;
                            }
                        }
                    }
                    paraconfig.listAIPoint.Add(aipoint);
                }

                paraconfig.Save();
                Var.MessageInfo("保存成功。");
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            ModelID = Common.mTestViewModel.ModelID.ToString();
            LoadConfig();
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
                List<AIPoint> lstAIPoint = paraconfig.listAIPoint;
                if (lstAIPoint.Count > 0)
                {
                    for (int i = 0; i < lstAIPoint.Count; i++)
                    {
                        for (int j = 0; j < tabAI.Controls.Count; j++)
                        {
                            if (lstAIPoint[i].Index == tabAI.Controls[j].Tag.ToInt())
                            {
                                if (tabAI.Controls[j] is UITextBox)
                                {
                                    UITextBox txt = tabAI.Controls[j] as UITextBox;
                                    txt.Text = lstAIPoint[i].ProductName;
                                }
                                if (tabAI.Controls[j] is UICheckBox)
                                {
                                    UICheckBox chk = tabAI.Controls[j] as UICheckBox;
                                    chk.Checked = lstAIPoint[i].isStartUsing;
                                }
                            }
                        }

                    }
                }
                else
                {
                    for (int j = 0; j < tabAI.Controls.Count; j++)
                    {
                        if (tabAI.Controls[j] is UITextBox)
                        {
                            UITextBox txt = tabAI.Controls[j] as UITextBox;
                            txt.Text = "";
                        }
                        if (tabAI.Controls[j] is UICheckBox)
                        {
                            UICheckBox chk = tabAI.Controls[j] as UICheckBox;
                            chk.Checked = false;
                        }
                    }
                }

                List<DIPoint> lstDIPoint = paraconfig.listDIPoint;
                if (lstDIPoint.Count > 0)
                {
                    for (int i = 0; i < lstDIPoint.Count; i++)
                    {
                        for (int j = 0; j < tabDI24.Controls.Count; j++)
                        {
                            if (lstDIPoint[i].Index == tabDI24.Controls[j].Tag.ToInt())
                            {
                                if (tabDI24.Controls[j] is UITextBox)
                                {
                                    UITextBox txt = tabDI24.Controls[j] as UITextBox;
                                    txt.Text = lstDIPoint[i].ProductName;
                                }
                                if (tabDI24.Controls[j] is UICheckBox)
                                {
                                    UICheckBox chk = tabDI24.Controls[j] as UICheckBox;
                                    chk.Checked = lstDIPoint[i].isStartUsing;
                                }
                            }
                        }
                        for (int j = 0; j < tabDI110.Controls.Count; j++)
                        {
                            if (lstDIPoint[i].Index == tabDI110.Controls[j].Tag.ToInt())
                            {
                                if (tabDI110.Controls[j] is UITextBox)
                                {
                                    UITextBox txt = tabDI110.Controls[j] as UITextBox;
                                    txt.Text = lstDIPoint[i].ProductName;
                                }
                                if (tabDI110.Controls[j] is UICheckBox)
                                {
                                    UICheckBox chk = tabDI110.Controls[j] as UICheckBox;
                                    chk.Checked = lstDIPoint[i].isStartUsing;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < tabDI24.Controls.Count; j++)
                    {
                        if (tabDI24.Controls[j] is UITextBox)
                        {
                            UITextBox txt = tabDI24.Controls[j] as UITextBox;
                            txt.Text = "";
                        }
                        if (tabDI24.Controls[j] is UICheckBox)
                        {
                            UICheckBox chk = tabDI24.Controls[j] as UICheckBox;
                            chk.Checked = false;
                        }
                    }
                    for (int j = 0; j < tabDI110.Controls.Count; j++)
                    {
                        if (tabDI110.Controls[j] is UITextBox)
                        {
                            UITextBox txt = tabDI110.Controls[j] as UITextBox;
                            txt.Text = "";
                        }
                        if (tabDI110.Controls[j] is UICheckBox)
                        {
                            UICheckBox chk = tabDI110.Controls[j] as UICheckBox;
                            chk.Checked = false;
                        }
                    }
                }

                List<DOPoint> lstDOPoint = paraconfig.listDOPoint;
                if (lstDOPoint.Count > 0)
                {
                    for (int i = 0; i < lstDOPoint.Count; i++)
                    {
                        for (int j = 0; j < tabDO.Controls.Count; j++)
                        {
                            if (lstDOPoint[i].Index == tabDO.Controls[j].Tag.ToInt())
                            {
                                if (tabDO.Controls[j] is UITextBox)
                                {
                                    UITextBox txt = tabDO.Controls[j] as UITextBox;
                                    txt.Text = lstDOPoint[i].ProductName;
                                }
                                if (tabDO.Controls[j] is UICheckBox)
                                {
                                    UICheckBox chk = tabDO.Controls[j] as UICheckBox;
                                    chk.Checked = lstDOPoint[i].isStartUsing;
                                }
                            }
                        }

                    }
                }
                else
                {
                    for (int j = 0; j < tabDO.Controls.Count; j++)
                    {
                        if (tabDO.Controls[j] is UITextBox)
                        {
                            UITextBox txt = tabDO.Controls[j] as UITextBox;
                            txt.Text = "";
                        }
                        if (tabDO.Controls[j] is UICheckBox)
                        {
                            UICheckBox chk = tabDO.Controls[j] as UICheckBox;
                            chk.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ModelID))
                LoadConfig();
        }
    }
}
