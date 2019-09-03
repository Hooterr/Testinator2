using System.Collections.Generic;
using System.Windows.Input;
using Testinator.Core;
using Testinator.Server.TestSystem.Implementation;

namespace Testinator.Server.Core
{
    using ABCQuestionEditor = IQuestionEditor<TestSystem.Implementation.Questions.MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple choice question in test editor
    /// </summary>
    public class MultipleChoiceQuestionTestEditorViewModel : BaseViewModel
    {
        #region Private Members

        private ABCQuestionEditor mQuestionEditor;

        #endregion

        #region Public Properties

        public string Task { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int RightAnswerIndex { get; set; }
        public int PointsForRightAnswer { get; set; }

        public string ErrorMessage { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to submit this question to the test
        /// </summary>
        public ICommand SubmitCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MultipleChoiceQuestionTestEditorViewModel()
        {
            // Create commands
            SubmitCommand = new RelayCommand(Submit);

            // Create editor for this type of question
            mQuestionEditor = Editors.MultipleChoiceQuestion
                .NewQuestion()
                .UseNewestVersion()
                .Build();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Tries to submit current state of question to the test
        /// </summary>
        private void Submit()
        {
            // Copy all the question properties to the editor to validate
            mQuestionEditor.Task.Text.Content = Task;
            //mQuestionEditor.Options.Options.Add(AnswerA);
            //mQuestionEditor.Options.Options.Add(AnswerB);
            //mQuestionEditor.Options.Options.Add(AnswerC);
            //mQuestionEditor.Options.Options.Add(AnswerD);
            mQuestionEditor.Options.SetOptions(AnswerA, AnswerB, AnswerC, AnswerD);
            mQuestionEditor.Scoring.CorrectAnswerIdx = RightAnswerIndex;
            mQuestionEditor.Scoring.MaximumScore = PointsForRightAnswer;

            // Build the question in the editor
            var operation = mQuestionEditor.Build();

            // If there are errors
            if (operation.Failed)
            {
                // Show them and do not submit anything
                ErrorMessage = string.Join("\n", operation.Errors.ToArray());
                return;
            }

            // Otherwise, question built successfully, add it to the test
            // TODO:
        }

        #endregion
    }
}
