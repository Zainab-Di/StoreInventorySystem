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
            // هذا الكود يتأكد من أن الاتصال شغال تمام بمجرد فتح البرنامج
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // يمكنك إلغاء هذه الرسالة لاحقاً، لكنها ممتازة الآن للتأكد من نجاح الربط عند التشغيل
                    MessageBox.Show("تم الاتصال بنجاح بقاعدة البيانات المخصصة للمخزن!", "نجاح الربط", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("عذراً، فشل الاتصال بقاعدة البيانات: " + ex.Message, "خطأ في الاتصال", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}