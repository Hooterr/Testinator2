using Testinator.TestSystem.Abstractions.Questions.Task;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IQuestionEditor
    {
        int GetVersion();
        ITaskEditor Task { get; }
        
    }
}
