using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace PGUtil
{
    public partial class RefTables : Form
    {
        public RefTables()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.MainForm.Activate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PG.CheckConnection()) connectionStatusLabel.Text = "подключено";
            else connectionStatusLabel.Text = "не подключено";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }

            List<List<string>> list = PG.GetTable("works");

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            foreach(string str in list[0]) 
            {
                dataGridView1.Columns.Add(str, str);
            }
            bool skip = false;
            foreach(List<string> l in list)
            {
                if (skip)
                {
                    string[] v = new string[l.Count];
                    int k = 0;
                    foreach (string str in l)
                    {
                        v[k] = str;
                        k++;
                    }
                    dataGridView1.Rows.Add(v);
                }
                skip = true;
            }

        }
    }
}
