using System.Data.OleDb;

namespace Caretaker_System_3
{
    class Finances
    {
        public double Balance, Charges;

        public Finances(double balance)
        {
            Balance = balance;
        }

        public int GetCurrentCharges(string connectionString, string guardian_password)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Charges FROM Patient WHERE Guardian_Password = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", guardian_password);
                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return -1; // Or throw an exception if preferred

                    return Convert.ToInt32(result);
                }
            }
        }

        public bool AddBalance(string connectionString, double amount, string guardian_password)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Add to existing Charges
                string query = "UPDATE Patient SET Charges = Charges + ? WHERE Guardian_Password = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", amount);
                    cmd.Parameters.AddWithValue("?", guardian_password);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                    
                }
            }
        }

        public void DeductBalance(string connectionString, double amount, string guardian_password)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // ✅ Step 1: Get current Charges
                string selectQuery = "SELECT Charges FROM Patient WHERE Guardian_Password = ?";
                double currentCharges = 0;

                using (OleDbCommand selectCmd = new OleDbCommand(selectQuery, connection))
                {
                    selectCmd.Parameters.AddWithValue("?", guardian_password);

                    object result = selectCmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Guardian contact not found.");
                        return;
                    }

                    currentCharges = Convert.ToDouble(result);
                }

                // Step 2: Check if enough balance exists
                if (currentCharges >= amount)
                {
                    // Step 3: Deduct the amount
                    string updateQuery = "UPDATE Patient SET Charges = Charges - ? WHERE Guardian_Password = ?";
                    using (OleDbCommand updateCmd = new OleDbCommand(updateQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("?", amount);
                        updateCmd.Parameters.AddWithValue("?", guardian_password);

                        int rowsAffected = updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Insufficient balance. Cannot deduct.");
                }
            }
        }
    }
}
