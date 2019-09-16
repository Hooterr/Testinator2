using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    internal class TestOptionsEditor : BaseEditor<TestOptions, ITestOptionsEditor>, ITestOptionsEditor
    {
        public TestOptionsEditor(TestOptions options, int version) : base(options, version) { }

        public TestOptionsEditor(int version) : base(version) { }

        protected override TestOptions BuildObject()
        {
            TestOptions result = null;
            if (IsInEditorMode())
                result = OriginalObject;
            else
                result = new TestOptions();

            return result;
        }
    }
}
