using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.UI;
using System.Data;
using RW.Data;
using RW.Components.Core.BLL;

namespace MainUI.BLL
{
    public class TestRecordBLL : BaseBLL
    {
        public TestRecordBLL(IDbBase database, string connectionString, string tableName)
            : base(database, connectionString, tableName)
        { }

        public override DataTable GetList()
        {
            string sql = string.Format("select * from Record ");
            return this.GetDataTable(sql);
        }



        public int SaveData(string kind, string model, string driver, string testid, string tester, string testtime, string reportPath)
        {
            string sql = string.Format("insert into Record (Kind,Model,Driver,TestID,Tester,TestTime,ReportPath) values ({0},'{1}','{2}','{3}','{4}','{5}','{6}')",
                kind, model, driver, testid, tester, testtime, reportPath);
            return base.Database.ExecuteNonQuery(sql, null);
        }

        public DataTable FindList(string lx, string xh, string bh, string czy, DateTime from, DateTime to)
        {
            string where = "and 1=1 ";
            if (!string.IsNullOrEmpty(lx))
                where += " and kind=" + lx;
            if (!string.IsNullOrEmpty(xh))
                where += " and Model='" + xh + "'";

            if (!string.IsNullOrEmpty(bh))
                where += " and TestID like '" + bh + "'";
            if (!string.IsNullOrEmpty(czy))
                where += " and Tester = '" + czy + "'";

            where += " and testTime>=#" + from.Date + "# and testTime<=#" + to.Date.AddDays(1) + "#";

            where += " order by TestTime desc";

            string sql1 = "select a.ID,a.model,a.driver,a.testid,a.tester,a.testtime,a.reportpath,b.modeltype from Record a,modeltype b where a.kind=b.id " + where;

            return this.GetDataTable(sql1);

        }
        public int DelData(int id)
        {
            string sql = string.Format("delete from Record where id = {0}", id);
            return base.Database.ExecuteNonQuery(sql,null);
        }
    }
}
