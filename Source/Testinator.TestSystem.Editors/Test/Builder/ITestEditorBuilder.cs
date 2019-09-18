using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors.Test.Builder
{
    public interface ITestEditorBuilder
    {
        ITestEditor Build();

        ITestEditorBuilder SetInitialTest(Implementation.Test test);

        ITestEditorBuilder NewTest();

        ITestEditorBuilder SetVersion(int version);

        ITestEditorBuilder UseNewestVersion();
    }
}
