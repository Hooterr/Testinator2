using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IQuestionEditor
    {
        ITaskEditor Task { get; }
        
    }
}
