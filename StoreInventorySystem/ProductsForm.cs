using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient; // 💡 تم إضافة هذه المكتبة للاتصال بقاعدة البيانات

namespace StoreInventorySystem
{
    public partial class ProductsForm : Form
    {
        // نص الاتصال بقاعدة البيانات - قم بتغيير اسم السيرفر (Data Source) حسب جهازك
        private string connectionString = "Data Source=.;Initial Catalog=StoreInventoryDB;Integrated Security=True;TrustServerCertificate=True;";

        public ProductsForm()
        {
            InitializeComponent();

            // ربط حدث تحميل الصفحة لتهيئة البيانات برمجياً عند فتح الشاشة
            this.Load += new EventHandler(ProductsForm_Load);

            // ربط حدث الضغط على خلايا الجدول لنقل البيانات فوراً عند اختيار منتج
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellClick);
        }

        // دالة مخصصة تُنفذ عند تشغيل الشاشة لأول مرة
        private void ProductsForm_Load(object sender, EventArgs e)
        {
            RefreshData();
            ClearInputs();
        }

        // دالة جلب البيانات من SQL Server وعرضها في الـ DataGridView
        private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductID, ProductCode, ProductName, Category, CurrentStock, UnitPrice FROM Products";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt; // ربط جدول الواجهة بالبيانات

                    // تسمية الأعمدة بشكل احترافي ومفهوم للمستخدم
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

        // دالة مساعدة لتنظيف وترسيت خانات الإدخال للقيم الافتراضية الذكية
        private void ClearInputs()
        {
            textBox1.Clear(); // كود المنتج
            textBox2.Clear(); // اسم المنتج
            textBox3.Clear(); // الفئة
            textBox4.Text = "0";    // الكمية الابتدائية (Default = 0 كما بالمتطلبات)
            textBox5.Text = "0.00"; // السعر (مجهز لـ Decimal)
            dataGridView1.ClearSelection();
        }

        // 🔘 زر الحفظ / الإضافة (btnSave / btnAdd) -> تفترض أنه button1
        private void button1_Click(object sender, EventArgs e)
        {
            // التحقق من الحقول الإلزامية لتجنب أخطاء الـ NULL في قاعدة البيانات
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("عذراً! كود المنتج واسمه حقول إلزامية لا يمكن تركها فارغة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Products (ProductCode, ProductName, Category, CurrentStock, UnitPrice) 
                                     VALUES (@ProductCode, @ProductName, @Category, @CurrentStock, @UnitPrice)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@ProductName", textBox2.Text.Trim()); // يدعم العربية (NVARCHAR)
                        cmd.Parameters.AddWithValue("@Category", string.IsNullOrEmpty(textBox3.Text) ? (object)DBNull.Value : textBox3.Text.Trim());

                        // تحويل آمن للأرقام لمنع الكراش
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
                // معالجة ذكية إذا تكرر كود المنتج (Unique Constraint violation)
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

        // 🔘 زر التعديل (btnUpdate) -> تفترض أنه button2
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
                using (SqlConnection conn = new SqlConnection(connectionString))
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

        // 🔘 زر الحذف (btnDelete) -> تفترض أنه button3
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
                    using (SqlConnection conn = new SqlConnection(connectionString))
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

        // 🔘 زر تنظيف الخانات وتفريغها (btnClear) -> تفترض أنه button4
        private void button4_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // حدث مخصص لنقل البيانات من السطر المختار في الـ DataGridView إلى صناديق النصوص فوق تلقائياً
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["ProductCode"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["ProductName"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["Category"].Value?.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells["CurrentStock"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["UnitPrice"].Value.ToString();
            }
        }

        // --- الأحداث التلقائية الأخرى المتروكة فارغة بناءً على طلبك لعدم الحاجة لها برمجياً ---
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { } // كود المنتج
        private void textBox2_TextChanged(object sender, EventArgs e) { } // اسم المنتج
        private void textBox3_TextChanged(object sender, EventArgs e) { } // الفئة
        private void textBox4_TextChanged(object sender, EventArgs e) { } // المخزون الحالي
        private void textBox5_TextChanged(object sender, EventArgs e) { } // السعر

        private void button5_Click(object sender, EventArgs e)
        {
           
        }
    }
}