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
    public delegate void ChangesInTable();
    public partial class InputDataForm : Form
    {
        public event ChangesInTable changesInTable;
        private bool addMode;
        private string tableName;
        private string selectedId;
        private Dictionary<string, string> specializations;
        private Dictionary<string, string> workers;
        private Dictionary<string, string> works;
        private Dictionary<string, string> customers;
        private Dictionary<string, string> paymentTypes;
        private Dictionary<string, string> orderStatuses;
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
        private TextBox descriptTextBox;
        private TextBox costTextBox;
        private Label descriptLabel;
        private Label costLabel;
        private Label specializationLabel;
        private Label workerLabel;
        private ComboBox specializationComboBox;
        private ComboBox workerComboBox;
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
        public InputDataForm(string tableName, bool addMode, string selectedId)
        {
            InitializeComponent();
            if (tableName == "customers")
            {
                InitForCustomersOrWorkers();
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"id = {selectedId}");
                    firstNameTextBox.Text = value[1][2];
                    nameTextBox.Text = value[1][1];
                    lastNameTextBox.Text = value[1][3];
                    phoneNumberTextBox.Text = value[1][4];
                }
            }
            else if (tableName == "workers")
            {
                InitForCustomersOrWorkers();
                innLabel = new Label();
                innTextBox = new TextBox();
                innLabel.Location = new System.Drawing.Point(22, 180);
                innLabel.Size = new System.Drawing.Size(93, 13);
                innLabel.Text = "ИНН*";
                innTextBox.Location = new System.Drawing.Point(121, 177);
                innTextBox.Size = new System.Drawing.Size(139, 20);
                Controls.Add(innLabel);
                Controls.Add(innTextBox);
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"inn = \'{selectedId}\'");
                    innTextBox.Text = value[1][0];
                    innTextBox.Enabled = false;
                    firstNameTextBox.Text = value[1][2];
                    nameTextBox.Text = value[1][1];
                    lastNameTextBox.Text = value[1][3];
                    phoneNumberTextBox.Text = value[1][4];
                }
            }
            else if (tableName == "works")
            {
                InitForWorks();
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"id = {selectedId}");
                    nameTextBox.Text = value[1][1];
                    descriptTextBox.Text = value[1][2];
                    costTextBox.Text = value[1][3];
                }
            }
            else if (tableName == "specializations")
            {
                InitForSpecOrPaymentOrStatuses();
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"id = {selectedId}");
                    nameTextBox.Text = value[1][1];
                }
            }
            else if (tableName == "order_statuses")
            {
                InitForSpecOrPaymentOrStatuses();
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"id = {selectedId}");
                    nameTextBox.Text = value[1][1];
                }
            }
            else if (tableName == "payment_types")
            {
                InitForSpecOrPaymentOrStatuses();
                if (!addMode)
                {
                    List<List<string>> value = PG.GetTableWithCondition(tableName, $"id = {selectedId}");
                    nameTextBox.Text = value[1][1];
                }
            }
            else if (tableName == "specializations_for_workers")
            {
                InitForSpecsForWorkers();
                specializations = new Dictionary<string, string>();
                List<List<string>> specs = PG.SendQuery("Select id, name FROM specializations");
                if (!addMode)
                {
                    List<List<string>> currentSpec = PG.SendQuery($"SELECT specialization_id FROM specializations_for_workers WHERE inn = \'{selectedId}\';");
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                        specializationComboBox.Items.Add(specs[i][1]);
                        if (specs[i][0] == currentSpec[1][0]) specializationComboBox.SelectedItem = specializationComboBox.Items[i - 1];
                    }
                    List<List<string>> worker = PG.SendQuery($"SELECT first_name, name, last_name FROM workers WHERE inn = \'{selectedId}\';");
                    workerComboBox.Items.Add($"{worker[1][0]} {worker[1][1][0]}. {worker[1][2][0]}.");
                    workerComboBox.SelectedItem = workerComboBox.Items[0];
                    workerComboBox.Enabled = false;
                }
                else
                {
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                        specializationComboBox.Items.Add(specs[i][1]);
                    }
                    this.workers = new Dictionary<string, string>();
                    List<List<string>> workers = PG.SendQuery($"SELECT first_name, name, last_name, inn FROM workers");
                    for (int i = 1; i < workers.Count; i++)
                    {
                        this.workers.Add($"{workers[i][0]} {workers[i][1][0]}. {workers[i][2][0]}.", workers[i][3]);
                        workerComboBox.Items.Add($"{workers[i][0]} {workers[i][1][0]}. {workers[i][2][0]}.");
                    }
                }
            }
            else if (tableName == "works_for_specializations")
            {
                InitForWorksForSpecs();
                specializations = new Dictionary<string, string>();
                List<List<string>> specs = PG.SendQuery("SELECT id, name FROM specializations");
                if (!addMode)
                {
                    List<List<string>> currentSpec = PG.SendQuery($"SELECT specialization_id FROM works_for_specializations WHERE work_id = {selectedId};");
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                        specializationComboBox.Items.Add(specs[i][1]);
                        if (specs[i][0] == currentSpec[1][0]) specializationComboBox.SelectedItem = specializationComboBox.Items[i - 1];
                    }
                    List<List<string>> work = PG.SendQuery($"SELECT name FROM works WHERE id = {selectedId};");
                    workComboBox.Items.Add(work[1][0]);
                    workComboBox.SelectedItem = workComboBox.Items[0];
                    workComboBox.Enabled = false;
                }
                else
                {
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                        specializationComboBox.Items.Add(specs[i][1]);
                    }
                    this.works = new Dictionary<string, string>();
                    List<List<string>> works = PG.SendQuery("SELECT id, name FROM works;");
                    for (int i = 1; i < works.Count; i++)
                    {
                        this.works.Add(works[i][1], works[i][0]);
                        workComboBox.Items.Add(works[i][1]);
                    }
                }
            }
            else if (tableName == "orders")
            {
                InitForOrders();
                customers = new Dictionary<string, string>();
                List<List<string>> customersTable = PG.SendQuery("SELECT first_name, name, last_name, id FROM customers;");
                workers = new Dictionary<string, string>();
                works = new Dictionary<string, string>();
                List<List<string>> worksTable = PG.SendQuery("SELECT name, id FROM works;");
                paymentTypes = new Dictionary<string, string>();
                List<List<string>> paymentTypesTable = PG.SendQuery("SELECT name, id FROM payment_types;");
                orderStatuses = new Dictionary<string, string>();
                List<List<string>> orderStatusesTable = PG.SendQuery("SELECT name, id FROM order_statuses;");
                specializations = new Dictionary<string, string>();
                List<List<string>> specs = PG.SendQuery("SELECT name, id FROM specializations;");
                if (!addMode)
                {
                    List<List<string>> currentCustomer = PG.SendQuery($"SELECT customer_id FROM orders WHERE id = {selectedId};");
                    for (int i = 1; i < customersTable.Count; i++)
                    {
                        customers.Add($"{customersTable[i][0]} {customersTable[i][1][0]}. {customersTable[i][2][0]}.", customersTable[i][3]);
                        customerComboBox.Items.Add($"{customersTable[i][0]} {customersTable[i][1][0]}. {customersTable[i][2][0]}.");
                        if (currentCustomer[1][0] == customersTable[i][3]) customerComboBox.SelectedItem = customerComboBox.Items[i - 1];
                    }
                    List<List<string>> currentWork = PG.SendQuery($"SELECT work_id FROM orders WHERE id = {selectedId};");
                    for (int i = 1; i < worksTable.Count; i++)
                    {
                        works.Add(worksTable[i][0], worksTable[i][1]);
                        workComboBox.Items.Add(worksTable[i][0]);
                        if (currentWork[1][0] == worksTable[i][1]) workComboBox.SelectedItem = workComboBox.Items[i - 1];
                    }
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                    }
                    UpdateWorkers(null, null);
                    List<List<string>> currentPaymentType = PG.SendQuery($"SELECT payment_type_id FROM orders WHERE id = {selectedId};");
                    for (int i = 1; i < paymentTypesTable.Count; i++)
                    {
                        paymentTypes.Add(paymentTypesTable[i][0], paymentTypesTable[i][1]);
                        paymentComboBox.Items.Add(paymentTypesTable[i][0]);
                        if (currentPaymentType[1][0] == paymentTypesTable[i][1]) paymentComboBox.SelectedItem = paymentComboBox.Items[i - 1];
                    }
                    List<List<string>> currentStatuse = PG.SendQuery($"SELECT statuse_id FROM orders WHERE id = {selectedId};");
                    for (int i = 1; i < orderStatusesTable.Count; i++)
                    {
                        orderStatuses.Add(orderStatusesTable[i][0], orderStatusesTable[i][1]);
                        statuseComboBox.Items.Add(orderStatusesTable[i][0]);
                        if (currentStatuse[1][0] == orderStatusesTable[i][1]) statuseComboBox.SelectedItem = statuseComboBox.Items[i - 1];
                    }
                    List<List<string>> currentWorker = PG.SendQuery($"SELECT worker_inn FROM orders WHERE id = {selectedId};");
                    for (int i = 0; i < workerComboBox.Items.Count; i++)
                    {
                        if (currentWorker[1][0] == workers[workerComboBox.Items[i].ToString()]) workerComboBox.SelectedItem = workerComboBox.Items[i];
                    }
                    List<List<string>> currentDescript = PG.SendQuery($"SELECT descript FROM orders WHERE id = {selectedId};");
                    descriptTextBox.Text = currentDescript[1][0];
                    List<List<string>> currentReceiptDate = PG.SendQuery($"SELECT receipt_date FROM orders WHERE id = {selectedId};");
                    receiptTextBox.Text = currentReceiptDate[1][0];
                    List<List<string>> currentCompletionDate = PG.SendQuery($"SELECT completion_date FROM orders WHERE id = {selectedId};");
                    completionTextBox.Text = currentCompletionDate[1][0];
                }
                else
                { 
                    for (int i = 1; i < customersTable.Count; i++)
                    {
                        customers.Add($"{customersTable[i][0]} {customersTable[i][1][0]}. {customersTable[i][2][0]}.", customersTable[i][3]);
                        customerComboBox.Items.Add($"{customersTable[i][0]} {customersTable[i][1][0]}. {customersTable[i][2][0]}.");
                    }
                    for (int i = 1; i < worksTable.Count; i++)
                    {
                        works.Add(worksTable[i][0], worksTable[i][1]);
                        workComboBox.Items.Add(worksTable[i][0]);
                    }
                    for (int i = 1; i < paymentTypesTable.Count; i++)
                    {
                        paymentTypes.Add(paymentTypesTable[i][0], paymentTypesTable[i][1]);
                        paymentComboBox.Items.Add(paymentTypesTable[i][0]);
                    }
                    for (int i = 1; i < orderStatusesTable.Count; i++)
                    {
                        orderStatuses.Add(orderStatusesTable[i][0], orderStatusesTable[i][1]);
                        statuseComboBox.Items.Add(orderStatusesTable[i][0]);
                    }
                    for (int i = 1; i < specs.Count; i++)
                    {
                        specializations.Add(specs[i][1], specs[i][0]);
                    }
                }
                workComboBox.SelectedValueChanged += UpdateWorkers;
            }
            this.addMode = addMode;
            this.tableName = tableName;
            this.selectedId = selectedId;
        }
        private void UpdateWorkers(Object sender, EventArgs args)
        {
            if (workComboBox.SelectedValue == "")
            {
                workerComboBox.Items.Clear();
            }
            else
            {
                workerComboBox.SelectedItem = null;
                workerComboBox.Items.Clear();
                workers.Clear();
                List<List<string>> workForSpec = PG.SendQuery($"SELECT specialization_id FROM works_for_specializations WHERE work_id = {works[workComboBox.SelectedItem.ToString()]};");
                string specId = workForSpec[1][0];
                List<List<string>> specForWorkers = PG.SendQuery($"SELECT inn FROM specializations_for_workers WHERE specialization_id = {specId};");
                for (int i = 1; i < specForWorkers.Count; i++)
                {
                    List<List<string>> workersTable = PG.SendQuery($"SELECT first_name, name, last_name FROM workers WHERE inn = \'{specForWorkers[i][0]}\'");
                    workers.Add($"{workersTable[1][0]} {workersTable[1][1][0]}. {workersTable[1][2][0]}.", specForWorkers[i][0]);
                    workerComboBox.Items.Add($"{workersTable[1][0]} {workersTable[1][1][0]}. {workersTable[1][2][0]}.");
                }
            }
        }
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
            firstNameLabel.Size = new System.Drawing.Size(60, 13);
            firstNameLabel.Text = "Фамилия*";
            firstNameTextBox.Location = new System.Drawing.Point(121, 25);
            firstNameTextBox.Size = new System.Drawing.Size(139, 20);
            firstNameLabel.Location = new System.Drawing.Point(22, 28);
            nameLabel.Location = new System.Drawing.Point(22, 63);
            nameLabel.Size = new System.Drawing.Size(40, 13);
            nameLabel.Text = "Имя*";
            nameTextBox.Location = new System.Drawing.Point(121, 60);
            nameTextBox.Size = new System.Drawing.Size(139, 20);
            lastNameLabel.Location = new System.Drawing.Point(22, 101);
            lastNameLabel.Size = new System.Drawing.Size(60, 13);
            lastNameLabel.Text = "Отчество*";
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
        private void InitForWorks()
        {
            nameTextBox = new TextBox();
            descriptTextBox = new TextBox();
            costTextBox = new TextBox();
            nameLabel = new Label();
            descriptLabel = new Label();
            costLabel = new Label();
            nameLabel.Size = new System.Drawing.Size(90, 13);
            nameLabel.Text = "Название*";
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
            costLabel.Text = "Стоимость*";
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
            nameLabel.Text = "Название*";
            nameTextBox.Location = new System.Drawing.Point(121, 25);
            nameTextBox.Size = new System.Drawing.Size(139, 20);
            nameLabel.Location = new System.Drawing.Point(22, 28);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
        }
        
        private void InitForSpecsForWorkers()
        {
            specializationLabel = new Label();
            workerLabel = new Label();
            specializationComboBox = new ComboBox();
            workerComboBox = new ComboBox();
            specializationLabel.Location = new System.Drawing.Point(13, 13);
            specializationLabel.Size = new System.Drawing.Size(90, 13);
            specializationLabel.Text = "Специализация*";
            workerLabel.Location = new System.Drawing.Point(13, 54);
            workerLabel.Size = new System.Drawing.Size(60, 13);
            workerLabel.Text = "Работник*";
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
            workLabel.Text = "Работа*";
            specializationLabel.Location = new System.Drawing.Point(13, 54);
            specializationLabel.Size = new System.Drawing.Size(90, 13);
            specializationLabel.Text = "Специализация*";
            workComboBox.Location = new System.Drawing.Point(114, 10);
            workComboBox.Size = new System.Drawing.Size(211, 21);
            specializationComboBox.Location = new System.Drawing.Point(114, 51);
            specializationComboBox.Size = new System.Drawing.Size(211, 21);
            Controls.Add(specializationLabel);
            Controls.Add(workLabel);
            Controls.Add(specializationComboBox);
            Controls.Add(workComboBox);
        }
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
            customerLabel.Text = "Заказчик*";
            workLabel.Text = "Работа*";
            workLabel.Location = new System.Drawing.Point(13, 40);
            workLabel.Size = new System.Drawing.Size(98, 13);
            descriptLabel.Text = "Описание";
            descriptLabel.Location = new System.Drawing.Point(13, 71);
            descriptLabel.Size = new System.Drawing.Size(98, 13);
            workerLabel.Text = "Работник*";
            workerLabel.Location = new System.Drawing.Point(13, 99);
            workerLabel.Size = new System.Drawing.Size(98, 13);
            receiptLabel.Location = new System.Drawing.Point(12, 126);
            receiptLabel.Size = new System.Drawing.Size(98, 13);
            receiptLabel.Text = "Дата получения*";
            completionLabel.Location = new System.Drawing.Point(12, 154);
            completionLabel.Size = new System.Drawing.Size(98, 13);
            completionLabel.Text = "Дата выполнения";
            statuseLabel.Location = new System.Drawing.Point(13, 181);
            statuseLabel.Size = new System.Drawing.Size(98, 13);
            statuseLabel.Text = "Статус заказа*";
            paymentLabel.Location = new System.Drawing.Point(13, 206);
            paymentLabel.Size = new System.Drawing.Size(98, 13);
            paymentLabel.Text = "Тип оплаты*";
            customerComboBox.Location = new System.Drawing.Point(137, 10);
            customerComboBox.Size = new System.Drawing.Size(249, 21);
            workerComboBox.Location = new System.Drawing.Point(137, 96);
            workerComboBox.Size = new System.Drawing.Size(249, 21);
            workComboBox.Location = new System.Drawing.Point(137, 37);
            workComboBox.Size = new System.Drawing.Size(249, 21);
            descriptTextBox.Location = new System.Drawing.Point(137, 68);
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

        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tableName)
                {
                    case "customers":
                        if (firstNameTextBox.Text == "" || nameTextBox.Text == "" || lastNameTextBox.Text == "") 
                            throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "first_name, name, last_name, phone", $"\'{firstNameTextBox.Text}\', " +
                                $"\'{nameTextBox.Text}\', \'{lastNameTextBox.Text}\', {(phoneNumberTextBox.Text == "" ? "NULL" : $"\'{phoneNumberTextBox.Text}\'")}");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"first_name = \'{firstNameTextBox.Text}\', " +
                                $"name = \'{nameTextBox.Text}\', last_name = \'{lastNameTextBox.Text}\', phone = {(phoneNumberTextBox.Text == "" ? "NULL" : $"\'{phoneNumberTextBox.Text}\'")}");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "workers":
                        if (firstNameTextBox.Text == "" || nameTextBox.Text == "" || lastNameTextBox.Text == "" || innTextBox.Text == "")
                            throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "inn, first_name, name, last_name, phone", $"\'{innTextBox.Text}\', \'{firstNameTextBox.Text}\', " +
                                $"\'{nameTextBox.Text}\', \'{lastNameTextBox.Text}\', {(phoneNumberTextBox.Text == "" ? "NULL" : $"\'{phoneNumberTextBox.Text}\'")}");
                        }
                        else
                        {
                            PG.Update(tableName, $"\'{selectedId}\'", "inn", $"first_name = \'{firstNameTextBox.Text}\', " +
                                $"name = \'{nameTextBox.Text}\', last_name = \'{lastNameTextBox.Text}\', phone = {(phoneNumberTextBox.Text == "" ? "NULL" : $"\'{phoneNumberTextBox.Text}\'")}");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "works":
                        if (nameTextBox.Text == "" || costTextBox.Text == "") throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "name, descript, cost", $"\'{nameTextBox.Text}\', \'{descriptTextBox.Text}\', \'{costTextBox.Text}\'");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"name = \'{nameTextBox.Text}\', descript = \'{descriptTextBox.Text}\', cost = \'{costTextBox.Text}\'");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "specializations":
                        if (nameTextBox.Text == "") throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "name", $"\'{nameTextBox.Text}\'");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"name = \'{nameTextBox.Text}\'");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "order_statuses":
                        if (nameTextBox.Text == "") throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "name", $"\'{nameTextBox.Text}\'");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"name = \'{nameTextBox.Text}\'");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "payment_types":
                        if (nameTextBox.Text == "") throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "name", $"\'{nameTextBox.Text}\'");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"name = \'{nameTextBox.Text}\'");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "specializations_for_workers":
                        if (specializationComboBox.SelectedItem.ToString() == "" || workerComboBox.SelectedItem.ToString() == "")
                            throw new ApplicationException("Зполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "specialization_id, inn", $"{specializations[specializationComboBox.SelectedItem.ToString()]}, " +
                                $"{workers[workerComboBox.SelectedItem.ToString()]}");
                        }
                        else
                        {
                            PG.Update(tableName, $"\'{selectedId}\'", "inn", $"specialization_id = {specializations[specializationComboBox.SelectedItem.ToString()]}");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "works_for_specializations":
                        if (specializationComboBox.SelectedItem.ToString() == "" || workComboBox.SelectedItem.ToString() == "")
                            throw new ApplicationException("Зполните обязательные поля!");
                        if (addMode) {
                            PG.Insert(tableName, "specialization_id, work_id", $"{specializations[specializationComboBox.SelectedItem.ToString()]}, " +
                                $"{works[workComboBox.SelectedItem.ToString()]}");
                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "work_id", $"specialization_id = {specializations[specializationComboBox.SelectedItem.ToString()]}");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                    case "orders":
                        if (customerComboBox.SelectedItem.ToString() == "" || workComboBox.SelectedItem.ToString() == ""
                            || workComboBox.SelectedItem.ToString() == "" || receiptTextBox.Text == "" || statuseComboBox.SelectedItem.ToString() == ""
                            || paymentComboBox.SelectedItem.ToString() == "") throw new ApplicationException("Заполните обязательные поля!");
                        if (addMode)
                        {
                            PG.Insert(tableName, "customer_id, worker_inn, work_id, descript, receipt_date, completion_date, statuse_id, payment_type_id",
                                $"{customers[customerComboBox.SelectedItem.ToString()]}, \'{workers[workerComboBox.SelectedItem.ToString()]}\', " +
                                $"{works[workComboBox.SelectedItem.ToString()]}, \'{descriptTextBox.Text}\', \'{receiptTextBox.Text}\', {(completionTextBox.Text == "" ? "NULL" : $"\'{completionTextBox.Text}\'")}, " +
                                $"{orderStatuses[statuseComboBox.SelectedItem.ToString()]}, {paymentTypes[paymentComboBox.SelectedItem.ToString()]}");

                        }
                        else
                        {
                            PG.Update(tableName, selectedId, "id", $"customer_id = {customers[customerComboBox.SelectedItem.ToString()]}, worker_inn = \'{workers[workerComboBox.SelectedItem.ToString()]}\', " +
                                $"work_id = {works[workComboBox.SelectedItem.ToString()]}, descript = \'{descriptTextBox.Text}\', receipt_date = \'{receiptTextBox.Text}\', " +
                                $"completion_date = {(completionTextBox.Text == "" ? "NULL" : $"\'{completionTextBox.Text}\'")}, statuse_id = {orderStatuses[statuseComboBox.SelectedItem.ToString()]}, payment_type_id = {paymentTypes[paymentComboBox.SelectedItem.ToString()]}");
                        }
                        changesInTable.Invoke();
                        this.Close();
                        this.Dispose();
                        break;
                }
            }
            catch (Exception ex)
                        {
                MessageBox.Show(ex.Message);
            }
        }
    }
}