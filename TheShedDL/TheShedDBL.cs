using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheShedDL
{
    public static class TheShedDBL
    {
        public static SqlConnection GetConnection()
        {
            //Data Source=10.155.200.25;Initial Catalog=ASTONEMAN;User ID=ASTONEMAN

            string myConnectionString = "Data Source=10.155.200.25;TrustServerCertificate=true;Initial Catalog=ASTONEMAN;User ID=ASTONEMAN;Password=prog1121$";
            SqlConnection mySqlConnection = new SqlConnection(myConnectionString);
            return mySqlConnection;
        }
    }
}
