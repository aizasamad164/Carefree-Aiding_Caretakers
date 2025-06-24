using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Caretaker_System_3
{
    public partial class Form3 : Form
    {
        Finances finances;

        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Aiza-NED\Object Oriented Programming\Caretaker_Management_System.accdb;";
        string received_password_guardian, received_name, expense_patient_id, Guardian_Queries;
        int expense_amount;
        string patient_id, appointment_category, appointment_name, appointment_description, filter_appointment, patient_id_appointment;
        DateTime appointment_datetime;


        public Form3(string name1, string guardian_password)
        {
            InitializeComponent();

            received_name = name1;
            Heading.Text = "Welcome, " + received_name + "!";

            finances = new Finances(expense_amount);
            Panel_Patient_Guardian.Visible = false;
            Expenses_Panel_View.Visible = false;
            Appointment_Panel.Visible = false;

            received_password_guardian = guardian_password;

            string[] filter_task = { "All", "Today", "This Week" };

            Filter_Appointments.Items.AddRange(filter_task);
            Filter_Appointments.SelectedItem = "All";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Patient_ID FROM Patient WHERE Guardian_Password = ?";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", received_password_guardian);

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())  // Check if the reader has any rows
                {
                    expense_patient_id = reader["Patient_ID"].ToString();
                }
                else
                {
                    MessageBox.Show("No matching patient found for the given guardian password.");
                    return;
                }
            }

            Patient_Expense_View.DefaultCellStyle.ForeColor = Color.MidnightBlue;
            Grid_Patient_Appointments.DefaultCellStyle.ForeColor = Color.MidnightBlue;
        }

        public void HighlightSelectedButton2(Panel panel, System.Windows.Forms.Button clickedButton)
        {
            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is System.Windows.Forms.Button btn)
                {
                    btn.BackColor = SystemColors.Control; // Reset color
                    btn.ForeColor = SystemColors.ControlText; // Reset color
                }
            }

            if (clickedButton != null)
            {
                clickedButton.BackColor = Color.MidnightBlue; // Highlight clicked
                clickedButton.ForeColor = Color.White; // Highlight clicked
            }
        }

        public void Patient_Guardian_Click(object sender, EventArgs e)
        {
            HighlightSelectedButton2(panel1, Patient_Guardian);

            Panel_Patient_Guardian.Visible = true;
            Panel_Patient_Guardian.BringToFront();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Patient WHERE Guardian_Password = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", received_password_guardian);

                        OleDbDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                // Fill in the textboxes from the database
                                Patient_Name_txt.Text = reader["Patient_Name"].ToString();
                                Patient_Age_txt.Text = reader["Patient_Age"].ToString();
                                Patient_Height_txt.Text = reader["Patient_Height"].ToString();
                                Patient_Weight_txt.Text = reader["Patient_Weight"].ToString();
                                Guardian_Name_txt.Text = reader["Guardian_Name"].ToString();
                                Guardian_Contact_txt.Text = reader["Guardian_Contact"].ToString();
                                Patient_Children_txt.Text = reader["Patient_Children"].ToString();
                                Patient_Region_txt.Text = reader["Patient_Region"].ToString();
                                Finances_Amount_txt.Text = reader["Charges"].ToString();
                                Insurance_Amount_txt.Text = reader["Insurance_Charge"].ToString();
                                Patient_Picture.Image = Image.FromFile(reader["Patient_Picture"].ToString());

                                // Check Radio Button based on the value in database
                                string gender = reader["Patient_Gender"].ToString();
                                if (gender == "Male")
                                {
                                    Patient_Male.Checked = true;
                                }
                                else
                                {
                                    Patient_Female.Checked = true;
                                }

                                // Set Smoker Radio Button
                                string smoker = reader["Patient_Smoker"].ToString();
                                if (smoker == "Yes")
                                {
                                    Yes_Smoker.Checked = true;
                                }
                                else
                                {
                                    No_Smoker.Checked = true;
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading age: " + ex.Message);
                }
            }
        }

        public void Appointment_btn_Click(object sender, EventArgs e)
        {
            HighlightSelectedButton2(panel1, Appointment_btn);

            Appointment_Panel.Visible = true;
            Appointment_Panel.BringToFront();
        }

        public void LoadAppointmentsForPatient(string patientID)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {                
                connection.Open();

                string query2 = "SELECT Patient_ID FROM Patient WHERE Guardian_Password = ?";

                using (OleDbCommand command2 = new OleDbCommand(query2, connection))
                {
                    command2.Parameters.AddWithValue("?", received_password_guardian);                    

                    using (OleDbDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientID = reader["Patient_ID"].ToString();
                        }
                        
                    }
                }
                
            }

            Appointment appointment = new Appointment(patient_id, appointment_datetime, appointment_category, appointment_name, appointment_description);

            string query = "SELECT Client_Name, Appointment_Category, Appointment_Description, Appointment_DateTime FROM Appointment WHERE Patient_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Step 1: Select tasks for the given patient ID
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", patientID);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string client_name = reader["Client_Name"].ToString();
                            string category = reader["Appointment_Category"].ToString();
                            string description = reader["Appointment_Description"].ToString();
                            DateTime Date = Convert.ToDateTime(reader["Appointment_DateTime"]);
                        }
                    }
                }

                // Step 4: Reload the modified appointments into the DataGridView
                using (OleDbCommand reloadCommand = new OleDbCommand(query, connection))
                {
                    reloadCommand.Parameters.AddWithValue("?", patientID);

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(reloadCommand))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the modified data to the DataGridView
                        Grid_Patient_Appointments.DataSource = dt;
                    }
                }
            }
        }

        public void Reload_Appointment_Click_1(object sender, EventArgs e)
        {
            LoadAppointmentsForPatient(received_password_guardian);
        }

        public void LoadFilteredAppointments(string patientID)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string query2 = "SELECT Patient_ID FROM Patient WHERE Guardian_Password = ?";

                using (OleDbCommand command2 = new OleDbCommand(query2, connection))
                {
                    command2.Parameters.AddWithValue("?", received_password_guardian);

                    using (OleDbDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patientID = reader["Patient_ID"].ToString();
                        }

                    }
                }

            }

            filter_appointment = Filter_Appointments.SelectedItem.ToString();

            string query = "SELECT Client_Name, Appointment_Category, Appointment_Description, Appointment_DateTime FROM Appointment WHERE Patient_ID = ?";

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (filter_appointment == "Today")
            {
                startDate = DateTime.Today;
                endDate = DateTime.Today.AddDays(1);
                query += " AND Appointment_DateTime >= ? AND Appointment_DateTime < ?";
            }
            else if (filter_appointment == "This Week")
            {
                // Get the start of the current week (Monday) and the end (Sunday)
                int diff = (int)DateTime.Today.DayOfWeek - 1;
                if (diff < 0) diff = 6; // If Sunday (0), make it 6
                startDate = DateTime.Today.AddDays(-diff);
                endDate = startDate.Value.AddDays(7);
                query += " AND Appointment_DateTime >= ? AND Appointment_DateTime < ?";
            }

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("?", patientID);

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        command.Parameters.AddWithValue("StartDate", startDate.Value);
                        command.Parameters.AddWithValue("EndDate", endDate.Value);
                    }

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Grid_Patient_Appointments.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tasks: " + ex.Message);
                }
            }
        }

        public void Filtered_Appointments_Click(object sender, EventArgs e)
        {
            LoadFilteredAppointments(patient_id);
        }

        public void Expenses_Caretaker_View_Click_1(object sender, EventArgs e)
        {

            HighlightSelectedButton2(panel1, Expenses_Caretaker_View);

            Expenses_Panel_View.Visible = true;
            Expenses_Panel_View.BringToFront();
        }

        public void LoadExpensesForPatient(string patient_id)
        {
            string query = "SELECT Expense_Name, Expense_Category, Expense_Amount, Expense_Date FROM Expenses WHERE Patient_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("?", patient_id);

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Patient_Expense_View.DataSource = dt;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tasks: " + ex.Message);
                }
            }
        }

        public void Load_Expense_Click_1(object sender, EventArgs e)
        {
            LoadExpensesForPatient(expense_patient_id);
        }

        public void Add_Expense_Click_1(object sender, EventArgs e)
        {
            if (double.TryParse(Finances_Amount.Text, out double amount) && amount >= 0)
            {
                bool success = finances.AddBalance(connectionString, amount, received_password_guardian);
                if (success)
                {
                    MessageBox.Show("Amount added successfully.");
                    Finances_Amount.Clear();
                }
                else
                {
                    MessageBox.Show("Guardian contact not found. No amount added.");
                }
            }
            else
            {
                MessageBox.Show("Enter a valid amount");
            }
        }

        public void Remove_Finance_Click(object sender, EventArgs e)
        {
            if (double.TryParse(Finances_Amount.Text, out double amount) && amount > 0)
            {
                double currentCharges = finances.GetCurrentCharges(connectionString, received_password_guardian);

                if (amount > currentCharges)
                {
                    MessageBox.Show("Insufficient balance to deduct.");
                    return;
                }

                finances.DeductBalance(connectionString, amount, received_password_guardian);
                MessageBox.Show("Amount deducted successfully.");
                Finances_Amount.Clear();
            }
            else
            {
                MessageBox.Show("Enter a valid amount.");
            }
        }

        public void Close_Button_Click(object sender, EventArgs e)
        {
            // Custom cleanup code
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Gracefully exits the app
            }
        }

        public void Query_Click(object sender, EventArgs e)
        {
            Guardian_Queries = Queries_txt.Text;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "UPDATE Patient SET Guardian_Queries = ? WHERE Guardian_Password = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", Guardian_Queries);

                    cmd.Parameters.AddWithValue("?", received_password_guardian);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Your query is received by the caretaker");
        }

        
    }
}
