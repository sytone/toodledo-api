using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model.API
{
    public interface IGeneral
    {
        IEnumerable<Folder> GetFolders();
        IEnumerable<Context> GetContexts();
        IEnumerable<Goal> GetGoals();
        IEnumerable<Goal> GetGoalNotes();

        int AddFolder(string title, bool isPrivate);
        int AddContext(string title);
        int AddGoal(string title, Level level, int contributes);

        bool EditFolder(Folder folder);
        bool EditContext(Context context);
        bool EditGoal(Goal goal);

        bool DeleteFolder(int id);
        bool DeleteContext(int id);
        bool DeleteGoal(int id);
    }
}
