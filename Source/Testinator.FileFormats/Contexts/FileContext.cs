using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Files
{
    public class FileContext
    {
        public int Version { get; set; }

        public IReadOnlyDictionary<string, string> Metadata { get; set; }
    }
}
