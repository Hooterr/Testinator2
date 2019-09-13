using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.TestSystem.Abstractions
{
    public interface IQuestionProvider
    {
        IQuestion GetQuestion();
    }
}
