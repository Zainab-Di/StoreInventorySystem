using StoreInventorySystem;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StoreInventoryDBSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MakeButtonRounded(button1, 25);
            MakeButtonRounded(button2, 25);
            MakeButtonRounded(button3, 25);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // استدعاء كلاس DatabaseHelper  لفحص الاتصال (تطبيق مبدأ SRP وإعادة استخدام الكود)
            if (DatabaseHelper.CheckConnection(out string errorMessage))
            {
                lblStatus.Text = "تم الاتصال بقاعدة البيانات StoreInventoryDB بنجاح! ";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "فشل الاتصال! تأكدي من السيرفر. " + errorMessage;
                lblStatus.ForeColor = Color.Red;
            }
        }

        //  الدالة  لزر إدارة المنتجات
        private void button1_Click(object sender, EventArgs e)
        {
            ProductsForm form = new ProductsForm();
            OpenFormDialog(form);//إعادة استخدام للدالة
        }

        //  الدالة  لزر الموردين
        private void button2_Click(object sender, EventArgs e)
        {
            frmSuppliers form = new frmSuppliers();
            OpenFormDialog(form);//إعادة استخدام للدالة
        }

        //  الدالة  لزر حركة المخزن
        private void button3_Click(object sender, EventArgs e)
        {
            stockMovementFrom form = new stockMovementFrom();
            OpenFormDialog(form);//إعادة استخدام للدالة
        }

        // دالة موحدة لفتح الشاشات (تمنع تكرار الكود وتسهل الصيانة - Refactoring)
        private void OpenFormDialog(Form targetForm)
        {
            this.Hide();
            targetForm.ShowDialog();
            this.Show();
        }

        private void MakeButtonRounded(Button btn, int borderRadius)
        {
            using (System.Drawing.Drawing2D.GraphicsPath edge = new System.Drawing.Drawing2D.GraphicsPath())
            {
                edge.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                edge.AddArc(btn.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
                edge.AddArc(btn.Width - borderRadius, btn.Height - borderRadius, borderRadius, borderRadius, 0, 90);
                edge.AddArc(0, btn.Height - borderRadius, borderRadius, borderRadius, 90, 90);
                btn.Region = new System.Drawing.Region(edge);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void lblStatus_Click(object sender, EventArgs e) { }
    }
}