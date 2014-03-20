using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Represents a Task
    /// </summary>
    public class Task : Item
    {
        /// <summary>
        /// Gets or sets the parent of this task.
        /// </summary>
        /// <value>The parent.</value>
        public Task Parent { get; set; }
        /// <summary>
        /// Gets or sets the subtasks.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<Task> Children { get; set; }
        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }
        /// <summary>
        /// Gets or sets the folder.
        /// </summary>
        /// <value>The folder.</value>
        public Folder Folder { get; set; }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public Context Context { get; set; }
        /// <summary>
        /// Gets or sets the goal toward which this task contributes.
        /// </summary>
        /// <value>The goal.</value>
        public Goal Goal { get; set; }
        /// <summary>
        /// Gets or sets the date and time this task was added.
        /// </summary>
        /// <value>The added.</value>
        public DateTime Added { get; set; }
        /// <summary>
        /// Gets or sets the date and time this task was last modified.
        /// </summary>
        /// <value>The modified.</value>
        public DateTime Modified { get; set; }
        /// <summary>
        /// Gets or sets the date and time this task is scheduled to start.
        /// </summary>
        /// <value>The start.</value>
        public DateTime Start { get; set; }
        /// <summary>
        /// Gets or sets the date and time this task is due.
        /// </summary>
        /// <value>The due.</value>
        public DateTime Due { get; set; }
        /// <summary>
        /// Gets or sets an optional modified for the due date.
        /// </summary>
        /// <value>"=" to specify the task is due <b>on</b> the due date. 
        /// ">" to specify the task is due <b>after</b> the due date.
        /// "?" to specify the task is due <b>on</b> the due date, but is <i>optional</i>, so when the due date passes, the task will be removed.
        /// </value>
        public string DueModifier { get; set; }
        /// <summary>
        /// Gets or sets the number of minutes prior to the due date/time that a reminder will be sent.
        /// </summary>
        /// <value>The reminder.</value>
        public int Reminder { get; set; }
        /// <summary>
        /// Gets or sets the date and time this task was completed.
        /// </summary>
        /// <value>The completed.</value>
        public DateTime Completed { get; set; }
        /// <summary>
        /// Gets or sets the frequency this task should be repeated.
        /// </summary>
        /// <value>The repeat.</value>
        public Frequency Repeat { get; set; }
        /// <summary>
        /// Gets or sets a special repeating pattern.
        /// </summary>
        /// <example>Every Monday</example>
        /// <remarks> Toodledo recognizes 3 different formats for advanced repeating
        /// <ul>
        /// <li>Format 1: "Every X T". Where X is a number and T is a unit of time (day/week/month/year). Examples: Every 3 days, Every 1 month, Every 2 years, Every 16 weeks.</li>
        /// <li>Format 2: "On the X D of each month". Where X is a number or numerical word (first, second, last, etc) and D is a day of the week (Mon, Tue, Wed, etc). Examples: On the second monday of each month, On the last Fri of each month, On the 3 Sat of each month.</li>
        /// <li>Format 3: "Every W". Where W is a day of the week (Mon, Tue, Wed, etc) or the special words "weekend" or "weekday". Examples: Every Mon, Every Tuesday and Thursday, Every Weekend, Every Weekday, Every Mon, Wed and Fri.</li>
        /// </ul>
        /// Additionally, you have the option of selecting whether you want the task to repeat from the due date or from the completion date.
        /// <list type="unordered">
        /// <item><code>From Due-date</code>: The task will be moved forward from the due-date, even if this due-date is overdue or in the future. For example, suppose you have a task that repeats weekly and is overdue by 6 days. When you complete this task, it will be moved forward from the due-date and the new due date willl be tomorrow.</item>
        /// </list>
        /// </remarks>
        /// <value>The advanced repeat.</value>
        public string AdvancedRepeat { get; set; }
        public Status Status { get; set; }
        public int Length { get; set; }
        public Priority Priority { get; set; }
        public bool HasStar { get; set; }
        public string Note { get; set; }
    }
}
