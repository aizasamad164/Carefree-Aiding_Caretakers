using System.Data.OleDb;

namespace Caretaker_System_3 
{
    class Task
    {
        private string Task_Name;
        private string Patient_ID;
        private string Task_Description;
        private string Task_Frequency;
        private string Task_Priority;
        private string Task_Category;
        DateTime Task_DateTime;

        public Task(string task_name, string task_priority, DateTime task_datetime, string patient_id, string task_description, string task_frequency, string task_category)
        {
            Task_Name = task_name;
            Task_DateTime = task_datetime;
            Patient_ID = patient_id;
            Task_Category = task_category;
            Task_Frequency = task_frequency;
            Task_Priority = task_priority;
            Task_Description = task_description ?? "";
        }

        // Returns the next date after today based on the given frequency
        public DateTime GenerateNextOccurrence(DateTime date, string frequency)
        {
            DateTime updated = date;

            while (updated <= DateTime.Now)
            {
                switch (frequency.ToLower())
                {
                    case "daily":
                        updated = updated.AddDays(1);
                        break;
                    case "alternate":
                        updated = updated.AddDays(2);
                        break;
                    case "weekly":
                        updated = updated.AddDays(7);
                        break;
                    default:
                        return date; // If invalid frequency, keep original
                }
            }

            return updated;
        }

        public bool Add_Task_Click(string connectionString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query2 = "INSERT INTO Task_Scheduler (Task_Name, Task_DateTime, Patient_ID, Task_Category, Task_Description, Task_Priority, Task_Frequency) VALUES (?, ?, ?, ?, ?, ?, ?)";
                                
                using (OleDbCommand cmd = new OleDbCommand(query2, connection))
                {
                    cmd.Parameters.AddWithValue("?", Task_Name);
                    cmd.Parameters.AddWithValue("?", Task_DateTime.ToString());
                    cmd.Parameters.AddWithValue("?", Patient_ID);
                    cmd.Parameters.AddWithValue("?", Task_Category);
                    cmd.Parameters.AddWithValue("?", Task_Description);
                    cmd.Parameters.AddWithValue("?", Task_Priority);
                    cmd.Parameters.AddWithValue("?", Task_Frequency);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return true;            
        }

        public void RemoveTask(string task_name, string connectionString)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Delete from Expenses table
                    string deleteQuery = "DELETE FROM Task_Scheduler WHERE Task_Name = ?";
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("?", task_name);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Task deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete task: " + ex.Message);
            }
        }
    }
}
