using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Represents the account information for the currently authenticated Toodledo user.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the unique Toodledo user id.
        /// </summary>
        /// <value>The user id.</value>
        public string UserId { get; set; }
        /// <summary>
        /// Gets or sets a user-friendly alias for the authenticated Toodledo user.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this user is a Pro member.
        /// </summary>
        /// <remarks>Only Pro accounts will be allowed to use subtasks.</remarks>
        /// <value><c>true</c> if this account is pro; otherwise, <c>false</c>.</value>
        public bool IsPro { get; set; }
        /// <summary>
        /// Gets or sets the user's preferred date format.
        /// </summary>
        /// <value>The date format.</value>
        public DateFormat DateFormat { get; set; }
        /// <summary>
        /// Gets or sets the number of half hours that the user's timezone is offset from the server's timezone.
        /// </summary>
        /// <example>A value of -4 means that the user's timezone is 2 hours earlier than the server's timezone.</example>
        /// <value>The time zone.</value>
        public int TimeZone { get; set; }
        /// <summary>
        /// Gets or sets the number of months into the future, tasks should be hidden.
        /// </summary>
        /// <value>The hide months.</value>
        public int HideMonths { get; set; }
        /// <summary>
        /// Gets or sets the priority value above which tasks should appear on the Hot List.
        /// </summary>
        /// <value>The hot list priority.</value>
        public Priority HotListPriority { get; set; }
        /// <summary>
        /// Gets or sets the due date lead-time by which tasks should appear on the Hot List.
        /// </summary>
        /// <value>The hot list due date.</value>
        public int HotListDueDate { get; set; }
        /// <summary>
        /// Gets or sets a timestamp that indicates the last time any task was added or edited on this account.
        /// </summary>
        /// <value>The last add edit.</value>
        public DateTime LastAddEdit { get; set; }
        /// <summary>
        /// Gets or sets a timestamp that indicates the last time any task was deleted from this account.
        /// </summary>
        /// <value>The last delete.</value>
        public DateTime LastDelete { get; set; }
        /// <summary>
        /// Gets or sets a timestamp that indicates the last time a folder was added, edited, or deleted..
        /// </summary>
        /// <value>The last folder edit.</value>
        public DateTime LastFolderEdit { get; set; }
        /// <summary>
        /// Gets or sets a timestamp that indicates the last time a context was added, edited, or deleted..
        /// </summary>
        /// <value>The last context edit.</value>
        public DateTime LastContextEdit { get; set; }
        /// <summary>
        /// Gets or sets a timestamp that indicates the last time a goal was added, edited, or deleted..
        /// </summary>
        /// <value>The last goal edit.</value>
        public DateTime LastGoalEdit { get; set; }
    }
}
