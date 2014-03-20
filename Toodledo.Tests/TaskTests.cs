using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toodledo.Client;
using Toodledo.Model;

using Toodledo.Model.API;
using Toodledo.Tests.Internal;
using Toodledo.Tests.Properties;

namespace Toodledo.Tests
{
    [TestClass]
    public class TaskTests : TestsBase
	{
        [TestMethod]
        public void Can_Get_Tasks_In_Folder()
        {
            var folder = General.GetFolders().First();
            var query = new TaskQuery() {Folder = folder};
            var tasks = Tasks.GetTasks(query);

            ExtAssert.Greater(tasks.Total, 0);

            var wrong = from task in tasks.Tasks
                        where task.Folder == null || task.Folder.Id != folder.Id
                        select task;

            Assert.AreEqual(0, wrong.Count());
        }

        [TestMethod]
        public void Can_Get_Tasks_In_ToBuy_Folder_and_AutoZone_Context()
        {
            var folder = General.GetFolders().Where(f => f.Name == "To Buy").First();
            Assert.AreEqual("To Buy", folder.Name);

            var context = General.GetContexts().Where(c => c.Name == "AutoZone").First();
            Assert.AreEqual("AutoZone", context.Name);

            var query = new TaskQuery() { Folder = folder, Context = context };
            var tasks = Tasks.GetTasks(query);

            ExtAssert.Greater(tasks.Total, 0);

            var wrong = from task in tasks.Tasks
                        where (task.Folder == null || task.Folder.Id != folder.Id)
                        || (task.Context == null || task.Context.Id != context.Id)
                        select task;

            Assert.AreEqual(0, wrong.Count());
        }

        [TestMethod]
        public void Can_Create_Task()
        {
            var task = new Task() {Name = "*Test Task*"};

            var added = Tasks.AddTask(task);
            ExtAssert.Greater(added, 0);

            var tasks = Tasks.GetTasks(new TaskQuery() { Id = added });

            ExtAssert.Greater(tasks.Number, 0);
            var result = tasks.Tasks.First();

            Assert.AreEqual(added, result.Id);
            Assert.AreEqual("*Test Task*", result.Name);
        }

        [TestMethod]
        public void Can_Edit_Task()
        {
            var tasks = Tasks.GetTasks(new TaskQuery() { Name = "*Test Task*" });

            ExtAssert.Greater(tasks.Number, 0);
            var result = tasks.Tasks.First();
            var id = result.Id;

            result.Name = "*Edited Task*";
            var success = Tasks.EditTask(result);
            Assert.IsTrue(success);

            tasks = Tasks.GetTasks(new TaskQuery() {Id = id});
            result = tasks.Tasks.First();

            Assert.IsTrue(result.Name == "*Edited Task*");
        }

        [TestMethod]
        public void Can_Delete_Task()
        {
            var tasks = Tasks.GetTasks(new TaskQuery() { Name = "*Edited Task*" });

            ExtAssert.Greater(tasks.Number, 0);
            var result = tasks.Tasks.First();

            var success = Tasks.DeleteTask(result.Id);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Can_Get_Deleted_Tasks()
        {
            var items = Tasks.GetDeleted(DateTime.Parse("10/1/2009"));
            ExtAssert.Greater(items.Count(), 0);
        }

    }
}
