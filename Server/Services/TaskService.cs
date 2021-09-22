using System;
using System.Collections.Generic;
using System.Linq;
using Server.Model;

namespace Server.Services
{
    public class TaskService : ITaskService
    {
        //TODO replace it with DTO
        private static readonly IEnumerable<TaskItem> TestTasks = new List<TaskItem>
        {
            new TaskItem("Title from Api","Description from api"),
            new TaskItem("Title from Api1","Description from api1"),
            new TaskItem("Title from Api2","Description from api2"),
            new TaskItem("Title from Api3","Description from api3")
        };
        
        public IEnumerable<TaskItem> GetAll()
        {
            return TestTasks;
        }

        public TaskItem GetByGuid(Guid guid)
        {
            return TestTasks.Single(task => task.Guid.Equals(guid));
        }
    }
}