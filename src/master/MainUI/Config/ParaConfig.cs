using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Configuration;
using System.Windows.Forms;
using System.ComponentModel;

namespace MainUI.Config
{
    public class ParaConfig : IniConfig
    {
        public ParaConfig()
          : base(Application.StartupPath + "\\config\\Para.ini")
        {
        }
        public ParaConfig(string sectionName)
            : base(Application.StartupPath + "\\config\\Para.ini")
        {
            this.SetSectionName(sectionName);
            Load();
        }
        /// <summary>
        /// 报表名称
        /// </summary>
        [IniKeyName("报表名称")]
        public string RptFile { get; set; }
        /// <summary>
        /// 喷嘴系数
        /// </summary>
        [IniKeyName("喷嘴系数")]
        public double Nozzle { get; set; }
        /// <summary>
        /// 校正系数
        /// </summary>
        [IniKeyName("校正系数")]
        public double Revise { get; set; }

        /// <summary>
        /// 喷嘴直径 mm
        /// </summary>
        [IniKeyName("喷嘴直径")]
        public double NozzleDiameter { get; set; }

        /// <summary>
        /// 信号供电电压 true:24V false:110V
        /// </summary>
        [IniKeyName("信号供电电压")]
        public bool isVolt { get; set; }

        /// <summary>
        /// 风源供电电压
        /// </summary>
        [IniKeyName("风源供电电压")]
        public int Volt { get; set; }
        /// <summary>
        /// 风源供电频率
        /// </summary>
        [IniKeyName("风源供电频率")]
        public int Frequency { get; set; }
        /// <summary>
        /// DI点位集合
        /// </summary>
        public List<DIPoint> listDIPoint { get; set; } = new List<DIPoint>();
        /// <summary>
        /// DO点位集合
        /// </summary>
        public List<DOPoint> listDOPoint { get; set; } = new List<DOPoint>();
        /// <summary>
        /// AI点位集合
        /// </summary>
        public List<AIPoint> listAIPoint { get; set; } = new List<AIPoint>();


        /// <summary>
        /// 性能试验工作压力
        /// </summary>
        [IniKeyName("性能试验工作压力")]
        public int Pressure { get; set; }
        /// <summary>
        /// 满负荷试验时间
        /// </summary>
        [IniKeyName("性能试验时间")]
        public int ProvingMTime { get; set; }

        /// <summary>
        /// 打气伺服阀开口
        /// </summary>
        [IniKeyName("打气伺服阀开口")]
        public double PercentServe { get; set; }
        
    }

    public class DIPoint
    {
        /// <summary>
        /// OPC下标
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 试验台名称
        /// </summary>
        public string TestbedName { get; set; }
        /// <summary>
        /// 产品航插名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isStartUsing { get; set; }
    }
    public class DOPoint
    {
        /// <summary>
        /// OPC下标
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 试验台名称
        /// </summary>
        public string TestbedName { get; set; }
        /// <summary>
        /// 产品航插名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isStartUsing { get; set; }
    }
    public class AIPoint
    {
        /// <summary>
        /// OPC下标
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 试验台名称
        /// </summary>
        public string TestbedName { get; set; }
        /// <summary>
        /// 产品航插名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isStartUsing { get; set; }
    }
}
