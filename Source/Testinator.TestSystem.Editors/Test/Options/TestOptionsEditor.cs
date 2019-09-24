using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="ITestOptionsEditor"/>
    /// </summary>
    internal class TestOptionsEditor : BaseEditor<TestOptions, ITestOptionsEditor>, ITestOptionsEditor
    {
        #region All Constructors

        /// <summary>
        /// Initializes this editor to create a new test options pack
        /// </summary>
        /// <param name="version">The version of the test system model to use</param>
        public TestOptionsEditor(int version) : base(version) { }

        /// <summary>
        /// Initializes this editor to edit an existing options pack
        /// </summary>
        /// <param name="options">The options to edit</param>
        /// <param name="version">The version of the test system model to use</param>
        public TestOptionsEditor(TestOptions options, int version) : base(options, version) { }

        #endregion

        #region Implementation

        protected override TestOptions BuildObject()
        {
            TestOptions result = null;
            if (IsInEditorMode())
                result = OriginalObject;
            else
                result = new TestOptions();

            return result;
        }

        protected override bool Validate()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
