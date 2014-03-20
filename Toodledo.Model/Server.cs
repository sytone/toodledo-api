using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toodledo.Model
{
    /// <summary>
    /// Represents some details about the Toodledo server and the current API session.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Gets or sets the current date and time on the server.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Gets or sets the number of minutes left before the current token expires.
        /// </summary>
        /// <value>The token life left.</value>
        public Single TokenExpires { get; set; }
    }
}
