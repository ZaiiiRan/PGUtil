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
            WorkersForm form = new WorkersForm();
            form.Show();
        }

        private void worksMenuItem_Click(object sender, EventArgs e)
        {
            WorksForm form = new WorksForm();
            form.Show();
        }

        private void specializationsMenuItem_Click(object sender, EventArgs e)
        {
            SpecializationsForm form = new SpecializationsForm();
            form.Show();
        }

        private void worksForSpecializationsMenuItem_Click(object sender, EventArgs e)
        {
            WorksForSpecializationsForm form = new WorksForSpecializationsForm();
            form.Show();
        }

        private void specializationsForWorkersMenuItem_Click(object sender, EventArgs e)
        {
            SpecializationsForWorkersForm form = new SpecializationsForWorkersForm();
            form.Show();
        }

        private void orderStatusesMenuItem_Click(object sender, EventArgs e)
        {
            OrderStatusesForm form = new OrderStatusesForm();
            form.Show();
        }

        private void paymentTypesMenuItem_Click(object sender, EventArgs e)
        {
            PaymentTypesForm form = new PaymentTypesForm();
            form.Show();
        }
    }
}
