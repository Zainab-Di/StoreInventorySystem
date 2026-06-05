using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StoreInventoryDBSystem
{
    public partial class stockMovementFrom : Form
    {
        public stockMovementFrom()
        {
            InitializeComponent();
        }

        //  زر الرجوع للشاشة الرئيسية
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Application.OpenForms["MainForm"] != null)
            {
                Application.OpenForms["MainForm"].Show();
            }
        }

        private void stockMovementFrom_Load(object sender, EventArgs e)
        {
            RefreshData();
            LoadSuppliers();
        }

        // تحميل الموردين
        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT SupplierID, SupplierName FROM Suppliers";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    combsuppliers.DataSource = dt;
                    combsuppliers.DisplayMember = "SupplierName";
                    combsuppliers.ValueMember = "SupplierID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في جلب الموردين: " + ex.Message);
            }
        }

        // دالة تحديث قراءة البيانات المخزنية وعرضها
        private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // 1. جلب المنتجات وعرضها داخل الـ DataGridView
                    string queryGrid = "SELECT ProductID AS [كود المنتج], ProductName AS [اسم المنتج], Category AS [القسم], CurrentStock AS [الكمية المتوفرة] FROM Products";
                    SqlDataAdapter da = new SqlDataAdapter(queryGrid, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // 2. تعبئة الـ ComboBox بأسماء المنتجات لاختيارها
                    string queryCombo = "SELECT ProductID, ProductName FROM Products";
                    SqlCommand cmd = new SqlCommand(queryCombo, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable comboTable = new DataTable();
                        comboTable.Load(reader);

                        comboBox1.DataSource = comboTable;
                        comboBox1.DisplayMember = "ProductName";
                        comboBox1.ValueMember = "ProductID";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء تحميل بيانات المخزن: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  زر حفظ وتطبيق الحركة المخزنية 
        private void button2_Click(object sender, EventArgs e)
        {

            int enteredQuantity = 0;
            bool isNumeric = int.TryParse(textBox1.Text.Trim(), out enteredQuantity);

            if (string.IsNullOrEmpty(textBox1.Text)||!isNumeric || enteredQuantity <= 0)
            {
                MessageBox.Show("يرجى إدخال كمية صحيحة وموجبة!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("يرجى تحديد نوع الحركة (وارد أو صادر) أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedProductID = Convert.ToInt32(comboBox1.SelectedValue);

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string checkQuery = "SELECT CurrentStock FROM Products WHERE ProductID = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    int currentQuantity = Convert.ToInt32(checkCmd.ExecuteScalar());

                    string updateQuery = "";

                    if (radioButton1.Checked)
                    {
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock + @EnteredQty WHERE ProductID = @ID";
                    }
                    else if (radioButton2.Checked)
                    {
                        if (enteredQuantity > currentQuantity)
                        {
                            MessageBox.Show($"عذراً، الكمية المطلوبة غير متوفرة! المتوفر حالياً هو ({currentQuantity}) قطع فقط.", "فشل العملية", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock - @EnteredQty WHERE ProductID = @ID";
                    }

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@EnteredQty", enteredQuantity);
                    updateCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("تم تنفيذ حركة المخزن وتحديث الكميات بنجاح!", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تنفيذ الحركة: " + ex.Message, "خطأ برمجي", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void combsuppliers_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}