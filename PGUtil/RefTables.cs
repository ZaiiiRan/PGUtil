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

        private void backMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.MainForm.Activate();
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

        private void workersMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("workers", "Работники");
            form.Show();
        }

        private void worksMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("works", "Работы");
            form.Show();
        }

        private void specializationsMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("specializations", "Специализации");
            form.Show();
        }

        private void worksForSpecializationsMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("works_for_specializations", "Специализации и их работы");
            form.Show();
        }

        private void specializationsForWorkersMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("specializations_for_workers", "Работники и их специализации");
            form.Show();
        }

        private void orderStatusesMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("order_statuses", "Статусы заказов");
            form.Show();
        }

        private void paymentTypesMenuItem_Click(object sender, EventArgs e)
        {
            TableForm form = new TableForm("payment_types", "Типы оплаты");
            form.Show();
        }
    }
}
