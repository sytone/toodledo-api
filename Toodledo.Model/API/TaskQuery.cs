using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public class TaskQuery
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public Folder Folder { get; set; }
        public Context Context { get; set; }
        public Goal Goal { get; set; }
        public Priority? Priority { get; set; }
        public Frequency? Repeat { get; set; }
        public string AdvancedRepeat { get; set; }
        public Status? Status { get; set; }
        public Task Parent { get; set; }

        public int? ShorterThan { get; set; }
        public int? LongerThan { get; set; }

        public DateTime? Before { get; set; }
        public DateTime? After { get; set; }

        public DateTime? StartBefore { get; set; }
        public DateTime? StartAfter { get; set; }

        public DateTime? ModifiedBefore { get; set; }
        public DateTime? ModifiedAfter { get; set; }

        public DateTime? CompletedBefore { get; set; }
        public DateTime? CompletedAfter { get; set; }

        public bool? NotCompleted { get; set; }
        public bool? HasStar { get; set; }
        public int? Id { get; set; }

        public int? PageStart { get; set; }
        public int? PageCount { get; set; }
    }
}
