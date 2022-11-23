using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace BusMS
{
    class DBConnection
    {
        private static OracleConnection conn;
        public static OracleConnection Connection()
        {
            try
            {
                conn = new OracleConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["Strconn"].ToString();
                //conn.Open();
                //MessageBox.Show("Database open");
                //using connection string to connect Oracle Database
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }


        }

    }
}
