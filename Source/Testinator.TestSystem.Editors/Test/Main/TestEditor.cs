using System.Linq;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Implementation;

namespace Testinator.TestSystem.Editors
{
    /// <summary>
    /// Default implementation of <see cref="ITestEditor"/>
    /// </summary>
    internal class TestEditor : BaseEditor<Implementation.Test, ITestEditor>, ITestEditor
    {
        #region Private Members

        private TestInfoEditor mInfo;
        private TestOptionsEditor mOptions;
        private GradingEditor mGrading;
        private QuestionEditorCollection mQuestions;

        #endregion

        #region Public Properties

        public ITestInfoEditor Info => mInfo;

        public ITestOptionsEditor Options => mOptions;

        public IGradingEditor Grading
        {
            get
            {
                // TODO, should to for now, but fix this later
                mGrading.mMaxPointScore = mQuestions.Sum(x => x.Scoring.MaximumScore);
                return mGrading;
            }
        }
        
        public IQuestionEditorCollection Questions => mQuestions;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the editor to edit an existing test
        /// </summary>
        /// <param name="test">The test to edit</param>
        /// <param name="version">The version of test system to use</param>
        public TestEditor(Implementation.Test test, int version) : base(test, version) { }

        /// <summary>
        /// Initializes the editor to create a new test
        /// </summary>
        /// <param name="version">The version of test system to use</param>
        public TestEditor(int version) : base(version) { }

        #endregion

        #region Overridden

        protected override void OnInitialize()
        {
            if (IsInEditorMode())
            {
                mInfo = new TestInfoEditor(OriginalObject.mInfo, Version);
                mOptions = new TestOptionsEditor(OriginalObject.mTestOptions, Version);
                mQuestions = new QuestionEditorCollection(OriginalObject.Questions.Select(x => x.GetQuestion()).ToList());
                mGrading = new GradingEditor(OriginalObject.mGrading, Version);
            }
            else
            {
                mInfo = new TestInfoEditor(Version);
                mOptions = new TestOptionsEditor(Version);
                mQuestions = new QuestionEditorCollection();
                mGrading = new GradingEditor(Version);
            }
        }

        protected override Implementation.Test BuildObject()
        {
            var infoBuildOperation = mInfo.Build();
            var optionsBuildOperation = mOptions.Build();
            var gradingBuildOperation = mGrading.Build();

            if (Helpers.AnyTrue(infoBuildOperation.Failed, optionsBuildOperation.Failed, gradingBuildOperation.Failed))
            {
                return null;
            }

            Implementation.Test test = null;
            if (IsInEditorMode())
                test = OriginalObject;
            else
                test = new Implementation.Test();

            test.mInfo = infoBuildOperation.Result;
            test.mTestOptions = optionsBuildOperation.Result;
            test.mGrading = gradingBuildOperation.Result;

            // TODO validate questions list length 
            test.mQuestionList = mQuestions.Select(x => new InMemoryQuestionProvider(x)).Cast<IQuestionProvider>().ToList();

            return test;
        }

        #endregion

        #region Public Methods

        public new OperationResult<ITest> Build()
        {
            var result = BuildObject();
            if (result == null)
            {
                return OperationResult<ITest>.Fail();
            }

            return OperationResult<ITest>.Success(result);
        } 

        #endregion
    }
}
