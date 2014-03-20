using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Toodledo.Model;
using Toodledo.Model.API;

namespace Toodledo.Client
{
	public partial class Session : IGeneral
	{
	    #region Implementation of IGeneral

	    IEnumerable<Folder> IGeneral.GetFolders()
	    {
	        var factory = new ItemFactory<Folder>("folder")
	            .Attribute<int>("Id", "id")
	            .Attribute<bool>("IsPrivate", "private")
	            .Attribute<bool>("IsArchived", "archived")
	            .Attribute<int>("Order", "order")
	            .Value<string>("Name");

	        var results = new List<Folder>();

	        var response = SendRequest("getFolders");
            foreach (var thisElement in response.Descendants(XName.Get("folder", "")))
                results.Add(factory.Create(thisElement));

	        return results;
	    }

	    IEnumerable<Context> IGeneral.GetContexts()
	    {
            var factory = new ItemFactory<Context>("context")
                .Attribute<int>("Id", "id")
                .Value<string>("Name");

            var results = new List<Context>();

            var response = SendRequest("getContexts");
            foreach (var thisElement in response.Descendants(XName.Get("context", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    IEnumerable<Goal> IGeneral.GetGoals()
	    {
            var factory = new ItemFactory<Goal>("goal")
                .Attribute<int>("Id", "id")
                .Attribute<Level>("Level", "level")
                .Attribute<int>("Contributes", "contributes")
                .Attribute<bool>("IsArchived", "archived")
                .Value<string>("Name");

            var results = new List<Goal>();

            var response = SendRequest("getGoals");
            foreach (var thisElement in response.Descendants(XName.Get("goal", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    IEnumerable<Goal> IGeneral.GetGoalNotes()
	    {
            var factory = new ItemFactory<Goal>("goal")
                .Attribute<int>("Id", "id")
                .Value<string>("Notes");

            var results = new List<Goal>();

            var response = SendRequest("getGoalNotes");
            foreach (var thisElement in response.Descendants(XName.Get("goal", "")))
                results.Add(factory.Create(thisElement));

            return results;
        }

	    int IGeneral.AddFolder(string title, bool isPrivate)
	    {
	        var parameters = String.Format("title={0};private={1}", title, (isPrivate) ? 1 : 0);
            var response = SendRequest("addFolder", parameters);
	        return ReadElement<int>(response);
        }

	    int IGeneral.AddContext(string title)
	    {
            var parameters = String.Format("title={0}", title);
            var response = SendRequest("addContext", parameters);
            return ReadElement<int>(response);
        }

	    int IGeneral.AddGoal(string title, Level level, int contributes)
	    {
            var parameters = String.Format("title={0};level={1};contributes={2}", title, (int)level, contributes);
            var response = SendRequest("addGoal", parameters);
            return ReadElement<int>(response);
        }

	    bool IGeneral.EditFolder(Folder folder)
	    {
	        var parameters = String.Format("id={0};title={1};private={2};archived={3}"
	                                       , folder.Id
	                                       , folder.Name
	                                       , (folder.IsPrivate) ? 1 : 0
	                                       , (folder.IsArchived) ? 1 : 0);
            var response = SendRequest("editFolder", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    bool IGeneral.EditContext(Context context)
	    {
            var parameters = String.Format("id={0};title={1}"
                                           , context.Id
                                           , context.Name);
            var response = SendRequest("editContext", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    bool IGeneral.EditGoal(Goal goal)
	    {
            var parameters = String.Format("id={0};title={1};level={2};contributes={3};archived={4}"
                                           , goal.Id
                                           , goal.Name
                                           , (int)goal.Level
                                           , goal.Contributes
                                           , (goal.IsArchived) ? 1 : 0);
            var response = SendRequest("editGoal", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    bool IGeneral.DeleteFolder(int id)
	    {
            var parameters = String.Format("id={0}", id);
            var response = SendRequest("deleteFolder", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    bool IGeneral.DeleteContext(int id)
	    {
            var parameters = String.Format("id={0}", id);
            var response = SendRequest("deleteContext", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    bool IGeneral.DeleteGoal(int id)
	    {
            var parameters = String.Format("id={0}", id);
            var response = SendRequest("deleteGoal", parameters);
            return (ReadElement<int>(response) == 1);
        }

	    #endregion
	}
}
