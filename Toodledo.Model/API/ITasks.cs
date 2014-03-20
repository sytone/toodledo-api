using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public interface ITasks
    {
        TaskSet GetTasks(TaskQuery query);
        int AddTask(Task task);
        bool EditTask(Task task);
        bool DeleteTask(int id);
        IEnumerable<DeletedItem> GetDeleted(DateTime after);
    }
}
