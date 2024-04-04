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

        public static List<List<string>> GetTable(string tableName)
        {
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * from public." + tableName + ";";
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
