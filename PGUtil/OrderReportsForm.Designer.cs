namespace PGUtil
{
    partial class OrderReportsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.orderIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.getDataButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewAllMenuItem,
            this.backMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(853, 28);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewAllMenuItem
            // 
            this.viewAllMenuItem.Name = "viewAllMenuItem";
            this.viewAllMenuItem.Size = new System.Drawing.Size(189, 24);
            this.viewAllMenuItem.Text = "Отобразить все данные";
            this.viewAllMenuItem.Click += new System.EventHandler(this.viewAllMenuItem_Click);
            // 
            // backMenuItem
            // 
            this.backMenuItem.Name = "backMenuItem";
            this.backMenuItem.Size = new System.Drawing.Size(65, 24);
            this.backMenuItem.Text = "Назад";
            this.backMenuItem.Click += new System.EventHandler(this.backMenuItem_Click);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionStatusLabel.Location = new System.Drawing.Point(229, 368);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 18);
            this.connectionStatusLabel.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 368);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "Статус подключения к БД:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(71, 105);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(725, 222);
            this.dataGridView1.TabIndex = 30;
            // 
            // orderIdTextBox
            // 
            this.orderIdTextBox.Location = new System.Drawing.Point(155, 58);
            this.orderIdTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orderIdTextBox.Name = "orderIdTextBox";
            this.orderIdTextBox.Size = new System.Drawing.Size(129, 22);
            this.orderIdTextBox.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(19, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 20);
            this.label1.TabIndex = 34;
            this.label1.Text = "Номер заказа:";
            // 
            // getDataButton
            // 
            this.getDataButton.Location = new System.Drawing.Point(304, 57);
            this.getDataButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.getDataButton.Name = "getDataButton";
            this.getDataButton.Size = new System.Drawing.Size(183, 23);
            this.getDataButton.TabIndex = 35;
            this.getDataButton.Text = "Сформировать отчет";
            this.getDataButton.UseVisualStyleBackColor = true;
            this.getDataButton.Click += new System.EventHandler(this.getDataButton_Click);
            // 
            // OrderReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 402);
            this.Controls.Add(this.getDataButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.orderIdTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "OrderReportsForm";
            this.Text = "Отчеты по заказам";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backMenuItem;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox orderIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button getDataButton;
    }
}