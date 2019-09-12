using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Questions;

namespace Testinator.TestCreator
{
    public interface ITestCreatorService
    {
        void InitializeNewTest(ITestInfo testInfo);
        void SubmitQuestion(IQuestion question);
    }
}
