using System;
using System.Collections.Generic;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Abstractions.Tests;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation;
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

        /// <summary>
        /// The new test that is being created in test creator
        /// Or the old one that is being edited
        /// As editor, which after building can return finished Test object
        /// </summary>
        private ITestEditor mCurrentTestEditor;
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
        public void InitializeNewTest()
        {
            mCurrentTestEditor = AllEditors.TestEditor
                .New()
                .UseNewestVersion()
                .Build();
        }

        public void InitializeEditTest(Test test)
        {
            mCurrentTestEditor = AllEditors.TestEditor
                .Edit(test)
                .Build();
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestInfo"/>
        /// </summary>
        /// <param name="testInfo">The instance of <see cref="ITestInfo"/> that can be preloaded in the editor</param>
        /// <returns>The editor for <see cref="ITestInfo"/></returns>
        public ITestInfoEditor GetEditorTestInfo(ITestInfo testInfo = null)
        {
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            return mCurrentTestEditor.Info;
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestOptions"/>
        /// </summary>
        /// <param name="testOptions">The instance of <see cref="ITestOptions"/> that can be preloaded in the editor</param>
        /// <returns>The editor for <see cref="ITestOptions"/></returns>
        public ITestOptionsEditor GetEditorTestOptions(ITestOptions testOptions = null)
        {
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            return mCurrentTestEditor.Options;
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
                var question = mCurrentTestEditor.Questions[questionNumber.Value] as MultipleChoiceQuestion;

                // Get the editor for this type of question
                return AllEditors.MultipleChoiceQuestion
                    .SetInitialQuestion(question)
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
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            return mCurrentTestEditor.Grading;
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

        #endregion
    }
}
