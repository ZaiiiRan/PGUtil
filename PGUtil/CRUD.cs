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
    public partial class CRUD : Form
    {
        private string tableName;
        private bool readOnlyMode;
        public string primaryKeyField;
        private int currentPage;
        private int recordsPerPage;
        private int totalPages;
        private int totalRecords;
        private bool isReport;
        private string condition;
        private Button generateReportButton;
        public CRUD(string tableName, bool readOnlyMode, string primaryKeyField)
        {
            InitializeComponent();
            this.tableName = tableName;
            tableNameLabel.Text = tableName;
            this.readOnlyMode = readOnlyMode;
            this.primaryKeyField = primaryKeyField;
            if (readOnlyMode)
            {
                addButton.Enabled = false;
                updateButton.Enabled = false;
                deleteButton.Enabled = false;
                generateReportButton = new Button();
                generateReportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                generateReportButton.Location = new System.Drawing.Point(12, 484);
                generateReportButton.Size = new System.Drawing.Size(92, 23);
                generateReportButton.Text = "Отчет";
                generateReportButton.Click += generateReportButton_Click;
                Controls.Add(generateReportButton);
            }
            timer1.Start();
            currentPage = 1;
            pageTextBox.Text = currentPage.ToString();
            if (PG.CheckConnection())
            {
                totalRecords = PG.GetTotalRecordsCount(tableName);
                recordsPerPage = dataGridView1.Height / dataGridView1.RowTemplate.Height - 2;
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            }
            isReport = false;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void LoadData()
        {
            if (currentPage == 1) previousPageButton.Enabled = false;
            else previousPageButton.Enabled = true;
            if (currentPage == totalPages) nextPageButton.Enabled = false;
            else nextPageButton.Enabled = true;
            if (isReport)
            {
                List<List<string>> data = PG.GetTableWithConditionForPage(tableName, condition, currentPage, recordsPerPage);
                PG.FillTableInDataGridView(data, dataGridView1);
                isReport = false;
            }
            else
            {
                List<List<string>> data = PG.GetTableForPage(tableName, currentPage, recordsPerPage);
                PG.FillTableInDataGridView(data, dataGridView1);
            }
            
        }

        private void CRUD_Load(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                LoadData();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<Object> ids = new List<Object>();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                ids.Add(row.Cells[primaryKeyField].Value);

            try
            {
                foreach (Object id in ids)
                {
                    if (tableName == "workers" || tableName == "specializations_for_workers") 
                        PG.Delete(tableName, $"\'{id.ToString()}\'", primaryKeyField);
                    else PG.Delete(tableName, id.ToString(), primaryKeyField);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            totalRecords = PG.GetTotalRecordsCount(tableName);
            totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            if (currentPage > totalPages) currentPage = totalPages;
            LoadData();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }
            currentPage = 1;
            pageTextBox.Text = currentPage.ToString();
            recordsPerPage = dataGridView1.Height / dataGridView1.RowTemplate.Height - 2;
            totalRecords = PG.GetTotalRecordsCount(tableName);
            totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
            LoadData();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            InputDataForm form = new InputDataForm(tableName, true, null);
            form.changesInTable += () =>
            {
                totalRecords = PG.GetTotalRecordsCount(tableName);
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                LoadData();
            };
            form.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!readOnlyMode && !PG.CheckConnection())
            {
                addButton.Enabled = false;
                deleteButton.Enabled = false;
                updateButton.Enabled = false;
            }
            else if (!readOnlyMode)
            {
                addButton.Enabled = true;
                deleteButton.Enabled = true;
                updateButton.Enabled = true;
            }
            else if (readOnlyMode && !PG.CheckConnection())
            {
                generateReportButton.Enabled = false;
            }
            else if (readOnlyMode)
            {
                generateReportButton.Enabled = true;
            }
            if (!PG.CheckConnection())
            {
                nextPageButton.Enabled = false;
                previousPageButton.Enabled = false;
                connectionStatusLabel.ForeColor = Color.Red;
                connectionStatusLabel.Text = "не подключено";
            }
            else
            {
                connectionStatusLabel.ForeColor = Color.Green;
                connectionStatusLabel.Text = "подключено";
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            isReport = false;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                InputDataForm form = new InputDataForm(tableName, false, row.Cells[primaryKeyField].Value.ToString());
                form.Show();
                form.changesInTable += () =>
                {
                    totalRecords = PG.GetTotalRecordsCount(tableName);
                    totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                    LoadData();
                };
                break;
            }
        }

        private void previousPageButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                pageTextBox.Text = currentPage.ToString();
                LoadData();
            }
        }

        private void nextPageButton_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                pageTextBox.Text = currentPage.ToString();
                LoadData();
            }
        }

        private void CRUD_ResizeEnd(object sender, EventArgs e)
        {
            if (PG.CheckConnection())
            {
                currentPage = 1;
                pageTextBox.Text = currentPage.ToString();
                recordsPerPage = dataGridView1.Height / dataGridView1.RowTemplate.Height - 2;
                totalRecords = PG.GetTotalRecordsCount(tableName);
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                LoadData();
            }
        }
        private void generateReportButton_Click(object sender, EventArgs e)
        {
            GenerateReportsForm form = new GenerateReportsForm(tableName);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                condition = form.condition;
                isReport = true;
                currentPage = 1;
                pageTextBox.Text = currentPage.ToString();
                recordsPerPage = dataGridView1.Height / dataGridView1.RowTemplate.Height - 2;
                totalRecords = PG.GetTotalRecordsWithConditionCount(tableName, condition);
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                LoadData();
            }
        }
    }
}
