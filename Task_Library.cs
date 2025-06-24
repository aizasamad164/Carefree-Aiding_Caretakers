namespace Caretaker_System_3
{
    class Task_Library
    {
        public List<Task> tasklist;

        public Task_Library()
        {
            tasklist = new List<Task>();
        }

        public void AddTask(Task t)
        {
            tasklist.Add(t);
        }

    }
}
