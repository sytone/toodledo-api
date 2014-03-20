using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public interface INotebook
    {
        IEnumerable<Note> GetNotes(DateTime? before, DateTime? after, int? id);
        int AddNote(Note note);
        bool EditNote(Note note);
        bool DeleteNote(int id);
        IEnumerable<DeletedItem> GetDeletedNotes(DateTime after);
    }
}
