using IntervalReport.Models;
using System;
using System.Data;
using System.Data.SqlClient;
namespace IntervalReport.DataAccessLayer
{
    /// <summary>
    /// this class used write ADO.net metthod to connect SQL server for CRUD operations
    /// </summary>
    public class DataAccess
    { 
        /// <summary>
        /// This property hold the database connection string 
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return Convert.ToString("Data Source=VENKATESWARAO\\VENKATESH;Initial Catalog=Test;persist security info=True;Integrated Security=SSPI;");
            }
        }
        /// <summary>
        /// this method can be verified connection open and close state 
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                 return con;
            }
        }
        /// <summary>
        /// this method used to retreived the data using store procedure
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteQuery(string sp,  CommandType commandType)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection(ConnectionString);
            using (SqlCommand com = new SqlCommand(sp, con))
            {
                com.CommandType = commandType;
                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    reader = com.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    ErrorDetails errorDetails = new ErrorDetails();
                    errorDetails.methodname = Convert.ToString("ExecuteQuery ") + System.Environment.NewLine;
                    errorDetails.methodname += "SP Name: " + sp;
                    errorDetails.errormessage = ex.Message;
                    var trace = new System.Diagnostics.StackTrace(ex);
                    errorDetails.stacktrace = Convert.ToString(trace);
                    //Log the exception here
                }
            }
            return reader;
        }
    }
}


