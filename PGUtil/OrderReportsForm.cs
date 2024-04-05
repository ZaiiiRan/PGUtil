﻿using System;
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
    public partial class OrderReportsForm : Form
    {
        public OrderReportsForm()
        {
            InitializeComponent();
            this.Shown += Form_Shown;
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

        private void backMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void viewAllMenuItem_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }

            List<List<string>> data = PG.GetTable("order_reports");
            PG.FillTableInDataGridView(data, dataGridView1);
        }

        private void Form_Shown(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                List<List<string>> data = PG.GetTable("order_reports");
                PG.FillTableInDataGridView(data, dataGridView1);
            }
        }
    }
}