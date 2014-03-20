using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Setting personal goals is a powerful way to direct your energy and determine what you want to achieve in life. Unlike tasks, which have a clear path of action, goals are usually more nebulous and difficult to quantify. Once you define your goals, you can assign them to individual tasks and keep track of your progress. This is a helpful way to see which goals you are progressing on and which goals need more attention. Pro accounts will also keep track of the number of tasks completed for each goal, and display a chain of the last 30 days.
    /// </summary>
    public class Goal : Item
    {
        /// <summary>
        /// Gets or sets the level of this goal.
        /// </summary>
        /// <value>The level.</value>
        public Level Level { get; set; }
        /// <summary>
        /// Gets or sets the Id of the goal toward which this goal contributes.
        /// </summary>
        /// <value>The parent goal id.</value>
        public int Contributes { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this goal has been archived.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this goal is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }
        /// <summary>
        /// Gets or sets a free-form note about this goal.
        /// </summary>
        /// <value>The note.</value>
        public string Notes { get; set; }

        /// <summary>
        /// The Toodledo API returns Goal Notes in a separate instance, so this method will combine the notes into an existing Goal object.
        /// </summary>
        /// <param name="withoutNotes">The Goal without notes.</param>
        /// <param name="withNotes">The Goal with notes.</param>
        /// <returns>A copy of the Goal with all the properties of the "withoutNotes" instance <i>plus</i> the Notes property of the "withNotes" instance.</returns>
        public static Goal MergeNotes(Goal withoutNotes, Goal withNotes)
        {
            withoutNotes.Notes = withNotes.Notes;
            return withoutNotes;
        }

        /// <summary>
        /// The Toodledo API returns Goal Notes in a separate instance, so this method will combine the notes into an existing Goal object.
        /// </summary>
        /// <param name="withoutNotes">A collection of Goals without notes.</param>
        /// <param name="withNotes">A collection of Goals with notes.</param>
        /// <returns>A collection of Goals with the Notes properties populated.</returns>
        public static IEnumerable<Goal> MergeNotes(IEnumerable<Goal> withoutNotes, IEnumerable<Goal> withNotes)
        {
            var query = from goal in withoutNotes
                        join note in withNotes on goal.Id equals note.Id
                        select new {goal, note};

            var goals = new List<Goal>();

            foreach(var result in query)
            {
                result.goal.Notes = result.note.Notes;
                goals.Add(result.goal);
            }

            return goals;
        }

    }
}
