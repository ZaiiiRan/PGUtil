using PGUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGUtil
{
    public partial class WorkersForm : Form
    {
        public WorkersForm()
        {
            InitializeComponent();
            this.Shown += WorkersForm_Shown;
            timer1.Start();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }

            List<List<string>> data = PG.GetTable("workers");
            PG.FillTableInDataGridView(data, dataGridView1);
        }

        private void WorkersForm_Shown(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                List<List<string>> data = PG.GetTable("workers");
                PG.FillTableInDataGridView(data, dataGridView1);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                connectionStatusLabel.ForeColor = Color.Green;
                connectionStatusLabel.Text = "подключено";
            }
            else
            {
                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "не подключено";
            }
        }
    }
}
