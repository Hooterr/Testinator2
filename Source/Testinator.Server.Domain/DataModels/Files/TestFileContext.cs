using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Domain
{
    public class TestFileContext
    {
        public string FilePath { get;  set; }
        public string TestName { get;  set; }
        public string Author { get; set; }
        public string[] Tags { get; set; }

        // more TBD
    }
}
