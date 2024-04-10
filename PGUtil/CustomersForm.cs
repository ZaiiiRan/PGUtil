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
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
            this.Shown += Form_Shown;
            timer1.Start();
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

            List<List<string>> data = PG.GetFullTable("customers");
            PG.FillTableInDataGridView(data, dataGridView1);
        }

        private void timer1_Tick(object sender, EventArgs e)
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
        private void Form_Shown(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                List<List<string>> data = PG.GetFullTable("customers");
                PG.FillTableInDataGridView(data, dataGridView1);
            }
        }
    }
}
