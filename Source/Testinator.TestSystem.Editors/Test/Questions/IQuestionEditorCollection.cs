using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Editors
{
    // This may be a bad idea
    public interface IQuestionEditorCollection : IReadOnlyList<IQuestion>
    {
        OperationResult Add(IQuestion question);
        OperationResult DeleteAt(int idx);
        OperationResult DeleteAll();
    }
}
