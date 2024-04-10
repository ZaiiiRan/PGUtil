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
    public partial class DataTables : Form
    {
        public DataTables()
        {
            InitializeComponent();
            timer1.Start();
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

        private void BackMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.MainForm.Activate();
        }

        private void customersMenuItem_Click(object sender, EventArgs e)
        {
            CustomersForm form = new CustomersForm();
            form.Show();
        }

        private void ordersMenuItem_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm();
            form.Show();
        }
    }
}
