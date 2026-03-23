using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.Data;
using System.Data;
using RW.Components.Core.BLL;

namespace MainUI.BLL
{
    public class ModelTypeBLL : BaseBLL
    {
        public ModelTypeBLL(IDbBase database, string connectionString, string tableName)
            : base(database, connectionString, tableName)
        { }

        public DataTable GetAllModelType()
        {

            string sql = "";
            sql = "select ID, ModelType from ModelType order by ID ";
            return this.GetDataTable(sql);
        }
    }
}
