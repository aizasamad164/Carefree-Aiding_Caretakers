namespace Caretaker_System_3
{
    class Appointment_Library
    {
        public List<Appointment> apptlist;

        public Appointment_Library()
        {
            apptlist = new List<Appointment>();

        }

        public void AddAppt(Appointment pA)
        {
            apptlist.Add(pA);
        }
    }
}
