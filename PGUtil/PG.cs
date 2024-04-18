using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PGUtil
{
    internal class PG
    {
        private static NpgsqlConnection connection = null;

        public static void OpenConnection(string host, string port, string user, string pass)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                connection.Dispose();
            }
            connection = new NpgsqlConnection(@"Server=" + host + ";Port=" + port + ";User Id=" + user + ";Password=" + pass +
                ";Database=photo_workshop");
            connection.Open();
            if (connection.State == ConnectionState.Open) MessageBox.Show("Подключено!");
            else MessageBox.Show("Не подключено!");
        }

        public static bool CheckConnection()
        {
            return connection != null && connection.State == ConnectionState.Open;
        }
        public static void Insert (string tableName, string fields, string value)
        {
            NpgsqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"INSERT INTO {tableName} ({fields}) VALUES ({value});";
            cmd.ExecuteNonQuery();
        }
        public static void Update(string tableName, string id, string idColumnName, string values)
        {
            NpgsqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"UPDATE {tableName} SET {values} WHERE {idColumnName} = {id};";
            cmd.ExecuteNonQuery();
        }
        public static void Delete(string tableName, string id, string idColumnName)
        {
            NpgsqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"DELETE FROM {tableName} WHERE {idColumnName} = {id};";
            cmd.ExecuteNonQuery();
        }

        public static List<List<string>> GetTableWithCondition(string tableName, string condition)
        {
            string query = "SELECT * FROM " + tableName + " WHERE " + condition + ";";
            return SendQuery(query);
        }
        public static List<List<string>> GetTableForPage(string tableName, int page, int recordsPerPage)
        {
            int offset = (page - 1) * recordsPerPage;
            string query = $"SELECT * FROM {tableName} LIMIT {recordsPerPage} OFFSET {offset};";
            return SendQuery(query);
        }
        public static List<List<string>> GetTableWithConditionForPage(string tableName, string condition, int page, int recordsPerPage)
        {
            int offset = (page - 1) * recordsPerPage;
            string query = $"SELECT * FROM {tableName} WHERE {condition} LIMIT {recordsPerPage} OFFSET {offset};";
            return SendQuery(query);
        }
        public static int GetTotalRecordsCount(string tableName)
        {
            string query = $"SELECT COUNT(*) FROM {tableName};";
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            int totalRecords = Convert.ToInt32(command.ExecuteScalar());
            return totalRecords;
        }
        public static int GetTotalRecordsWithConditionCount(string tableName, string condition)
        {
            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {condition};";
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            int totalRecords = Convert.ToInt32(command.ExecuteScalar());
            return totalRecords;
        }

        public static List<List<string>> SendQuery(string sql)
        {
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = sql;
            NpgsqlDataReader dt = command.ExecuteReader(CommandBehavior.Default);
            bool addNames = false;
            List<List<string>> table = new List<List<string>>();
            while (dt.Read())
            {
                try
                {
                    if (!addNames)
                    {
                        List<string> titles = new List<string>();
                        for (int i = 0; i < dt.FieldCount; i++)
                        {
                            titles.Add(dt.GetName(i));
                        }
                        table.Add(titles);
                        addNames = true;
                    }
                    List<string> dataList = new List<string>();
                    for (int i = 0; i < dt.FieldCount; i++)
                    {
                        dataList.Add(dt.GetValue(i).ToString());
                    }
                    table.Add(dataList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dt.Close();
            return table;
        }

        public static void FillTableInDataGridView(List<List<string>> data, DataGridView grid)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            
            foreach (string columnTitle in data[0])
            {
                grid.Columns.Add(columnTitle, columnTitle);
            }
            for (int i = 1; i < data.Count; i++)
            {
                string[] row = new string[data[i].Count];
                int columnNumber = 0;
                foreach (string cellData in data[i])
                {
                    row[columnNumber] = cellData;
                    columnNumber++;
                }
                grid.Rows.Add(row);
            }
        }
    }
}
