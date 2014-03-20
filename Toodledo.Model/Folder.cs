using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Folders are a convenient way to organize your to-do list.
    /// </summary>
    public class Folder : Item
    {
        /// <summary>
        /// Gets or sets a value indicating whether this folder is private.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this folder is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this folder has been archived.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this folder is archived; otherwise, <c>false</c>.
        /// </value>
        public bool IsArchived { get; set; }
        /// <summary>
        /// Gets or sets the order for sorting this folder in a folder list.
        /// </summary>
        /// <value>The sort order.</value>
        public int Order { get; set; }
    }
}
