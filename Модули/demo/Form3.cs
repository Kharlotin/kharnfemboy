using demo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class MainForm : Form
    {
        private readonly string _role;

        public MainForm(string role)
        {
            _role = role;
            InitializeComponent();
            this.Text = $"ООО \"Мясной цех №1\" — Рабочий стол ({role})";
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            o.OrderNumber AS [Номер заказа],
                            o.OrderDate AS [Дата],
                            o.Executor AS [Исполнитель],
                            c.Name AS [Заказчик],
                            SUM(oi.Quantity * oi.Price) AS [Сумма]
                        FROM Orders o
                        JOIN Customers c ON o.CustomerCode = c.CustomerCode
                        JOIN OrderItems oi ON o.OrderID = oi.OrderID
                        GROUP BY o.OrderNumber, o.OrderDate, o.Executor, c.Name";

                    using (var adapter = new SqlDataAdapter(sql, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dgvOrders.DataSource = table;
                        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заказов: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductions()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            p.ProductionNumber AS [Номер производства],
                            p.ProductionDate AS [Дата],
                            pi.ItemCode AS [Код],
                            CASE pi.ItemType
                                WHEN 'Product' THEN pr.Name
                                WHEN 'Material' THEN m.Name
                            END AS [Наименование],
                            pi.ItemType AS [Тип],
                            pi.Quantity AS [Количество],
                            pi.Unit AS [Ед. изм.]
                        FROM Productions p
                        JOIN ProductionItems pi ON p.ProductionID = pi.ProductionID
                        LEFT JOIN Products pr ON pi.ItemCode = pr.Code AND pi.ItemType = 'Product'
                        LEFT JOIN Materials m ON pi.ItemCode = m.Code AND pi.ItemType = 'Material'";

                    using (var adapter = new SqlDataAdapter(sql, conn))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        dgvOrders.DataSource = table;
                        dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки производств: " + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrders_Click(object sender, EventArgs e) => LoadOrders();
        private void btnProductions_Click(object sender, EventArgs e) => LoadProductions();
    }
}