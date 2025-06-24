using System.Data.OleDb;

namespace Caretaker_System_3
{
    class Patient : Person
    {
        private static Random rand = new Random();
        private static List<int> usedIDs = new List<int>();
        protected List<string> diseases;
        protected string Smoker, Guardian_Name, Patient_ID, Caretaker_ID, Received_ID, Caretaker_Notes_Patient, Patient_Region, Guardian_Queries, Patient_Picture;
        protected int Weight, Height, Children;
        protected int Guardian_Contact;

        // Patient class constructor
        public Patient(string name, string password, string gender, int age, string smoker, int children, int weight, int height, string guardian_name, int guardian_contact, string received_id, string caretaker_notes_patient, string patient_region, string picture) : base(name, password, gender, age)
        {
            diseases = new List<string>();
            
            Height = height;
            Weight = weight;
            Guardian_Name = guardian_name ?? "";
            Guardian_Contact = guardian_contact;
            Smoker = smoker ?? "No";
            Children = children;
            Received_ID = received_id;
            Caretaker_Notes_Patient = caretaker_notes_patient;
            Patient_Region = patient_region;
            Patient_Picture = picture ?? "";
        }

        // Generate password using Random
        public static int GeneratedID_Patient()
        {
            int id;
            do
            {
                id = rand.Next(1, 9999);
            }
            while (usedIDs.Contains(id));
            usedIDs.Add(id);
            return id;
        }

        // Guardian Sign in using name and password saved in the database
        public override bool Log_Enter_Click_1(string connectionString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Patient WHERE Guardian_Name = ? AND Guardian_Password = ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", Name);
                    command.Parameters.AddWithValue("?", Password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void Add_Insurance(string connectionString, int insurance_charges, int guardian_contact)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "UPDATE Patient SET Insurance_Charge = ? WHERE Guardian_Contact = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", insurance_charges);
                    cmd.Parameters.AddWithValue("?", guardian_contact);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Add new patient to the database
        public bool Add_Patient_Click(string connectionString)
        {
            // Generate Patient ID and Password using Random 
            Patient_ID = "P-" + GeneratedID_Patient();
            Password = GeneratedPassword().ToString();

            // Connect to te database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Select Caretaker ID against Caretaker password retrieved during the Caretaker sign in process
                string query = "SELECT Caretaker_ID FROM Caretaker WHERE Caretaker_Password = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", Received_ID);
                }

                // Add patient against the Caretaker_ID selected from the database
                string query2 = "INSERT INTO Patient (Patient_ID, Patient_Name, Guardian_Password, Patient_Age, Patient_Gender, Guardian_Name, Patient_Smoker, Patient_Children, Patient_Height, Patient_Weight, Guardian_Contact, Caretaker_ID, Caretaker_Notes_Patient, Patient_Region, Patient_Picture) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                
                using (OleDbCommand cmd = new OleDbCommand(query2, connection))
                {
                    cmd.Parameters.AddWithValue("?", Patient_ID);
                    cmd.Parameters.AddWithValue("?", Name);
                    cmd.Parameters.AddWithValue("?", Password);
                    cmd.Parameters.AddWithValue("?", Age);
                    cmd.Parameters.AddWithValue("?", Gender);
                    cmd.Parameters.AddWithValue("?", Guardian_Name);                   
                    cmd.Parameters.AddWithValue("?", Smoker);
                    cmd.Parameters.AddWithValue("?", Children);                   
                    cmd.Parameters.AddWithValue("?", Height);
                    cmd.Parameters.AddWithValue("?", Weight);
                    cmd.Parameters.AddWithValue("?", Guardian_Contact);
                    cmd.Parameters.AddWithValue("?", Received_ID);
                    cmd.Parameters.AddWithValue("?", Caretaker_Notes_Patient);
                    cmd.Parameters.AddWithValue("?", Patient_Region);
                    cmd.Parameters.AddWithValue("?", Patient_Picture);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Show the password generated
            MessageBox.Show("The password is " + Password);

            return true;
        }

        // Update details of the existing patient
        public void Update_Patient_Click(string connectionString, string patient_id)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "UPDATE Patient SET Patient_Name = ?, Patient_Age = ?, Patient_Gender = ?, Guardian_Name = ?, Patient_Smoker = ?, Patient_Children = ?, Patient_Height = ?, Patient_Weight = ?, Guardian_Contact = ?, Caretaker_Notes_Patient = ?, Patient_Region = ?, Patient_Picture = ? WHERE Patient_ID = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {                    
                    cmd.Parameters.AddWithValue("?", Name);
                    cmd.Parameters.AddWithValue("?", Age);
                    cmd.Parameters.AddWithValue("?", Gender);
                    cmd.Parameters.AddWithValue("?", Guardian_Name);
                    cmd.Parameters.AddWithValue("?", Smoker);
                    cmd.Parameters.AddWithValue("?", Children);
                    cmd.Parameters.AddWithValue("?", Height);
                    cmd.Parameters.AddWithValue("?", Weight);
                    cmd.Parameters.AddWithValue("?", Guardian_Contact);                 
                    cmd.Parameters.AddWithValue("?", Caretaker_Notes_Patient);
                    cmd.Parameters.AddWithValue("?", Patient_Region);
                    cmd.Parameters.AddWithValue("?", Patient_Picture);

                    cmd.Parameters.AddWithValue("?", patient_id);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete existing patient from the database
        public void Delete_Patient_Click(int guar_contact, string connectionString)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // The Guardian contact selected in the DataGridView in Form2
                    string deleteQuery = "DELETE FROM Patient WHERE Guardian_Contact = ?";
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("?", guar_contact);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete patient: " + ex.Message);
            }
        }        
    }
}
