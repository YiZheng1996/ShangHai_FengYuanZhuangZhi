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
    public class TestItemsBLL : BaseBLL
    {
        public TestItemsBLL(IDbBase database, string connectionString, string tableName)
            : base(database, connectionString, tableName)
        { }



        public bool Add(int Serial, string ItemName, string SavePath, int Modelid)
        {
            string sql = string.Format("insert into  [TestItems](ItemName,Serial,Modelid,SavePath) values('{0}',{1},'{2}','{3}')",
                ItemName, Serial, Modelid, SavePath);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public bool Update(int ID, int Serial, string ItemName, string SavePath, int Modelid)
        {
            string sql = string.Format("update [TestItems] set Serial='{1}',ItemName='{2}',SavePath='{3}' where ID={0}",
               ID, Serial, ItemName, SavePath);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public bool Delete(int id)
        {
            string sql = string.Format("delete from [TestItems] where ID = {0}", id);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public DataTable GetTestItems(int Modelid)
        {
            string sql = "select * from [TestItems]  where Modelid = " + Modelid + " order by Serial ASC";
            return this.GetDataTable(sql);
        }













    }
}
