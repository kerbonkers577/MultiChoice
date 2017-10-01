using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MultipleChoiceLibrary
{
    class DataAccess
    {

        
        
        //Database connection testing method
        public void TestConnection(string connectionString, SqlConnection dbConn)
        {
            try
            {
                dbConn = new SqlConnection(connectionString);
                dbConn.Open();
                Console.WriteLine("Database connection established");
                Console.ReadKey();
                dbConn.Close();
            }
            catch(SqlException e)
            {
                Console.WriteLine("SQL connection failed " + e.Message);
            }
        }


        //Student table access
        //StudentAnswer table access
        //Question table access
        //Teacher table access
    }
}
