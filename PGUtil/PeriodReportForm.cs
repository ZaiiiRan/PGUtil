using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGUtil
{
    public partial class PeriodReportForm : Form
    {
        public PeriodReportForm()
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

            List<List<string>> data = PG.GetFullTable("period_reports");
            PG.FillTableInDataGridView(data, dataGridView1);
        }
        private void Form_Shown(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                List<List<string>> data = PG.GetFullTable("period_reports");
                PG.FillTableInDataGridView(data, dataGridView1);
            }
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }

            if (innTextBox.Text == "" || periodTextBox1.Text == "" || periodTextBox2.Text == "")
            {
                MessageBox.Show("Для генерации отчета необходимо, чтобы все поля были заполнены!");
                return;
            }

            if (DateTime.TryParse(periodTextBox1.Text, out DateTime date1) && DateTime.TryParse(periodTextBox2.Text, out DateTime date2))
            {
                if (date1 > date2)
                {
                    MessageBox.Show("Неверный интервал. Первая дата не может быть позднее второй.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Неверный формат даты! Введите дату в формате xx.xx.xxxx или xx.xx.xx.");
                return;
            }

            if (!Regex.IsMatch(innTextBox.Text, @"^\d{12}$"))
            {
                MessageBox.Show("Неверный формат ИНН!");
                return;
            }

            try
            {
                List<List<string>> data = PG.GetTableWithCondition("period_reports", $"ИНН_исполнителя = '{innTextBox.Text}' " +
                    $"AND дата_выполнения >= '{periodTextBox1.Text}' AND дата_выполнения <= '{periodTextBox2.Text}'");
                PG.FillTableInDataGridView(data, dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message);
            }
        }
    }
}
