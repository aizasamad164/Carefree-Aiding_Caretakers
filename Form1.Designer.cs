namespace Caretaker_System_3
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Caretaker_Signin = new Panel();
            pictureBox4 = new PictureBox();
            label1 = new Label();
            Gender_Box = new GroupBox();
            Caretaker_Female = new RadioButton();
            Caretaker_Male = new RadioButton();
            label6 = new Label();
            Add_Caretaker = new Button();
            Caretaker_Notes_txt = new TextBox();
            Caretaker_Contact_txt = new TextBox();
            Caretaker_Age_txt = new TextBox();
            Caretaker_Name_txt = new TextBox();
            label5 = new Label();
            label3 = new Label();
            label2 = new Label();
            Name = new Label();
            bindingSource1 = new BindingSource(components);
            Caretaker_Choose_Panel = new Panel();
            pictureBox3 = new PictureBox();
            label11 = new Label();
            label7 = new Label();
            Caretaker_signs = new Button();
            Caretaker_logs = new Button();
            Panel_Login = new Panel();
            pictureBox2 = new PictureBox();
            label9 = new Label();
            label4 = new Label();
            label8 = new Label();
            Name_txt = new TextBox();
            Password_txt = new TextBox();
            Log_Enter = new Button();
            Role_Panel = new Panel();
            label12 = new Label();
            pictureBox1 = new PictureBox();
            label10 = new Label();
            Guardian_Role = new Button();
            Caretaker_Role = new Button();
            Caretaker_Signin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            Gender_Box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            Caretaker_Choose_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            Panel_Login.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            Role_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Caretaker_Signin
            // 
            Caretaker_Signin.BackColor = Color.FromArgb(152, 220, 255);
            Caretaker_Signin.Controls.Add(pictureBox4);
            Caretaker_Signin.Controls.Add(label1);
            Caretaker_Signin.Controls.Add(Gender_Box);
            Caretaker_Signin.Controls.Add(label6);
            Caretaker_Signin.Controls.Add(Add_Caretaker);
            Caretaker_Signin.Controls.Add(Caretaker_Notes_txt);
            Caretaker_Signin.Controls.Add(Caretaker_Contact_txt);
            Caretaker_Signin.Controls.Add(Caretaker_Age_txt);
            Caretaker_Signin.Controls.Add(Caretaker_Name_txt);
            Caretaker_Signin.Controls.Add(label5);
            Caretaker_Signin.Controls.Add(label3);
            Caretaker_Signin.Controls.Add(label2);
            Caretaker_Signin.Controls.Add(Name);
            Caretaker_Signin.Dock = DockStyle.Fill;
            Caretaker_Signin.Location = new Point(0, 0);
            Caretaker_Signin.Name = "Caretaker_Signin";
            Caretaker_Signin.Size = new Size(1902, 1033);
            Caretaker_Signin.TabIndex = 2;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(183, 195);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(909, 634);
            pictureBox4.TabIndex = 36;
            pictureBox4.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 13.8F);
            label1.Location = new Point(1217, 546);
            label1.Name = "label1";
            label1.Size = new Size(91, 26);
            label1.TabIndex = 35;
            label1.Text = "Contact";
            // 
            // Gender_Box
            // 
            Gender_Box.Controls.Add(Caretaker_Female);
            Gender_Box.Controls.Add(Caretaker_Male);
            Gender_Box.Font = new Font("Arial", 13.8F);
            Gender_Box.Location = new Point(1365, 456);
            Gender_Box.Name = "Gender_Box";
            Gender_Box.Size = new Size(260, 64);
            Gender_Box.TabIndex = 34;
            Gender_Box.TabStop = false;
            // 
            // Caretaker_Female
            // 
            Caretaker_Female.AutoSize = true;
            Caretaker_Female.Location = new Point(135, 26);
            Caretaker_Female.Name = "Caretaker_Female";
            Caretaker_Female.Size = new Size(108, 30);
            Caretaker_Female.TabIndex = 24;
            Caretaker_Female.TabStop = true;
            Caretaker_Female.Text = "Female";
            Caretaker_Female.UseVisualStyleBackColor = true;
            // 
            // Caretaker_Male
            // 
            Caretaker_Male.AutoSize = true;
            Caretaker_Male.Location = new Point(11, 26);
            Caretaker_Male.Name = "Caretaker_Male";
            Caretaker_Male.Size = new Size(82, 30);
            Caretaker_Male.TabIndex = 23;
            Caretaker_Male.TabStop = true;
            Caretaker_Male.Text = "Male";
            Caretaker_Male.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.MidnightBlue;
            label6.Location = new Point(1435, 242);
            label6.Name = "label6";
            label6.Size = new Size(127, 35);
            label6.TabIndex = 13;
            label6.Text = "Sign Up";
            // 
            // Add_Caretaker
            // 
            Add_Caretaker.BackColor = Color.MidnightBlue;
            Add_Caretaker.FlatStyle = FlatStyle.Popup;
            Add_Caretaker.Font = new Font("Arial", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Add_Caretaker.ForeColor = SystemColors.ControlLightLight;
            Add_Caretaker.Location = new Point(1552, 749);
            Add_Caretaker.Name = "Add_Caretaker";
            Add_Caretaker.Size = new Size(206, 39);
            Add_Caretaker.TabIndex = 12;
            Add_Caretaker.Text = "Add Caretaker";
            Add_Caretaker.UseVisualStyleBackColor = false;
            Add_Caretaker.Click += Add_Caretaker_Click;
            // 
            // Caretaker_Notes_txt
            // 
            Caretaker_Notes_txt.Font = new Font("Arial", 13.8F);
            Caretaker_Notes_txt.Location = new Point(1365, 605);
            Caretaker_Notes_txt.Multiline = true;
            Caretaker_Notes_txt.Name = "Caretaker_Notes_txt";
            Caretaker_Notes_txt.Size = new Size(392, 81);
            Caretaker_Notes_txt.TabIndex = 11;
            // 
            // Caretaker_Contact_txt
            // 
            Caretaker_Contact_txt.Font = new Font("Arial", 13.8F);
            Caretaker_Contact_txt.Location = new Point(1365, 543);
            Caretaker_Contact_txt.Name = "Caretaker_Contact_txt";
            Caretaker_Contact_txt.Size = new Size(392, 34);
            Caretaker_Contact_txt.TabIndex = 10;
            // 
            // Caretaker_Age_txt
            // 
            Caretaker_Age_txt.Font = new Font("Arial", 13.8F);
            Caretaker_Age_txt.Location = new Point(1365, 412);
            Caretaker_Age_txt.Name = "Caretaker_Age_txt";
            Caretaker_Age_txt.Size = new Size(392, 34);
            Caretaker_Age_txt.TabIndex = 8;
            // 
            // Caretaker_Name_txt
            // 
            Caretaker_Name_txt.Font = new Font("Arial", 13.8F);
            Caretaker_Name_txt.Location = new Point(1365, 356);
            Caretaker_Name_txt.Name = "Caretaker_Name_txt";
            Caretaker_Name_txt.Size = new Size(392, 34);
            Caretaker_Name_txt.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial", 13.8F);
            label5.Location = new Point(1219, 605);
            label5.Name = "label5";
            label5.Size = new Size(63, 26);
            label5.TabIndex = 5;
            label5.Text = "Skills";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial", 13.8F);
            label3.Location = new Point(1217, 469);
            label3.Name = "label3";
            label3.Size = new Size(88, 26);
            label3.TabIndex = 3;
            label3.Text = "Gender";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 13.8F);
            label2.Location = new Point(1217, 415);
            label2.Name = "label2";
            label2.Size = new Size(52, 26);
            label2.TabIndex = 2;
            label2.Text = "Age";
            // 
            // Name
            // 
            Name.AutoSize = true;
            Name.Font = new Font("Arial", 13.8F);
            Name.Location = new Point(1217, 356);
            Name.Name = "Name";
            Name.Size = new Size(73, 26);
            Name.TabIndex = 0;
            Name.Text = "Name";
            // 
            // Caretaker_Choose_Panel
            // 
            Caretaker_Choose_Panel.BackColor = Color.FromArgb(152, 220, 255);
            Caretaker_Choose_Panel.Controls.Add(pictureBox3);
            Caretaker_Choose_Panel.Controls.Add(label11);
            Caretaker_Choose_Panel.Controls.Add(label7);
            Caretaker_Choose_Panel.Controls.Add(Caretaker_signs);
            Caretaker_Choose_Panel.Controls.Add(Caretaker_logs);
            Caretaker_Choose_Panel.Dock = DockStyle.Fill;
            Caretaker_Choose_Panel.Location = new Point(0, 0);
            Caretaker_Choose_Panel.Name = "Caretaker_Choose_Panel";
            Caretaker_Choose_Panel.Size = new Size(1902, 1033);
            Caretaker_Choose_Panel.TabIndex = 36;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(783, 258);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1015, 714);
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(152, 220, 255);
            label11.Font = new Font("Arial", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.MidnightBlue;
            label11.Location = new Point(54, 395);
            label11.Name = "label11";
            label11.Size = new Size(366, 39);
            label11.TabIndex = 3;
            label11.Text = "Don't have an account?";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.MidnightBlue;
            label7.Font = new Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlLightLight;
            label7.Location = new Point(-9, 110);
            label7.Name = "label7";
            label7.Padding = new Padding(0, 8, 0, 8);
            label7.Size = new Size(534, 62);
            label7.TabIndex = 2;
            label7.Text = "             Caretaker                 ";
            // 
            // Caretaker_signs
            // 
            Caretaker_signs.BackColor = Color.MidnightBlue;
            Caretaker_signs.FlatStyle = FlatStyle.Popup;
            Caretaker_signs.Font = new Font("Segoe UI", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Caretaker_signs.ForeColor = SystemColors.ControlLightLight;
            Caretaker_signs.Location = new Point(153, 511);
            Caretaker_signs.Name = "Caretaker_signs";
            Caretaker_signs.Size = new Size(170, 60);
            Caretaker_signs.TabIndex = 1;
            Caretaker_signs.Text = "Sign Up";
            Caretaker_signs.UseVisualStyleBackColor = false;
            Caretaker_signs.Click += Caretaker_signs_Click_1;
            // 
            // Caretaker_logs
            // 
            Caretaker_logs.BackColor = Color.MidnightBlue;
            Caretaker_logs.FlatStyle = FlatStyle.Popup;
            Caretaker_logs.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Caretaker_logs.ForeColor = SystemColors.ControlLightLight;
            Caretaker_logs.Location = new Point(153, 258);
            Caretaker_logs.Name = "Caretaker_logs";
            Caretaker_logs.Size = new Size(170, 60);
            Caretaker_logs.TabIndex = 0;
            Caretaker_logs.Text = "Sign In";
            Caretaker_logs.UseVisualStyleBackColor = false;
            Caretaker_logs.Click += Caretaker_logs_Click_1;
            // 
            // Panel_Login
            // 
            Panel_Login.BackColor = Color.FromArgb(152, 220, 255);
            Panel_Login.Controls.Add(pictureBox2);
            Panel_Login.Controls.Add(label9);
            Panel_Login.Controls.Add(label4);
            Panel_Login.Controls.Add(label8);
            Panel_Login.Controls.Add(Name_txt);
            Panel_Login.Controls.Add(Password_txt);
            Panel_Login.Controls.Add(Log_Enter);
            Panel_Login.Dock = DockStyle.Fill;
            Panel_Login.Location = new Point(0, 0);
            Panel_Login.Name = "Panel_Login";
            Panel_Login.Size = new Size(1902, 1033);
            Panel_Login.TabIndex = 37;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(183, 195);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(909, 634);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Arial", 13.8F);
            label9.Location = new Point(1236, 545);
            label9.Name = "label9";
            label9.Size = new Size(112, 26);
            label9.TabIndex = 6;
            label9.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial", 13.8F);
            label4.Location = new Point(1236, 457);
            label4.Name = "label4";
            label4.Size = new Size(73, 26);
            label4.TabIndex = 5;
            label4.Text = "Name";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.MidnightBlue;
            label8.Location = new Point(1385, 353);
            label8.Name = "label8";
            label8.Size = new Size(113, 35);
            label8.TabIndex = 4;
            label8.Text = "Sign In";
            // 
            // Name_txt
            // 
            Name_txt.Font = new Font("Arial", 13.8F);
            Name_txt.Location = new Point(1379, 455);
            Name_txt.Name = "Name_txt";
            Name_txt.Size = new Size(290, 34);
            Name_txt.TabIndex = 3;
            // 
            // Password_txt
            // 
            Password_txt.Font = new Font("Arial", 13.8F);
            Password_txt.Location = new Point(1379, 542);
            Password_txt.Name = "Password_txt";
            Password_txt.PasswordChar = '*';
            Password_txt.Size = new Size(290, 34);
            Password_txt.TabIndex = 2;
            // 
            // Log_Enter
            // 
            Log_Enter.BackColor = Color.MidnightBlue;
            Log_Enter.FlatStyle = FlatStyle.Popup;
            Log_Enter.Font = new Font("Arial", 13.8F);
            Log_Enter.ForeColor = SystemColors.ControlLightLight;
            Log_Enter.Location = new Point(1539, 628);
            Log_Enter.Name = "Log_Enter";
            Log_Enter.Size = new Size(130, 39);
            Log_Enter.TabIndex = 1;
            Log_Enter.Text = "Enter ";
            Log_Enter.UseVisualStyleBackColor = false;
            Log_Enter.Click += Log_Enter_Click;
            // 
            // Role_Panel
            // 
            Role_Panel.BackColor = Color.FromArgb(152, 220, 255);
            Role_Panel.Controls.Add(label12);
            Role_Panel.Controls.Add(pictureBox1);
            Role_Panel.Controls.Add(label10);
            Role_Panel.Controls.Add(Guardian_Role);
            Role_Panel.Controls.Add(Caretaker_Role);
            Role_Panel.Dock = DockStyle.Fill;
            Role_Panel.Location = new Point(0, 0);
            Role_Panel.Name = "Role_Panel";
            Role_Panel.Size = new Size(1902, 1033);
            Role_Panel.TabIndex = 39;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.MidnightBlue;
            label12.Font = new Font("Arial", 60F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ControlLightLight;
            label12.Location = new Point(397, 110);
            label12.Name = "label12";
            label12.Padding = new Padding(0, 10, 0, 10);
            label12.Size = new Size(1620, 135);
            label12.TabIndex = 5;
            label12.Text = "   Carefree - Aiding Caretakers     ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(69, 299);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(853, 639);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Arial", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.MidnightBlue;
            label10.Location = new Point(1221, 483);
            label10.Name = "label10";
            label10.Size = new Size(351, 44);
            label10.TabIndex = 3;
            label10.Text = "Choose your Role:";
            // 
            // Guardian_Role
            // 
            Guardian_Role.BackColor = Color.MidnightBlue;
            Guardian_Role.FlatStyle = FlatStyle.Popup;
            Guardian_Role.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            Guardian_Role.ForeColor = SystemColors.Window;
            Guardian_Role.Location = new Point(1255, 729);
            Guardian_Role.Name = "Guardian_Role";
            Guardian_Role.Size = new Size(278, 67);
            Guardian_Role.TabIndex = 2;
            Guardian_Role.Text = "Guardian";
            Guardian_Role.UseVisualStyleBackColor = false;
            Guardian_Role.Click += Guardian_Role_Click_1;
            // 
            // Caretaker_Role
            // 
            Caretaker_Role.BackColor = Color.MidnightBlue;
            Caretaker_Role.FlatStyle = FlatStyle.Popup;
            Caretaker_Role.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Caretaker_Role.ForeColor = SystemColors.Window;
            Caretaker_Role.Location = new Point(1255, 611);
            Caretaker_Role.Name = "Caretaker_Role";
            Caretaker_Role.Size = new Size(278, 63);
            Caretaker_Role.TabIndex = 1;
            Caretaker_Role.Text = "Caretaker";
            Caretaker_Role.UseVisualStyleBackColor = false;
            Caretaker_Role.Click += Caretaker_Role_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1902, 1033);
            Controls.Add(Caretaker_Signin);
            Controls.Add(Caretaker_Choose_Panel);
            Controls.Add(Panel_Login);
            Controls.Add(Role_Panel);
            Text = "Carefree";
            WindowState = FormWindowState.Maximized;
            Caretaker_Signin.ResumeLayout(false);
            Caretaker_Signin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            Gender_Box.ResumeLayout(false);
            Gender_Box.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            Caretaker_Choose_Panel.ResumeLayout(false);
            Caretaker_Choose_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            Panel_Login.ResumeLayout(false);
            Panel_Login.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            Role_Panel.ResumeLayout(false);
            Role_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel Caretaker_Signin;
        private GroupBox Gender_Box;
        private RadioButton Caretaker_Female;
        private RadioButton Caretaker_Male;
        private Label label6;
        private Button Add_Caretaker;
        private TextBox Caretaker_Notes_txt;
        private TextBox Caretaker_Contact_txt;
        private TextBox Caretaker_Age_txt;
        private TextBox Caretaker_Name_txt;
        private Label label5;
        private Label label3;
        private Label label2;
        private Label Name;
        private BindingSource bindingSource1;
        private Label label1;
        private Panel Caretaker_Choose_Panel;
        private Label label7;
        private Button Caretaker_signs;
        private Button Caretaker_logs;
        private Panel Panel_Login;
        private Label label9;
        private Label label4;
        private Label label8;
        private TextBox Name_txt;
        private TextBox Password_txt;
        private Button Log_Enter;
        private Panel Role_Panel;
        private Button Guardian_Role;
        private Button Caretaker_Role;
        private Label label10;
        private Label label11;
        private PictureBox pictureBox1;
        private Label label12;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
    }
}
