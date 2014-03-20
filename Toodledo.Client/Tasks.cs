using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Toodledo.Model;
using Toodledo.Model.API;

namespace Toodledo.Client
{
	public partial class Session : ITasks
	{
	    #region Implementation of ITasks

	    TaskSet ITasks.GetTasks(TaskQuery query)
	    {
            var factory = new ItemFactory<Task>("task")
                .Element<int>("Id", "id")
                .Element<string>("Name", "title")
                .Element<string>("Tag", "tag")
                .Element<Folder>("Folder", new ItemFactory<Folder>("folder")
                                                 .Value<int>("Id"))
                .Element<Context>("Context", new ItemFactory<Context>("context")
                                                 .Attribute<int>("Id", "id")
                                                 .Value<string>("Name"))
                .Element<Goal>("Goal", new ItemFactory<Goal>("goal")
                                                 .Attribute<int>("Id", "id")
                                                 .Value<string>("Name"))
                .Element<DateTime>("Added", "added")
                .Element<DateTime>("Modified", "modified")
                .Element<DateTime>("Start", "startdate")
                .Element<DateTime>("Due", "duedate")
                .Element<DateTime>("Due", "duetime")
                .Element<DateTime>("Start", "starttime")
                .Element<int>("Reminder", "reminder")
                .Element<DateTime>("Completed", "completed")
                .Element<Frequency>("Repeat", "repeat")
                .Element<string>("AdvancedRepeat", "rep_advanced")
                .Element<Status>("Status", "status")
                .Element<bool>("HasStar", "star")
                .Element<Priority>("Priority", "priority")
                .Element<int>("Length", "length")
                .Element<string>("Note", "note");

            var results = new TaskSet();
	        var tasks = new List<Task>();

	        var builder = new StringBuilder();

            if (!String.IsNullOrEmpty(query.Name)) builder.AppendFormat("title={0};", encodeString(query.Name));
            if (!String.IsNullOrEmpty(query.Tag)) builder.AppendFormat("tag={0};", encodeString(query.Tag));
            if (query.Folder != null) builder.AppendFormat("folder={0};", query.Folder.Id);
            if (query.Context != null) builder.AppendFormat("context={0};", query.Context.Id);
            if (query.Goal != null) builder.AppendFormat("goal={0};", query.Goal.Id);
            if (query.Priority.HasValue) builder.AppendFormat("priority={0};", (int)query.Priority);
            if (query.Repeat.HasValue) builder.AppendFormat("repeat={0};", (int)query.Repeat);
            if (query.Status.HasValue) builder.AppendFormat("status={0};;", (int)query.Status);
            if (query.Parent != null) builder.AppendFormat("parent={0};", query.Parent.Id);
            if (query.ShorterThan.HasValue) builder.AppendFormat("shorter={0};", query.ShorterThan);
            if (query.LongerThan.HasValue) builder.AppendFormat("longer={0};", query.LongerThan);
            if (query.Before.HasValue) builder.AppendFormat("before={0};", query.Before);
            if (query.After.HasValue) builder.AppendFormat("after={0};", query.After);
            if (query.StartBefore.HasValue) builder.AppendFormat("startbefore={0};", query.StartBefore);
            if (query.StartAfter.HasValue) builder.AppendFormat("startafter={0};", query.StartAfter);
            if (query.ModifiedBefore.HasValue) builder.AppendFormat("modbefore={0};", query.ModifiedBefore);
            if (query.ModifiedAfter.HasValue) builder.AppendFormat("modafter={0};", query.ModifiedAfter);
            if (query.CompletedBefore.HasValue) builder.AppendFormat("compbefore={0};", query.CompletedBefore);
            if (query.CompletedAfter.HasValue) builder.AppendFormat("compafter={0};", query.CompletedAfter);
            if (query.NotCompleted.HasValue) builder.AppendFormat("notcomp={0};", query.NotCompleted.Value ? 1 : 0);
            if (query.HasStar.HasValue) builder.AppendFormat("star={0};", query.HasStar.Value ? 1 : 0);
            if (query.Id.HasValue) builder.AppendFormat("id={0};", query.Id);
            if (query.PageStart.HasValue) builder.AppendFormat("start={0};", query.PageStart);
            if (query.PageCount.HasValue) builder.AppendFormat("end={0};", query.PageCount);

            var response = SendRequest("getTasks", builder.ToString());
            foreach (var thisElement in response.Descendants(XName.Get("task", "")))
                tasks.Add(factory.Create(thisElement));

	        results.Tasks = tasks;
	        results.Number = ReadAttribute<int>(response.Root, "num");
            results.Total = ReadAttribute<int>(response.Root, "total");
            results.PageStart = ReadAttribute<int>(response.Root, "start");
            results.PageCount = ReadAttribute<int>(response.Root, "end");
            return results;
        }

        private string encodeString(string text)
        {
            return text.Replace("&", "%26").Replace(";", "%3B");
        }

	    int ITasks.AddTask(Task task)
	    {
            if (String.IsNullOrEmpty(task.Name))
                throw new InvalidDataException("Task.Name is required");

            var builder = new StringBuilder();

            builder.Append(formatArgument(task.Name, "title"));
            builder.Append(formatArgument(task.Tag, "tag"));
            builder.Append(formatArgument(task.Folder, "folder"));
            builder.Append(formatArgument(task.Context, "context"));
            builder.Append(formatArgument(task.Goal, "goal"));
            builder.Append(formatArgument(task.Parent, "parent"));
            builder.Append(formatArgument(task.Due.Date, "duedate"));
            builder.Append(formatArgument(task.Due.TimeOfDay, "duetime"));
            builder.Append(formatArgument(task.Start.Date, "startdate"));
            builder.Append(formatArgument(task.Start.TimeOfDay, "starttime"));
            builder.Append(formatArgument(task.Reminder, "reminder"));
            builder.Append(formatArgument((int)task.Repeat, "repeat"));
            builder.Append(formatArgument(task.AdvancedRepeat, "rep_advanced"));
            builder.Append(formatArgument((int)task.Status, "status"));
            builder.Append(formatArgument(task.Length, "length"));
            builder.Append(formatArgument((int)task.Priority, "priority"));
            builder.Append(formatArgument(task.HasStar, "star"));
            builder.Append(formatArgument(task.Note, "note"));

            var response = SendRequest("addTask", builder.ToString());
            return ReadElement<int>(response);
        }

	    bool ITasks.EditTask(Task task)
	    {
            if (task.Id <= 0)
                throw new InvalidDataException("Task.Id is required");

            var builder = new StringBuilder();

            builder.Append(formatArgument(task.Id, "id"));
            builder.Append(formatArgument(task.Name, "title"));
            builder.Append(formatArgument(task.Tag, "tag"));
            builder.Append(formatArgument(task.Folder, "folder"));
            builder.Append(formatArgument(task.Context, "context"));
            builder.Append(formatArgument(task.Goal, "goal"));
            builder.Append(formatArgument(task.Parent, "parent"));
            builder.Append(formatArgument(task.Due.Date, "duedate"));
            builder.Append(formatArgument(task.Due.TimeOfDay, "duetime"));
            builder.Append(formatArgument(task.Start.Date, "startdate"));
            builder.Append(formatArgument(task.Start.TimeOfDay, "starttime"));
            builder.Append(formatArgument(task.Reminder, "reminder"));
            builder.Append(formatArgument((int)task.Repeat, "repeat"));
            builder.Append(formatArgument(task.AdvancedRepeat, "rep_advanced"));
            builder.Append(formatArgument((int)task.Status, "status"));
            builder.Append(formatArgument(task.Length, "length"));
            builder.Append(formatArgument((int)task.Priority, "priority"));
            builder.Append(formatArgument(task.HasStar, "star"));
            builder.Append(formatArgument(task.Note, "note"));
            builder.Append(formatArgument(task.Completed, "completedon"));
            builder.Append(formatArgument(task.Completed != DateTime.MinValue, "completed"));

            var response = SendRequest("editTask", builder.ToString());
            return (ReadElement<int>(response) != 0);
        }

	    bool ITasks.DeleteTask(int id)
	    {
            var parameters = String.Format("id={0}", id);
            var response = SendRequest("deleteTask", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    IEnumerable<DeletedItem> ITasks.GetDeleted(DateTime after)
	    {
	        var factory = new ItemFactory<DeletedItem>("task")
	            .Element<int>("Id", "id")
	            .Element<DateTime>("Deleted", "stamp");
            var results = new List<DeletedItem>();

	        var parameters = String.Format("after={0}", after);
            var response = SendRequest("getDeleted", parameters);
            foreach (var thisElement in response.Descendants(XName.Get("task", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    #endregion
	}
}
