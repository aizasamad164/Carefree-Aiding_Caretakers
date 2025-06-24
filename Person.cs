namespace Caretaker_System_3
{
    public abstract class Person
    {
        protected static Random rand = new Random();
        protected static List<int> usedIDs = new List<int>();
        protected string Name;
        protected string Password; 
        protected string Gender;
        protected int Age;

        public Person(string name, string password, string gender, int age)
        {
            Name = name;
            Password = password;
            Gender = gender;
            Age = age;
        }

        public virtual bool Log_Enter_Click_1(string connectionString)
        {
            return false;
        }

        // Generate passwrd using Random
        public static int GeneratedPassword()
        {
            int id;
            do
            {
                id = rand.Next(1, 99999);
            }
            while (usedIDs.Contains(id));
            usedIDs.Add(id);
            return id;
        }

        // clears all textboxes and radiobuttons inside a panel
        public void ClearControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }               
                else if (control is RadioButton radioButton)
                {
                    radioButton.Checked = false;
                }
                else if (control.HasChildren)
                {
                    ClearControls(control); // Recursively clear child controls
                }
            }
        }
    }
}

