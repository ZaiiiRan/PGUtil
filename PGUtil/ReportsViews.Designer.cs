namespace PGUtil
{
    partial class ReportsViews
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
            this.connectionStatusLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.periodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderReceiptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.AutoSize = true;
            this.connectionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.connectionStatusLabel.Location = new System.Drawing.Point(229, 78);
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 18);
            this.connectionStatusLabel.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(196, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "Статус подключения к БД:";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // periodMenuItem
            // 
            this.periodMenuItem.Name = "periodMenuItem";
            this.periodMenuItem.Size = new System.Drawing.Size(137, 24);
            this.periodMenuItem.Text = "Отчет за период";
            this.periodMenuItem.Click += new System.EventHandler(this.periodMenuItem_Click);
            // 
            // orderMenuItem
            // 
            this.orderMenuItem.Name = "orderMenuItem";
            this.orderMenuItem.Size = new System.Drawing.Size(132, 24);
            this.orderMenuItem.Text = "Отчет по заказу";
            this.orderMenuItem.Click += new System.EventHandler(this.orderMenuItem_Click);
            // 
            // orderReceiptMenuItem
            // 
            this.orderReceiptMenuItem.Name = "orderReceiptMenuItem";
            this.orderReceiptMenuItem.Size = new System.Drawing.Size(219, 24);
            this.orderReceiptMenuItem.Text = "Квитанция на оплату заказа";
            this.orderReceiptMenuItem.Click += new System.EventHandler(this.orderReceiptMenuItem_Click);
            // 
            // BackMenuItem
            // 
            this.BackMenuItem.Name = "BackMenuItem";
            this.BackMenuItem.Size = new System.Drawing.Size(65, 24);
            this.BackMenuItem.Text = "Назад";
            this.BackMenuItem.Click += new System.EventHandler(this.BackMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.periodMenuItem,
            this.orderMenuItem,
            this.orderReceiptMenuItem,
            this.BackMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(705, 28);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ReportsViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 141);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.connectionStatusLabel);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReportsViews";
            this.Text = "Отчеты";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label connectionStatusLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem periodMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderReceiptMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BackMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}