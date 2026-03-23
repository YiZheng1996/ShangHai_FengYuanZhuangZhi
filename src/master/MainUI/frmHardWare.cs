using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainUI.Modules;
using RW.Modules;
using System.Threading;
using RW.EventLog;
using MainUI.Config;
using System.Diagnostics;
using MainUI.Procedure;

namespace MainUI
{
    public partial class frmHardWare : Form
    {
        Dictionary<int, UCCalibration> dicAI = new Dictionary<int, UCCalibration>();
        Dictionary<int, UCCalibration> dicAO = new Dictionary<int, UCCalibration>();
        AIGrp AIgrp = Common.AIgrp;
        AOGrp AOgrp = Common.AOgrp;
        PLCCalibration Plcc = Common.plcc;
        public frmHardWare()
        {
            InitializeComponent();
            SetDic();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmHardWare_Load(object sender, EventArgs e)
        {
            InitZeroGain();
            timer1.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in dicAO)
                {
                    switch (item.Key)
                    {
                        case 2:
                            dicAO[item.Key].Value = AOgrp.SignalVolt;
                            break;
                        case 3:
                            dicAO[item.Key].Value = AOgrp.ServoValve;
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < AIgrp.AIList.Count(); i++)
                {
                    if (dicAI.ContainsKey(i))
                    {
                        dicAI[i].Value = AIgrp[i];
                    }
                }
            }
            catch (Exception ex)
            {
                Var.MessageError ("刷新AIO发生了错误，原因：" + ex.Message);
            }
        }
        /// <summary>
        /// 初始化零点增益
        /// </summary>
        public void InitZeroGain()
        {
            try
            {
                InitZeroGainAI();
                InitZeroGainAO();
            }
            catch (Exception)
            {
                Var.MessageError("硬件校准界面，初始化零点增益有误。");
            }
        }
        /// <summary>
        /// AI初始化零点增益
        /// </summary>
        private void InitZeroGainAI()
        {
            foreach (var item in dicAI)
            {
                item.Value.InitData();
                item.Value.Submit();
            }
        }

        /// <summary>
        /// AO初始化零点增益
        /// </summary>
        private void InitZeroGainAO()
        {
            foreach (var item in dicAO)
            {
                item.Value.InitData();
                item.Value.Submit();
            }
        }
        /// <summary>
        /// 绑定键值对
        /// </summary>
        private void SetDic()
        {
            //AI
            dicAI.Clear();
            foreach (Control item in grpAI.Controls)
            {
                if (item is UCCalibration)
                {
                    UCCalibration tmp = item as UCCalibration;
                    dicAI.Add(tmp.Index, tmp);
                }
            }
            //AO 
            dicAO.Clear();
            foreach (Control item in grpAO.Controls)
            {
                if (item is UCCalibration)
                {
                    UCCalibration tmp = item as UCCalibration;
                    dicAO.Add(tmp.Index, tmp);
                }
            }
        }
        //AI 通用的
        private void ucCalibration_AI_Submited(object sender, SubmitArgs e)
        {
            try
            {
                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;
                Plcc.SetAIZero(e.Index, e.Zero);
                Plcc.SetAIGain(e.Index, e.Gain);
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_AI_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }
        //AO 通用的
        private void ucCalibration_AO_Submited(object sender, SubmitArgs e)
        {
            try
            {
                UCCalibration cuCur = sender as UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;
                Plcc.SetAOZero(e.Index, e.Zero);
                Plcc.SetAOGain(e.Index, e.Gain);
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_AO_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }

    }
}
