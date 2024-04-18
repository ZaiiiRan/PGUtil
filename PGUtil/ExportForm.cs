using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace PGUtil
{
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
            timer1.Start();
            comboBox1.Items.Add("Книга Excel");
            comboBox1.Items.Add("HTML файл");
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                connectionStatusLabel.ForeColor = Color.Green;
                connectionStatusLabel.Text = "подключено";
                ordersReportsCheckBox.Enabled = true;
                ordersReceiptsCheckBox.Enabled = true;
                periodReportsCheckBox.Enabled = true;
            }
            else
            {
                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "не подключено";
                ordersReportsCheckBox.Enabled = false;
                ordersReceiptsCheckBox.Enabled = false;
                periodReportsCheckBox.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отсутствует подключение к БД! Экспорт не возможен!");
                return;
            }
            if (ordersReportsCheckBox.Checked)
            {
                List<List<string>> data = null;
                if (radioButton1.Checked)
                {
                    data = PG.SendQuery("SELECT * FROM order_reports");
                }
                else if (radioButton2.Checked)
                {
                    GenerateReportsForm form = new GenerateReportsForm("order_reports", "Генерация отчета по заказу");
                    if(form.ShowDialog(this) == DialogResult.OK)
                    {
                        data = PG.SendQuery($"SELECT * FROM order_reports WHERE {form.condition};");
                    }
                }
                if (comboBox1.SelectedIndex == 0) ExportToExcel(data);
                else if (comboBox1.SelectedIndex == 1) ExportToHTML(data, "Отчеты по заказам");
            }
            if (ordersReceiptsCheckBox.Checked)
            {
                List<List<string>> data = null;
                if (radioButton3.Checked)
                {
                    data = PG.SendQuery("SELECT * FROM order_receipt");
                }
                else if (radioButton4.Checked)
                {
                    GenerateReportsForm form = new GenerateReportsForm("order_receipt", "Генерация квитанции на оплату заказа");
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        data = PG.SendQuery($"SELECT * FROM order_receipt WHERE {form.condition};");
                    }
                }
                if (comboBox1.SelectedIndex == 0) ExportToExcel(data);
                else if (comboBox1.SelectedIndex == 1) ExportToHTML(data, "Квитанции на оплату заказов");
            }
            if (periodReportsCheckBox.Checked)
            {
                List<List<string>> data = null;
                if (radioButton5.Checked)
                {
                    data = PG.SendQuery("SELECT * FROM period_reports");
                }
                else if (radioButton6.Checked)
                {
                    GenerateReportsForm form = new GenerateReportsForm("period_reports", "Генерация отчета за период");
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        data = PG.SendQuery($"SELECT * FROM period_reports WHERE {form.condition};");
                    }
                }
                if (comboBox1.SelectedIndex == 0) ExportToExcel(data);
                else if (comboBox1.SelectedIndex == 1) ExportToHTML(data, "Отчеты за периоды");
            }
        }

        private void ordersReportsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = ordersReportsCheckBox.Checked;
        }

        private void ordersReceiptsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = ordersReceiptsCheckBox.Checked;
        }

        private void periodReportsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = periodReportsCheckBox.Checked;
        }

        private void ExportToExcel(List<List<string>> data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Title = "Экспорт в Excel";
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = false;
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                ((dynamic)worksheet.Cells).NumberFormat = "@";
                int row = 1;
                foreach (List<string> list in data)
                {
                    int col = 1;
                    foreach (string item in list)
                    {
                        worksheet.Cells[row, col] = item;
                        col++;
                    }
                    row++;
                }
                Excel.Range rng = (Excel.Range)worksheet.Rows[1];
                rng.Font.Bold = true;
                rng = worksheet.UsedRange;
                rng.Columns.AutoFit();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();
                    MessageBox.Show("Файл сохранен!");
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при сохранении файла!");
            }
        }
        private void ExportToHTML(List<List<string>> data, string name)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "HTML files(*.html)|*.html";
            saveFileDialog.DefaultExt = "html";
            saveFileDialog.Title = "Экспорт в HTML";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                try
                {
                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.WriteLine("<html>");
                    sw.WriteLine("<head>");
                    sw.WriteLine("<meta content=\"text/html; charset=utf-8\" http-equiv=\"ContentType\\\">");
                    sw.WriteLine("<title>" + name + "</title>");
                    sw.WriteLine("</head>");
                    sw.WriteLine("<body>");
                    sw.WriteLine("<table align=\"center\" border=1 style=\"border-collapse: collapse;\">");
                    sw.WriteLine("<tr>");
                    for (int i = 0; i < data[0].Count; i++)
                    {
                        sw.WriteLine("<td style=\"padding: 5px;\" ><b>" + data[0][i] + "</b></td>");
                    }
                    sw.WriteLine("</tr>");
                    for (int i = 1; i < data.Count; i++)
                    {
                        sw.WriteLine("<tr>");
                        for (int j = 0; j < data[i].Count; j++)
                        {
                            sw.WriteLine("<td style=\"padding: 5px;\" >" + data[i][j] + "</td>");
                        }
                        sw.WriteLine("</tr>");
                    }
                    sw.WriteLine("</table></body></html>");
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("Файл сохранен!");
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при сохранении файла!");
                }
                fs.Close();
            }
        } 
    }
}
