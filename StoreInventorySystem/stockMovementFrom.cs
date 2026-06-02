using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreInventoryDBSystem
{
    public partial class stockMovementFrom : Form
    {
        // سلسلة الاتصال بالسيرفر المحلي الخاص بكِ
        private string connectionString = @"Server=.;Database=StoreInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;"; public stockMovementFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. إغلاق شاشة حركة المخزن الحالية
            this.Close();

            // 2. إظهار الشاشة الرئيسية القديمة مجدداً
            // (الفيجوال ستوديو سيتعرف تلقائياً على الفورم المفتوح في الخلفية ويظهره)
            if (Application.OpenForms["MainForm"] != null)
            {
                Application.OpenForms["MainForm"].Show();
            }
        }

        private void stockMovementFrom_Load(object sender, EventArgs e)
        {
            // تشغيل دالة جلب البيانات لتعبئة الجدول والقائمة فور إقلاع الشاشة
            RefreshData();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SupplierID, SupplierName FROM Suppliers";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    combsuppliers.DataSource = dt;
                    combsuppliers.DisplayMember = "SupplierName"; // الاسم الذي يظهر للمستخدم
                    combsuppliers.ValueMember = "SupplierID";     // المعرف المخفي برمجياً
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في جلب الموردين: " + ex.Message);
            }
        }
        

        // دالة مخصصة لقراءة البيانات وتحديثها في الشاشة في أي وقت
        private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 1. جلب المنتجات وعرضها داخل الـ DataGridView
                    string queryGrid = "SELECT ProductID AS [كود المنتج], ProductName AS [اسم المنتج], Category AS [القسم], CurrentStock AS [الكمية المتوفرة] FROM Products";
                    SqlDataAdapter da = new SqlDataAdapter(queryGrid, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt; // تأكدي من اسم الـ DataGridView عندكِ

                    // 2. تعبئة الـ ComboBox بأسماء المنتجات لاختيارها
                    string queryCombo = "SELECT ProductID, ProductName FROM Products";
                    SqlCommand cmd = new SqlCommand(queryCombo, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable comboTable = new DataTable();
                    comboTable.Load(reader);

                    comboBox1.DataSource = comboTable;
                    comboBox1.DisplayMember = "ProductName"; // النص الذي يظهر للمستخدم (اسم المنتج)
                    comboBox1.ValueMember = "ProductID";     // القيمة المخفية البرمجية (رقم المنتج)
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تحميل بيانات المخزن: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            // 1. التحقق من إدخال كمية صحيحة
            if (string.IsNullOrEmpty(textBox1.Text) || !int.TryParse(textBox1.Text, out int enteredQuantity) || enteredQuantity <= 0)
            {
                MessageBox.Show("يرجى إدخال كمية صحيحة وموجبة!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. التحقق من تحديد نوع الحركة
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("يرجى تحديد نوع الحركة (وارد أو صادر) أولاً!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. جلب رقم المنتج المختار من الكومبو بوكس
            int selectedProductID = Convert.ToInt32(comboBox1.SelectedValue);

            // 4. الاتصال بالسيرفر والتعديل بناءً على اسم العمود الصحيح CurrentStock
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // قراءة المخزون الحالي المتوفر في جدول المنتجات
                    string checkQuery = "SELECT CurrentStock FROM Products WHERE ProductID = @ID";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    int currentQuantity = Convert.ToInt32(checkCmd.ExecuteScalar());

                    string updateQuery = "";

                    // حالة حركة وارد -> زيادة المخزون
                    if (radioButton1.Checked)
                    {
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock + @EnteredQty WHERE ProductID = @ID";
                    }
                    // حالة حركة صادر -> خصم المخزون مع الحماية من السحب الفائض
                    else if (radioButton2.Checked)
                    {
                        if (enteredQuantity > currentQuantity)
                        {
                            MessageBox.Show($"عذراً، الكمية المطلوبة غير متوفرة! المتوفر حالياً هو ({currentQuantity}) قطع فقط.", "فشل العملية", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        updateQuery = "UPDATE Products SET CurrentStock = CurrentStock - @EnteredQty WHERE ProductID = @ID";
                    }

                    // تنفيذ أمر التحديث النهائي في قاعدة البيانات
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@EnteredQty", enteredQuantity);
                    updateCmd.Parameters.AddWithValue("@ID", selectedProductID);
                    updateCmd.ExecuteNonQuery();

                    // إشعار بالنجاح وتحديث الشاشة
                    MessageBox.Show("تم تنفيذ حركة المخزن وتحديث الكميات بنجاح!", "نجاح العملية", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    textBox1.Clear();
                    RefreshData(); // استدعاء دالة التحديث ليرى المستخدم الأرقام الجديدة بالجدول
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء تنفيذ الحركة: " + ex.Message, "خطأ برمي", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void combsuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


