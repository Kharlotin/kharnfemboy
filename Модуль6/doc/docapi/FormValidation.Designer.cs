namespace docapi
{
    partial class FormValidation
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelWindow = new System.Windows.Forms.TableLayoutPanel();
            this.buttonGet = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.tableLayoutPanelWindow.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelWindow
            // 
            this.tableLayoutPanelWindow.ColumnCount = 2;
            this.tableLayoutPanelWindow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWindow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWindow.Controls.Add(this.buttonGet, 0, 0);
            this.tableLayoutPanelWindow.Controls.Add(this.buttonSend, 0, 1);
            this.tableLayoutPanelWindow.Controls.Add(this.labelText, 1, 0);
            this.tableLayoutPanelWindow.Controls.Add(this.labelResult, 1, 1);
            this.tableLayoutPanelWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelWindow.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelWindow.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanelWindow.MinimumSize = new System.Drawing.Size(420, 320);
            this.tableLayoutPanelWindow.Name = "tableLayoutPanelWindow";
            this.tableLayoutPanelWindow.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanelWindow.RowCount = 2;
            this.tableLayoutPanelWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelWindow.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelWindow.TabIndex = 0;
            // 
            // buttonGet
            // 
            this.buttonGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGet.Location = new System.Drawing.Point(40, 40);
            this.buttonGet.Margin = new System.Windows.Forms.Padding(20);
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(340, 165);
            this.buttonGet.TabIndex = 0;
            this.buttonGet.Text = "ПОЛУЧИТЬ ДАННЫЕ";
            this.buttonGet.UseVisualStyleBackColor = true;
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSend.Location = new System.Drawing.Point(40, 245);
            this.buttonSend.Margin = new System.Windows.Forms.Padding(20);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(340, 165);
            this.buttonSend.TabIndex = 1;
            this.buttonSend.Text = "ОТПРАВИТЬ РЕЗУЛЬТАТ";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelText.Location = new System.Drawing.Point(403, 20);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(374, 205);
            this.labelText.TabIndex = 2;
            this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelResult.Location = new System.Drawing.Point(403, 225);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(374, 205);
            this.labelResult.TabIndex = 3;
            this.labelResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanelWindow);
            this.Name = "FormValidation";
            this.Text = "Валидация данных";
            this.tableLayoutPanelWindow.ResumeLayout(false);
            this.tableLayoutPanelWindow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelWindow;
        private System.Windows.Forms.Button buttonGet;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelResult;
    }
}

