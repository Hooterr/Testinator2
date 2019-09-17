using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestCreator
{
    using QuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The main test creator service that handles every interactions with data needed in test creator for Testinator.Server app
    /// </summary>
    public class TestCreatorService : ITestCreatorService
    {
        #region Private Members

        /// <summary>
        /// The pool manager that handles current user's pool of questions that they can use for the test
        /// </summary>
        private IPoolManager mPoolManager;

        #endregion

        #region Internal Properties

        /// <summary>
        /// The new test that is being created in test creator
        /// Or the old one that is being edited
        /// As editor, which after building can return finished Test object
        /// </summary>
        internal ITestEditor CurrentTestEditor { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorService(IPoolManager poolManager)
        {
            // Inject DI services
            mPoolManager = poolManager;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Initializes new test in test creator
        /// </summary>
        /// <param name="test">Test instance which will be edited if provided, otherwise brand-new test will be created</param>
        public void InitializeNewTest(ITest test = null)
        {
            // If we have a test provided...
            if (test != null)
            {
                // Get the editor for test
                CurrentTestEditor = AllEditors.TestEditor;
                // TODO: Preload given test
                /*.SetInitialTest(test)
                // TODO: Check test's version to use, for now just UseNewestVersion
                .UseNewestVersion()
                // Return built editor
                .Build();*/
            }
            // Otherwise...
            else
            {
                // Get the editor for test
                CurrentTestEditor = AllEditors.TestEditor;
                // No pre-data provided, so create new test
                /*.NewTest()
                // Use latest version since its new test
                .UseNewestVersion()
                // Return built editor
                .Build();*/
            }
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestInfo"/>
        /// </summary>
        /// <param name="testInfo">The instance of <see cref="ITestInfo"/> that can be preloaded in the editor</param>
        /// <returns>The editor for <see cref="ITestInfo"/></returns>
        public ITestInfoEditor GetEditorTestInfo(ITestInfo testInfo = null)
        {
            // If we have the info provided...
            if (testInfo != null)
            {
                // Get the editor for test info
                return AllEditors.InfoEditor;
                // TODO: Preload given info
                /*.SetInitialInfo(testInfo)
                // TODO: Check info's version to use, for now just UseNewestVersion
                .UseNewestVersion()
                // Return built editor
                .Build();*/
            }

            // Otherwise...
            // Get the editor for test info
            return AllEditors.InfoEditor;
            // No pre-data provided, so create new info
            /*.NewInfo()
            // Use latest version since its new info
            .UseNewestVersion()
            // Return built editor
            .Build();*/
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestOptions"/>
        /// </summary>
        /// <param name="testOptions">The instance of <see cref="ITestOptions"/> that can be preloaded in the editor</param>
        /// <returns>The editor for <see cref="ITestOptions"/></returns>
        public ITestOptionsEditor GetEditorTestOptions(ITestOptions testOptions = null)
        {
            // If we have the options provided...
            if (testOptions != null)
            {
                // Get the editor for test options
                return AllEditors.OptionsEditor;
                // TODO: Preload given options
                /*.SetInitialOptions(testOptions)
                // TODO: Check options's version to use, for now just UseNewestVersion
                .UseNewestVersion()
                // Return built editor
                .Build();*/
            }

            // Otherwise...
            // Get the editor for test options
            return AllEditors.OptionsEditor;
            // No pre-data provided, so create new options
            /*.NewOptions()
            // Use latest version since its new options
            .UseNewestVersion()
            // Return built editor
            .Build();*/
        }

        /// <summary>
        /// Gets the editor for <see cref="MultipleChoiceQuestion"/>
        /// </summary>
        /// <param name="questionNumber">The index of question to preload from test, if not provided, brand-new question will be created</param>
        /// <returns>The editor of type MultipleChoice</returns>
        public QuestionEditorMultipleChoice GetEditorMultipleChoice(int? questionNumber = null)
        {
            // If we have a question provided...
            if (questionNumber.HasValue)
            {
                // Get question from the test
                var question = CurrentTestEditor.GetQuestionFromTestAt(questionNumber.Value) as MultipleChoiceQuestion;

                // Get the editor for this type of question
                return AllEditors.MultipleChoiceQuestion
                    // Preload given question
                    .SetInitialQuestion(question)
                    // TODO: Check question's version to use, for now just UseNewestVersion
                    .UseNewestVersion()
                    // Return built editor
                    .Build();
            }

            // Otherwise...
            // Get the editor for this type of question
            return AllEditors.MultipleChoiceQuestion
                // No pre-data provided, so create new question
                .NewQuestion()
                // Use latest version since its new question
                .UseNewestVersion()
                // Return built editor
                .Build();
        }

        /// <summary>
        /// Gets the editor for <see cref="IGrading"/>
        /// </summary>
        /// <param name="grading">The instance of <see cref="IGrading"/> that can be preloaded in the editor</param>
        /// <returns>The editor for <see cref="IGrading"/></returns>
        public IGradingEditor GetEditorGrading(IGrading grading = null)
        {
            // If we have the grading provided...
            if (grading != null)
            {
                // Get the editor for test options
                return AllEditors.GradingEditor;
                // TODO: Preload given grading
                /*.SetInitialGrading(grading)
                // TODO: Check grading's version to use, for now just UseNewestVersion
                .UseNewestVersion()
                // Return built editor
                .Build();*/
            }

            // Otherwise...
            // Get the editor for grading
            return AllEditors.GradingEditor;
            // No pre-data provided, so create new grading
            /*.NewGrading()
            // Use latest version since its new grading
            .UseNewestVersion()
            // Return built editor
            .Build();*/
        }

        /// <summary>
        /// Checks the pool of current user and returns all the questions inside that they can include in the test
        /// </summary>
        /// <returns>Collection of questions</returns>
        public ICollection<IQuestion> GetPossibleQuestionsFromPool()
        {
            // Get all the questions from the pool
            var poolQuestions = mPoolManager.GetQuestionPoolForCurrentUser();

            // And return it as is
            return poolQuestions;
        }

        public void SubmitTestInfo(ITestInfo testInfo)
        {
            CurrentTestEditor.SubmitInfo(testInfo);
        }

        /// <summary>
        /// Submits provided question to the current test
        /// </summary>
        /// <param name="question">The question object to attach to the test</param>
        public void SubmitQuestion(IQuestion question)
        {
            // TODO: Maybe make .Validate() method on question for super easy sanity check there
            // TODO: Add the way of checking if question was already in the test and then just replace instead of adding
            // TODO: Add question to the user's Pool

            // Add question to the test
            CurrentTestEditor.SubmitQuestion(question);
        }

        public void SubmitGrading(IGrading grading)
        {
            CurrentTestEditor.SubmitGrading(grading);
        }

        public void SubmitTestOptions(ITestOptions testOptions)
        {
            CurrentTestEditor.SubmitOptions(testOptions);
        }

        public ITest SubmitTest()
        {
            return CurrentTestEditor.Build().Result;
        }

        #endregion
    }
}
