using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.Server.Domain
{
    public class GradingPresetFileContext
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public DateTime CreatedData { get; set; }
        public int NumberOfGrades { get; set; }
    }
}
