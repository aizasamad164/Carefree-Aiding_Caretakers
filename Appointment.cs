using System.Data.OleDb;

namespace Caretaker_System_3
{
    class Appointment
    {
        private string Patient_ID;
        public DateTime Appointment_DateTime;
        private string Category;
        private string Client_Name;
        private string Description;

        public Appointment(string patient_id, DateTime appointment_datetime, string category, string client_name, string description)
        {
            Patient_ID = patient_id;
            Appointment_DateTime = appointment_datetime;
            Category = category;
            Client_Name = client_name;
            Description = description;
        }

        public void showinfo() { }

        public bool Add_Appointment_Click(string connectionString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query2 = "INSERT INTO Appointment (Client_Name, Appointment_Category, Appointment_DateTime, Patient_ID, Appointment_Description) VALUES (?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query2, connection))
                {
                    cmd.Parameters.AddWithValue("?", Client_Name);
                    cmd.Parameters.AddWithValue("?", Category);
                    cmd.Parameters.AddWithValue("?", Appointment_DateTime.ToString());
                    cmd.Parameters.AddWithValue("?", Patient_ID);
                    cmd.Parameters.AddWithValue("?", Description);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }

        public void RemoveAppointment(string appointment_name, string connectionString)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Delete from Expenses table
                    string deleteQuery = "DELETE FROM Appointment WHERE Client_Name = ?";
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("?", appointment_name);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete appointment: " + ex.Message);
            }
        }
    }
}
