using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Represents a Note that doesn't specifically belong in your to-do list.
    /// </summary>
    public class Note : Item
    {
        /// <summary>
        /// Gets or sets the date this note was added.
        /// </summary>
        /// <value>The added date.</value>
        public DateTime Added { get; set; }
        /// <summary>
        /// Gets or sets the date this note was last modified.
        /// </summary>
        /// <value>The modified date.</value>
        public DateTime Modified { get; set; }
        /// <summary>
        /// Gets or sets the folder where this note is stored.
        /// </summary>
        /// <value>The folder.</value>
        public Folder Folder { get; set; }
        /// <summary>
        /// Gets or sets the text of this note.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this note is private.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this note is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }
    }
}
