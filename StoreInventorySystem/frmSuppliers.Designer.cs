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
            dgvSuppliers = new DataGridView();
            gbDetails = new GroupBox();
            gbActions = new GroupBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).BeginInit();
            gbDetails.SuspendLayout();
            gbActions.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 52);
            label1.Name = "label1";
            label1.Size = new Size(108, 20);
            label1.TabIndex = 0;
            label1.Text = "Supplier Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 99);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 1;
            label2.Text = "Phone";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 144);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 2;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(31, 190);
            label4.Name = "label4";
            label4.Size = new Size(62, 20);
            label4.TabIndex = 3;
            label4.Text = "Address";
            // 
            // txtName
            // 
            txtName.Location = new Point(159, 45);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 27);
            txtName.TabIndex = 4;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(159, 91);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 27);
            txtPhone.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(159, 136);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 27);
            txtEmail.TabIndex = 6;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(159, 182);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(100, 27);
            txtAddress.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(42, 36);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(42, 91);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 29);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(42, 148);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(42, 198);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 29);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "Refrech";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvSuppliers
            // 
            dgvSuppliers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSuppliers.Dock = DockStyle.Bottom;
            dgvSuppliers.Location = new Point(3, 62);
            dgvSuppliers.Margin = new Padding(3, 4, 3, 4);
            dgvSuppliers.Name = "dgvSuppliers";
            dgvSuppliers.RowHeadersWidth = 51;
            dgvSuppliers.RowTemplate.Height = 24;
            dgvSuppliers.Size = new Size(559, 175);
            dgvSuppliers.TabIndex = 12;
            dgvSuppliers.CellClick += dgvSuppliers_CellClick;
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
            gbDetails.Location = new Point(21, 39);
            gbDetails.Margin = new Padding(3, 4, 3, 4);
            gbDetails.Name = "gbDetails";
            gbDetails.Padding = new Padding(3, 4, 3, 4);
            gbDetails.Size = new Size(299, 246);
            gbDetails.TabIndex = 13;
            gbDetails.TabStop = false;
            gbDetails.Text = "بيـانات المورد";
            // 
            // gbActions
            // 
            gbActions.Controls.Add(btnRefresh);
            gbActions.Controls.Add(btnSave);
            gbActions.Controls.Add(btnUpdate);
            gbActions.Controls.Add(btnDelete);
            gbActions.Location = new Point(425, 39);
            gbActions.Margin = new Padding(3, 4, 3, 4);
            gbActions.Name = "gbActions";
            gbActions.Padding = new Padding(3, 4, 3, 4);
            gbActions.Size = new Size(161, 259);
            gbActions.TabIndex = 14;
            gbActions.TabStop = false;
            gbActions.Text = "العمليــات";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(dgvSuppliers);
            groupBox1.Location = new Point(21, 329);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(565, 241);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "قائمـة المورديـن";
            // 
            // button1
            // 
            button1.Location = new Point(247, 13);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(133, 29);
            button1.TabIndex = 12;
            button1.Text = "العودة للرئسية";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmSuppliers
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(710, 585);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(gbActions);
            Controls.Add(gbDetails);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmSuppliers";
            Text = "frmSuppliers";
            Load += frmSuppliers_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSuppliers).EndInit();
            gbDetails.ResumeLayout(false);
            gbDetails.PerformLayout();
            gbActions.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.GroupBox groupBox1;
        private Button button1;
    }
}