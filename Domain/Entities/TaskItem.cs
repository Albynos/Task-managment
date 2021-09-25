using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class TaskItem
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditTime { get; set; }
        public TaskStatus Status { get; set; }
    }
}