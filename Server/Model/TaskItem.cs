using System;

namespace Server.Model
{
    public enum TaskStatus
    {
        Done,
        Undone,
        InProgress,
    }
    
    public class TaskItem
    {
        public string Description { get; set; }
        public string Title { get; set; }
        
        public Guid Guid { get; private set; }
        public TaskStatus Status { get; private set; }

        public TaskItem(string title, string description)
        {
            Title = title;
            Description = description;

            Guid = Guid.NewGuid();
            Status = TaskStatus.Undone;
        }
    }
}