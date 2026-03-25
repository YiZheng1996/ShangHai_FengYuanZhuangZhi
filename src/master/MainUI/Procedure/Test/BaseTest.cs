using MainUI.Config;
using Sunny.UI;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MainUI.Procedure
{
    public class BaseTest
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern uint GetTickCount();
        public static ParaConfig para = new ParaConfig();

        public delegate void TestStateHandler(bool isTesting);
        public static event TestStateHandler TestStateChanged;

        public delegate void TipsHandler(object sender, object info);
        public static event TipsHandler TipsChanged;

        public delegate void TipsTiming(object sender, int s);
        public static event TipsTiming TimingChanged;

        public delegate bool WaitCallback();

        public delegate void WaitTicked(int tick);
        public event WaitTicked WaitTick;

        public static event WaitStepTicked WaitStepTick;
        public delegate void WaitStepTicked(int tick, string stepName);

        public delegate void Del();

        public bool IsTesting { get; set; }

        public void Delay(double seconds)
        {
            Delay(seconds, 100, delegate { return true; });
        }
        public bool Delay(double sj, int interval, WaitCallback wait)
        {
            int t = 0;
            double timeout = sj * 1000;
            // int timeout = sj * 100; // 模拟测试，暂时把延时时间改短。
            while (t <= timeout && IsTesting && wait())
            {
                t += interval;
                Thread.Sleep(interval);
                if (WaitTick != null) WaitTick(t);
            }
            return t > timeout;
        }
        public void Delay(int seconds, string waitName)
        {
            Delay(seconds, 100, delegate { return true; }, waitName);
        }
        public bool Delay(int sj, int interval, WaitCallback wait, string waitName)
        {
            int t = 0;
            int timeout = sj * 1000;
            // int timeout = sj * 100; // 模拟测试，暂时把延时时间改短。
            while (t <= timeout && wait() && IsTesting)
            {
                t += interval;
                Thread.Sleep(interval);
                if (WaitStepTick != null) WaitStepTick(t, waitName);
            }
            return t > timeout;
        }
        /// <summary>
        /// 显示已执行的时间
        /// </summary>
        public void lblTime(int time, string waitName)
        {
            if (WaitStepTick != null) WaitStepTick(time, waitName);
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public void TxtTips(object str)
        {
            if (TipsChanged != null)
                TipsChanged(this, str);
        }
        public void TxtTiming(int s)
        {
            if (TimingChanged != null)
            {
                TimingChanged(this, s);
            }
        }



        #region 通用弹窗（线程安全、置顶）

        // 使用场景
        // 场景1：是否判定，直接写报表
        // ShowConfirmAndWrite("WGJC", "外观检查是否合格？");

        // 场景2：纯提示，不写值
        // ShowInfo("请检查油路接头，确认无渗油后点击确定");

        // 场景3：是否判定，拿返回值自己处理
        // bool isOk = ShowConfirm("绝缘电阻是否大于100MΩ？");
        // if (!isOk) return false;  // 不合格直接终止

        // 场景4：手动输入值，直接写报表
        // ShowInputAndWrite("JDDC", "请输入绝缘电阻测量值(MΩ)：");

        // 场景5：手动输入值，拿回来自己处理再写
        // string val = ShowInput("请输入实测噪声值(dB)：");
        // if (val != null)
        // {
        //     double noise = double.Parse(val);
        //         Common.mResultAll.dicReport["ZS"] = noise.ToString("F1");
        //     Common.mResultAll.dicReport["ZS_PD"] = noise <= 78 ? "合格" : "不合格";
        // }

        /// <summary>
        /// 保证弹窗在父窗口之上
        /// </summary>
        /// <param name="message">提示消息</param>
        /// <param name="title">标题栏</param>
        /// <returns></returns>
        protected DialogResult ShowDialog(string message, string title = "系统提示")
        {
            DialogResult result = DialogResult.No;
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                result = MessageBox.Show(
                    Application.OpenForms[0],
                    message,
                    title,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
            }));
            return result;
        }

        /// <summary>
        /// 提示弹窗（仅确认，无返回值）
        /// </summary>
        protected void ShowInfo(string message, string title = "系统提示")
        {
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                MessageBox.Show(Application.OpenForms[0], message, title,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }

        /// <summary>
        /// 是否判定弹窗，返回 true=是 / false=否
        /// </summary>
        protected bool ShowConfirm(string message, string title = "系统提示")
        {
            DialogResult result = DialogResult.No;
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                result = MessageBox.Show(Application.OpenForms[0], message, title,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }));
            return result == DialogResult.Yes;
        }

        //protected bool ShowConfirm(string message, string title = "系统提示")
        //{
        //    bool result = false;
        //    Application.OpenForms[0].Invoke(new Action(() =>
        //    {
        //        result = UIMessageBox.Show(message, title, UIStyle.Blue, UIMessageBoxButtons.OKCancel, true, true);
        //    }));
        //    return result;
        //}

        /// <summary>
        /// 是否判定弹窗，直接写 dicReport
        /// 问题应正向描述，如"外观检查是否合格？"，是=OK，否=NG
        /// </summary>
        protected void ShowConfirmAndWrite(string reportKey, string message,
            string title = "系统提示", string okValue = "OK", string ngValue = "NG")
        {
            bool yes = ShowConfirm(message, title);
            Common.mResultAll.dicReport[reportKey] = yes ? okValue : ngValue;
        }

        /// <summary>
        /// 输入弹窗，返回用户输入的字符串（取消返回 null）
        /// </summary>
        protected string ShowInput(string prompt, string title = "请输入", string defaultValue = "")
        {
            string inputValue = null;
            Application.OpenForms[0].Invoke(new Action(() =>
            {
                using (Form inputForm = new Form())
                {
                    // 窗体 - 与 frmSetOutValue 一致
                    inputForm.Text = title;
                    inputForm.StartPosition = FormStartPosition.CenterScreen;
                    inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    inputForm.ControlBox = false;
                    inputForm.BackColor = Color.FromArgb(243, 249, 255);
                    inputForm.Font = new Font("宋体", 10f);

                    // GroupBox - 微软雅黑 11F，标题居中
                    GroupBox grp = new GroupBox()
                    {
                        Text = prompt,
                        Font = new Font("微软雅黑", 11f),
                        Location = new Point(12, 14),
                        Size = new Size(474, 105)
                    };

                    // TextBox - 微软雅黑 26F，居中
                    TextBox txt = new TextBox()
                    {
                        Font = new Font("微软雅黑", 26f),
                        Location = new Point(60, 37),
                        Size = new Size(354, 54),
                        Text = defaultValue,
                        TextAlign = HorizontalAlignment.Center
                    };
                    grp.Controls.Add(txt);

                    // 确定按钮 - 微软雅黑 11F，120x40
                    Button btnOk = new Button()
                    {
                        Text = "确定",
                        Font = new Font("微软雅黑", 11f),
                        Size = new Size(120, 40),
                        Location = new Point(225, 139),
                        Cursor = Cursors.Hand,
                        DialogResult = DialogResult.OK
                    };

                    // 取消按钮
                    Button btnCancel = new Button()
                    {
                        Text = "取消",
                        Font = new Font("微软雅黑", 11f),
                        Size = new Size(120, 40),
                        Location = new Point(366, 139),
                        Cursor = Cursors.Hand,
                        DialogResult = DialogResult.Cancel
                    };

                    inputForm.ClientSize = new Size(501, 201);
                    inputForm.Controls.AddRange(new Control[] { grp, btnOk, btnCancel });
                    inputForm.AcceptButton = btnOk;
                    inputForm.CancelButton = btnCancel;

                    txt.Focus();
                    txt.SelectAll();

                    if (inputForm.ShowDialog(Application.OpenForms[0]) == DialogResult.OK)
                    {
                        inputValue = txt.Text;
                    }
                }
            }));
            return inputValue;
        }

        /// <summary>
        /// 输入弹窗，直接写 dicReport
        /// </summary>
        protected bool ShowInputAndWrite(string reportKey, string prompt, string title = "请输入")
        {
            string value = ShowInput(prompt, title);
            if (value != null)
            {
                Common.mResultAll.dicReport[reportKey] = value;
                return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 试验状态
        /// </summary>
        public void TestStatus(bool isTest)
        {
            IsTesting = isTest;
            if (TestStateChanged != null)
            {
                TestStateChanged(isTest);
            }
        }

        /// <summary>
        /// 在子类中执行试验过程
        /// </summary>
        public virtual bool Execute() { return true; }

        /// <summary>
        /// 在子类中执行试验过程
        /// </summary>
        public virtual string Execute2() { return ""; }

        /// <summary>
        /// 试验项点初始化
        /// </summary>
        public virtual void Init() { }

    }
}
