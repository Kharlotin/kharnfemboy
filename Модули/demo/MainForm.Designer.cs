namespace demo
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnProductions;
        private System.Windows.Forms.Panel pnlTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnProductions = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvOrders.Location = new System.Drawing.Point(0, 48);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(800, 452);
            this.dgvOrders.TabIndex = 0;
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOrders.ForeColor = System.Drawing.Color.White;
            this.btnOrders.Location = new System.Drawing.Point(10, 8);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(180, 34);
            this.btnOrders.TabIndex = 0;
            this.btnOrders.Text = "Заказы покупателей";
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // btnProductions
            // 
            this.btnProductions.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnProductions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnProductions.ForeColor = System.Drawing.Color.White;
            this.btnProductions.Location = new System.Drawing.Point(200, 8);
            this.btnProductions.Name = "btnProductions";
            this.btnProductions.Size = new System.Drawing.Size(140, 34);
            this.btnProductions.TabIndex = 1;
            this.btnProductions.Text = "Производство";
            this.btnProductions.UseVisualStyleBackColor = false;
            this.btnProductions.Click += new System.EventHandler(this.btnProductions_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnOrders);
            this.pnlTop.Controls.Add(this.btnProductions);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(800, 48);
            this.pnlTop.TabIndex = 1;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}