using System.Collections.Generic;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IQuestionEditorCollection : IReadOnlyList<IQuestion>
    {
        OperationResult Add(IQuestion question);
        OperationResult DeleteAt(int idx);
        OperationResult DeleteAll();
    }
}
