using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// Contains information about a file
    /// </summary>
    public class FileContext
    {
        /// <summary>
        /// The software version
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// File's metadata
        /// </summary>
        public IReadOnlyDictionary<string, string> Metadata { get; set; }
    }
}
