using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Caretaker_System_3
{
    public class Caretaker : Person
    {
        protected string Caretaker_Notes, Caretaker_ID;
        protected int Caretaker_Contact;
        
        public Caretaker(string name, string password, string gender, int age, int caretaker_contact, string caretaker_notes) : base(name, password, gender, age)
        {           
            Caretaker_Contact = caretaker_contact;
            Caretaker_Notes = caretaker_notes;
        }

        public Caretaker(string name, string password, string gender, int age) : base(name, password, gender, age) { }

        // Generate ID using Random
        public static int GeneratedID_Caretaker()
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

        public override bool Log_Enter_Click_1(string connectionString)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Caretaker WHERE Caretaker_Name = ? AND Caretaker_Password = ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", Name);
                    command.Parameters.AddWithValue("?", Password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public bool Add_Caretaker_Click(string connectionString)
        {
            Caretaker_ID = "C-" + GeneratedID_Caretaker();
            Password = GeneratedID_Caretaker().ToString();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string query = "INSERT INTO Caretaker (Caretaker_ID, Caretaker_Name, Caretaker_Password, Caretaker_Age, Caretaker_Gender, Caretaker_Contact, Caretaker_Notes) VALUES (?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {                  
                    cmd.Parameters.AddWithValue("?", Caretaker_ID);
                    cmd.Parameters.AddWithValue("?", Name);
                    cmd.Parameters.AddWithValue("?", Password);
                    cmd.Parameters.AddWithValue("?", Age);
                    cmd.Parameters.AddWithValue("?", Gender);
                    cmd.Parameters.AddWithValue("?", Caretaker_Contact);
                    cmd.Parameters.AddWithValue("?", Caretaker_Notes);

                    connection.Open();
                    cmd.ExecuteNonQuery();                   
                }
            }

            MessageBox.Show("The password is " + Password);

            return true;            
        }
    }
}

