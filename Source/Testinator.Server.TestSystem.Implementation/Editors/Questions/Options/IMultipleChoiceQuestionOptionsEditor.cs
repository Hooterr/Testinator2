using System.Collections.Generic;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionOptionsEditor : IOptionsEditor
    {
        [EditorProperty]
        List<string> Options { get; set; }

        int GetMaximumCount();
    }
}
