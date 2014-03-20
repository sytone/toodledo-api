using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Toodledo.Model;
using Toodledo.Model.API;

namespace Toodledo.Client
{
	public partial class Session : INotebook
	{
	    #region Implementation of INotebook

	    IEnumerable<Note> INotebook.GetNotes(DateTime? before, DateTime? after, int? id)
	    {
	        var factory = new ItemFactory<Note>("note")
	            .Element<int>("Id", "id")
                .Element<DateTime>("Added", "added")
                .Element<DateTime>("Modified", "modified")
                .Element<Folder>("Folder", new ItemFactory<Folder>("folder").Value<int>("Id"))
                .Element<string>("Text", "text")
                .Element<string>("Name", "title")
                .Element<bool>("IsPrivate", "private");
            
            var builder = new StringBuilder();
	        builder.Append(formatArgument(before, "modbefore"));
            builder.Append(formatArgument(after, "modafter"));
            builder.Append(formatArgument(id, "id"));

            var results = new List<Note>();

	        var response = SendRequest("getNotes", builder.ToString());
            foreach (var thisElement in response.Descendants(XName.Get("note", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    int INotebook.AddNote(Note note)
	    {
            var builder = new StringBuilder();
            builder.Append(formatArgument(note.Name, "title"));
            if (note.Folder != null)
                builder.Append(formatArgument(note.Folder.Id, "folder"));
            builder.Append(formatArgument(note.IsPrivate, "private"));
            builder.Append(formatArgument(note.Added, "addedon"));
            builder.Append(formatArgument(note.Text, "note"));

            var response = SendRequest("addNote", builder.ToString());
            return ReadElement<int>(response);
        }

	    bool INotebook.EditNote(Note note)
	    {
            var builder = new StringBuilder();
            builder.Append(formatArgument(note.Id, "id"));
            builder.Append(formatArgument(note.Name, "title"));
            builder.Append(formatArgument(note.Folder.Id, "folder"));
            builder.Append(formatArgument(note.IsPrivate, "private"));
            builder.Append(formatArgument(note.Added, "addedon"));
            builder.Append(formatArgument(note.Text, "note"));

            var response = SendRequest("editNote", builder.ToString());
            return (ReadElement<int>(response) == 1);
        }

	    bool INotebook.DeleteNote(int id)
	    {
            var parameters = String.Format("id={0}", id);
            var response = SendRequest("deleteNote", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    IEnumerable<DeletedItem> INotebook.GetDeletedNotes(DateTime after)
	    {
            var factory = new ItemFactory<DeletedItem>("note")
                .Element<int>("Id", "id")
                .Element<DateTime>("Deleted", "stamp");
            var results = new List<DeletedItem>();

            var parameters = String.Format("after={0}", after);
            var response = SendRequest("getDeletedNotes", parameters);
            foreach (var thisElement in response.Descendants(XName.Get("note", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    #endregion
	}
}
