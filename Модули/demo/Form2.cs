using demo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace demo
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT UserID, Login, Role, IsBlocked FROM Users ORDER BY UserID";
                    using (var adapter = new SqlDataAdapter(sql, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);

                        // Переименовываем столбцы для отображения
                        table.Columns["UserID"].ColumnName = "ID";
                        table.Columns["Login"].ColumnName = "Логин";
                        table.Columns["Role"].ColumnName = "Роль";
                        table.Columns["IsBlocked"].ColumnName = "Заблокирован";

                        dgvUsers.DataSource = table;
                        dgvUsers.Columns["ID"].Width = 50;
                        dgvUsers.Columns["Логин"].Width = 150;
                        dgvUsers.Columns["Роль"].Width = 150;
                        dgvUsers.Columns["Заблокирован"].Width = 110;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки пользователей: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var dialog = new UserEditForm(null))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    AddUser(dialog.UserLogin, dialog.UserPassword, dialog.UserRole);
                }
            }
        }

        private void AddUser(string login, string password, string role)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Проверяем существование логина
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Login = @Login";
                    using (var cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"Пользователь с логином «{login}» уже существует.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertSql = "INSERT INTO Users (Login, Password, Role) VALUES (@Login, @Password, @Role)";
                    using (var cmd = new SqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Пользователь успешно добавлен.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления пользователя: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int userId = (int)dgvUsers.CurrentRow.Cells["ID"].Value;
            string currentLogin = dgvUsers.CurrentRow.Cells["Логин"].Value.ToString();
            string currentRole = dgvUsers.CurrentRow.Cells["Роль"].Value.ToString();

            using (var dialog = new UserEditForm(new UserInfo { Login = currentLogin, Role = currentRole }))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateUser(userId, dialog.UserLogin, dialog.UserPassword, dialog.UserRole);
                }
            }
        }

        private void UpdateUser(int userId, string login, string password, string role)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Проверяем дублирование логина (не считая текущего пользователя)
                    string checkSql = "SELECT COUNT(*) FROM Users WHERE Login = @Login AND UserID <> @Id";
                    using (var cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Id", userId);
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show($"Пользователь с логином «{login}» уже существует.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string sql = string.IsNullOrEmpty(password)
                        ? "UPDATE Users SET Login = @Login, Role = @Role WHERE UserID = @Id"
                        : "UPDATE Users SET Login = @Login, Password = @Password, Role = @Role WHERE UserID = @Id";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@Id", userId);
                        if (!string.IsNullOrEmpty(password))
                            cmd.Parameters.AddWithValue("@Password", password);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Данные пользователя обновлены.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления пользователя: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToggleBlock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;

            int userId = (int)dgvUsers.CurrentRow.Cells["ID"].Value;
            bool isBlocked = (bool)dgvUsers.CurrentRow.Cells["Заблокирован"].Value;
            string action = isBlocked ? "разблокировать" : "заблокировать";

            var result = MessageBox.Show($"Вы уверены, что хотите {action} этого пользователя?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE Users SET IsBlocked = @IsBlocked, FailedAttempts = 0 WHERE UserID = @Id";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsBlocked", !isBlocked);
                        cmd.Parameters.AddWithValue("@Id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    string msg = isBlocked ? "Пользователь разблокирован." : "Пользователь заблокирован.";
                    MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToMain_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm("Администратор");
            mainForm.Show();
        }
    }

    public class UserInfo
    {
        public string Login { get; set; }
        public string Role { get; set; }
    }
}