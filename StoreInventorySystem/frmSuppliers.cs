using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreInventorySystem
{
    public partial class frmSuppliers : Form
    {
        // سلسلة الاتصال المباشرة بسيرفر ريان وقاعدتها الحالية
        private string connectionString = @"Server=DESKTOP-IR0K6JE\SQLEXPRESS;Database=StoreInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;";

        public frmSuppliers()
        {
            InitializeComponent();
        }

        // دالة جلب وقراءة بيانات الموردين من القاعدة للجدول
        private void LoadSuppliers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Suppliers";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvSuppliers.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ في جلب البيانات: " + ex.Message);
                }
            }
        }

        private void frmSuppliers_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        // كود زر الحفظ (Save)
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("الرجاء تعبئة اسم المورد ورقم الهاتف");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Suppliers (SupplierName, ContactPhone, Email, Address) VALUES (@name, @phone, @email, @address)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم حفظ المورد بنجاح!");

                    LoadSuppliers();

                    txtName.Clear();
                    txtPhone.Clear();
                    txtEmail.Clear();
                    txtAddress.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message);
                }
            }
        }

        // كود الضغط على صف داخل الجدول لعرض البيانات في الخانات
        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];

                txtName.Text = row.Cells["SupplierName"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["ContactPhone"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
            }
        }

        // كود زر التعديل (Update)
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "UPDATE Suppliers SET SupplierName=@name, ContactPhone=@phone, Email=@email, Address=@address WHERE SupplierID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("تم التحديث بنجاح!");
                    LoadSuppliers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء التحديث: " + ex.Message);
                }
            }
        }

        // كود زر الحذف (Delete)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);

            if (MessageBox.Show("هل أنت متأكد من حذف هذا المورد؟", "تأكيد", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        string query = "DELETE FROM Suppliers WHERE SupplierID=@id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        LoadSuppliers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ أثناء الحذف: " + ex.Message);
                    }
                }
            }
        }

        // كود زر التحديث وتفريغ الخانات (Refresh)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            if (dgvSuppliers.DataSource != null)
            {
                dgvSuppliers.ClearSelection();
            }
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
    }
}