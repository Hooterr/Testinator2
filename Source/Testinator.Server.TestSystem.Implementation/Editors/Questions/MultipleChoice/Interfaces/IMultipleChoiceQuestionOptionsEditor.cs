using System.Collections.Generic;
using Testinator.Server.TestSystem.Implementation.Attributes;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IMultipleChoiceQuestionOptionsEditor : IQeustionOptionsEditor
    {
        [EditorProperty]
        List<string> Options { get; set; }

        void SetOptions(params string[] options);

        int GetMaximumCount();
    }
}
