using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class DB
    {
        SqlConnection SqlConnection = new SqlConnection("Data Source=localhost; Initial Catalog=OSNOVA; Integrated Security=True");

        public void OpenConnection()
        {
            SqlConnection.Open();
        }
        public void CloseConnection()
        {
            SqlConnection.Close();
        }

        public SqlConnection GetConnection()
        {
            return SqlConnection;
        }
    }
}
