using System.Data.SqlClient;

namespace demo
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
            "Server=WIN-IIVPL9JM0AG\\SQLEXPRESS; Database=MeatProcessing;Trusted_Connection=True;";

        //public static string connectionString = @"Server=ИМЯ_СЕРВЕРА_ИЛИ_IP;
        //Database=demo;
        //User Id=demo;
        //Password=demo;
        //TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}