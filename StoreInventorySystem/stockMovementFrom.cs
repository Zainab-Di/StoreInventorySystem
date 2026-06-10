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
    /// <summary>
    /// SOLID Principles (Lecture 8):
    /// ملاحظة نقدية للمناقشة: هذه الشاشة تحتوي حالياً على أكواد SQL مباشرة (Tight Coupling).
    /// وفقاً للمحاضرة الثامنة، يفضل مستقبلاً عزل هذه الأكواد في كلاس منفصل (Data Access Layer) 
    /// لتحقيق مبدأ المسؤولية الواحدة (SRP) بشكل كامل وفصل واجهة المستخدم عن تفاصيل قاعدة البيانات.
    /// </summary>
    public partial class stockMovementFrom : Form
    {
        public stockMovementFrom()
        {
            InitializeComponent();
        }

        // زر الرجوع للشاشة الرئيسية
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

        // تحميل الموردين داخل الـ ComboBox
        private void LoadSuppliers()
        {
            try
            { 
                // لحماية النظام من تسريب البيانات أو الموارد (Memory Leaks).
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
                    SqlDataAdapter daCombo = new SqlDataAdapter(queryCombo, conn);
                    DataTable comboTable = new DataTable();
                    daCombo.Fill(comboTable);

                    comboBox1.DataSource = comboTable;
                    comboBox1.DisplayMember = "ProductName";
                    comboBox1.ValueMember = "ProductID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء تحميل بيانات المخزن: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // زر حفظ وتطبيق الحركة المخزنية (وارد / صادر)
        private void button2_Click(object sender, EventArgs e)
        {
            int enteredQuantity = 0;
            // التحقق من المدخلات لضمان جودة البيانات ومنع الأخطاء البرمجية المفاجئة
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

                    // فحص الكمية الحالية المتوفرة في المخزن قبل الخصم
                    string checkQuery = "SELECT CurrentStock FROM Products WHERE ProductID = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    int currentQuantity = Convert.ToInt32(checkCmd.ExecuteScalar());

                    string updateQuery = "";

                    // تحديد نوع العملية بناءً على اختيار المستخدم
                    if (radioButton1.Checked)
                    {
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock + @EnteredQty WHERE ProductID = @ID";
                    }
                    else if (radioButton2.Checked)
                    {
                        // حماية النظام: منع صرف كمية أكبر من المتوفرة في المخزن لضمان سلامة البيانات (Data Integrity)
                        if (enteredQuantity > currentQuantity)
                        {
                            MessageBox.Show($"عذراً، الكمية المطلوبة غير متوفرة! المتوفر حالياً هو ({currentQuantity}) قطع فقط.", "فشل العملية", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock - @EnteredQty WHERE ProductID = @ID";
                    }

                    // تنفيذ أمر التحديث في قاعدة البيانات
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@EnteredQty", enteredQuantity);
                    updateCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("تم تنفيذ حركة المخزن وتحديث الكميات بنجاح!", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // تنظيف الشاشة وتحديث البيانات بعد النجاح
                    textBox1.Clear();
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تنفيذ الحركة: " + ex.Message, "خطأ برمجي", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // أحداث فارغة يمكن تركها أو مسحها لاحقاً حسب تصميم الواجهة
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void combsuppliers_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}