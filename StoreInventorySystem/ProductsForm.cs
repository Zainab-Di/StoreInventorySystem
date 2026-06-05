using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreInventorySystem
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();

            
            this.Load += new EventHandler(ProductsForm_Load);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            ClearInputs();
        }

        // 1. دالة جلب البيانات معتمدة على الـ DatabaseHelper المركزي (Code Reuse)
        private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
                {
                    string query = "SELECT ProductID, ProductCode, ProductName, Category, CurrentStock, UnitPrice FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns["ProductID"].HeaderText = "المعرف";
                        dataGridView1.Columns["ProductCode"].HeaderText = "كود المنتج";
                        dataGridView1.Columns["ProductName"].HeaderText = "اسم المنتج";
                        dataGridView1.Columns["Category"].HeaderText = "الفئة";
                        dataGridView1.Columns["CurrentStock"].HeaderText = "المخزون الحالي";
                        dataGridView1.Columns["UnitPrice"].HeaderText = "السعر";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Text = "0";
            textBox5.Text = "0.00";
            dataGridView1.ClearSelection();
        }

        //  زر الحفظ والإضافة  
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("عذراً! كود المنتج واسمه حقول إلزامية لا يمكن تركها فارغة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
                {
                    string query = @"INSERT INTO Products (ProductCode, ProductName, Category, CurrentStock, UnitPrice) 
                                     VALUES (@ProductCode, @ProductName, @Category, @CurrentStock, @UnitPrice)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductName", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", string.IsNullOrEmpty(textBox3.Text) ? (object)DBNull.Value : textBox3.Text.Trim());

                        int stock = int.TryParse(textBox4.Text, out stock) ? stock : 0;
                        decimal price = decimal.TryParse(textBox5.Text, out price) ? price : 0.00m;

                        cmd.Parameters.AddWithValue("@CurrentStock", stock);
                        cmd.Parameters.AddWithValue("@UnitPrice", price);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم حفظ المنتج بنجاح في قاعدة البيانات!", "تمت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                        ClearInputs();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("خطأ: كود المنتج هذا مسجل مسبقاً! يرجى إدخال كود فريد وغير مكرر.", "خطأ قيد البيانات", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"خطأ في قاعدة البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //  زر التعديل  
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار منتج من الجدول أولاً ليتم تعديله.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);

            try
            {
                using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
                {
                    string query = @"UPDATE Products 
                                     SET ProductCode = @ProductCode, ProductName = @ProductName, 
                                         Category = @Category, CurrentStock = @CurrentStock, UnitPrice = @UnitPrice 
                                     WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ProductCode", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductName", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Category", string.IsNullOrEmpty(textBox3.Text) ? (object)DBNull.Value : textBox3.Text.Trim());

                        int stock = int.TryParse(textBox4.Text, out stock) ? stock : 0;
                        decimal price = decimal.TryParse(textBox5.Text, out price) ? price : 0.00m;

                        cmd.Parameters.AddWithValue("@CurrentStock", stock);
                        cmd.Parameters.AddWithValue("@UnitPrice", price);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم تحديث بيانات المنتج بنجاح!", "تمت العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshData();
                        ClearInputs();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء عملية التعديل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //  زر الحذف 
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("يرجى اختيار المنتج المراد حذفه من الجدول السفلية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ProductID"].Value);
            string productName = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();

            DialogResult dialog = MessageBox.Show($"هل أنت متأكد من حذف المنتج ({productName})؟\nحذف المنتج سيحذف حركاته المخزنية المرتبطة به.", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
                    {
                        string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            conn.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("تم حذف المنتج بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                            ClearInputs();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"خطأ أثناء الحذف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["ProductCode"].Value?.ToString() ?? "";
                textBox2.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value?.ToString() ?? "";
                textBox3.Text = dataGridView1.CurrentRow.Cells["Category"].Value?.ToString() ?? "";
                textBox4.Text = dataGridView1.CurrentRow.Cells["CurrentStock"].Value?.ToString() ?? "0";
                textBox5.Text = dataGridView1.UnderlyingCellClickFix(e.RowIndex); // تم استخدام معالجة آمنة
            }
        }

        //  زر الرجوع للشاشة الرئيسية 
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Application.OpenForms["MainForm"] != null)
            {
                Application.OpenForms["MainForm"].Show();
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "0" || textBox5.Text == "0.00") textBox5.Text = "";
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text)) textBox5.Text = "0.00";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "0") textBox4.Text = "";
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text)) textBox4.Text = "0";
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void ProductsForm_Load_1(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }

    // كلاس داخلي لتوسيع الخصائص وحل مشكلة جلب السعر الآمن 
    public static class DataGridViewExtension
    {
        public static string UnderlyingCellClickFix(this DataGridView dgv, int rowIndex)
        {
            return dgv.Rows[rowIndex].Cells["UnitPrice"].Value?.ToString() ?? "0.00";
        }
    }
}