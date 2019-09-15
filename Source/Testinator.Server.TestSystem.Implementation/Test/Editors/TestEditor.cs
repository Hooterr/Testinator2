using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.Server.TestSystem.Implementation
{
    internal class TestEditor : BaseEditor<Test, ITestEditor>, ITestEditor
    {
        private TestInfoEditor mInfo;
        private TestOptionsEditor mOptions;
        private GradingEditor mGrading;
        private QuestionEditorCollection mQuestions;

        #region Public Properties

        public ITestInfoEditor Info => mInfo;

        public ITestOptionsEditor Options => mOptions;

        public IGradingEditor Grading
        {
            get
            {
                mGrading.mMaxPointScore = mQuestions.Sum(x => x.Scoring.MaximumScore);
                return mGrading;
            }
        }
        
        public IQuestionEditorCollection Questions => mQuestions;

        #endregion

        #region Constructors

        public TestEditor(Test test, int version) : base(test, version) { }

        public TestEditor(int version) : base(version) { }

        #endregion

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

        protected override Test BuildObject()
        {
            var infoBuildOperation = mInfo.Build();
            var optionsBuildOperation = mOptions.Build();
            var gradingBuildOperation = mGrading.Build();

            if(Helpers.AnyTrue(infoBuildOperation.Failed, optionsBuildOperation.Failed, gradingBuildOperation.Failed))
            {
                return null;
            }

            Test test = null;
            if (IsInEditorMode())
                test = OriginalObject;
            else
                test = new Test();

            test.mInfo = infoBuildOperation.Result;
            test.mTestOptions = optionsBuildOperation.Result;
            test.mGrading = gradingBuildOperation.Result;

            // TODO validate questions' list length 
            test.mQuestionList = mQuestions.Select(x => new InMemoryQuestionProvider(x)).Cast<IQuestionProvider>().ToList();

            return test;
        }

        OperationResult<ITest> IBuildable<ITest>.Build()
        {
            var result = BuildObject();
            if(result == null)
            {
                return OperationResult<ITest>.Fail();
            }

            return OperationResult<ITest>.Success(result);
        }
    }
}
