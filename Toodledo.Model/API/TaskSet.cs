using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public class TaskSet
    {
        public int Number { get; set; }
        public int Total { get; set; }
        public int PageStart { get; set; }
        public int PageCount { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
    }
}
