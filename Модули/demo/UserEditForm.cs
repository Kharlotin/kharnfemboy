using demo;
using System;
using System.Windows.Forms;

namespace demo
{
    public class UserEditForm : Form
    {
        public string UserLogin { get; private set; }
        public string UserPassword { get; private set; }
        public string UserRole { get; private set; }

        private TextBox txtLogin;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnOk;
        private Button btnCancel;

        public UserEditForm(UserInfo existingUser)
        {
            InitializeComponents(existingUser);
        }

        private void InitializeComponents(UserInfo existing)
        {
            this.Text = existing == null ? "Добавить пользователя" : "Изменить пользователя";
            this.ClientSize = new System.Drawing.Size(360, 230);
            this.MinimumSize = new System.Drawing.Size(376, 270);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            var font = new System.Drawing.Font("Segoe UI", 10F);

            var lblLogin = new Label { Text = "Логин:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(80, 24), Font = font };
            txtLogin = new TextBox { Location = new System.Drawing.Point(110, 18), Size = new System.Drawing.Size(220, 26), Font = font };

            var lblPassword = new Label { Text = "Пароль:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(80, 24), Font = font };
            txtPassword = new TextBox { Location = new System.Drawing.Point(110, 58), Size = new System.Drawing.Size(220, 26), Font = font, PasswordChar = '●' };

            var lblRole = new Label { Text = "Роль:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(80, 24), Font = font };
            cmbRole = new ComboBox
            {
                Location = new System.Drawing.Point(110, 98),
                Size = new System.Drawing.Size(220, 26),
                Font = font,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.AddRange(new object[] { "Администратор", "Пользователь" });

            if (existing != null)
            {
                txtLogin.Text = existing.Login;
                cmbRole.SelectedItem = existing.Role;
                var hint = new Label
                {
                    Text = "Оставьте пароль пустым, чтобы не менять",
                    Location = new System.Drawing.Point(20, 140),
                    Size = new System.Drawing.Size(320, 20),
                    Font = new System.Drawing.Font("Segoe UI", 8F),
                    ForeColor = System.Drawing.Color.Gray
                };
                this.Controls.Add(hint);
            }
            else
            {
                cmbRole.SelectedIndex = 1;
            }

            btnOk = new Button
            {
                Text = "Сохранить",
                Location = new System.Drawing.Point(80, 175),
                Size = new System.Drawing.Size(100, 34),
                Font = font,
                BackColor = System.Drawing.Color.SteelBlue,
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button
            {
                Text = "Отмена",
                Location = new System.Drawing.Point(195, 175),
                Size = new System.Drawing.Size(100, 34),
                Font = font,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                lblLogin, txtLogin,
                lblPassword, txtPassword,
                lblRole, cmbRole,
                btnOk, btnCancel
            });
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserLogin = txtLogin.Text.Trim();
            UserPassword = txtPassword.Text;
            UserRole = cmbRole.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}