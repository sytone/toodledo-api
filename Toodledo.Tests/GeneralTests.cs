using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toodledo.Model;
using Toodledo.Tests.Internal;

namespace Toodledo.Tests
{
    [TestClass]
    public class GeneralTests : TestsBase
    {
        [TestMethod]
        public void Can_Get_Folders()
        {
            var folders = General.GetFolders();

            Assert.IsNotNull(folders);
            ExtAssert.Greater(folders.Count(), 0);
        }

        [TestMethod]
        public void Can_Get_Contexts()
        {
            var contexts = General.GetContexts();

            Assert.IsNotNull(contexts);
            ExtAssert.Greater(contexts.Count(), 0);
        }

        [TestMethod]
        public void Can_Get_Goals()
        {
            var goals = General.GetGoals();

            Assert.IsNotNull(goals);
            ExtAssert.Greater(goals.Count(), 0);
        }

        [TestMethod]
        public void Can_Get_GoalNotes()
        {
            var notes = General.GetGoalNotes();

            Assert.IsNotNull(notes);
        }

        [TestMethod]
        public void Can_Merge_GoalNotes_With_Goal()
        {
            var goals = General.GetGoals();
            var notes = General.GetGoalNotes();

            var goal = goals.First();
            var note = notes.Where(n => n.Id == goal.Id).First();

            goal.Notes = String.Empty;
            note.Notes = "Sample Goal Note";

            goal = Goal.MergeNotes(goal, note);

            Assert.AreEqual(note.Notes, goal.Notes);
        }

        [TestMethod]
        public void Can_Merge_GoalNotes_With_Goals()
        {
            var goals = General.GetGoals();
            var notes = General.GetGoalNotes();
            var TEST_NOTE = "Test Note for Goal.Id == {0}";

            foreach (var note in notes)
                note.Notes = String.Format(TEST_NOTE, note.Id);

            goals = Goal.MergeNotes(goals, notes);

            foreach (var goal in goals)
                Assert.AreEqual(String.Format(TEST_NOTE, goal.Id), goal.Notes);

        }

        [TestMethod]
        public void Can_Create_Folder()
        {
            var name = "*Test Folder*";
            var added = General.AddFolder(name, true);
            ExtAssert.Greater(added, 0);

            var folders = General.GetFolders();
            var query = from folder in folders
                        where folder.Id == added
                        select folder;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            Assert.AreEqual(added, result.Id);
            Assert.AreEqual(true, result.IsPrivate);
            Assert.AreEqual(name, result.Name);
        }

        [TestMethod]
        public void Can_Edit_Folder()
        {
            var name = "*Test Folder*";

            var folders = General.GetFolders();
            var query = from folder in folders
                        where folder.Name == name
                        select folder;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            result.Name = "*Edited Folder*";
            var success = General.EditFolder(result);
            Assert.IsTrue(success);
            Assert.IsTrue(result.Name != name);
        }

        [TestMethod]
        public void Can_Delete_Folder()
        {
            var name = "*Edited Folder*";

            var folders = General.GetFolders();
            var query = from folder in folders
                        where folder.Name == name
                        select folder;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            var success = General.DeleteFolder(result.Id);
            Assert.IsTrue(success);

            folders = General.GetFolders();
            query = from folder in folders
                    where folder.Name == name
                    select folder;
            Assert.AreEqual(0, query.Count());
        }

        [TestMethod]
        public void Can_Create_Context()
        {
            var name = "*Test Context*";
            var added = General.AddContext(name);
            ExtAssert.Greater(added, 0);

            var contexts = General.GetContexts();
            var query = from context in contexts
                        where context.Id == added
                        select context;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            Assert.AreEqual(added, result.Id);
            Assert.AreEqual(name, result.Name);
        }

        [TestMethod]
        public void Can_Edit_Context()
        {
            var name = "*Test Context*";

            var contexts = General.GetContexts();
            var query = from context in contexts
                        where context.Name == name
                        select context;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            result.Name = "*Edited Context*";
            var success = General.EditContext(result);
            Assert.IsTrue(success);
            Assert.IsTrue(result.Name != name);
        }

        [TestMethod]
        public void Can_Delete_Context()
        {
            var name = "*Edited Context*";

            var contexts = General.GetContexts();
            var query = from context in contexts
                        where context.Name == name
                        select context;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            var success = General.DeleteContext(result.Id);
            Assert.IsTrue(success);

            contexts = General.GetContexts();
            query = from context in contexts
                    where context.Name == name
                    select context;
            Assert.AreEqual(0, query.Count());
        }

        [TestMethod]
        public void Can_Create_Goal()
        {
            var name = "*Test Goal*";
            var level = Level.ShortTerm;
            var contributes = 0;
            var added = General.AddGoal(name, level, contributes);
            ExtAssert.Greater(added, 0);

            var goals = General.GetGoals();
            var query = from goal in goals
                        where goal.Id == added
                        select goal;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            Assert.AreEqual(added, result.Id);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(level, result.Level);
            Assert.AreEqual(contributes, result.Contributes);
        }

        [TestMethod]
        public void Can_Edit_Goal()
        {
            var name = "*Test Goal*";

            var goals = General.GetGoals();
            var query = from goal in goals
                        where goal.Name == name
                        select goal;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            result.Name = "*Edited Goal*";
            var success = General.EditGoal(result);
            Assert.IsTrue(success);
            Assert.IsTrue(result.Name != name);
        }

        [TestMethod]
        public void Can_Delete_Goal()
        {
            var name = "*Edited Goal*";

            var goals = General.GetGoals();
            var query = from goal in goals
                        where goal.Name == name
                        select goal;
            ExtAssert.Greater(query.Count(), 0);
            var result = query.First();

            var success = General.DeleteGoal(result.Id);
            Assert.IsTrue(success);

            goals = General.GetGoals();
            query = from goal in goals
                    where goal.Name == name
                    select goal;
            Assert.AreEqual(0, query.Count());
        }
    }
}
