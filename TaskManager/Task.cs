namespace TaskManager
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Task()
        {
            
        }

        public Task(string title, string description)
        {
            Title = title;
            Description = description;
            Status = "PENDENTE";
        }
    }
}
