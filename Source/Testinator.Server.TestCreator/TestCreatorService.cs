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
        /*public TestCreatorService(IPoolManager poolManager)
        {
            // Inject DI services
            mPoolManager = poolManager;
        }*/

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Initializes new test in test creator
        /// </summary>
        /// <param name="test">The test that can be provided to pre-load in the editor</param>
        public void InitializeNewTest(ITest test = null)
        {
            // If we dont have test provided...
            if (test == null)
            {
                // Setup the test editor
                mCurrentTestEditor = AllEditors.TestEditor
                    // Create brand-new test
                    .NewTest()
                    // Use newest version since its new test
                    .UseNewestVersion()
                    // Build the editor
                    .Build();
            }
            // Otherwise...
            else
            {
                // Setup the test editor
                mCurrentTestEditor = AllEditors.TestEditor
                    // Pre-load given test
                    .SetInitialTest(test as Test)
                    // Build the editor
                    .Build();
            }
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestInfo"/>
        /// </summary>
        public ITestInfoEditor GetEditorTestInfo()
        {
            // Make sure we have ready editor
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            // Return specific editor for test info
            return mCurrentTestEditor.Info;
        }

        /// <summary>
        /// Gets the editor for <see cref="ITestOptions"/>
        /// </summary>
        public ITestOptionsEditor GetEditorTestOptions()
        {
            // Make sure we have ready editor
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            // Return specific editor for test options
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
                    // Pre-load the question
                    .SetInitialQuestion(question)
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
        public IGradingEditor GetEditorGrading()
        {
            // Make sure we have ready editor
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            // Return specific editor for grading
            return mCurrentTestEditor.Grading;
        }

        public ITest BuildTest()
        {
            // Make sure we have ready editor
            if (mCurrentTestEditor == null)
                throw new InvalidOperationException("Editor not initialized.");

            // Return the test object from editor
            return mCurrentTestEditor.Build().Result;
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
