using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGUtil
{
    public partial class GenerateReportsForm : Form
    {
        public string viewName;
        public string condition;
        private Label orderIdLabel;
        private Label periodLabel;
        private Label periodLabel1;
        private Label periodLabel2;
        private Label innLabel;
        private TextBox innTextBox;
        private TextBox periodTextBox1;
        private TextBox periodTextBox2;
        private TextBox orderIdTextBox;
        public GenerateReportsForm(string viewName)
        {
            InitializeComponent();
            this.viewName = viewName;
            if (viewName == "order_reports" || viewName == "order_receipt") 
            {
                orderIdLabel = new Label();
                orderIdTextBox = new TextBox();
                orderIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                orderIdLabel.Location = new System.Drawing.Point(9, 20);
                orderIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                orderIdLabel.Size = new System.Drawing.Size(104, 17);
                orderIdLabel.Text = "Номер заказа:";
                orderIdTextBox.Location = new System.Drawing.Point(128, 20);
                orderIdTextBox.Margin = new System.Windows.Forms.Padding(2);
                orderIdTextBox.Size = new System.Drawing.Size(98, 20);
                Controls.Add(orderIdLabel);
                Controls.Add(orderIdTextBox);
            }
            else
            {
                periodLabel = new Label();
                periodLabel1 = new Label();
                periodLabel2 = new Label();
                periodTextBox1 = new TextBox();
                periodTextBox2 = new TextBox();
                innLabel = new Label();
                innTextBox = new TextBox();
                periodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                periodLabel.Location = new System.Drawing.Point(11, 9);
                periodLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                periodLabel.Size = new System.Drawing.Size(62, 17);
                periodLabel.Text = "Период:";
                periodLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                periodLabel1.Location = new System.Drawing.Point(79, 9);
                periodLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                periodLabel1.Size = new System.Drawing.Size(23, 17);
                periodLabel1.Text = "от";
                periodTextBox1.Location = new System.Drawing.Point(106, 9);
                periodTextBox1.Margin = new System.Windows.Forms.Padding(2);
                periodTextBox1.Size = new System.Drawing.Size(96, 20);
                periodLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                periodLabel2.Location = new System.Drawing.Point(220, 9);
                periodLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                periodLabel2.Size = new System.Drawing.Size(24, 17);
                periodLabel2.Text = "до";
                periodTextBox2.Location = new System.Drawing.Point(247, 9);
                periodTextBox2.Margin = new System.Windows.Forms.Padding(2);
                periodTextBox2.Size = new System.Drawing.Size(96, 20);
                innLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                innLabel.Location = new System.Drawing.Point(11, 43);
                innLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                innLabel.Size = new System.Drawing.Size(136, 17);
                innLabel.Text = "ИНН исполнителя: ";
                innTextBox.Location = new System.Drawing.Point(144, 42);
                innTextBox.Margin = new System.Windows.Forms.Padding(2);
                innTextBox.Size = new System.Drawing.Size(199, 20);
                Controls.Add(periodLabel);
                Controls.Add(periodLabel1);
                Controls.Add(periodLabel2);
                Controls.Add(innLabel);
                Controls.Add(innTextBox);
                Controls.Add(periodTextBox1);
                Controls.Add(periodTextBox2);
            }
        }

        private void getDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (viewName)
                {
                    case "order_reports":
                        if (orderIdTextBox.Text == "")
                        {
                            throw new ApplicationException("Для генерации отчета необходимо ввести номер заказа!");
                        }
                        condition = $"номер_заказа = {orderIdTextBox.Text}";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "order_receipt":
                        if (orderIdTextBox.Text == "")
                        {
                            throw new ApplicationException("Для генерации отчета необходимо ввести номер заказа!");
                        }
                        condition = $"номер_заказа = {orderIdTextBox.Text}";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "period_reports":
                        if (innTextBox.Text == "" || periodTextBox1.Text == "" || periodTextBox2.Text == "")
                        {
                            MessageBox.Show("Для генерации отчета необходимо, чтобы все поля были заполнены!");
                        }
                        if (DateTime.TryParse(periodTextBox1.Text, out DateTime date1) && DateTime.TryParse(periodTextBox2.Text, out DateTime date2))
                        {
                            if (date1 > date2)
                            {
                                throw new ApplicationException("Неверный интервал. Первая дата не может быть позднее второй.");
                            }
                        }
                        else
                        {
                            throw new ApplicationException("Неверный формат даты! Введите дату в формате xx.xx.xxxx или xx.xx.xx.");
                        }
                        if (!Regex.IsMatch(innTextBox.Text, @"^\d{12}$"))
                        {
                            throw new ApplicationException("Неверный формат ИНН!");
                        }
                        condition = $"ИНН_исполнителя = \'{innTextBox.Text}\' AND дата_выполнения >= \'{periodTextBox1.Text}\' AND дата_выполнения <= \'{periodTextBox2.Text}\'";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            catch(ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
