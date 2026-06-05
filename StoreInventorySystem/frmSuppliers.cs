using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace StoreInventorySystem
{
    public partial class frmSuppliers : Form
    {
        public frmSuppliers()
        {
            InitializeComponent();
        }

        // 1. دالة جلب وقراءة بيانات الموردين 
        private void LoadSuppliers()
        {
            // Code Reuse: استدعاء الاتصال المركزي 
            using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
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

            // تخصيص أسماء الأعمدة للمستخدم
            if (dgvSuppliers.Columns["SupplierID"] != null) dgvSuppliers.Columns["SupplierID"].HeaderText = "المعرف";
            if (dgvSuppliers.Columns["SupplierName"] != null) dgvSuppliers.Columns["SupplierName"].HeaderText = "اسم المورد";
            if (dgvSuppliers.Columns["ContactPhone"] != null) dgvSuppliers.Columns["ContactPhone"].HeaderText = "رقم الهاتف";
            if (dgvSuppliers.Columns["Email"] != null) dgvSuppliers.Columns["Email"].HeaderText = "البريد الإلكتروني";
            if (dgvSuppliers.Columns["Address"] != null) dgvSuppliers.Columns["Address"].HeaderText = "العنوان";
        }

        // 2. كود زر الحفظ (Save) بعد عمل Refactoring لتنظيفه
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show(" الرجاء تعبئة جميع البيانات");
                return;
            }

            using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = "INSERT INTO Suppliers (SupplierName, ContactPhone, Email, Address) VALUES (@name, @phone, @email, @address)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تم حفظ المورد بنجاح!");
                    LoadSuppliers();
                    ClearFormFields(); // الالتزام بـ Clean Code عبر استدعاء دالة المسح
                }
                catch (Exception ex)
                {
                    MessageBox.Show("حدث خطأ أثناء الحفظ: " + ex.Message);
                }
            }
        }

        // كود الضغط على صف داخل الجدول لعرض البيانات في الخانات (بقي كما هو لسلامته)
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);

            using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
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

        // 4. كود زر الحذف (Delete) 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvSuppliers.CurrentRow.Cells["SupplierID"].Value);

            if (MessageBox.Show("هل أنت متأكد من حذف هذا المورد؟", "تأكيد", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = StoreInventoryDBSystem.DatabaseHelper.GetConnection())
                {
                    try
                    {
                        string query = "DELETE FROM Suppliers WHERE SupplierID=@id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("تم الحذف بنجاح!");
                        LoadSuppliers();
                        ClearFormFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("حدث خطأ أثناء الحذف: " + ex.Message);
                    }
                }
            }
        }

        // 5. زر التحديث وإعادة الضبط (تفريغ الخانات)
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearFormFields();
            if (dgvSuppliers.DataSource != null)
            {
                dgvSuppliers.ClearSelection();
            }
        }

        // 6. زر العودة للشاشة الرئيسية
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            if (Application.OpenForms["MainForm"] != null)
            {
                Application.OpenForms["MainForm"].Show();
            }
        }

        // Refactoring (Extract Method): دالة موحدة لتفريغ الحقول لمنع تكرار الأسطر 
        private void ClearFormFields()
        {
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
        }


        private void label2_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void dgvSuppliers_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void gbDetails_Enter(object sender, EventArgs e) { }
    }
}

