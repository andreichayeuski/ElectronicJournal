using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SHARED.Common.Extensions
{
    public static class DbExtensions
    {
        public static ColumnInfo[] GetColumnInfos(this SqlConnection @this, string tableName)
        {
            var columns = GetColumnInfosInternal(@this, tableName);
            if (!columns.Any())
                throw new Exception(string.Format("Table with name {0} can't be found", tableName));

            return columns;
        }

        private static ColumnInfo[] GetColumnInfosInternal(this SqlConnection @this, string tableName)
        {
            var command = CreateReadColumnsCommand(@this, tableName);
            using (var reader = command.ExecuteReader())
            {
                var rawEntries = ReadRawValuesEntriesData(reader);
                return rawEntries.ToArray();
            }
        }

        private static SqlCommand CreateReadColumnsCommand(SqlConnection connection, string tableName)
        {
            var columnInfo =
                "select COLUMN_NAME, DATA_TYPE from information_schema.columns  where table_name = @tableName";
            var columnInfoCommand = connection.CreateCommand();
            columnInfoCommand.CommandType = CommandType.Text;
            columnInfoCommand.Parameters.Add(new SqlParameter("@tableName", SqlDbType.VarChar) { Value = tableName });
            columnInfoCommand.CommandText = columnInfo;

            return columnInfoCommand;
        }

        private static IEnumerable<ColumnInfo> ReadRawValuesEntriesData(IDataReader reader)
        {
            var columnName = reader.GetOrdinal("COLUMN_NAME");
            var dataType = reader.GetOrdinal("DATA_TYPE");

            while (reader.Read())
            {
                var nameColumn = reader.GetString(columnName);
                var typeColumn = reader.GetString(dataType);

                yield return new ColumnInfo
                {
                    Name = nameColumn,
                    DataType = typeColumn
                };
            }
        }
    }

    public class ColumnInfo
    {
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}