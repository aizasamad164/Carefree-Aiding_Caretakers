using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caretaker_System_3
{
    class Expenses
    {
        private static Random rand = new Random();
        private static List<int> usedIDs = new List<int>();
        private string Expense_ID;
        private string Expense_Category;
        private string Expense_Name;
        private int Expense_Amount;
        private DateTime Expense_Date;
        private string Patient_Expense_ID;
        public string guardian_password;

        public Expenses(string name, string category, int amount, DateTime expense_datetime, string patient_expense_id)
        {
            Expense_Name = name;
            Expense_Category = category;
            Expense_Amount = amount;
            Expense_Date = expense_datetime;
            Patient_Expense_ID = patient_expense_id;
        }

        // generate Expense ID
        public static int Generated_Expenses_ID()
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

        public void Add_Expenses_Click(string connectionString)
        {
            Expense_ID = "E-" + Generated_Expenses_ID();
                                                    
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query2 = "INSERT INTO Expenses (Expense_ID, Expense_Name, Expense_Category, Expense_Amount, Expense_Date, Patient_ID) VALUES (?, ?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query2, connection))
                {
                    cmd.Parameters.AddWithValue("?", Expense_ID);
                    cmd.Parameters.AddWithValue("?", Expense_Name);
                    cmd.Parameters.AddWithValue("?", Expense_Category);
                    cmd.Parameters.AddWithValue("?", Expense_Amount);
                    cmd.Parameters.AddWithValue("?", Expense_Date.ToString("MM/dd/yyyy hh:mm:ss"));
                    cmd.Parameters.AddWithValue("?", Patient_Expense_ID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            UpdatePatientTotalExpenses(Patient_Expense_ID, connectionString);       
        }

        public void DeleteExpense(string expenseID, string connectionString)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Delete from Expenses table
                    string deleteQuery = "DELETE FROM Expenses WHERE Expense_Name = ?";
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, connection))
                    {
                        deleteCmd.Parameters.AddWithValue("?", expenseID);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

                // Recalculate and update patient total expenses
                UpdatePatientTotalExpenses(Patient_Expense_ID, connectionString);

                MessageBox.Show("Expense deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to delete expense: " + ex.Message);
            }
        }


        public void UpdatePatientTotalExpenses(string patientId, string connectionString)
        {
            double totalExpenses = 0;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Step 1: Get total expenses for this patient
                string sumQuery = "SELECT SUM(Expense_Amount) FROM Expenses WHERE Patient_ID = ?";
                using (OleDbCommand sumCommand = new OleDbCommand(sumQuery, connection))
                {
                    sumCommand.Parameters.AddWithValue("?", patientId);
                    object result = sumCommand.ExecuteScalar();
                    totalExpenses = (result != DBNull.Value) ? Convert.ToDouble(result) : 0;
                }

                // Step 2: Update Patient table
                string updateQuery = "UPDATE Patient SET Charges = ? WHERE Patient_ID = ?";
                using (OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("?", totalExpenses);
                    updateCommand.Parameters.AddWithValue("?", patientId);
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

    }
}

