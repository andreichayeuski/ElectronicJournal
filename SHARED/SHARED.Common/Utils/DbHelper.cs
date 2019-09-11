using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SHARED.Common.Utils
{
    public class DbHelper
    {
        public static List<string> GetDatabaseTables(string connection, string database)
        {
            using (var conn = new SqlConnection(connection))
            {
                var query =
                    @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_CATALOG=@database";

                conn.Open();
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("database", SqlDbType.NVarChar) {Value = database});

                var dr = cmd.ExecuteReader();

                var resultList = new List<string>();

                // Loop through the rows and output the data
                while (dr.Read())
                    resultList.Add((string) dr["TABLE_NAME"]);

                return resultList;
            }
        }

        /// <summary>
        ///     Экспортирует возвращенный набор из БД в CSV
        /// </summary>
        /// <param name="connection">строка подключения</param>
        /// <param name="query">запрос на данные</param>
        /// <param name="encoding">кодировка</param>
        /// <returns>набор байт</returns>
        public static byte[] ExportSqlToCsv(string connection, string query, Encoding encoding)
        {
            using (var conn = new SqlConnection(connection))
            using (var ms = new MemoryStream())
            using (var fs = new StreamWriter(ms, encoding))
            {
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                var dr = cmd.ExecuteReader();

                // Loop through the fields and add headers
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    var name = dr.GetName(i);
                    if (name.Contains(","))
                        name = "\"" + name + "\"";

                    fs.Write(name + ";");
                }
                fs.WriteLine();

                var k = 0;

                // Loop through the rows and output the data
                while (dr.Read())
                {
                    for (var i = 0; i < dr.FieldCount; i++)
                    {
                        var value = dr[i].ToString();
                        if (value.Contains(","))
                            value = "\"" + value + "\"";

                        fs.Write(value + ";");
                    }
                    fs.WriteLine();
                    k++;
                }

                return ms.ToArray();
            }
        }
    }
}