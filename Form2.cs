using System.Data;
using System.Data.OleDb;

namespace Caretaker_System_3
{
    public partial class Form2 : Form
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Aiza-NED\Object Oriented Programming\Caretaker_Management_System.accdb;";

        string name, password, guardian_name, smoker, gender, caretaker_notes_patient, received_id, patient_id, picture, region;
        string task_name, task_description, task_category, task_priority, task_frequency, filter_task;
        DateTime task_datetime, expense_datetime, appointment_datetime;
        string expense_name, expense_patient_id, expense_category;
        string appointment_name, appointment_category, appointment_description, filter_appointment;
        int expense_amount, age, height, weight, children, guardian_contact, predicted_charges;
        double balance;
        bool isUpdating;

        string patient_id_task = string.Empty;
        string patient_id_appointment = string.Empty;

        private string received_password, received_name;

        public Form2(string name1, string caretaker_password)
        {          
            InitializeComponent();

            received_name = name1;
            Heading.Text = "Welcome, " + received_name + "!";

            Panel_AddPatient_Caretaker.Visible = false;
            Panel_ShowPatient_Caretaker.Visible = false;
            Expenses_Panel_View.Visible = false;
            Schedule_View.Visible = false;
            Patient_View_Tasks.Visible = false;
            Patient_Insurance_Panel.Visible = false;
            Patient_Finances_View.Visible = false;
            Prediction_Panel.Visible = false;
            Patient_View_Appointments.Visible = false;
            Appointment_Panel.Visible = false;

            received_password = caretaker_password;

            // Adding a collection of items
            string[] categories = { "Medicine Administration", "Patient Hygeine", "Patient Diet", "Other" };
            string[] priorities = { "High", "Medium", "Low" };
            string[] frequencies = { "Daily", "Alternate", "Weekly" };
            string[] filter_task = { "All", "Today", "This Week" };
            string[] region = { "southwest", "northwest", "southeast", "northeast" };
            string[] expense_category = { "Medication", "Doctors visit", "Hospital stay", "Patient care objects", "Miscellanous" };
            string[] appointment = { "Doctors", "Personal", "Social", "Therpaists", "Others" };

            Category_dropdown.Items.AddRange(categories);
            Priority_dropdown.Items.AddRange(priorities);
            Frequency_dropdown.Items.AddRange(frequencies);
            Filter_Task.Items.AddRange(filter_task);
            Patient_Region.Items.AddRange(region);
            Expense_Category_dropdown.Items.AddRange(expense_category);
            Appointment_Category.Items.AddRange(appointment);
            Filter_Appointments.Items.AddRange(filter_task);

            Filter_Task.SelectedItem = "All";
            Filter_Appointments.SelectedItem = "All";

            DateTime_Task.Format = DateTimePickerFormat.Custom;
            DateTime_Task.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            DateTime_Task.ShowUpDown = true;

            Appointment_DateTime.Format = DateTimePickerFormat.Custom;
            Appointment_DateTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt";
            Appointment_DateTime.ShowUpDown = true;

            Grid_Patient_Appointments.DefaultCellStyle.ForeColor = Color.MidnightBlue;
            Grid_Patient_Task2.DefaultCellStyle.ForeColor = Color.MidnightBlue;
        }

        public void HighlightSelectedButton(Panel panel, System.Windows.Forms.Button clickedButton)
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

        // Button event in Form2
        // Button to open panel to add Patient details by bringing the panel to front
        public void Add_New_Patient_Click_1(object sender, EventArgs e)
        {
            Patient patient = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);

            patient.ClearControls(Panel_AddPatient_Caretaker);
            Patient_Region.SelectedIndex = -1;
            Patient_Picture.Image = null;
            Expenses_Panel_View.Visible = false;
            Panel_ShowPatient_Caretaker.Visible = false;

            Panel_AddPatient_Caretaker.Visible = true;
            Panel_AddPatient_Caretaker.BringToFront();
        }

        // View panel containing DataGridView loading all the Patients against the Caretaker ID
        // Patient name, Guardian name and contact can be viewed
        public void Patient_Caretaker_View_Click(object sender, EventArgs e)
        {
            // The button will be highlighted when clicked inside the specific panel
            HighlightSelectedButton(panel1, Patient_Caretaker_View);

            Panel_ShowPatient_Caretaker.Visible = true;
            Panel_ShowPatient_Caretaker.BringToFront();
            LoadData();
        }

        // Finances Panel
        public void Finances_Caretaker_View_Click_1(object sender, EventArgs e)
        {
            HighlightSelectedButton(panel1, Expenses_Caretaker_View);

            Finances finances = new Finances(balance);

            Patient_Finances_View.Visible = true;
            Patient_Finances_View.BringToFront();
            LoadData();
        }


        public void Schedule_Click_1(object sender, EventArgs e)
        {
            HighlightSelectedButton(panel1, Schedule);

            Patient_View_Tasks.Visible = true;
            Patient_View_Tasks.BringToFront();
            LoadData();
        }

        public void Appointment_btn_Click(object sender, EventArgs e)
        {
            HighlightSelectedButton(panel1, Appointment_btn);

            Patient_View_Appointments.Visible = true;
            Patient_View_Appointments.BringToFront();
            LoadData();
        }

        public void Insurance_txt_Click(object sender, EventArgs e)
        {
            HighlightSelectedButton(panel1, Insurance_txt);

            Patient patient = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);

            Patient_Insurance_Panel.Visible = true;
            Patient_Insurance_Panel.BringToFront();
            LoadData();
        }

        public void Train_Model_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Model trained successfully!");
            MLModel.MLModel.TrainAndPredict();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("We predict these costs using the given data and a Fastatree \n" +
                            "regression model. The model is trained on a dataset obtained from \n" +
                            "kaggle. The model makes predictions with a mean absolute error of 2500\n" +
                            "and has an R- squared value of 0.87 proving the models reliability.");
        }

        // Load data from the database inside the DataGridView
        public void LoadData()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Caretaker_ID FROM Caretaker WHERE Caretaker_Password = ?";
                    string caretakerId = string.Empty; ;

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // The password that Caretaker Sign In with added in Form2
                        command.Parameters.AddWithValue("?", received_password);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            caretakerId = Convert.ToString(result);
                            received_id = caretakerId;
                        }
                        else
                        {
                            MessageBox.Show("Caretaker not found.");
                        }
                    }

                    // retrieve all Patients against the Caretaker ID against the Caretaker password
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT Patient_Name, Guardian_Name, Guardian_Contact, Caretaker_Notes_Patient, Guardian_Queries FROM Patient WHERE Caretaker_ID = ?", connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("?", caretakerId);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Load in all these grids the same data from the database
                        Patient_Display.DataSource = dt;
                        Patient_Display_Tasks.DataSource = dt;
                        Patient_Display_Appointments.DataSource = dt;
                        Patient_Insurance_Display.DataSource = dt;
                        Patient_Finances_Grid.DataSource = dt;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void LoadData2(System.Windows.Forms.TextBox textBox)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Caretaker_ID FROM Caretaker WHERE Caretaker_Password = ?";
                    string caretakerId = string.Empty;

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        // The password received from Form2
                        command.Parameters.AddWithValue("?", received_password);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            caretakerId = Convert.ToString(result);
                            received_id = caretakerId;
                        }
                        else
                        {
                            MessageBox.Show("Caretaker not found.");
                            return;
                        }
                    }

                    // Assuming PatientNameTextBox.Text contains the patient name filter (can be empty)
                    string patientNameFilter = textBox.Text.Trim();

                    string selectQuery = "SELECT Patient_Name, Guardian_Name, Guardian_Contact, Caretaker_Notes_Patient, Guardian_Queries " +
                                         "FROM Patient WHERE Caretaker_ID = ?";

                    if (!string.IsNullOrEmpty(patientNameFilter))
                    {
                        selectQuery += " AND Patient_Name LIKE ?";
                    }

                    using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", caretakerId);

                        if (!string.IsNullOrEmpty(patientNameFilter))
                        {
                            command.Parameters.AddWithValue("?", patientNameFilter + "%");
                        }

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            Patient_Display.DataSource = dt;
                            Patient_Display_Tasks.DataSource = dt;
                            Patient_Display_Appointments.DataSource = dt;
                            Patient_Insurance_Display.DataSource = dt;
                            Patient_Finances_Grid.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        // Retrieve all details of the Patient against the Guardian contact clicked in the DataGirdView 
        public void Patient_Display_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            Panel_AddPatient_Caretaker.BringToFront();
            Panel_AddPatient_Caretaker.Visible = true;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Extract the clicked row
                    DataGridViewRow row = Patient_Display.Rows[e.RowIndex];

                    // Get Patient_Name from the row — assuming the column is named exactly "Patient_Name"
                    int guardian_contact = Convert.ToInt32(row.Cells["Guardian_Contact"].Value);

                    connection.Open();

                    string query = "SELECT * FROM Patient WHERE Guardian_Contact = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", guardian_contact);

                        OleDbDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                patient_id = reader["Patient_ID"].ToString(); // Get the Patient_ID from the database row

                                // Fill in the textboxes with relevant field's data from the database
                                Patient_Name_txt.Text = reader["Patient_Name"].ToString();
                                Patient_Age_txt.Text = reader["Patient_Age"].ToString();
                                Patient_Height_txt.Text = reader["Patient_Height"].ToString();
                                Patient_Weight_txt.Text = reader["Patient_Weight"].ToString();
                                Guardian_Name_txt.Text = reader["Guardian_Name"].ToString();
                                Guardian_Contact_txt.Text = reader["Guardian_Contact"].ToString();
                                Patient_Children_txt.Text = reader["Patient_Children"].ToString();
                                Patient_Region.Text = reader["Patient_Region"].ToString();

                                // Show picture in PictureBox for the path saved in the database 
                                Browse_txt.Text = reader["Patient_Picture"].ToString();
                                Patient_Picture.Image = Image.FromFile(Browse_txt.Text);

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

                                // Set Smoker Radio Button with condition saved in the database
                                string smoker = reader["Patient_Smoker"].ToString();
                                if (smoker == "Yes")
                                {
                                    Yes_Smoker.Checked = true;
                                }
                                else
                                {
                                    No_Smoker.Checked = true;
                                }

                                isUpdating = true;

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

        public void Patient_Display_Tasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // Get the clicked row
            DataGridViewRow row = Patient_Display_Tasks.Rows[e.RowIndex];

            // Get the patient name from the clicked row
            int guardian_contact = Convert.ToInt32(row.Cells["Guardian_Contact"].Value);

            // Fetch the corresponding Patient_ID from the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Patient_ID FROM Patient WHERE Guardian_Contact = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", guardian_contact);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            patient_id_task = result.ToString();  // Set the patient_id_task
                            Schedule_View.Visible = true;
                            Schedule_View.BringToFront();
                        }
                        else
                        {
                            MessageBox.Show("Patient not found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void LoadTasksForPatient(string patient_id_task)
        {
            Task task = new Task(task_name, task_priority, task_datetime, patient_id_task, task_description, task_frequency, task_category);

            string query = "SELECT Task_Name, Task_Description, Task_DateTime, Task_Category, Task_Frequency, Task_Priority FROM Task_Scheduler WHERE Patient_ID = ?";
            string updateQuery = "UPDATE Task_Scheduler SET Task_DateTime = ? WHERE Task_Name = ? AND Patient_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Step 1: Select tasks for the given patient ID
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", patient_id_task);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string taskName = reader["Task_Name"].ToString();
                            DateTime originalDate = Convert.ToDateTime(reader["Task_DateTime"]);
                            string frequency = reader["Task_Frequency"].ToString();

                            // Step 2: Generate next occurrence
                            DateTime newDate = task.GenerateNextOccurrence(originalDate, frequency);

                            // Step 3: If new date is different, update it
                            if (newDate != originalDate)
                            {
                                using (OleDbCommand updateCmd = new OleDbCommand(updateQuery, connection))
                                {
                                    updateCmd.Parameters.AddWithValue("?", newDate);
                                    updateCmd.Parameters.AddWithValue("?", taskName);
                                    updateCmd.Parameters.AddWithValue("?", patient_id_task);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                // Step 4: Reload the modified tasks into the DataGridView
                using (OleDbCommand reloadCommand = new OleDbCommand(query, connection))
                {
                    reloadCommand.Parameters.AddWithValue("?", patient_id_task);

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(reloadCommand))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Bind the modified data to the DataGridView
                        Grid_Patient_Task2.DataSource = dt;
                    }
                }
            }
        }

        public void LoadFilteredTasks(string patient_id_task)
        {
            patient_id = patient_id_task;
            filter_appointment = Filter_Task.SelectedItem.ToString();
            string query = "SELECT Task_Name, Task_Description, Task_DateTime, Task_Category, Task_Frequency, Task_Priority FROM Task_Scheduler WHERE Patient_ID = ?";

            DateTime? startDate = null;
            DateTime? endDate = null;

            if (filter_appointment == "Today")
            {
                startDate = DateTime.Today;
                endDate = DateTime.Today.AddDays(1);
                query += " AND Task_DateTime >= ? AND Task_DateTime < ?";
            }
            else if (filter_task == "This Week")
            {
                // Get the start of the current week (Monday) and the end (Sunday)
                int diff = (int)DateTime.Today.DayOfWeek - 1;
                if (diff < 0) diff = 6; // If Sunday (0), make it 6
                startDate = DateTime.Today.AddDays(-diff);
                endDate = startDate.Value.AddDays(7);
                query += " AND Task_DateTime >= ? AND Task_DateTime < ?";
            }

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("?", patient_id_task);

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        command.Parameters.AddWithValue("StartDate", startDate.Value);
                        command.Parameters.AddWithValue("EndDate", endDate.Value);
                    }

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        Grid_Patient_Task2.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading tasks: " + ex.Message);
                }
            }
        }

        public void Add_Patient_Click_1(object sender, EventArgs e)
        {
           
            bool isRadioButtonChecked1 = false;
            foreach (Control control in Gender_Box.Controls)
            {
                if (control is System.Windows.Forms.RadioButton rb && rb.Checked)
                {
                    isRadioButtonChecked1 = true;
                    break;
                }
            }

            bool isRadioButtonChecked2 = false;
            foreach (Control control in Smoker_Box.Controls)
            {
                if (control is System.Windows.Forms.RadioButton rb && rb.Checked)
                {
                    isRadioButtonChecked2 = true;
                    break;
                }
            }

            if (string.IsNullOrEmpty(Patient_Name_txt.Text) || string.IsNullOrEmpty(Patient_Age_txt.Text) ||
                string.IsNullOrEmpty(Guardian_Name_txt.Text) || string.IsNullOrEmpty(Guardian_Contact_txt.Text) ||
                string.IsNullOrEmpty(Patient_Height_txt.Text) || string.IsNullOrEmpty(Patient_Weight_txt.Text) ||
                string.IsNullOrEmpty(Patient_Children_txt.Text) || string.IsNullOrEmpty(Patient_Region.Text) ||
                !isRadioButtonChecked1 || !isRadioButtonChecked2)
            {
                MessageBox.Show("All required fields need to be filled");
                return;
            }

            name = Patient_Name_txt.Text;
            age = int.Parse(Patient_Age_txt.Text);
            guardian_name = Guardian_Name_txt.Text;
            guardian_contact = int.Parse(Guardian_Contact_txt.Text);
            height = int.Parse(Patient_Height_txt.Text);
            weight = int.Parse(Patient_Weight_txt.Text);
            caretaker_notes_patient = C_Notes_P_txt.Text;
            children = int.Parse(Patient_Children_txt.Text);
            region = Patient_Region.SelectedItem.ToString();

            picture = Browse_txt.Text;
            Patient_Picture.Image = Image.FromFile(picture);

            if (Patient_Male != null && Patient_Male.Checked)
            {
                gender = "Male";
            }
            else if (Patient_Female != null && Patient_Female.Checked)
            {
                gender = "Female";
            }

            if (Yes_Smoker != null && Yes_Smoker.Checked)
            {
                smoker = "Yes";
            }
            else if (No_Smoker != null && No_Smoker.Checked)
            {
                smoker = "No";
            }

            Patient patient = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);

            if (isUpdating)
            {
                // Update mode
                patient.Update_Patient_Click(connectionString, patient_id);
                MessageBox.Show("Patient updated successfully!");
                patient.ClearControls(Panel_AddPatient_Caretaker);
                Patient_Picture.Image = null;
                Patient_Region.SelectedIndex = -1;
                isUpdating = false;
            }
            else
            {
                // Add mode
                if (patient.Add_Patient_Click(connectionString))
                {
                    MessageBox.Show("Patient saved successfully!");
                    patient.ClearControls(Panel_AddPatient_Caretaker);
                    Patient_Picture.Image = null;
                    Patient_Region.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Invalid credentials!");
                }
            }
        }

        public void Delete_Patient_Click_1(object sender, EventArgs e)
        {
            Patient patient = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);

            string guardianContactValue = Patient_Display.CurrentRow.Cells["Guardian_Contact"].Value.ToString();

            // Attempt to convert the value to an integer
            if (int.TryParse(guardianContactValue, out guardian_contact))
            {
                // Ask the user to confirm the deletion
                DialogResult result = MessageBox.Show("Are you sure you want to delete this patient?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Call the deletion method (ensure it's properly defined in the Patient class)
                    patient.Delete_Patient_Click(guardian_contact, connectionString);
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.");
            }
        }

        Task_Library taskLibrary = new Task_Library();

        public void Add_Task_Click_1(object sender, EventArgs e)
        {
            task_datetime = DateTime_Task.Value;
            task_name = Task_Name_txt.Text;
            task_description = Task_Description_txt.Text;

            task_category = Category_dropdown.SelectedItem.ToString();
            task_priority = Priority_dropdown.SelectedItem.ToString();
            task_frequency = Frequency_dropdown.SelectedItem.ToString();

            Task task = new Task(task_name, task_priority, task_datetime, patient_id_task, task_description, task_frequency, task_category);

            if (task.Add_Task_Click(connectionString))
            {
                MessageBox.Show("Task Added!");
            }
            else
            {
                MessageBox.Show("Information incomplete");
            }

            taskLibrary.AddTask(task);

            Task_Name_txt.Clear();
            Task_Description_txt.Clear();
            Category_dropdown.SelectedIndex = -1;
            Priority_dropdown.SelectedIndex = -1;
            Frequency_dropdown.SelectedIndex = -1;
        }

        public void Remove_Task_Click_1(object sender, EventArgs e)
        {
            Task task = new Task(task_name, task_priority, task_datetime, patient_id_task, task_description, task_frequency, task_category);

            if (Grid_Patient_Task2.SelectedRows.Count > 0)
            {
                // Get selected Expense_ID and Patient_ID
                string task_name = Grid_Patient_Task2.SelectedRows[0].Cells["Task_Name"].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this task?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    task.RemoveTask(task_name, connectionString);
                }
            }
            else
            {
                MessageBox.Show("Please select a task to delete.");
            }
        }

        public void Grid_Patient_Task2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // Get the clicked row
            DataGridViewRow row = Grid_Patient_Task2.Rows[e.RowIndex];

        }

        public void Reload_Task_Click_1(object sender, EventArgs e)
        {
            LoadTasksForPatient(patient_id_task);
        }

        public void Filter_Click_1(object sender, EventArgs e)
        {
            LoadFilteredTasks(patient_id_task);
        }

        public void Patient_Display_Appointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // Get the clicked row
            DataGridViewRow row = Patient_Display_Appointments.Rows[e.RowIndex];

            // Get the patient name from the clicked row
            int guardian_contact = Convert.ToInt32(row.Cells["Guardian_Contact"].Value);

            // Fetch the corresponding Patient_ID from the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Patient_ID FROM Patient WHERE Guardian_Contact = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", guardian_contact);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            patient_id_appointment = result.ToString();  // Set the patient_id_appointment
                            Appointment_Panel.Visible = true;
                            Appointment_Panel.BringToFront();
                        }
                        else
                        {
                            MessageBox.Show("Patient not found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void LoadAppointmentsForPatient(string patient_id_appointment)
        {
            Appointment appointment = new Appointment(patient_id, appointment_datetime, appointment_category, appointment_name, appointment_description);

            string query = "SELECT Client_Name, Appointment_Category, Appointment_Description, Appointment_DateTime FROM Appointment WHERE Patient_ID = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Step 1: Select tasks for the given patient ID
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", patient_id_appointment);

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
                    reloadCommand.Parameters.AddWithValue("?", patient_id_appointment);

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

        public void Filtered_Appointments_Click(object sender, EventArgs e)
        {
            LoadFilteredAppointments(patient_id_appointment);
        }

        public void LoadFilteredAppointments(string patient_id_appointment)
        {
            patient_id = patient_id_appointment;
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
                    command.Parameters.AddWithValue("?", patient_id_appointment);

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

        public void Reload_Appointment_Click(object sender, EventArgs e)
        {
            LoadAppointmentsForPatient(patient_id_appointment);
        }

        Appointment_Library appointmentLibrary = new Appointment_Library();

        public void Add_Appointment_Click(object sender, EventArgs e)
        {
            appointment_datetime = Appointment_DateTime.Value;
            appointment_name = Appointment_Name_txt.Text;
            appointment_description = Appointment_Description_txt.Text;
            appointment_category = Appointment_Category.SelectedItem.ToString();

            Appointment appointment = new Appointment(patient_id_appointment, appointment_datetime, appointment_category, appointment_name, appointment_description);


            if (appointment.Add_Appointment_Click(connectionString))
            {
                MessageBox.Show("Appointment Added!");
            }
            else
            {
                MessageBox.Show("Information incomplete");
            }

            appointmentLibrary.AddAppt(appointment);

            Appointment_Name_txt.Clear();
            Appointment_Description_txt.Clear();
            Appointment_Category.SelectedIndex = -1;
        }

        public void Remove_Appointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment(patient_id, appointment_datetime, appointment_category, appointment_name, appointment_description);

            if (Grid_Patient_Appointments.SelectedRows.Count > 0)
            {
                // Get selected Expense_ID and Patient_ID
                string appointment_name = Grid_Patient_Appointments.SelectedRows[0].Cells["Client_Name"].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this appointment?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    appointment.RemoveAppointment(appointment_name, connectionString);
                }
            }
            else
            {
                MessageBox.Show("Please select a appointment to delete.");
            }
        }

        public void Patient_Insurance_Display_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            Prediction_Panel.BringToFront();
            Prediction_Panel.Visible = true;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Extract the clicked row
                    DataGridViewRow row = Patient_Display.Rows[e.RowIndex];

                    // Get Patient_Name from the row — assuming the column is named exactly "Patient_Name"
                    int guardian_contact = Convert.ToInt32(row.Cells["Guardian_Contact"].Value);

                    connection.Open();

                    string query = "SELECT * FROM Patient WHERE Guardian_Contact = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", guardian_contact);

                        OleDbDataReader reader = command.ExecuteReader();
                        {
                            if (reader.Read())
                            {
                                int weightKg, heightCm;
                                double bmi2;

                                patient_id = reader["Patient_ID"].ToString(); // Get the Patient_ID from the database row

                                // Fill in the textboxes from the database
                                Name_Insurance_txt.Text = reader["Patient_Name"].ToString();
                                Age_Insurance_txt.Text = reader["Patient_Age"].ToString();
                                Children_Insurance_txt.Text = reader["Patient_Children"].ToString();
                                Smoker_Insurance_txt.Text = reader["Patient_Smoker"].ToString();
                                Region_Insurance_txt.Text = reader["Patient_Region"].ToString();
                                Gender_Insurance_txt.Text = reader["Patient_Gender"].ToString();

                                Height_txt.Text = reader["Patient_Height"].ToString();
                                Weight_txt.Text = reader["Patient_Weight"].ToString();

                                heightCm = Convert.ToInt32(Height_txt.Text);  // Add this field if not already there
                                weightKg = Convert.ToInt32(Weight_txt.Text);

                                double heightM = heightCm / 100.0;  // ensure division is done in double
                                bmi2 = weightKg / (heightM * heightM);

                                BMI_Insurance_txt.Text = bmi2.ToString("F2");

                                isUpdating = true;

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

        public void Make_Prediction_Click_1(object sender, EventArgs e)
        {
            int age = 0, heightCm = 0, weightKg = 0, children = 0, patient_charges = 0;
            string region = "", gender = "", smoker = "";
            float bmi = 0;


            age = Convert.ToInt32(Age_Insurance_txt.Text);
            bmi = float.Parse(BMI_Insurance_txt.Text);

            children = Convert.ToInt32(Children_Insurance_txt.Text);
            smoker = Smoker_Insurance_txt.Text.ToLower();
            region = Region_Insurance_txt.Text.ToLower();
            gender = Gender_Insurance_txt.Text.ToLower();

            // Then, create an entry for prediction
            InsuranceInfo dbInsuranceInfo = new InsuranceInfo
            {
                Age = age,
                Sex = gender,
                BMI = bmi,
                Children = children,
                Smoker = smoker,
                Region = region,
                Charges = patient_charges
            };

            // Get prediction
            float predictedCharges = MLModel.MLModel.PredictFromDatabaseEntry(dbInsuranceInfo);

            // Show result in a MessageBox
            MessageBox.Show($"Predicted Charges: ${predictedCharges:F2}", "Insurance Prediction");

            Patient patient = new Patient(name, password, gender, age, smoker, children, weight, height, guardian_name, guardian_contact, received_id, caretaker_notes_patient, region, picture);

            patient.Add_Insurance(connectionString, Convert.ToInt32(predictedCharges), guardian_contact);

            Predicted_Insurance_txt.Text = predictedCharges.ToString();
        }

        public void Patient_Finances_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            // Get the clicked row
            DataGridViewRow row = Patient_Finances_Grid.Rows[e.RowIndex];

            // Get the patient name from the clicked row
            int guardian_contact = Convert.ToInt32(row.Cells["Guardian_Contact"].Value);

            // Fetch the corresponding Patient_ID from the database
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Patient_ID FROM Patient WHERE Guardian_Contact = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", guardian_contact);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            expense_patient_id = result.ToString();  // Set the expense_patient_id
                            Expenses_Panel_View.Visible = true;
                            Expenses_Panel_View.BringToFront();
                        }
                        else
                        {
                            MessageBox.Show("Patient not found!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        public void Add_Expenses_Click_1(object sender, EventArgs e)
        {
            expense_name = Expense_Name.Text;
            expense_amount = int.Parse(Expense_Amount.Text);
            expense_datetime = DateTime.Now;

            expense_category = Expense_Category_dropdown.SelectedItem.ToString();

            Expenses expenses = new Expenses(expense_name, expense_category, expense_amount, expense_datetime, expense_patient_id);

            expenses.Add_Expenses_Click(connectionString);
            MessageBox.Show("Expense Added!");

            Expense_Name.Clear();
            Expense_Amount.Clear();
            Expense_Category_dropdown.SelectedIndex = -1;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT Insurance_Charge FROM Patient WHERE Patient_ID = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", expense_patient_id);

                        object result = command.ExecuteScalar();

                        predicted_charges = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            if (predicted_charges >= expense_amount)
            {
                MessageBox.Show($"Your current expenses will be covered by the insurance charge", "Expenses Information");
            }
            else
            {
                MessageBox.Show($"Your current expenses will not be covered by insurance charges", "Expenses Information");
            }
        }

        public void Remove_Expenses_Click_1(object sender, EventArgs e)
        {
            Expenses expenses = new Expenses(expense_name, expense_category, expense_amount, expense_datetime, expense_patient_id);

            if (Patient_Expense_View.SelectedRows.Count > 0)
            {
                // Get selected Expense_ID and Patient_ID
                string expenseID = Patient_Expense_View.SelectedRows[0].Cells["Expense_Name"].Value.ToString();

                DialogResult result = MessageBox.Show("Are you sure you want to delete this expense?", "Confirm Delete", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    expenses.DeleteExpense(expenseID, connectionString);
                }
            }
            else
            {
                MessageBox.Show("Please select an expense to delete.");
            }
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

        public void Load_Expense_Click(object sender, EventArgs e)
        {
            LoadExpensesForPatient(expense_patient_id);
        }

        // Exit Program
        public void Close_Button_Click_1(object sender, EventArgs e)
        {
            // Custom cleanup code
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Gracefully exits the app
            }
        }

        // Browse for picture to add to the PictureBox
        public void Patient_Browse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "My picture | *.jpg; *.png; *.jpeg; *.bmp";
            open.ShowDialog();
            string file = open.FileName.ToString();
            Browse_txt.Text = file;
            Patient_Picture.Image = Image.FromFile(file);
        }

        public void Reload_Patient_Click(object sender, EventArgs e)
        {
            LoadData2(PatientName_Insurance);
        }

        public void Expense_Patient_Filter_Click(object sender, EventArgs e)
        {
            LoadData2(PatientName_Expense);
        }

        public void Information_Patient_Filter_Click(object sender, EventArgs e)
        {
            LoadData2(PatientName_Information);
        }

        public void Task_Patient_Filter_Click(object sender, EventArgs e)
        {
            LoadData2(PatientName_Task);
        }

        public void Appointment_Patient_Filter_Click(object sender, EventArgs e)
        {
            LoadData2(PatientName_Appointment);
        }
    }
}

