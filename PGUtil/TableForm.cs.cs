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
    public partial class TableForm : Form
    {
        private string tableName;
        public TableForm(string tableName, string formName)
        {
            InitializeComponent();
            this.Text = formName;
            this.tableName = tableName;
            this.Shown += Form_Shown;
            timer1.Start();

        }

        private void GetAndFillData()
        {
            List<List<string>> data = PG.GetFullTable(tableName);
            PG.FillTableInDataGridView(data, dataGridView1);
        }

        private void updateMenuItem_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }

            GetAndFillData();
        }

        private void backMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                GetAndFillData();
            }
        }
    }
}