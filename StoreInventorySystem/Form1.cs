using System;
using System.Windows.Forms;
using System.Data.SqlClient; // المكتبة الخاصة بالتعامل مع SQL Server

namespace StoreInventorySystem
{
    public partial class MainForm : Form
    {
        // سلسلة الاتصال الخاصة بجهازك وقاعدتك المخصصة
        private string connectionString = "Data Source=.;Initial Catalog=StoreInventoryDB;Integrated Security=True;TrustServerCertificate=True;";

        public MainForm()
        {
            InitializeComponent();


            // الكود السحري لجعل حواف الأزرار دائرية وناعمة
            MakeButtonRounded(button1, 25);
            MakeButtonRounded(button2, 25);
            MakeButtonRounded(button3, 25);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=YOUR_SERVER; Database=StoreInventoryDB; Trusted_Connection=True;";




        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            // 1. الأكواد للحواف الدائرية الخاصة بكِ
            MakeButtonRounded(button1, 25);
            MakeButtonRounded(button2, 25);
            MakeButtonRounded(button3, 25);

            // 2. تعريف السلسلة واختبار الاتصال في نفس المكان
            string connectionString = "Server=.; Database=StoreInventoryDB; Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open(); // محاولة فتح الاتصال

                    // إذا نجح الاتصال
                    lblStatus.Text = "StoreInventoryDB:متصل بقاعدة البيانات بنجاح ";
                    lblStatus.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    // إذا فشل الاتصال
                    lblStatus.Text = "فشل الاتصال بقاعدة البيانات! يرجى التحقق من السيرفر.";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ProductsForm form = new ProductsForm();
            form.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

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

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            // إنشاء نسخة من شاشتكِ الجديدة
            stockMovementFrom stockForm = new stockMovementFrom();
            this.Hide();
            // أمر فتح الشاشة الجديدة فوق الشاشة الرئيسية
            stockForm.ShowDialog();
        
    }
    }
}