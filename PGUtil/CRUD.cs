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
            }
            timer1.Start();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void LoadData()
        {
            List<List<string>> data = PG.GetFullTable(tableName);
            PG.FillTableInDataGridView(data, dataGridView1);
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
                    PG.Delete(tableName, id.ToString(), primaryKeyField);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadData();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (!PG.CheckConnection())
            {
                MessageBox.Show("Отображение данных невозможно, так как отсутствует подключение к БД!");
                return;
            }
            LoadData();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            InputDataForm form = new InputDataForm(tableName);
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
        }
    }
}
