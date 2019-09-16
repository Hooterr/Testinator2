using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Editors
{
    public interface ITestCreatorService
    {
        void InitializeNewTest(ITestInfo testInfo);
        void SubmitQuestion(IQuestion question);
    }
}
