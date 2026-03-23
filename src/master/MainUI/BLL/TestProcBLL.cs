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
    public class TestProcBLL : BaseBLL
    {
        public TestProcBLL(IDbBase database, string connectionString, string tableName)
            : base(database, connectionString, tableName)
        { }

        public bool Add(int TestItemId, int Serial, string Operate, string Point, string Decide, string TestValue)
        {
            string sql = string.Format("insert into  [TestProc](TestItemId,Serial,Operate,Point,Decide,TestValue) values('{0}',{1},'{2}','{3}','{4}','{5}')",
                TestItemId, Serial, Operate, Point, Decide, TestValue);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public bool Update(int ID, int Serial, string Operate, string Point, string Decide, string TestValue)
        {
            string sql = string.Format("update [TestProc] set Serial='{1}',Operate='{2}',Point='{3}',Decide='{4}',TestValue='{5}' where ID={0}",
               ID, Serial, Operate, Point, Decide, TestValue);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public bool Delete(int id)
        {
            string sql = string.Format("delete from [TestProc] where ID = {0}", id);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }
        public bool DeleteByTestItemId(int TestItemId)
        {
            string sql = string.Format("delete from [TestProc] where TestItemId = {0}", TestItemId);
            return base.Database.ExecuteNonQuery(sql, null) >= 1 ? true : false;
        }

        public DataTable GetTestProcAll(int Modelid)
        {
            string sql = "select * from [TestProc]  where TestItemId = " + Modelid + " order by Serial ASC";
            return this.GetDataTable(sql);
        }
        public DataTable GetTestProc(int ID)
        {
            string sql = "select * from [TestProc]  where ID = " + ID + " order by Serial ASC";
            return this.GetDataTable(sql);
        }

    }
}
