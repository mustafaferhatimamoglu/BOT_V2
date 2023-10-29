



using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOT_V2.Operations
{
    internal class Database
    {
        public static string SQLCon = "Data Source=" + "localhost" +
                    "; Initial Catalog=" + "BINANCE_V19" +
                    "; USER ID=" + "sa" +
                    ";PASSWORD=" + "sapass" + "";

        public static DataTable SQL_query(string SQL_Query)//Databaseden verileri datatale olarak döndürür
        {
            using (SqlConnection connect = new SqlConnection(SQLCon + ";Connect Timeout=60;Persist Security Info=True;MultipleActiveResultSets=true;"))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(SQL_Query, connect))
                {
                    try
                    {
                        da.Fill(dt);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("There is already an object named"))
                        {
                            return dt;
                        }
                        else if (ex.Message.Contains("Error converting data type varchar to numeric."))
                        {
                            MessageBox.Show("aaaaaaaaaaaaaaaaaaa");
                            return dt;
                        }
                        else
                        {
                            return dt;
                        }
                    }
                }
            }
        }
    }
}
