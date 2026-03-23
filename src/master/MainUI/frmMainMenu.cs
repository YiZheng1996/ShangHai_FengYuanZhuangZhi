using System;
using System.Windows.Forms;
using MainUI.Properties;
using MainUI.Procedure;
using MainUI.Modules;
using RW.EventLog;
using RW.Components.User;
using System.Drawing;
using MainUI.Procedure.User;
using MainUI.BLL;
using System.Data;

namespace MainUI
{
    public partial class frmMainMenu : Form
    {
        public ucHMI hmi = null;
        frmHardWare hard;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        OpcStatusGrp opcStatus;
        public frmMainMenu()
        {
            InitializeComponent();
            this.lblTitle.Text = Var.SoftName;
            this.Text = Var.SoftName;
            timer1_Tick(null, null);
        }
        /// <summary>
        /// C#winform实现界面拖动
        /// </summary>
        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
                hmi = new ucHMI();
                hmi.EmergencyStatusChanged += new ucHMI.RunStatusHandler(hmi_EnmergencyStatusChanged);
                hmi.Dock = DockStyle.Fill;
                hmi.Init();
                Common.WSDgrp.WSDvalueGrpChaned += WSDgrp_WSDvalueGrpChaned;
                Common.AIgrp.AIvalueGrpChanged += AIgrp_AIvalueGrpChanged;
                BaseTest.TestStateChanged += BaseTest_TestStateChanged;
                opcStatus = Common.opcStatus;
                hard = new frmHardWare();
                hard.InitZeroGain();
                this.splitContainer1.Panel2.Controls.Add(hmi);
                timer1_Tick(null, null);
                RW.Components.User.BLL.UserBLL userBll = new RW.Components.User.BLL.UserBLL();
                switch (RW.UI.RWUser.User.Permission)
                {
                    case "管理员"://系统管理员
                        break;
                    case "工艺员"://工程师
                        this.btnMainData.Visible = false;
                        this.btnProc.Visible = false;
                        this.btnMetering.Visible = false;
                        break;
                    case "操作员"://操作员
                        this.btnMainData.Visible = false;
                        this.btnHardwareTest.Visible = false;
                        this.btnProc.Visible = false;
                        this.btnMetering.Visible = false;
                        break;
                    default:
                        this.btnMainData.Visible = false;
                        this.btnHardwareTest.Visible = false;
                        this.btnProc.Visible = false;
                        this.btnMetering.Visible = false;
                        break;
                }
                timerPLC.Enabled = true;
                //计量日期过期判断
                MeasureBLL bll = new MeasureBLL(Var.Database, Var.ConnectionString, "Measure");
                DataTable dt = bll.GetMeasure(DateTime.Now.ToString("yyyy-MM-dd"));
                string rn = Environment.NewLine;
                if (dt.Rows.Count > 0)
                {
                    string tip = "请注意：以下传感器计量时间已过期，请尽快计量。" + rn;
                    foreach (DataRow item in dt.Rows)
                    {
                        tip += "编号【" + item["Number"] + "】的【" + item["SensorType"] + "】" + rn;
                    }
                    tip += "" + rn;
                    Var.MessageInfo(tip);
                }
                else
                {
                    DateTime dt2 = DateTime.Now.AddDays(30);
                    dt = bll.GetMeasure(dt2.ToString("yyyy-MM-dd"));
                    if (dt.Rows.Count > 0)
                    {
                        string tip = "请注意：以下传感器计量时间即将过期，请尽快计量。" + rn;
                        foreach (DataRow item in dt.Rows)
                        {
                            tip += "编号【" + item["Number"] + "】的【" + item["SensorType"] + "】" + rn;
                        }
                        tip += "" + rn;
                        Var.MessageInfo(tip);
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            if (index == 0) //环境压力
            {
                lblYaLi.Text = "环境压力：" + value.ToString("f1") + "kPa";
            }
        }

        private void WSDgrp_WSDvalueGrpChaned(object sender, int index, double value)
        {
            if (index == 0) //环境温度
            {
                lblWenDu.Text = "环境温度：" + value.ToString("f1") + "℃";
            }
            else if (index == 1)    //环境湿度
            {
                lblShiDu.Text = "环境湿度：" + value.ToString("f1") + "%RH";
            }
        }

        private void BaseTest_TestStateChanged(bool isTesting)
        {
            btnHardwareTest.Enabled = !isTesting;
            btnMainData.Enabled = !isTesting;
            btnReports.Enabled = !isTesting;
            btnChangePwd.Enabled = !isTesting;
            btnExit.Enabled = !isTesting;
        }

        /// <summary>
        /// 显示界面时间
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 急停事件
        /// </summary>
        /// <param name="obj"></param> 
        private void hmi_EnmergencyStatusChanged(bool obj)
        {
            if (!obj)
            {
                this.splitContainer1.Panel2.Enabled = false;
                EventLogHelper.Log(EventLogType.Error, "急停被按下。" + RW.UI.RWUser.User.Username);
                this.picRunStatus.Image = Resources.scram;

            }
            else
            {
                this.splitContainer1.Panel2.Enabled = true;
                this.picRunStatus.Image = Resources.normal;

            }
        }

        /// <summary>
        /// 硬件校准
        /// </summary>
        private void btnHardwareTest_Click(object sender, EventArgs e)
        {
            frmHardWare hard = new frmHardWare();
            hard.ShowDialog();
        }
        /// <summary>
        /// 数据管理
        /// </summary>
        private void btnMainData_Click(object sender, EventArgs e)
        {

            frmSettingMain main = new frmSettingMain();
            main.AddNodes("用户管理", new ucUserManager());
            main.AddNodes("型号管理", new ucModelManage());
            main.AddNodes("参数管理", new ucTestParams());
            main.AddNodes("项点管理", new ucTestItems());
            main.AddNodes("点位管理", new ucTestPoint());
            main.ShowDialog();
            hmi.sRefresh();
        }
        /// <summary>
        /// 历史报表
        /// </summary>
        private void btnReports_Click(object sender, EventArgs e)
        {
            frmDataManager fdm = new frmDataManager();
            fdm.ShowDialog();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            frmChangePwd changePwd = new frmChangePwd();
            changePwd.ShowDialog();
        }
        /// <summary>
        /// 注销
        /// </summary>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        /// <summary>
        /// 退出
        /// </summary>      
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (Var.MessageOKCancel("确定要退出试验吗？"))
            {
                Common.AOgrp.SignalVolt = 0;
                Common.powerControlgrp.SetFrequency = 0;
                Common.powerControlgrp.SetVolt = 0;
                //关闭MV01 外供风关闭
                Common.DOgrp[8] = false;
                //关闭伺服阀
                Common.DOgrp[4] = false;
                Var.Close();
                Application.Exit();
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.lblDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void timerPLC_Tick(object sender, EventArgs e)
        {
            try
            {
                tslblUser.Text = "当前登录用户：" + RW.UI.RWUser.User.Username;
                tslblPLC.Text = opcStatus.NoError ? " PLC连接正常 " : " PLC连接失败 ";
                tslblPLC.BackColor = opcStatus.NoError ? Color.FromArgb(110, 190, 40) : Color.Salmon;
                if (opcStatus.Simulated)
                    tslblPLC.Text = tslblPLC.Text + " 仿真模式 ";
                lblPenZui.Text = "喷嘴系数：" + Common.paraconfig.Nozzle.ToString();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void btnProc_Click(object sender, EventArgs e)
        {
            frmTestItem frm = new frmTestItem();
            frm.Show();
        }

        private void btnMetering_Click(object sender, EventArgs e)
        {
            frmMetering frm = new frmMetering();
            frm.Show();
        }
    }
}
