using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SHARED.Common.Utils
{
    public static class SqlConnectionHelper
    {
        public static bool IsServerConnected(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static async Task<bool> IsServerConnectedAsync(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        } 
    }
}
