using System;

namespace BlazorTaskManager.Model
{
    public class TaskModel
    {
        public Guid Guid { get; private set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsComplete { get; private set; }
        
        public TaskModel(string name, string content)
        {
            Name = name;
            Content = content;
            Guid = Guid.NewGuid();
            IsComplete = false;
        }

        public void Completed()
        {
            IsComplete = true;
        }
    }
}