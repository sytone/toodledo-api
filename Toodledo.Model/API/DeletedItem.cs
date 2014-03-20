using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public class DeletedItem
    {
        public int Id { get; set; }
        public DateTime Deleted { get; set; }
    }
}
