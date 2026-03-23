using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MainUI.Model
{
    // 极简版储风缸压力闭环控制器
    public class SimplePressureController
    {
        // 核心参数（可直接改）
        private const double TargetPressure = 900; // 目标压力 kPa（表压，直接用）
        private readonly Timer _controlTimer;     // 调节定时器（每200ms调一次）

        //PID 参数调试（核心！按这个顺序调）
        //先关积分、微分：把_ki=0、_kd=0，只留_kp；
        //若_kp=0.8压力稳不住，慢慢增大（如 1.0、1.5），直到压力在 900kPa 附近小幅震荡；
        //开积分消误差：慢慢调大_ki（从 0.01→0.05→0.1），直到压力震荡消失，稳定在 900kPa；
        //开微分防超调：若压力超过 900kPa 又回落（超调），调大_kd（从 0.05→0.1→0.2），抑制超调
        // PID参数（先按这个值试，不行再微调）
        private double _kp = 0.8;  // 比例系数（核心，先调这个）
        private double _ki = 0; // 积分系数（消除误差）
        private double _kd = 0;  // 微分系数（防超调）

        // 状态变量
        private double _currentPressure = 0; // 当前压力（kPa）
        private double _lastError = 0;       // 上一次偏差
        private double _integral = 0;        // 积分累计
        private double _currentOpening = 0;  // 当前阀门开度（0-100%）

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="controlIntervalMs">调节周期（默认200ms）</param>
        public SimplePressureController(int controlIntervalMs = 200)
        {
            _controlTimer = new Timer(controlIntervalMs);
            _controlTimer.Elapsed += (s, e) => AdjustValve(); // 定时调节阀门
        }

        /// <summary>
        /// 【关键】更新传感器压力（对接硬件用）
        /// </summary>
        /// <param name="pressure">压力传感器读取的表压（kPa）</param>
        public void UpdatePressure(double pressure)
        {
            _currentPressure = Common.AIgrp[2];
        }

        /// <summary>
        /// 启动闭环控制
        /// </summary>
        public void Start()
        {
            _controlTimer.Start();
        }

        /// <summary>
        /// 停止控制
        /// </summary>
        public void Stop()
        {
            _controlTimer.Stop();
        }

        /// <summary>
        /// PID核心调节逻辑（自动找合适开度）
        /// </summary>
        private void AdjustValve()
        {
            // 1. 计算压力偏差（目标 - 当前）
            double error = TargetPressure - _currentPressure;

            // 2. PID计算（极简版，无复杂限幅也能用）
            _integral += error * _controlTimer.Interval / 1000; // 积分（时间转秒）
            double derivative = (error - _lastError) / (_controlTimer.Interval / 1000); // 微分
            double adjust = _kp * error + _ki * _integral + _kd * derivative; // 开度调整量

            // 3. 更新开度（限制0-100%）
            _currentOpening = Clamp(_currentOpening + adjust, 0, 100);

            // 4. 输出开度到执行器（对接硬件的核心行）
            SetValveOpening(_currentOpening);

            // 调试输出（可选）
            Console.WriteLine($"当前压力：{_currentPressure:F1}kPa | 开度：{_currentOpening:F1}% | 偏差：{error:F1}kPa");

            // 5. 保存本次偏差，用于下次微分计算
            _lastError = error;
        }

        /// <summary>
        /// 【对接硬件】设置阀门开度（需根据执行器改）
        /// </summary>
        /// <param name="opening">开度（0-100%）</param>
        private void SetValveOpening(double opening)
        {
            Common.AOgrp.ServoValve = opening;
        }

        // 测试用（直接运行看效果）
        public void Main()
        {
            try
            {
                var controller = new SimplePressureController();
                controller.Start();

                // 模拟压力传感器数据（实际替换为硬件读取）
                double simPressure = 850; // 初始压力850kPa
                var simTimer = new Timer(200);
                simTimer.Elapsed += (s, e) =>
                {
                    controller.UpdatePressure(simPressure);
                    // 模拟压力随开度变化（接近目标时波动减小）
                    simPressure += (900 - simPressure) * 0.05 - (controller._currentOpening * 0.1);
                };
                simTimer.Start();

                Console.WriteLine("按任意键停止...");
                Console.ReadKey();
                controller.Stop();
                simTimer.Stop();
            }
            catch (Exception ex)
            {
                Var.MessageError(ex.Message);
            }
        }
        double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        private void AdjustValve2()
        {
            if (_currentPressure > 900) _currentOpening += 1; // 压力高，开大1%
            if (_currentPressure < 900) _currentOpening -= 1; // 压力低，关小1%
            _currentOpening = Clamp(_currentOpening, 0, 100);
            SetValveOpening(_currentOpening);
        }
    }
}
