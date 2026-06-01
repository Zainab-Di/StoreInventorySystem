namespace StoreInventorySystem
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            button1 = new Button();
            imageList1 = new ImageList(components);
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.AliceBlue;
            button1.FlatAppearance.BorderColor = Color.FromArgb(128, 255, 255);
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatAppearance.MouseDownBackColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.ImageIndex = 1;
            button1.ImageList = imageList1;
            button1.Location = new Point(48, 110);
            button1.Name = "button1";
            button1.Size = new Size(280, 90);
            button1.TabIndex = 0;
            button1.Text = "إدارة المنتجات";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "order.png");
            imageList1.Images.SetKeyName(1, "inventory.png");
            imageList1.Images.SetKeyName(2, "supplier.png");
            // 
            // button2
            // 
            button2.BackColor = Color.AliceBlue;
            button2.FlatAppearance.BorderColor = Color.FromArgb(128, 255, 128);
            button2.FlatAppearance.BorderSize = 2;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImageIndex = 2;
            button2.ImageList = imageList1;
            button2.Location = new Point(479, 110);
            button2.Name = "button2";
            button2.Size = new Size(280, 90);
            button2.TabIndex = 1;
            button2.Text = "إدارة الموردين";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.AliceBlue;
            button3.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            button3.FlatAppearance.BorderSize = 2;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImageIndex = 0;
            button3.ImageList = imageList1;
            button3.Location = new Point(250, 250);
            button3.Name = "button3";
            button3.Size = new Size(280, 90);
            button3.TabIndex = 2;
            button3.Text = "حركة المخزن";
            button3.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(-3, 410);
            panel1.Name = "panel1";
            panel1.Size = new Size(804, 37);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(443, 7);
            label1.Name = "label1";
            label1.Size = new Size(358, 23);
            label1.TabIndex = 0;
            label1.Text = "StoreInventoryDB :متصل بقاعدة البيانات بنجاح";
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkBlue;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(-3, -4);
            panel2.Name = "panel2";
            panel2.Size = new Size(804, 59);
            panel2.TabIndex = 4;
            panel2.Paint += panel2_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Image = Properties.Resources.box;
            label3.ImageAlign = ContentAlignment.MiddleLeft;
            label3.Location = new Point(9, 11);
            label3.Name = "label3";
            label3.Size = new Size(695, 41);
            label3.TabIndex = 5;
            label3.Text = "     Store Inventory System-لوحة تحكم إدارة المخزون";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(430, 10);
            label2.Name = "label2";
            label2.Size = new Size(0, 20);
            label2.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 448);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "الشاشة الرئسية ";
            Load += Form1_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private Label label3;
        private ImageList imageList1;
    }
}
