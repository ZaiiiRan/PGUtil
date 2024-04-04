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

        public static void OpenConnection(string host, string port, string user, string pass, string db)
        {
            if (connection != null)
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                connection.Dispose();
            }
            connection = new NpgsqlConnection(@"Server=" + host + ";Port=" + port + ";User Id=" + user + ";Password=" + pass +
                ";Database=" + db);
            connection.Open();
            if (connection.State == ConnectionState.Open) MessageBox.Show("Подключено!");
            else MessageBox.Show("Не подключено!");
        }

        public static bool CheckConnection()
        {
            return connection != null;
        }

        public static List<List<string>> GetTable(string tableName)
        {
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * from public." + tableName + ";";
            NpgsqlDataReader dt = command.ExecuteReader(CommandBehavior.Default);
            bool addNames = false;
            List<List<string>> list = new List<List<string>>();
            while (dt.Read())
            {
                try
                {
                    if (!addNames)
                    {
                        List<string> listNames = new List<string>();
                        for (int i = 0; i < dt.FieldCount; i++)
                        {
                            listNames.Add(dt.GetName(i));
                        }
                        list.Add(listNames);
                        addNames = true;
                    }
                    List<string> dataList = new List<string>();
                    for (int i = 0; i < dt.FieldCount; i++)
                    {
                        dataList.Add(dt.GetValue(i).ToString());
                    }
                    list.Add(dataList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dt.Close();
            return list;
        }
    }
}
