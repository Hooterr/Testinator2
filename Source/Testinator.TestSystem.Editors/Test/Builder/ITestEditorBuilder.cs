using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors.Test.Builder
{
    public interface ITestEditorBuilder
    {
        ITestEditor Build();

        ITestEditorBuilder Edit(Implementation.Test test);

        ITestEditorBuilder New();

        ITestEditorBuilder SetVersion(int version);

        ITestEditorBuilder UseNewestVersion();
    }
}
