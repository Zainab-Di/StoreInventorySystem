using StoreInventorySystem;
using System;
using System.Data.SqlClient; // المكتبة الخاصة بالتعامل مع SQL Server
using System.Drawing;
using System.Windows.Forms;

namespace StoreInventoryDBSystem
{
    public partial class MainForm : Form
    {
        // 1. سلسلة الاتصال الصحيحة والنهائية الموجهة لجهاز ريان وقاعدتها
        private string connectionString = @"Server=DESKTOP-IR0K6JE\SQLEXPRESS;Database=StoreInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        public MainForm()
        {
            InitializeComponent();

            // الكود السحري لجعل حواف الأزرار دائرية وناعمة
            MakeButtonRounded(button1, 25);
            MakeButtonRounded(button2, 25);
            MakeButtonRounded(button3, 25);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // عند تشغيل الشاشة، نقوم بفحص الاتصال فوراً بالاسم الصحيح
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // محاولة فتح الاتصال

                    // إذا نجح الاتصال
                    lblStatus.Text = "تم الاتصال بقاعدة البيانات StoreInventoryDB بنجاح! ";
                    lblStatus.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    // إذا فشل الاتصال، نظهر الخطأ الأصلي لنعرف سببه
                    lblStatus.Text = "فشل الاتصال! تأكدي من السيرفر. " + ex.Message;
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductsForm form = new ProductsForm();
            form.Show();
            // كود زر إدارة المنتجات (يمكنكِ إضافة الأكواد هنا لاحقاً)
        }

        private void button2_Click(object sender, EventArgs e)
        {
        
            // 1. إنشاء نسخة من واجهة الموردين النظيفة التي أصلحناها
            frmSuppliers suppliersForm = new frmSuppliers();

            // 2. إخفاء الشاشة الرئيسية الحالية مؤقتاً
            this.Hide();

            // 3. فتح واجهة الموردين كـ نافذة حوارية مستقلة
            suppliersForm.ShowDialog();

            // 4. بعد إغلاق واجهة الموردين، تعود الشاشة الرئيسية للظهور تلقائياً
            this.Show();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            // إنشاء نسخة من شاشتكِ الجديدة عند الضغط على حركة المخزن
            stockMovementFrom stockForm = new stockMovementFrom();
            this.Hide();
            // أمر فتح الشاشة الجديدة فوق الشاشة الرئيسية
            stockForm.ShowDialog();
            this.Show(); // لإعادة إظهار الشاشة الرئيسية بعد إغلاق شاشة الحركة
        }

        private void MakeButtonRounded(Button btn, int borderRadius)
        {
            System.Drawing.Drawing2D.GraphicsPath edge = new System.Drawing.Drawing2D.GraphicsPath();

            // رسم المستطيل ذو الحواف الدائرية
            edge.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
            edge.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
            edge.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90);
            edge.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90);

            btn.Region = new System.Drawing.Region(edge);
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void lblStatus_Click(object sender, EventArgs e) { }
    }
}