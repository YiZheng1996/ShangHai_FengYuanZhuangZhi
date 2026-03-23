using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using RW.UI.Controls.Report;
using MainUI.Model;
using RW.UI;
using Microsoft.Office;
using MainUI.BLL;
using Microsoft.Office.Interop.Excel;
using RW;

namespace MainUI
{
    public partial class frmReport : Form
    {
        public TestViewModel viewMole { get; set; }
        public string saveFilepath { get; set; }
        public string Filename { get; set; }  //报表的文件地址
        public event OpenHandler Opened;
        int rowIndex = 0;

        public frmReport()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmReport_Load(object sender, EventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                string destFileTemp = System.Windows.Forms.Application.StartupPath + "\\reports\\tempReport.xls";
                SystemHelper.KillProcess("Excel");
                Thread.Sleep(200);
                if (File.Exists(saveFilepath))
                {
                    File.Copy(saveFilepath, destFileTemp, true);
                }
                else
                {
                    File.Copy(Filename, destFileTemp, true);
                }
                this.rwReport1.Filename = destFileTemp;
                this.rwReport1.Init();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 上翻
        /// </summary>
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int value = (int)this.numericUpDown1.Value;
            rowIndex -= value;
            if (rowIndex < 1)
                rowIndex = 1;
            this.rwReport1.ScrollIndex(rowIndex);
        }
        /// <summary>
        /// 下翻
        /// </summary>
        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int value = (int)this.numericUpDown1.Value;
            rowIndex += value;
            if (rowIndex > 100)
                rowIndex = 100;
            this.rwReport1.ScrollIndex(rowIndex);
        }
        /// <summary>
        /// 打印
        /// </summary>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.rwReport1.Print();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (rwReport1 == null)
                    return;
                TestRecordBLL recordbll = new TestRecordBLL(Var.Database, Var.ConnectionString, "Record");
                int ret = recordbll.SaveData(viewMole.ModelTypeID.ToString(), viewMole.ModelName, "", viewMole.TestNO, RW.UI.RWUser.User.Username, DateTime.Now.ToString(), saveFilepath);

                if (System.IO.Directory.Exists(Var.rootRptSave) == false)
                    System.IO.Directory.CreateDirectory(Var.rootRptSave);
                this.rwReport1.SaveAS(saveFilepath);
                Var.MessageInfo("保存成功");

            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// 读取key中的值，使用第一个worksheet
        /// </summary>
        /// <returns></returns>
        public object Read(string key)
        {
            return this.rwReport1.Read(1, key);
        }
        /// <summary>
        /// 将value写入到key中，使用第一个worksheet
        /// </summary>
        public void Write(string key, object value)
        {
            this.Write(1, key, value);
        }
        /// <summary>
        /// 将value写入到key中，使用指定的worksheet
        /// </summary>
        public void Write(int sheetIndex, string key, object value)
        {
            this.rwReport1.Write(sheetIndex, key, value);
        }
        /// <summary>
        /// 报表打开时
        /// </summary>
        private void rwReport1_Opened(object sender, EventArgs e)
        {
            if (this.Opened != null)
                this.Opened(this, e);
        }
        /// <summary>
        /// 插入图片
        /// </summary>
        public void InsertPicture(string key, string PicturePath)
        {
            this.rwReport1.InsertPicture(key, PicturePath);
        }
    }
}
