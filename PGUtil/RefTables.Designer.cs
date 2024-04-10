namespace PGUtil
{
    partial class RefTables
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.workersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specializationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.worksForSpecializationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specializationsForWorkersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderStatusesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentTypesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workersMenuItem,
            this.worksMenuItem,
            this.specializationsMenuItem,
            this.worksForSpecializationsMenuItem,
            this.specializationsForWorkersMenuItem,
            this.orderStatusesMenuItem,
            this.paymentTypesMenuItem,
            this.BackMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1164, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // workersMenuItem
            // 
            this.workersMenuItem.Name = "workersMenuItem";
            this.workersMenuItem.Size = new System.Drawing.Size(97, 24);
            this.workersMenuItem.Text = "Работники";
            this.workersMenuItem.Click += new System.EventHandler(this.workersMenuItem_Click);
            // 
            // worksMenuItem
            // 
            this.worksMenuItem.Name = "worksMenuItem";
            this.worksMenuItem.Size = new System.Drawing.Size(74, 24);
            this.worksMenuItem.Text = "Работы";
            this.worksMenuItem.Click += new System.EventHandler(this.worksMenuItem_Click);
            // 
            // specializationsMenuItem
            // 
            this.specializationsMenuItem.Name = "specializationsMenuItem";
            this.specializationsMenuItem.Size = new System.Drawing.Size(134, 24);
            this.specializationsMenuItem.Text = "Специализации";
            this.specializationsMenuItem.Click += new System.EventHandler(this.specializationsMenuItem_Click);
            // 
            // worksForSpecializationsMenuItem
            // 
            this.worksForSpecializationsMenuItem.Name = "worksForSpecializationsMenuItem";
            this.worksForSpecializationsMenuItem.Size = new System.Drawing.Size(223, 24);
            this.worksForSpecializationsMenuItem.Text = "Специализации и их работы";
            this.worksForSpecializationsMenuItem.Click += new System.EventHandler(this.worksForSpecializationsMenuItem_Click);
            // 
            // specializationsForWorkersMenuItem
            // 
            this.specializationsForWorkersMenuItem.Name = "specializationsForWorkersMenuItem";
            this.specializationsForWorkersMenuItem.Size = new System.Drawing.Size(243, 24);
            this.specializationsForWorkersMenuItem.Text = "Работники и их специализации";
            this.specializationsForWorkersMenuItem.Click += new System.EventHandler(this.specializationsForWorkersMenuItem_Click);
            // 
            // orderStatusesMenuItem
            // 
            this.orderStatusesMenuItem.Name = "orderStatusesMenuItem";
            this.orderStatusesMenuItem.Size = new System.Drawing.Size(135, 24);
            this.orderStatusesMenuItem.Text = "Статусы заказов";
            this.orderStatusesMenuItem.Click += new System.EventHandler(this.orderStatusesMenuItem_Click);
            // 
            // paymentTypesMenuItem
            // 
            this.paymentTypesMenuItem.Name = "paymentTypesMenuItem";
            this.paymentTypesMenuItem.Size = new System.Drawing.Size(115, 24);
            this.paymentTypesMenuItem.Text = "Типы оплаты";
            this.paymentTypesMenuItem.Click += new System.EventHandler(this.paymentTypesMenuItem_Click);
            // 
            // BackMenuItem
            // 
            this.BackMenuItem.Name = "BackMenuItem";
            this.BackMenuItem.Size = new System.Drawing.Size(65, 24);
            this.BackMenuItem.Text = "Назад";
            this.BackMenuItem.Click += new System.EventHandler(this.backMenuItem_Click);
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionStatusLabel.Location = new System.Drawing.Point(232, 79);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 18);
            this.connectionStatusLabel.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(15, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Статус подключения к БД:";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RefTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 146);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RefTables";
            this.Text = "Справочники";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem BackMenuItem;
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem workersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem worksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specializationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specializationsForWorkersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem worksForSpecializationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderStatusesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentTypesMenuItem;
    }
}