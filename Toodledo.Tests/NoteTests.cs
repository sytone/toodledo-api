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
    public class NoteTests : TestsBase
	{
        [TestMethod]
        public void Can_Get_Notes_Before()
        {
            var notes = Notebook.GetNotes(DateTime.Now, null, null);
            ExtAssert.Greater(notes.Count(), 0);
        }

        [TestMethod]
        public void Can_Get_Note_By_Id()
        {
            var notes = Notebook.GetNotes(null, null, 2453271);
            ExtAssert.Greater(notes.Count(), 0);
        }

        [TestMethod]
        public void Can_Create_Note()
        {
            var note = new Note() {IsPrivate = true, Name = "*Test Note*", Text = "Testing Note"};

            var added = Notebook.AddNote(note);
            ExtAssert.Greater(added, 0);

            var notes = Notebook.GetNotes(null, null, null).Where(n => n.Name == "*Test Note*");

            ExtAssert.Greater(notes.Count(), 0);
            var result = notes.First();

            Assert.AreEqual(added, result.Id);
            Assert.AreEqual("*Test Note*", result.Name);
        }

        [TestMethod]
        public void Can_Edit_Note()
        {
            var notes = Notebook.GetNotes(null, null, null).Where(n => n.Name == "*Test Note*");

            ExtAssert.Greater(notes.Count(), 0);
            var result = notes.First();
            var id = result.Id;

            result.Name = "*Edited Note*";
            var success = Notebook.EditNote(result);
            Assert.IsTrue(success);

            notes = Notebook.GetNotes(null, null, id);
            result = notes.First();

            Assert.IsTrue(result.Name == "*Edited Note*");
        }

        [TestMethod]
        public void Can_Delete_Note()
        {
            var notes = Notebook.GetNotes(null, null, null).Where(n => n.Name == "*Edited Note*");

            ExtAssert.Greater(notes.Count(), 0);
            var result = notes.First();

            var success = Notebook.DeleteNote(result.Id);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void Can_Get_Deleted_Notes()
        {
            var items = Notebook.GetDeletedNotes(DateTime.Parse("10/1/2009"));
            ExtAssert.Greater(items.Count(), 0);
        }

    }
}
