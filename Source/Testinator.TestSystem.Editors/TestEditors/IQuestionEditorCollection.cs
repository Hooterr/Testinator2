using System.Collections.Generic;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.TestSystem.Editors
{
    public interface IQuestionEditorCollection : IReadOnlyList<IQuestion>
    {
        OperationResult Add(IQuestion question);
        OperationResult DeleteAt(int idx);
        OperationResult DeleteAll();
    }
}
