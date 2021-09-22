using System;
using System.Collections.Generic;
using Server.Model;

namespace Server.Services
{
    public interface ITaskService
    {
        public IEnumerable<TaskItem> GetAll();
        public TaskItem GetByGuid(Guid guid);
    }
}