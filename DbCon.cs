
using System.Data.SqlClient;

namespace CrudWithDataTablesUsingAdo.net
{
    public class DbCon
    {
      public static SqlConnection GetCon()
        {
            string dbcs = "Data Source=ZEESHANN-LAP\\SQLEXPRESS;Initial Catalog=StudentDb;Integrated Security=true;";
            SqlConnection con = new SqlConnection(dbcs);
            return con;
        }

    }
}
