using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MainUI.Model;
using MainUI.Modules;
using MainUI.Config;
using RW.UI.Controls;
using MainUI.Procedure;
using System.Threading;
using MainUI.Procedure.Test;
using System.Diagnostics;
using RW.Fonts;
using Sunny.UI;
using MainUI.BLL;
using System.Data;
using System.Linq;

namespace MainUI
{
    public partial class ucHMI : UserControl
    {
        public delegate void RunStatusHandler(bool obj);
        public event RunStatusHandler EmergencyStatusChanged;

        Dictionary<int, ucDIControl> dicDI = new Dictionary<int, ucDIControl>();
        Dictionary<int, ucDOControl> dicDO = new Dictionary<int, ucDOControl>();
        Dictionary<int, ucAIControl> dicAI = new Dictionary<int, ucAIControl>();
        Dictionary<int, ucAIControl> dicPowerRead = new Dictionary<int, ucAIControl>();

        Dictionary<int, UIPanel> dicElec = new Dictionary<int, UIPanel>();
        Dictionary<int, UIPanel> dicNozzle = new Dictionary<int, UIPanel>();
        Dictionary<int, UIPanel> dicSubjects = new Dictionary<int, UIPanel>();
        //Dictionary<int, UIPanel> dicWSD = new Dictionary<int, UIPanel>();
        List<UICheckBox> listChk = new List<UICheckBox>();
        Dictionary<string, Func<BaseTest>> dicBase = new Dictionary<string, Func<BaseTest>>(); // 注册映射
        string RptFilename = "";  //报表名称
        string RptFilePath = "";  //报表地址
        string saveRptFile = "";  //报表保存地址
        frmReport report = null;
        string rn = Environment.NewLine;
        private delegate void Del();

        public ucHMI()
        {
            InitializeComponent();
        }

        //初始化
        public void Init()
        {
            try
            {
                Common.Init();
                LoadModule();
                Common.AIgrp.AIvalueGrpChanged += AIgrp_AIvalueGrpChanged;
                Common.AIgrp.Fresh();
                Common.DIgrp.DIGroupChanged += DIgrp_DIGroupChanged;
                Common.DIgrp.Fresh();
                Common.DOgrp.DOgrpChanged += DOgrp_DOgrpChanged;
                Common.DOgrp.Fresh();
                Common.Nozzlegrp.NozzlevalueGrpChaned += Nozzlegrp_NozzlevalueGrpChaned;
                Common.Nozzlegrp.Fresh();
                Common.Subjectsgrp.SubjectsvalueGrpChaned += Subjectsgrp_SubjectsvalueGrpChaned;
                Common.Subjectsgrp.Fresh();
                //Common.WSDgrp.WSDvalueGrpChaned += WSDgrp_WSDvalueGrpChaned;
                //Common.WSDgrp.Fresh();
                Common.powerReadgrp.PowerReadvalueGrpChaned += PowerReadgrp_PowerReadvalueGrpChaned;
                Common.powerReadgrp.Fresh();
                BaseTest.TipsChanged += UcBase_TipsChanged;
                BaseTest.TimingChanged += BaseTest_TimingChanged;
                labsfkk.Text = Common.AOgrp.ServoValve.ToString("f0") + "%";
                //默认打开变频电源控制
                Common.powerControlgrp.SetModel = 2;
                Common.powerControlgrp.SetGears = 2;
                Common.powerControlgrp.SetStartUp = 1;
                //打开MV01 外供风打开
                Common.DOgrp[8] = true;
                //打开MV03
                Common.DOgrp[10] = true;
                //打开伺服阀供电
                Common.DOgrp[4] = true;

            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }

        private void RegisterTestItems()
        {
            dicBase.Clear();
            dicBase.Add("WGJC", () => new WGJCTest());
            dicBase.Add("DQCS", () => new DQCSTest());
            dicBase.Add("YWJC", () => new YWJCTest());
            dicBase.Add("DJZX", () => new DJZXTest());
            dicBase.Add("EDZS", () => new EDZSTest());
            dicBase.Add("XLSY", () => new XLSYTest());
            dicBase.Add("XNSY", () => new XNSYTest());
            dicBase.Add("YLKG", () => new YLKGTest());
            dicBase.Add("GLCS", () => new GLCSTest());
            dicBase.Add("XNSYKQZL", () => new XNSYKQZLTest());
        }

        #region 事件
        private void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            if (dicAI.ContainsKey(index))
            {
                dicAI[index].Value = value;
            }
            //else if (index == 0)
            //{
            //    numAI0.Text = value.ToString("f1");
            //}
            else if (index == 10)
            {
                ucAIControl9.Value = value;
            }
            else if (index == 12)
            {
                numAI12.Text = value.ToString("f1");
            }
            else if (index == 13)
            {
                numAI13.Text = value.ToString("f1");
            }
        }
        #region 超负荷测试相关变量
        //int nMillisecond = 0;   //计时器
        int PS2 = 0;
        int PS3 = 0;
        int Over = 0;
        int Temp = 0;
        DateTime nPS2_Open = DateTime.Now;      //ps2打开的时间戳
        DateTime nPS2_Close = DateTime.Now;     //ps2关闭的时间戳
        DateTime nPS3_Open = DateTime.Now;      //ps3打开的时间戳
        DateTime nPS3_Close = DateTime.Now;     //ps3关闭的时间戳
        DateTime nPS2_Timeout = DateTime.Now;   //ps2超时的时间戳
        DateTime nPS3_Timeout = DateTime.Now;   //ps3超时的时间戳
        DateTime nPS2_3_Timeout = DateTime.Now;   //ps2和ps3共存超时的时间戳
        bool isTemp = false;    //是否油温过热
        bool isOverload = false;//是否电机过载
        bool isChaoFuHe = false;//是否测试超负荷标志
        TimeSpan ts = new TimeSpan();
        #endregion

        private void DIgrp_DIGroupChanged(object sender, int index, bool value)
        {
            try
            {
                if (dicDI.ContainsKey(index))
                {
                    dicDI[index].Value = value;
                }
                if (index == 0)
                {
                    if (EmergencyStatusChanged != null)
                    {
                        EmergencyStatusChanged(value);
                    }
                    if (value == false)
                    {
                        btnStop_Click(this, null);
                        Var.MessageInfo("请注意，急停按钮已按下");
                    }
                }
                //超负荷时记录点位变化
                if (isChaoFuHe)
                {

                    if (index == PS2)
                    {
                        nPS2_Timeout = DateTime.Now;
                        if (value)
                            nPS2_Open = DateTime.Now;
                        else
                        {
                            nPS2_Close = DateTime.Now;
                            ts = nPS2_Close - nPS2_Open;
                            Common.mResultAll.listPS2.Add(ts.TotalSeconds);
                        }
                    }
                    if (index == PS3)
                    {
                        nPS3_Timeout = DateTime.Now;
                        if (value)
                            nPS3_Open = DateTime.Now;
                        else
                        {
                            nPS3_Close = DateTime.Now;
                            ts = nPS3_Close - nPS3_Open;
                            Common.mResultAll.listPS3.Add(ts.TotalSeconds);
                        }
                    }
                    if (index == PS2 || index == PS3)
                    {
                        isTemp |= Common.DIgrp[Temp];  //检查油温有没有过热
                        isOverload |= Common.DIgrp[Over];  //检测电机有没有过载
                        if (Common.DIgrp[PS2] == true && Common.DIgrp[PS3] == false)
                        {
                            ts = nPS3_Close - nPS2_Open;
                            Common.mResultAll.listPS2_3.Add(ts.TotalSeconds);
                            Common.mResultAll.listTime.Add(DateTime.Now.ToString("HH:mm:ss"));
                            Common.mResultAll.listIsTemp.Add(isTemp);
                            Common.mResultAll.listIsOverload.Add(isOverload);
                        }


                    }
                }

            }
            catch (Exception)
            {
                Debug.WriteLine("DI模块事件报错");
            }
        }
        private void DOgrp_DOgrpChanged(object sender, int index, bool value)
        {
            if (dicDO.ContainsKey(index))
            {
                dicDO[index].Value = value;
            }
            if (index == 9)
            {
                if (value)
                    MV02.ValveColor = Color.FromArgb(110, 190, 40);
                else
                    MV02.ValveColor = Color.Silver;
            }
            if (index == 10)
            {
                if (value)
                    MV03.ValveColor = Color.FromArgb(110, 190, 40);
                else
                    MV03.ValveColor = Color.Silver;
            }
        }
        private void Nozzlegrp_NozzlevalueGrpChaned(object sender, int index, double value)
        {
            if (dicNozzle.ContainsKey(index))
            {
                dicNozzle[index].Text = value.ToString("f1");
            }
        }
        private void Subjectsgrp_SubjectsvalueGrpChaned(object sender, int index, double value)
        {
            if (dicSubjects.ContainsKey(index))
            {
                dicSubjects[index].Text = value.ToString("f1");
            }
        }
        //private void WSDgrp_WSDvalueGrpChaned(object sender, int index, double value)
        //{
        //    if (dicWSD.ContainsKey(index))
        //    {
        //        dicWSD[index].Text = value.ToString("f1");
        //    }
        //}
        private void PowerReadgrp_PowerReadvalueGrpChaned(object sender, int index, double value)
        {
            if (dicPowerRead.ContainsKey(index))
            {
                dicPowerRead[index].Value = value;
            }
            if (index == 15)
            {
                ucAIControl8.Value = Common.powerReadgrp.ZPower;
            }
        }

        //写试验信息提示
        private void UcBase_TipsChanged(object sender, object info)
        {
            this.Invoke(new Del(delegate { lblStatus.Text = info.ToString(); }));
        }
        //写试验时间
        private void BaseTest_TimingChanged(object sender, int ns)
        {
            long h, m, s;
            h = ns / 3600;
            ns %= 3600;
            m = ns / 60;
            ns %= 60;
            s = ns / 1;
            ns %= 1;
            this.Invoke(new Del(delegate { lblTiming.Text = h.ToString().PadLeft(2, '0') + ":" + m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0'); }));
        }
        //伺服阀
        private void labsfkk_Click(object sender, EventArgs e)
        {
            frmSetOutValue frmsetAir = new frmSetOutValue(Convert.ToDouble(Common.AOgrp.ServoValve), "设定伺服开度", 100, 0);
            DialogResult diaRst = frmsetAir.ShowDialog();
            if (diaRst == DialogResult.OK)
            {
                if (frmsetAir.OutValue > 100)
                {
                    frmsetAir.OutValue = 100;
                }
                Common.AOgrp.ServoValve = frmsetAir.OutValue;
                labsfkk.Text = frmsetAir.OutValue.ToString() + "%";
            }
        }
        //可调电源
        private void labdykk_Click(object sender, EventArgs e)
        {
            frmSetOutValue frmsetAir = new frmSetOutValue(Convert.ToDouble(Common.AOgrp.SignalVolt), "设定信号电源电压", 150, 0);
            DialogResult diaRst = frmsetAir.ShowDialog();
            if (diaRst == DialogResult.OK)
            {
                if (frmsetAir.OutValue > 150)
                {
                    frmsetAir.OutValue = 100;
                }
                Common.AOgrp.SignalVolt = frmsetAir.OutValue;
                labdykk.Text = frmsetAir.OutValue.ToString() + "V";
            }
        }
        #endregion

        #region 初始化

        /// <summary>
        /// 加载模块
        /// </summary>
        private void LoadModule()
        {
            //DI
            dicDI.Clear();
            foreach (var item in palDI.Controls)
            {
                if (item is ucDIControl)
                {
                    ucDIControl dicon = item as ucDIControl;
                    dicDI.Add(dicon.Index, dicon);
                }
            }
            Common.DIgrp.Fresh();
            //AI
            dicAI.Clear();
            foreach (var item in palAI.Controls)
            {
                if (item is ucAIControl)
                {
                    ucAIControl aicon = item as ucAIControl;
                    dicAI.Add(aicon.Index, aicon);
                }
            }
            Common.AIgrp.Fresh();
            //DO
            dicDO.Clear();
            foreach (var item in palDO.Controls)
            {
                if (item is ucDOControl)
                {
                    ucDOControl docon = item as ucDOControl;
                    dicDO.Add(docon.Index, docon);
                }
            }
            Common.DOgrp.Fresh();
            //电量表
            dicPowerRead.Clear();
            dicPowerRead.Add(ucAIControl1.Index, ucAIControl1);
            dicPowerRead.Add(ucAIControl2.Index, ucAIControl2);
            dicPowerRead.Add(ucAIControl3.Index, ucAIControl3);
            dicPowerRead.Add(ucAIControl4.Index, ucAIControl4);
            dicPowerRead.Add(ucAIControl5.Index, ucAIControl5);
            dicPowerRead.Add(ucAIControl6.Index, ucAIControl6);
            dicPowerRead.Add(ucAIControl7.Index, ucAIControl7);
            dicPowerRead.Add(ucAIControl10.Index, ucAIControl10);
            Common.powerReadgrp.Fresh();
            ////温度
            //dicWSD.Clear();
            //dicWSD.Add(numTemper.Tag.ToInt(), numTemper);
            //dicWSD.Add(numHumidity.Tag.ToInt(), numHumidity);
            //喷嘴
            dicNozzle.Clear();
            dicNozzle.Add(nozzle0.Tag.ToInt(), nozzle0);
            dicNozzle.Add(nozzle1.Tag.ToInt(), nozzle1);
            //主体
            dicSubjects.Clear();
            dicSubjects.Add(subjects0.Tag.ToInt(), subjects0);
            dicSubjects.Add(subjects1.Tag.ToInt(), subjects1);
        }

        #endregion

        #region 参数
        /// <summary>
        /// 读取配置文件，配置参数
        /// </summary>
        private void InitParaConfig()
        {
            try
            {
                if (Common.mTestViewModel == null)
                    return;
                Common.paraconfig.SetSectionName(Common.mTestViewModel.ModelID.ToString());
                Common.paraconfig.Load();
                BaseTest.para = Common.paraconfig;
                if (!string.IsNullOrEmpty(Common.paraconfig.RptFile))
                {
                    RptFilename = Common.paraconfig.RptFile;
                    saveRptFile = Var.rootRptSave + "\\" + Common.mTestViewModel.ModelName + "_" + Common.mTestViewModel.TestNO + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".xls";
                    string filePath = Application.StartupPath + "\\reports\\" + Common.paraconfig.RptFile;
                    RptFilePath = filePath;

                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        #endregion

        #region 报表
        /// <summary>
        /// 试验报表
        /// </summary>
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                report = new frmReport();
                Common.mTestViewModel.TestNO = txtNumber.Text;
                Common.mTestViewModel.ElecNO = txtElecNO.Text;
                Common.mTestViewModel.HeadNO = txtHeadNO.Text;
                report.viewMole = Common.mTestViewModel;
                report.Filename = RptFilePath;
                report.saveFilepath = saveRptFile;
                report.Opened += Report_Opened;
                report.ShowDialog();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        /// <summary>
        /// 加载报表
        /// </summary>
        private void Report_Opened(object sender, EventArgs e)
        {

            try
            {
                SetReport sr = new SetReport(report, Common.paraconfig);
                sr.SetRep();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        #endregion

        //产品选择
        private void btnProductSelection_Click(object sender, EventArgs e)
        {
            frmSpec fs = new frmSpec();
            fs.ShowDialog();
            sRefresh();
            txtNumber.Focus();
        }
        /// <summary>
        /// 制造编号值改变事件，将输入的制造编号信息保存至缓存型号中
        /// </summary>
        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            sTempNO = txtNumber.Text;
        }
        string sTempNO;
        private void txtNumber_Leave(object sender, EventArgs e)
        {
            Common.mTestViewModel.TestNO = txtNumber.Text;
            saveRptFile = Var.rootRptSave + "\\" + Common.mTestViewModel.ModelName + "_" + Common.mTestViewModel.TestNO + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".xls";
            Common.mResultAll.dic = new Dictionary<string, List<string>>();
        }
        //刷新型号
        public void sRefresh()
        {
            if (string.IsNullOrEmpty(Common.mTestViewModel.ModelName))
            {
                return;
            }
            Common.mResultAll.dic = new Dictionary<string, List<string>>();
            txtType.Text = Common.mTestViewModel.ModelType;
            txtModel.Text = Common.mTestViewModel.ModelName;
            txtElecNO.Text = Common.mTestViewModel.Mark;
            InitParaConfig();
            palAI.Controls.Clear();
            palDI.Controls.Clear();
            palDO.Controls.Clear();
            ucAIControl aicon = new ucAIControl();
            aicon.LabelText = "喷嘴流量(m³/min)";
            aicon.Index = 99;
            aicon.Decimal = 3;
            palAI.Controls.Add(aicon);
            //根据型号刷新AI/DI/DO
            foreach (var item in Common.paraconfig.listAIPoint)
            {
                if (item.isStartUsing)
                {
                    aicon = new ucAIControl();
                    aicon.LabelText = item.ProductName;
                    aicon.Index = item.Index;
                    palAI.Controls.Add(aicon);
                }
            }
            foreach (var item in Common.paraconfig.listDIPoint)
            {
                if (item.isStartUsing)
                {
                    ucDIControl dicon = new ucDIControl();
                    dicon.LabelText = item.ProductName;
                    dicon.Index = item.Index;
                    palDI.Controls.Add(dicon);
                }
            }
            ucDOControl docon = new ucDOControl();
            docon.LabelText = "伺服阀供电";
            docon.Index = 4;
            palDO.Controls.Add(docon);
            docon = new ucDOControl();
            docon.LabelText = "风源供电";
            docon.Index = 5;
            palDO.Controls.Add(docon);
            docon = new ucDOControl();
            docon.LabelText = "24V输出";
            docon.Index = 6;
            palDO.Controls.Add(docon);
            docon = new ucDOControl();
            docon.LabelText = "110V输出";
            docon.Index = 7;
            palDO.Controls.Add(docon);
            foreach (var item in Common.paraconfig.listDOPoint)
            {
                if (item.isStartUsing)
                {
                    docon = new ucDOControl();
                    docon.LabelText = item.ProductName;
                    docon.Index = item.Index;
                    palDO.Controls.Add(docon);
                }
            }
            //刷新结果对象
            Common.mResultAll = new ResultAll();
            //重新加载模块
            LoadModule();
            //重新加载测试项点
            dicBase.Clear();
            listChk.Clear();
            TestItemsBLL bll = new TestItemsBLL(Var.Database, Var.ConnectionString, "TestItems");
            DataTable dt = bll.GetTestItems(Common.mTestViewModel.ModelID);
            uiFlowLayoutPanel1.Panel.Controls.Clear();
            foreach (DataRow item in dt.Rows)
            {
                UICheckBox chk = new UICheckBox();
                chk.Width = 210;
                chk.Height = 29;
                chk.Text = item["ItemName"].ToString();
                chk.Tag = item;
                uiFlowLayoutPanel1.Panel.Controls.Add(chk);
                listChk.Add(chk);
            }
            //根据选择的型号打开24V或110V
            if (Common.paraconfig.isVolt)
            {
                Common.DOgrp[7] = false;
                Common.DOgrp[6] = true;
                Common.AOgrp.SignalVolt = 24;
            }
            else
            {
                Common.DOgrp[6] = false;
                Common.DOgrp[7] = true;
                Common.AOgrp.SignalVolt = 110;
            }
            //下发风源主电路的电压与频率
            Common.powerControlgrp.SetVolt = Common.paraconfig.Volt;
            Common.powerControlgrp.SetFrequency = Common.paraconfig.Frequency;
            RegisterTestItems();
        }

        //全选
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in uiFlowLayoutPanel1.Panel.Controls)
                {
                    if (item is UICheckBox)
                    {
                        UICheckBox chk = item as UICheckBox;
                        chk.Checked = chkAll.Checked;
                    }

                }
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        #region 自动试验
        public Thread th;
        private void btnStart_Click(object sender, EventArgs e)
        {

            foreach (var item in listChk)
            {
                item.BackColor = Color.FromArgb(243, 249, 255);
            }
            if (string.IsNullOrEmpty(Common.mTestViewModel.ModelName)) { Var.MessageInfo("请选择产品后开始试验"); return; }
            if (Common.DIgrp[0] == false) { Var.MessageInfo("急停情况下无法启动"); return; }
            if (string.IsNullOrEmpty(Common.mTestViewModel.TestNO)) { Var.MessageInfo("请填写[供风模块序列号]后再进行试验"); return; }
            if (string.IsNullOrEmpty(Common.mTestViewModel.ElecNO)) { Var.MessageInfo("请填写[双塔干燥器序列号]后再进行试验"); return; }
            if (string.IsNullOrEmpty(Common.mTestViewModel.HeadNO)) { Var.MessageInfo("请填写[空压机序列号]后再进行试验"); return; }

            Initialise();
            lblTiming.Text = "00:00:00";
            lblStatus.Text = "试验中";

            bool temp = false;
            foreach (var item in listChk)
            {
                temp |= item.Checked;
            }
            if (!temp)
            {
                Var.MessageInfo("未选择试验项点！请先选择一个试验项点");
                return;
            }
            ControlLock(true);
            th = new Thread(new ThreadStart(TestStart));
            th.Start();
        }
        /// <summary>
        /// 初始化电池阀及EP阀
        /// </summary>
        private void Initialise()
        {
            isTestBegin = false;
            //Common.DOgrp[5] = false;
            //for (int i = 12; i <= 24; i++)
            //{
            //    Common.DOgrp[i] = false;
            //}
        }
        bool isTestBegin = false;
        DateTime dtTest;
        /// <summary>
        /// 试验前点位输出
        /// </summary>
        private bool Prepare()
        {
            isTestBegin = true;
            dtTest = DateTime.Now;
            return true;
        }

        TestProcBLL bll = new TestProcBLL(Var.Database, Var.ConnectionString, "TestProc");
        Autotest test = new Autotest();

        public void TestStart()
        {
            string tip = "";
            tip += "请先确保如下事项：" + rn;
            tip += "1、确认产品安装是否正确。" + rn;
            tip += "2、确认航插或电源线已正确安装。" + rn;
            tip += "3、确认喷嘴直径是否为【" + Common.paraconfig.NozzleDiameter + "】mm。" + rn;
            tip += "点击【是】开始试验，点击【否】返回。" + rn;

            if (!Var.MessageOKCancel(tip))
            {
                TestStop();
                return;
            }
            if (!Prepare()) return;

            Common.mResultAll.dicReport.Clear();  // 每次试验前清空

            #region 循环试验项点

            foreach (var item in listChk)
            {
                if (!item.Checked) continue;

                isNext = false;
                item.BackColor = Color.Yellow;

                DataRow drTag = item.Tag as DataRow;
                string testCode = drTag["TestCode"]?.ToString() ?? "";
                string itemName = item.Text;
                //string savePath = drTag["SavePath"].ToString();

                this.Invoke(new Del(delegate
                {
                    uiProcessBar1.Value = 0;
                    lblStatus.Text = "试验中 - " + itemName;
                }));

                if (string.IsNullOrEmpty(testCode) || !dicBase.ContainsKey(testCode))
                {
                    Var.MessageInfo("【" + itemName + "】未配置TestCode或未注册试验逻辑");
                    continue;
                }

                BaseTest testItem = dicBase[testCode]();

                // 超负荷前置
                if (testCode == "CFH")
                {
                    Common.mResultAll.listPS2 = new List<double>();
                    Common.mResultAll.listPS3 = new List<double>();
                    Common.mResultAll.listPS2_3 = new List<double>();
                    Common.mResultAll.listIsOverload = new List<bool>();
                    Common.mResultAll.listIsTemp = new List<bool>();
                    Common.mResultAll.listTime = new List<string>();
                    frmOverload frm = new frmOverload(Common.paraconfig);
                    frm.ShowDialog(this);
                    if (frm.DialogResult != DialogResult.OK)
                    {
                        Var.MessageInfo("未保存测试点位，测试终止");
                        break;
                    }
                    PS2 = frm.PS2;
                    PS3 = frm.PS3;
                    Over = frm.Over;
                    Temp = frm.Temp;
                    isChaoFuHe = true;
                }

                bool success = testItem.Execute();

                if (testCode == "CFH")
                {
                    isChaoFuHe = false;
                }

                if (success)
                {
                    item.BackColor = Color.LightGreen;
                }
                else
                {
                    item.BackColor = Color.Red;
                }
            }

            #endregion

            lblStatus.Text = "试验完成";
            Initialise();
            Common.DOgrp[9] = true;
            while (Common.AIgrp[5] > 50)
            {
                Thread.Sleep(100);
            }
            Common.DOgrp[9] = false;
            ControlLock(false);
        }

        //public void TestStart()
        //{

        //    string tip = "";
        //    tip += "请先确保如下事项：" + rn;
        //    tip += "1、确认产品安装是否正确。" + rn;
        //    tip += "2、确认航插或电源线已正确安装。" + rn;
        //    tip += "3、确认喷嘴直径是否为【" + Common.paraconfig.NozzleDiameter + "】mm。" + rn;
        //    tip += "点击【是】开始试验，点击【否】返回。" + rn;
        //    if (!Var.MessageOKCancel(tip))
        //    {
        //        TestStop();
        //        return;
        //    }
        //    if (!Prepare()) return;


        //    #region 循环试验项点

        //    //循环试验项点
        //    foreach (var item in listChk)
        //    {

        //        if (item.Checked)
        //        {
        //            isNext = false;
        //            item.BackColor = Color.Yellow;
        //            DataRow drTag = item.Tag as DataRow;
        //            DataTable dt = bll.GetTestProcAll(drTag["ID"].ToInt());
        //            DataColumn dc = new DataColumn();
        //            dc.ColumnName = "State";
        //            dt.Columns.Add(dc);
        //            this.Invoke(new Del(delegate
        //            {
        //                uiProcessBar1.Value = 0;
        //                uiDataGridView1.DataSource = dt;
        //                lblStatus.Text = "试验中";
        //            }));
        //            List<string> lst = new List<string>();
        //            if (item.Text.IndexOf("超负荷") >= 0)
        //            {
        //                Common.mResultAll.listPS2 = new List<double>();
        //                Common.mResultAll.listPS3 = new List<double>();
        //                Common.mResultAll.listPS2_3 = new List<double>();
        //                Common.mResultAll.listIsOverload = new List<bool>();
        //                Common.mResultAll.listIsTemp = new List<bool>();
        //                Common.mResultAll.listTime = new List<string>();
        //                frmOverload frm = new frmOverload(Common.paraconfig);
        //                frm.ShowDialog(this);
        //                if (frm.DialogResult != DialogResult.OK)
        //                {
        //                    Var.MessageInfo("未保存测试点位，测试终止");
        //                    break;
        //                }
        //                PS2 = frm.PS2;
        //                PS3 = frm.PS3;
        //                Over = frm.Over;
        //                Temp = frm.Temp;
        //                isChaoFuHe = true;
        //                nPS2_Open = DateTime.Now;      //ps2打开的时间戳
        //                nPS2_Close = DateTime.Now;     //ps2关闭的时间戳
        //                nPS3_Open = DateTime.Now;      //ps3打开的时间戳
        //                nPS3_Close = DateTime.Now;     //ps3关闭的时间戳
        //                nPS2_Timeout = DateTime.Now;   //ps2超时的时间戳
        //                nPS3_Timeout = DateTime.Now;   //ps3超时的时间戳
        //                nPS2_3_Timeout = DateTime.Now;
        //            }
        //            //循环试验步骤
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {

        //                dt.Rows[i]["State"] = "▷";
        //                this.Invoke(new Del(delegate
        //                {

        //                    uiDataGridView1.DataSource = dt;

        //                }));
        //                test.dtProc = dt.Rows[i];
        //                string sVal = test.Execute();
        //                if (!string.IsNullOrEmpty(sVal))
        //                {
        //                    lst.Add(sVal);
        //                }

        //                dt.Rows[i]["State"] = "▶";
        //                this.Invoke(new Del(delegate
        //                {
        //                    uiDataGridView1.DataSource = dt;
        //                    uiProcessBar1.Value = ((i + 1) * 100 / dt.Rows.Count).ToInt();
        //                }));
        //                //控制单步，不一下跑完
        //                while (isExceute == false && isNext == false)
        //                {
        //                    Thread.Sleep(100);
        //                }
        //                isNext = false;

        //            }
        //            if (item.Text.IndexOf("超负荷") >= 0)
        //            {
        //                isChaoFuHe = false;
        //            }
        //            item.BackColor = Color.LightGreen;
        //            if (Common.mResultAll.dic.ContainsKey(drTag["SavePath"].ToString()))
        //            {
        //                Common.mResultAll.dic.Remove(drTag["SavePath"].ToString());
        //            }
        //            Common.mResultAll.dic.Add(drTag["SavePath"].ToString(), lst);

        //        }
        //    }
        //    #endregion


        //    lblStatus.Text = "试验完成";
        //    Initialise();
        //    Common.DOgrp[9] = true;
        //    while (Common.AIgrp[5] > 50)
        //    {
        //        Thread.Sleep(100);
        //    }
        //    Common.DOgrp[9] = false;
        //    ControlLock(false);
        //}
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (th.IsAlive)
                {
                    th.Abort();
                }

                Thread th2 = new Thread(new ThreadStart(TestStop));
                th2.Start();
            }
            catch (Exception)
            {
                lblStatus.Text = "试验终止";
            }
        }
        public void TestStop()
        {
            Initialise();
            isChaoFuHe = false;
            //Common.DOgrp[9] = true;
            //while (Common.AIgrp[5] > 50)
            //{
            //    Thread.Sleep(100);
            //}
            //Common.DOgrp[9] = false;
            this.Invoke(new Del(delegate
            {
                lblStatus.Text = "试验终止";

                ControlLock(false);
                BaseTest bt = new BaseTest();
                bt.TestStatus(false);
            }));


        }
        /// <summary>
        /// 试验时，控件上锁
        /// </summary>
        private void ControlLock(bool temp)
        {
            btnStart.Enabled = !temp;
            btnStop.Enabled = temp;
            foreach (var item in listChk)
            {
                item.Enabled = !temp;
            }
            btnProductSelection.Enabled = !temp;
            chkAll.Enabled = !temp;
        }

        #endregion
        private void timLiuLiang_Tick(object sender, EventArgs e)
        {
            try
            {
                //刷新伺服阀值
                labsfkk.Text = Common.AOgrp.ServoValve.ToString() + "%";
                labdykk.Text = Common.AOgrp.SignalVolt.ToString() + "V";
                bool temp = false;
                ucAIControl aicon = new ucAIControl();
                foreach (var item in palAI.Controls)
                {
                    if (item is ucAIControl)
                    {
                        aicon = item as ucAIControl;
                        if (aicon.Index == 99)
                        {
                            temp = true;
                            break;
                        }
                    }
                }
                //刷新流量值
                if (Common.paraconfig != null && temp)
                {
                    double Revise = Common.paraconfig.Revise;              //流量校正系数
                    double Coefficient = Common.paraconfig.Nozzle;         //流量系数
                    double Diameter = Common.paraconfig.NozzleDiameter;    //喷嘴直径
                    double xqwEnvironMentT = Common.WSDgrp[0];  //环境温度
                    double NozzleT = (Common.Nozzlegrp[0] + Common.Nozzlegrp[1]) / 2.0;  //喷嘴前温度1、2
                    double AtmospherekPa = Common.AIgrp[0];  //大气压力（kPa）
                    double Differential = (Common.AIgrp[12] + Common.AIgrp[13]) / 2.0;  //喷嘴压差
                    Common.liuliangRst = (Var.Flux(Coefficient, Diameter, xqwEnvironMentT, NozzleT, AtmospherekPa, Differential) * Revise);
                    string Temp = Common.liuliangRst.ToString("#0.0000");
                    if (double.IsNaN(Convert.ToDouble(Temp)))
                        Common.liuliangRst = 0.00;
                    else
                        Common.liuliangRst = Convert.ToDouble(Temp);
                    aicon.Value = Common.liuliangRst;
                }
                //试验开始计时，试验结束停止
                if (isTestBegin)
                {
                    TimeSpan ts = DateTime.Now - dtTest;
                    BaseTest_TimingChanged(null, Convert.ToInt32(ts.TotalSeconds));
                }
                //检查有没有超时
                if (isChaoFuHe)
                {
                    TimeSpan ts = DateTime.Now - nPS2_Timeout;
                    if (ts.TotalSeconds > 181)
                    {
                        Common.mResultAll.listPS2.Add(ts.TotalSeconds);
                    }
                    ts = DateTime.Now - nPS3_Timeout;
                    if (ts.TotalSeconds > 181)
                    {
                        Common.mResultAll.listPS3.Add(ts.TotalSeconds);
                    }
                    if (Common.mResultAll.listPS2.Count >= 1)
                    {
                        if (Common.mResultAll.listPS2.Max() > 181)
                        {
                            isChaoFuHe = false;
                            Common.DOgrp[0] = true;     //红灯
                            Common.DOgrp[2] = true;     //蜂鸣器
                            Var.MessageInfo("[" + Common.paraconfig.listDIPoint[PS2 - 1].ProductName + "]高电平或低电平的持续时间超过180s");
                            Common.DOgrp[0] = false;     //红灯
                            Common.DOgrp[2] = false;     //蜂鸣器
                            btnStop_Click(this, null);
                        }
                    }
                    if (Common.mResultAll.listPS3.Count >= 1)
                    {
                        if (Common.mResultAll.listPS3.Max() > 181)
                        {
                            isChaoFuHe = false;
                            Common.DOgrp[0] = true;     //红灯
                            Common.DOgrp[2] = true;     //蜂鸣器
                            Var.MessageInfo("[" + Common.paraconfig.listDIPoint[PS3 - 1].ProductName + "]高电平或低电平的持续时间超过180s");
                            Common.DOgrp[0] = false;     //红灯
                            Common.DOgrp[2] = false;     //蜂鸣器
                            btnStop_Click(this, null);
                        }
                    }

                    if (Common.DIgrp[PS2] == true && Common.DIgrp[PS3] == false)
                    {
                        nPS2_3_Timeout = DateTime.Now;
                    }
                    if (Common.DIgrp[PS2] == false && Common.DIgrp[PS3] == true)
                    {
                        nPS2_3_Timeout = DateTime.Now;
                    }

                    ts = DateTime.Now - nPS2_3_Timeout;
                    if (ts.TotalSeconds > 31)
                    {
                        Common.mResultAll.listPS2_3.Add(ts.TotalSeconds);
                    }
                    if (Common.mResultAll.listPS2_3.Count >= 1)
                    {
                        if (Common.mResultAll.listPS2_3.Max() > 30)
                        {
                            isChaoFuHe = false;
                            Common.DOgrp[0] = true;     //红灯
                            Common.DOgrp[2] = true;     //蜂鸣器
                            Var.MessageInfo("同时为高电平或同时为低电平的时间超过30s");
                            Common.DOgrp[0] = false;     //红灯
                            Common.DOgrp[2] = false;     //蜂鸣器
                            btnStop_Click(this, null);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("流量计算错误：" + ex);
            }
        }

        private void DCF_Click(object sender, EventArgs e)
        {
            UIValve val = sender as UIValve;
            bool temp = val.Active;
            Common.DOgrp[val.Tag.ToInt()] = temp;
            val.Active = temp;

        }
        bool isExceute = true;
        bool isNext = false;
        private void btnExceute_Click(object sender, EventArgs e)
        {
            //true：代表连续； false：代表单步；
            if (btnExceute.Tag.ToBool())
            {
                isExceute = false;
                btnExceute.Tag = false;
                btnExceute.Text = "连续执行";
                lblExceute.Text = "执行状态：单步";
                btnNext.Visible = true;

            }
            else
            {
                isExceute = true;
                btnExceute.Tag = true;
                btnExceute.Text = "单步执行";
                lblExceute.Text = "执行状态：连续";
                btnNext.Visible = false;
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            isNext = true;
        }

        private void btnGather_Click(object sender, EventArgs e)
        {
            test.isGather = false;
        }

        /// <summary>
        /// 载入，根据输入的编号查找历史报表
        /// </summary>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            frmLoginRep frm = new frmLoginRep();
            frm.ShowDialog();
            saveRptFile = frm.sPath;
        }

        private void txtElecNO_Leave(object sender, EventArgs e)
        {
            Common.mTestViewModel.ElecNO = txtElecNO.Text;
        }

        private void txtHeadNO_Leave(object sender, EventArgs e)
        {
            Common.mTestViewModel.HeadNO = txtHeadNO.Text;

        }
    }
}
