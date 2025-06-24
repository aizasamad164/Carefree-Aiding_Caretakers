namespace Caretaker_System_3
{
    public partial class Form1 : Form
    {
        // Location of the database (.accdb file)
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Aiza-NED\Object Oriented Programming\Caretaker_Management_System.accdb;";
        string selectedRole, name, password, guardian_name, smoker, gender, caretaker_notes, received_id, caretaker_notes_patient, region, guardian_queries, picture;
        int age, guardian_contact, caretaker_contact;
        int weight, height, children;

        // Reference variable of an uninitialised class
        Person person;

        // Form1 loads
        public Form1()
        {
            InitializeComponent();
            // Control panel visibility when the form loads
            Role_Panel.Visible = true;
            Caretaker_Choose_Panel.Visible = false;
            Panel_Login.Visible = false;
            Caretaker_Signin.Visible = false;            
        }

        // Chooses the Caretaker role
        public void Caretaker_Role_Click_1(object sender, EventArgs e)
        {
            selectedRole = "Caretaker";
            Caretaker_Choose_Panel.Visible = true;
            Caretaker_Choose_Panel.BringToFront();
        }

        // If the Caretaker already has an account
        public void Caretaker_logs_Click_1(object sender, EventArgs e)
        {
            Panel_Login.Visible = true;
            Panel_Login.BringToFront();
        }

        // If the Caretaker does not have an account
        public void Caretaker_signs_Click_1(object sender, EventArgs e)
        {
            Caretaker_Signin.Visible = true;
            Caretaker_Signin.BringToFront();
        }

        public void Guardian_Role_Click_1(object sender, EventArgs e)
        {
            selectedRole = "Guardian";
            Panel_Login.Visible = true;
            Panel_Login.BringToFront();
        }

        public void Log_Enter_Click(object sender, EventArgs e)
        {
            name = Name_txt.Text;
            password = Password_txt.Text;

            switch (selectedRole)
            {
                case "Caretaker":
                    person = new Caretaker(name, password, gender, age, caretaker_contact, caretaker_notes);
                    break;
                case "Guardian":
                    person = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);
                    break;
                default:
                    return;
            }

            if (person.Log_Enter_Click_1(connectionString))
            {
                if (selectedRole == "Caretaker")
                {
                    Form2 form2 = new Form2(name, password);  // Create an instance of the new form                   
                    this.Hide();
                    form2.Show();               // Show it non-modally                                  
                }
                else
                {
                    Form3 form3 = new Form3(name, password);
                    this.Hide();
                    form3.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Add_Caretaker_Click(object sender, EventArgs e)
        {
            Person person = null;

            name = Caretaker_Name_txt.Text;
            age = int.Parse(Caretaker_Age_txt.Text);
            caretaker_contact = int.Parse(Caretaker_Contact_txt.Text);
            caretaker_notes = Caretaker_Notes_txt.Text;

            if (Caretaker_Male != null && Caretaker_Male.Checked)
            {
                gender = "Male";
            }
            else if (Caretaker_Female != null && Caretaker_Female.Checked)
            {
                gender = "Female";
            }

            Caretaker caretaker = new Caretaker(name, password, gender, age, caretaker_contact, caretaker_notes);

            if (caretaker.Add_Caretaker_Click(connectionString))
            {
                MessageBox.Show("Sign in successful!");
                caretaker.ClearControls(Caretaker_Signin);
            }
            else
            {
                MessageBox.Show("Invalid credentials!");
            }

            Panel_Login.Visible = true;
            Panel_Login.BringToFront();
        }
    }
}
