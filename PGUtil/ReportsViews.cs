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
    public partial class ReportsViews : Form
    {
        public ReportsViews()
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

        private void periodMenuItem_Click(object sender, EventArgs e)
        {
            CRUD form = new CRUD("period_reports", true, null);
            form.Show();
        }

        private void orderMenuItem_Click(object sender, EventArgs e)
        {
            CRUD form = new CRUD("order_reports", true, null);
            form.Show();
        }

        private void orderReceiptMenuItem_Click(object sender, EventArgs e)
        {
            CRUD form = new CRUD("order_receipt", true, null);
            form.Show();
        }
    }
}
