using RW.Components.Core.BLL;
using RW.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.BLL
{
    public class MeasureBLL : BaseBLL
    {
        public MeasureBLL(IDbBase database, string connectionString, string tableName)
            : base(database, connectionString, tableName)
        { }



        public bool Add(MeasureModel model)
        {
            string sql = string.Format("insert into  [Measure]([Number],SignalType,SensorType,SignalMin,SignalMax,MeasTime,ValidTime,MeasNumber) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                model.Number, model.SignalType, model.SensorType, model.SignalMin, model.SignalMax, model.MeasTime, model.ValidTime, model.MeasNumber);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public bool Delete()
        {
            string sql = string.Format("delete from [Measure]");
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public DataTable GetMeasure()
        {
            string sql = "select * from [Measure] order by ID ASC";
            return this.GetDataTable(sql);
        }
        public DataTable GetMeasure(string date)
        {
            string sql = "select * from [Measure] where ValidTime <= #" + date + "# order by ID ASC";
            return this.GetDataTable(sql);
        }

    }
    public class MeasureModel
    {
        public int ID { get; set; }
        /// <summary>
        /// 传感器编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 信号类型
        /// </summary>
        public string SignalType { get; set; }
        /// <summary>
        /// 传感器类型
        /// </summary>
        public string SensorType { get; set; }
        /// <summary>
        /// 信号最小值
        /// </summary>
        public string SignalMin { get; set; }
        /// <summary>
        /// 信号最大值
        /// </summary>
        public string SignalMax { get; set; }
        /// <summary>
        /// 计量时间
        /// </summary>
        public string MeasTime { get; set; }
        /// <summary>
        /// 有效期时间
        /// </summary>
        public string ValidTime { get; set; }
        /// <summary>
        /// 计量证书编号
        /// </summary>
        public string MeasNumber { get; set; }
    }
}
