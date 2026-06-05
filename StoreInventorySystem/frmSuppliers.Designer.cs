namespace StoreInventorySystem
{
    partial class frmSuppliers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuppliers));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtName = new TextBox();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            gbDetails = new GroupBox();
            gbActions = new GroupBox();
            button1 = new Button();
            panel1 = new Panel();
            label5 = new Label();
            dgvSuppliers = new DataGridView();
            groupBox1 = new GroupBox();
            gbDetails.SuspendLayout();
            gbActions.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label1.Location = new Point(319, 42);
            label1.Name = "label1";
            label1.Size = new Size(102, 28);
            label1.TabIndex = 0;
            label1.Text = "اسم المورد";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.Location = new Point(741, 42);
            label2.Name = "label2";
            label2.Size = new Size(66, 28);
            label2.TabIndex = 1;
            label2.Text = "الهاتف";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.Location = new Point(741, 115);
            label3.Name = "label3";
            label3.Size = new Size(151, 28);
            label3.TabIndex = 2;
            label3.Text = "البرريد الإلكتروني";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.Location = new Point(326, 111);
            label4.Name = "label4";
            label4.Size = new Size(71, 28);
            label4.TabIndex = 3;
            label4.Text = "العنوان";
            // 
            // txtName
            // 
            txtName.Location = new Point(70, 42);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(203, 27);
            txtName.TabIndex = 4;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(532, 42);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(203, 27);
            txtPhone.TabIndex = 5;
            txtPhone.TextChanged += txtPhone_TextChanged;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(532, 115);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(203, 27);
            txtEmail.TabIndex = 6;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(70, 108);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(203, 27);
            txtAddress.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.DarkBlue;
            btnSave.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnSave.ForeColor = SystemColors.ButtonHighlight;
            btnSave.Location = new Point(33, 30);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(137, 45);
            btnSave.TabIndex = 8;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.DarkBlue;
            btnUpdate.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnUpdate.ForeColor = SystemColors.ButtonHighlight;
            btnUpdate.Location = new Point(262, 30);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(137, 45);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "تعديل";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.DarkBlue;
            btnDelete.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnDelete.ForeColor = SystemColors.ButtonHighlight;
            btnDelete.Location = new Point(483, 30);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(137, 45);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "حذف";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.DarkBlue;
            btnRefresh.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            btnRefresh.ForeColor = SystemColors.ButtonHighlight;
            btnRefresh.Location = new Point(720, 28);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(137, 45);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "تنظيف";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // gbDetails
            // 
            gbDetails.Controls.Add(txtName);
            gbDetails.Controls.Add(label1);
            gbDetails.Controls.Add(label2);
            gbDetails.Controls.Add(label3);
            gbDetails.Controls.Add(label4);
            gbDetails.Controls.Add(txtPhone);
            gbDetails.Controls.Add(txtEmail);
            gbDetails.Controls.Add(txtAddress);
            gbDetails.Location = new Point(21, 64);
            gbDetails.Margin = new Padding(3, 4, 3, 4);
            gbDetails.Name = "gbDetails";
            gbDetails.Padding = new Padding(3, 4, 3, 4);
            gbDetails.Size = new Size(890, 176);
            gbDetails.TabIndex = 13;
            gbDetails.TabStop = false;
            gbDetails.Text = "بيـانات المورد";
            gbDetails.Enter += gbDetails_Enter;
            // 
            // gbActions
            // 
            gbActions.Controls.Add(btnRefresh);
            gbActions.Controls.Add(btnSave);
            gbActions.Controls.Add(btnUpdate);
            gbActions.Controls.Add(btnDelete);
            gbActions.Location = new Point(19, 269);
            gbActions.Margin = new Padding(3, 4, 3, 4);
            gbActions.Name = "gbActions";
            gbActions.Padding = new Padding(3, 4, 3, 4);
            gbActions.Size = new Size(892, 88);
            gbActions.TabIndex = 14;
            gbActions.TabStop = false;
            gbActions.Text = "العمليــات و التحكم";
            // 
            // button1
            // 
            button1.BackColor = Color.SteelBlue;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(26, 13);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(126, 36);
            button1.TabIndex = 12;
            button1.Text = "الرجوع للرئسية";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkBlue;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(-7, -5);
            panel1.Name = "panel1";
            panel1.Size = new Size(937, 59);
            panel1.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label5.ForeColor = SystemColors.Control;
            label5.Location = new Point(379, 11);
            label5.Name = "label5";
            label5.Size = new Size(551, 41);
            label5.TabIndex = 13;
            label5.Text = "Suppliers Management  - إدارة الموردين";
            // 
            // dgvSuppliers
            // 
            dgvSuppliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSuppliers.BackgroundColor = SystemColors.Control;
            dgvSuppliers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSuppliers.Dock = DockStyle.Bottom;
            dgvSuppliers.Location = new Point(3, 34);
            dgvSuppliers.Margin = new Padding(3, 4, 3, 4);
            dgvSuppliers.Name = "dgvSuppliers";
            dgvSuppliers.RowHeadersWidth = 51;
            dgvSuppliers.RowTemplate.Height = 24;
            dgvSuppliers.Size = new Size(886, 175);
            dgvSuppliers.TabIndex = 12;
            dgvSuppliers.CellClick += dgvSuppliers_CellClick;
            dgvSuppliers.CellContentClick += dgvSuppliers_CellContentClick;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(dgvSuppliers);
            groupBox1.Location = new Point(19, 372);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(892, 213);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "قائمـة المورديـن";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // frmSuppliers
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 600);
            Controls.Add(panel1);
            Controls.Add(groupBox1);
            Controls.Add(gbActions);
            Controls.Add(gbDetails);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmSuppliers";
            Text = "الموردين";
            Load += frmSuppliers_Load;
            gbDetails.ResumeLayout(false);
            gbDetails.PerformLayout();
            gbActions.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.GroupBox gbActions;
        private Button button1;
        private Panel panel1;
        private Label label5;
        private DataGridView dgvSuppliers;
        private GroupBox groupBox1;
    }
}