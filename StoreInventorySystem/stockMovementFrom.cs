using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StoreInventorySystem
{
    public partial class stockMovementFrom : Form
    {
        // سلسلة الاتصال بالسيرفر المحلي الخاص بكِ
        private string connectionString = "Server=.; Database=StoreInventoryDB; Trusted_Connection=True;";
        public stockMovementFrom()
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
    }
}

