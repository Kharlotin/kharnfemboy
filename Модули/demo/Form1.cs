using demo;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ShowError("Поля «Логин» и «Пароль» обязательны для заполнения.");
                return;
            }

            TryLogin(login, password);
        }

        private void TryLogin(string login, string password)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    const string sql = @"
                        SELECT UserID, Password, Role, IsBlocked, FailedAttempts
                        FROM Users WHERE Login = @Login";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                ShowWrongCredentials();
                                return;
                            }

                            int userId = reader.GetInt32(0);
                            string dbPassword = reader.GetString(1);
                            string role = reader.GetString(2);
                            bool isBlocked = reader.GetBoolean(3);
                            int failed = reader.GetInt32(4);
                            reader.Close();

                            if (isBlocked)
                            {
                                ShowError("Вы заблокированы. Обратитесь к администратору.");
                                return;
                            }

                            if (dbPassword != password)
                            {
                                int newFailed = failed + 1;
                                bool blockNow = newFailed >= 3;
                                UpdateBlock(conn, userId, blockNow, newFailed);

                                if (blockNow)
                                    ShowError("Вы заблокированы. Обратитесь к администратору.");
                                else
                                    ShowWrongCredentials();

                                return;
                            }

                            UpdateBlock(conn, userId, false, 0);
                            MessageBox.Show("Вы успешно авторизовались!", "Авторизация",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OpenNext(role);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBlock(SqlConnection conn, int userId, bool blocked, int failed)
        {
            const string sql = "UPDATE Users SET IsBlocked = @B, FailedAttempts = @F WHERE UserID = @Id";
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@B", blocked);
                cmd.Parameters.AddWithValue("@F", failed);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.ExecuteNonQuery();
            }
        }

        private void ShowWrongCredentials() =>
            MessageBox.Show(
                "Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введённые данные.",
                "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void ShowError(string msg) =>
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void OpenNext(string role)
        {
            this.Hide();
            Form next = role == "Администратор"
                ? (Form)new AdminForm()
                : new MainForm(role);
            next.FormClosed += (s, a) => this.Close();
            next.Show();
        }
    }
}