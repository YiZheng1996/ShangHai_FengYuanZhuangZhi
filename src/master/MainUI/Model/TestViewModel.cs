using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.Model
{
    /// <summary>
    /// 当前测试选择型号
    /// </summary>

    public class TestViewModel
    {
        private int _ModelID;
        /// <summary>
        /// 被试品型号ID
        /// </summary>
        public int ModelID
        {
            get { return _ModelID; }
            set { _ModelID = value; }
        }

        private string _ModelName;
        /// <summary>
        /// 被试品型号
        /// </summary>
        public string ModelName
        {
            get { return _ModelName; }
            set { _ModelName = value; }
        }

        private int _ModelTypeID;
        /// <summary>
        /// 被试品类型ID
        /// </summary>
        public int ModelTypeID
        {
            get { return _ModelTypeID; }
            set { _ModelTypeID = value; }
        }

        private string _ModelType;
        /// <summary>
        /// 被试品类型
        /// </summary>
        public string ModelType
        {
            get { return _ModelType; }
            set { _ModelType = value; }
        }

        private string _Mark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Mark
        {
            get { return _Mark; }
            set { _Mark = value; }
        }

        private string _TestNO;
        /// <summary>
        /// 制造编号
        /// </summary>
        public string TestNO
        {
            get { return _TestNO; }
            set { _TestNO = value; }
        }

        private string _ElecNO;
        /// <summary>
        /// 电机序列号
        /// </summary>
        public string ElecNO
        {
            get { return _ElecNO; }
            set { _ElecNO = value; }
        }

        private string _HeadNO;
        /// <summary>
        /// 机头序列号
        /// </summary>
        public string HeadNO
        {
            get { return _HeadNO; }
            set { _HeadNO = value; }
        }

        private string _User;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }


 

      



    }
}

