using System;

namespace Testinator.TestSystem.Editors.Test.Builder
{
    /// <summary>
    /// Default implementation of <see cref="ITestEditorBuilder"/>
    /// </summary>
    internal class TestEditorBuilder : ITestEditorBuilder
    {
        #region Private Members

        /// <summary>
        /// The version of test system model to use
        /// </summary>
        private int mVersion;

        /// <summary>
        /// Initial object to edit
        /// If null, create a new one
        /// </summary>
        private Implementation.Test mInitialTest;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestEditorBuilder()
        {
            NewTest();
        }

        #endregion

        #region Public Methods

        public ITestEditor Build()
        {
            TestEditor editor;
            if (mInitialTest != null)
                editor = new TestEditor(mInitialTest, mVersion);
            else
                editor = new TestEditor(mVersion);

            editor.Initialize();
            return editor;
        }

        public ITestEditorBuilder SetInitialTest(Implementation.Test test)
        {
            mInitialTest = test;
            mVersion = test.Version;
            return this;
        }

        public ITestEditorBuilder NewTest()
        {
            mInitialTest = null;
            mVersion = Versions.Highest;
            return this;
        }

        public ITestEditorBuilder SetVersion(int version)
        {
            // There already is a test to edit and the caller wants to change the version
            if (mInitialTest != null && mInitialTest.Version != version)
            {
                throw new NotSupportedException("Changing test version is not supported yet.");
                // Tho it may be one day
            }

            if (Versions.NotInRange(version))
            {
                throw new ArgumentException($"Version must be between highest ({Versions.Highest}) and lowest ({Versions.Lowest}).");
            }
            return this;
        }

        public ITestEditorBuilder UseNewestVersion()
        {
            return SetVersion(Versions.Highest);
        } 

        #endregion
    }
}
