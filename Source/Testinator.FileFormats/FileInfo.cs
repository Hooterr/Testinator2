using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.FileFormats
{
    public class FileInfo
    {
        public int Version { get; internal set; }

        public string AbsolutePath { get; internal set; }

        public string Name { get; internal set; }

        public IReadOnlyDictionary<string, string> Metadata { get; internal set; }
    }
}
