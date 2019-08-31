using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IQuestionScoringEditor
    {
        [EditorProperty]
        int MaximumScore { get; set; }
    }
}
