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
    public partial class InputDataForm : Form
    {
        public InputDataForm(string tableName)
        {
            InitializeComponent();
            switch (tableName)
            {
                case "customers":
                    InitForCustomersOrWorkers();
                    break;
                case "workers":
                    InitForCustomersOrWorkers();
                    innLabel = new Label();
                    innTextBox = new TextBox();
                    innLabel.Location = new System.Drawing.Point(22, 180);
                    innLabel.Size = new System.Drawing.Size(93, 13);
                    innLabel.Text = "ИНН";
                    innTextBox.Location = new System.Drawing.Point(121, 177);
                    innTextBox.Size = new System.Drawing.Size(139, 20);
                    Controls.Add(innLabel);
                    Controls.Add(innTextBox);
                    break;
                case "works":
                    InitForWorks();
                    break;
                case "specializations":
                    InitForSpecOrPaymentOrStatuses();
                    break;
                case "order_statuses":
                    InitForSpecOrPaymentOrStatuses();
                    break;
                case "payment_types":
                    InitForSpecOrPaymentOrStatuses();
                    break;
                case "specializations_for_workers":
                    InitForSpecsForWorkers();
                    break;
                case "works_for_specializations":
                    InitForWorksForSpecs();
                    break;
                case "orders":
                    InitForOrders();
                    break;

            }
        }

        private TextBox firstNameTextBox;
        private TextBox nameTextBox;
        private TextBox lastNameTextBox;
        private TextBox phoneNumberTextBox;
        private Label firstNameLabel;
        private Label nameLabel;
        private Label lastNameLabel;
        private Label phoneNumberLabel;
        private Label innLabel;
        private TextBox innTextBox;
        private void InitForCustomersOrWorkers()
        {
            firstNameTextBox = new TextBox();
            firstNameLabel = new Label();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            lastNameLabel = new Label();
            lastNameTextBox = new TextBox();
            phoneNumberLabel = new Label();
            phoneNumberTextBox = new TextBox();
            firstNameLabel.Size = new System.Drawing.Size(56, 13);
            firstNameLabel.Text = "Фамилия";
            firstNameTextBox.Location = new System.Drawing.Point(121, 25);
            firstNameTextBox.Size = new System.Drawing.Size(139, 20);
            firstNameLabel.Location = new System.Drawing.Point(22, 28);
            nameLabel.Location = new System.Drawing.Point(22, 63);
            nameLabel.Size = new System.Drawing.Size(29, 13);
            nameLabel.Text = "Имя";
            nameTextBox.Location = new System.Drawing.Point(121, 60);
            nameTextBox.Size = new System.Drawing.Size(139, 20);
            lastNameLabel.Location = new System.Drawing.Point(22, 101);
            lastNameLabel.Size = new System.Drawing.Size(54, 13);
            lastNameLabel.Text = "Отчество";
            lastNameTextBox.Location = new System.Drawing.Point(121, 101);
            lastNameTextBox.Size = new System.Drawing.Size(139, 20);
            phoneNumberLabel.Location = new System.Drawing.Point(22, 140);
            phoneNumberLabel.Size = new System.Drawing.Size(93, 13);
            phoneNumberLabel.Text = "Номер телефона";
            phoneNumberTextBox.Location = new System.Drawing.Point(121, 137);
            phoneNumberTextBox.Size = new System.Drawing.Size(139, 20);
            Controls.Add(phoneNumberLabel);
            Controls.Add(phoneNumberTextBox);
            Controls.Add(lastNameLabel);
            Controls.Add(lastNameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(firstNameLabel);
            Controls.Add(firstNameTextBox);
        }
        private TextBox descriptTextBox;
        private TextBox costTextBox;
        private Label descriptLabel;
        private Label costLabel;
        private void InitForWorks()
        {
            nameTextBox = new TextBox();
            descriptTextBox = new TextBox();
            costTextBox = new TextBox();
            nameLabel = new Label();
            descriptLabel = new Label();
            costLabel = new Label();
            nameLabel.Size = new System.Drawing.Size(90, 13);
            nameLabel.Text = "Название";
            nameTextBox.Location = new System.Drawing.Point(121, 25);
            nameTextBox.Size = new System.Drawing.Size(139, 20);
            nameLabel.Location = new System.Drawing.Point(22, 28);
            descriptLabel.Location = new System.Drawing.Point(22, 63);
            descriptLabel.Size = new System.Drawing.Size(90, 13);
            descriptLabel.Text = "Описание";
            descriptTextBox.Location = new System.Drawing.Point(121, 60);
            descriptTextBox.Size = new System.Drawing.Size(139, 20);
            costLabel.Location = new System.Drawing.Point(22, 101);
            costLabel.Size = new System.Drawing.Size(90, 13);
            costLabel.Text = "Стоимость";
            costTextBox.Location = new System.Drawing.Point(121, 101);
            costTextBox.Size = new System.Drawing.Size(139, 20);
            Controls.Add(nameLabel);
            Controls.Add(descriptLabel);
            Controls.Add(costLabel);
            Controls.Add(nameTextBox);
            Controls.Add(descriptTextBox);
            Controls.Add(costTextBox);
        }
        private void InitForSpecOrPaymentOrStatuses()
        {
            nameTextBox = new TextBox();
            nameLabel = new Label();
            nameLabel.Size = new System.Drawing.Size(90, 13);
            nameLabel.Text = "Название";
            nameTextBox.Location = new System.Drawing.Point(121, 25);
            nameTextBox.Size = new System.Drawing.Size(139, 20);
            nameLabel.Location = new System.Drawing.Point(22, 28);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
        }
        private Label specializationLabel;
        private Label workerLabel;
        private ComboBox specializationComboBox;
        private ComboBox workerComboBox;
        private void InitForSpecsForWorkers()
        {
            specializationLabel = new Label();
            workerLabel = new Label();
            specializationComboBox = new ComboBox();
            workerComboBox = new ComboBox();
            specializationLabel.Location = new System.Drawing.Point(13, 13);
            specializationLabel.Size = new System.Drawing.Size(86, 13);
            specializationLabel.Text = "Специализация";
            workerLabel.Location = new System.Drawing.Point(13, 54);
            workerLabel.Size = new System.Drawing.Size(55, 13);
            workerLabel.Text = "Работник";
            specializationComboBox.Location = new System.Drawing.Point(114, 10);
            specializationComboBox.Size = new System.Drawing.Size(211, 21);
            workerComboBox.Location = new System.Drawing.Point(114, 51);
            workerComboBox.Size = new System.Drawing.Size(211, 21);
            Controls.Add(specializationLabel);
            Controls.Add(workerLabel);
            Controls.Add(specializationComboBox);
            Controls.Add(workerComboBox);
        }
        private ComboBox workComboBox;
        private Label workLabel;
        private void InitForWorksForSpecs()
        {
            specializationLabel = new Label();
            specializationComboBox = new ComboBox();
            workLabel = new Label();
            workComboBox = new ComboBox();
            workLabel.Location = new System.Drawing.Point(13, 13);
            workLabel.Size = new System.Drawing.Size(86, 13);
            workLabel.Text = "Работа";
            specializationLabel.Location = new System.Drawing.Point(13, 54);
            specializationLabel.Size = new System.Drawing.Size(86, 13);
            specializationLabel.Text = "Специализация";
            workComboBox.Location = new System.Drawing.Point(114, 10);
            workComboBox.Size = new System.Drawing.Size(211, 21);
            specializationComboBox.Location = new System.Drawing.Point(114, 51);
            specializationComboBox.Size = new System.Drawing.Size(211, 21);
            Controls.Add(specializationLabel);
            Controls.Add(workLabel);
            Controls.Add(specializationComboBox);
            Controls.Add(workComboBox);
        }
        private Label customerLabel;
        private Label receiptLabel;
        private Label completionLabel;
        private Label statuseLabel;
        private Label paymentLabel;
        private ComboBox customerComboBox;
        private TextBox receiptTextBox;
        private TextBox completionTextBox;
        private ComboBox statuseComboBox;
        private ComboBox paymentComboBox;
        private void InitForOrders()
        {
            customerLabel = new Label();
            customerComboBox = new ComboBox();
            receiptLabel = new Label();
            completionLabel = new Label();
            receiptTextBox = new TextBox();
            completionTextBox = new TextBox();
            statuseLabel = new Label();
            paymentLabel = new Label();
            statuseComboBox = new ComboBox();
            paymentComboBox = new ComboBox();
            workerLabel = new Label();
            workerComboBox = new ComboBox();
            workLabel = new Label();
            workComboBox = new ComboBox();
            descriptLabel = new Label();
            descriptTextBox = new TextBox();
            customerLabel.Location = new System.Drawing.Point(13, 13);
            customerLabel.Size = new System.Drawing.Size(98, 13);
            customerLabel.Text = "Заказчик";
            workerLabel.Location = new System.Drawing.Point(13, 40);
            workerLabel.Size = new System.Drawing.Size(98, 13);
            workerLabel.Text = "Работник";
            workLabel.Location = new System.Drawing.Point(13, 71);
            workLabel.Size = new System.Drawing.Size(98, 13);
            workLabel.Text = "Работа";
            descriptLabel.Location = new System.Drawing.Point(13, 99);
            descriptLabel.Size = new System.Drawing.Size(98, 13);
            descriptLabel.Text = "Описание";
            receiptLabel.Location = new System.Drawing.Point(12, 126);
            receiptLabel.Size = new System.Drawing.Size(98, 13);
            receiptLabel.Text = "Дата получения";
            completionLabel.Location = new System.Drawing.Point(12, 154);
            completionLabel.Size = new System.Drawing.Size(98, 13);
            completionLabel.Text = "Дата выполнения";
            statuseLabel.Location = new System.Drawing.Point(13, 181);
            statuseLabel.Size = new System.Drawing.Size(98, 13);
            statuseLabel.Text = "Статус заказа";
            paymentLabel.Location = new System.Drawing.Point(13, 206);
            paymentLabel.Size = new System.Drawing.Size(98, 13);
            paymentLabel.Text = "Тип оплаты";
            customerComboBox.Location = new System.Drawing.Point(137, 10);
            customerComboBox.Size = new System.Drawing.Size(249, 21);
            workerComboBox.Location = new System.Drawing.Point(137, 37);
            workerComboBox.Size = new System.Drawing.Size(249, 21);
            workComboBox.Location = new System.Drawing.Point(137, 68);
            workComboBox.Size = new System.Drawing.Size(249, 21);
            descriptTextBox.Location = new System.Drawing.Point(137, 96);
            descriptTextBox.Size = new System.Drawing.Size(249, 20);
            receiptTextBox.Location = new System.Drawing.Point(137, 123);
            receiptTextBox.Size = new System.Drawing.Size(249, 20);
            completionTextBox.Location = new System.Drawing.Point(137, 151);;
            completionTextBox.Size = new System.Drawing.Size(249, 20);
            statuseComboBox.Location = new System.Drawing.Point(137, 178);
            statuseComboBox.Size = new System.Drawing.Size(249, 21);
            paymentComboBox.Location = new System.Drawing.Point(137, 203);
            paymentComboBox.Size = new System.Drawing.Size(249, 21);
            Controls.Add(customerLabel);
            Controls.Add(customerComboBox);
            Controls.Add(receiptLabel);
            Controls.Add(completionLabel);
            Controls.Add(receiptTextBox);
            Controls.Add(completionTextBox);
            Controls.Add(paymentLabel);
            Controls.Add(statuseLabel);
            Controls.Add(paymentComboBox);
            Controls.Add(statuseComboBox);
            Controls.Add(workerLabel);
            Controls.Add(workerComboBox);
            Controls.Add(workLabel);
            Controls.Add(workComboBox);
            Controls.Add(descriptLabel);
            Controls.Add(descriptTextBox);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
