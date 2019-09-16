using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.TestSystem.Editors
{
    public interface ITestEditor : IBuildable<ITest>
    {
        ITestInfoEditor Info { get; }

        ITestOptionsEditor Options { get; }

        IGradingEditor Grading { get; }

        IQuestionEditorCollection Questions { get; }
    }
}
