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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (hostTextBox.Text == "") hostTextBox.Text = "localhost";
            if (portTextBox.Text == "") portTextBox.Text = "5432";
            if (userTextBox.Text == "") userTextBox.Text = "postgres";
            if (passTextBox.Text == "")
            {
                MessageBox.Show("Поле с паролем должно быть заполнено!");
                return;
            }
            try
            {
                PG.OpenConnection(hostTextBox.Text, portTextBox.Text, userTextBox.Text, passTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении! " + ex.Message);
            }
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

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RefTables form = new RefTables();
            form.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTables form = new DataTables();
            form.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ReportsViews form = new ReportsViews();
            form.Show();
        }
    }
}
