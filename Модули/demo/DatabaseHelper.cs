using System.Data.SqlClient;

namespace demo
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString =
            "Server=WIN-IIVPL9JM0AG\\SQLEXPRESS; Database=MeatProcessing;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}