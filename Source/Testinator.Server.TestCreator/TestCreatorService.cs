using System.Collections.Generic;
using System.Linq;
using Testinator.Server.TestSystem.Implementation;
using Testinator.Server.TestSystem.Implementation.Questions;
using Testinator.TestCreator;
using Testinator.TestSystem.Abstractions;

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
        /// </summary>
        internal ITest CurrentTest { get; set; }

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
        /// Initializes new test in test creator with basic data provided
        /// </summary>
        /// <param name="testInfo">All the initial informations about the test, in this specific implementation - <see cref="TestInfo"/></param>
        public void InitializeNewTest(ITestInfo testInfo)
        {
            // Cast provided interface to Testinator.Server implementation of it
            var testData = testInfo as TestInfo;

            // TODO: Create test once implementation is done
        }

        /// <summary>
        /// Gets the editor for <see cref="MultipleChoiceQuestion"/> prepared to create new question
        /// </summary>
        /// <returns>The editor of type MultipleChoice</returns>
        public QuestionEditorMultipleChoice GetEditorMultipleChoice()
        {
            // Get the editor for this type of question
            return Editors.MultipleChoiceQuestion
                // No pre-data provided, so create new question
                .NewQuestion()
                // Use latest version since its new question
                .UseNewestVersion()
                // Return built editor
                .Build();
        }

        /// <summary>
        /// Gets the editor for <see cref="MultipleChoiceQuestion"/> that has pre-loaded data for existing question
        /// </summary>
        /// <param name="questionNumber">The index of question to preload from test</param>
        /// <returns>The editor of type MultipleChoice</returns>
        public QuestionEditorMultipleChoice GetEditorMultipleChoice(int questionNumber)
        {
            // Get question from the test
            var question = GetQuestionFromTestAt(questionNumber) as MultipleChoiceQuestion;

            // Get the editor for this type of question
            return Editors.MultipleChoiceQuestion
                // Preload given question
                .SetInitialQuestion(question)
                // TODO: Check question's version to use, for now just UseNewestVersion
                .UseNewestVersion()
                // Return built editor
                .Build();
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

        /// <summary>
        /// Submits provided question to the current test
        /// </summary>
        /// <param name="question">The question object to attach to the test</param>
        public void SubmitQuestion(IQuestion question)
        {
            // TODO: Maybe make .Validate() method on question for super easy sanity check there
            // TODO: Add question to the test once implementation is done
            // TODO: Add the way of checking if question was already in the test and then just replace instead of adding
            // TODO: Add question to the user's Pool
        }

        public void SubmitGrading(IGrading grading)
        {
            // TODO: Implement this and TestOptions once test editor is done   
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets question from the current test at specified index
        /// </summary>
        /// <param name="index">The index of a question to take from test</param>
        /// <returns>Question object as <see cref="IQuestion"/></returns>
        private IQuestion GetQuestionFromTestAt(int index) => CurrentTest.Questions.Questions.ElementAt(index).GetQuestion();

        #endregion
    }
}
