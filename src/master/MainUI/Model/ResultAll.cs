using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Model
{
    public class ResultAll
    {
        public Dictionary<string, List<string>> dic { get; set; } = new Dictionary<string, List<string>>();

        /// <summary>
        /// PS2亮的时间
        /// </summary>
        public List<double> listPS2 { get; set; } = new List<double>();
        /// <summary>
        /// PS3亮的时间
        /// </summary>
        public List<double> listPS3 { get; set; } = new List<double>();

        /// <summary>
        /// PS2和PS3一起亮的时间
        /// </summary>
        public List<double> listPS2_3 { get; set; } = new List<double>();
        /// <summary>
        /// 是否油温过热
        /// </summary>
        public List<bool> listIsTemp { get; set; } = new List<bool>();
        /// <summary>
        /// 是否电机过载
        /// </summary>
        public List<bool> listIsOverload { get; set; } = new List<bool>();
        /// <summary>
        /// 时间节点
        /// </summary>
        public List<string> listTime { get; set; } = new List<string>();
    }
}
