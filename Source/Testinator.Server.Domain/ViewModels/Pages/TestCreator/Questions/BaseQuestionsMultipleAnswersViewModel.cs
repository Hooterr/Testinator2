using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model that provides base functionality to the question view models that have multiple answers available
    /// </summary>
    public class BaseQuestionsMultipleAnswersViewModel<TQuestion, TOptionsEditor, TScoringEditor> : BaseViewModel
        where TOptionsEditor : IQuestionMultipleAnswersOptionsEditor
    {
        #region Protected Members

        /// <summary>
        /// The editor for current question
        /// </summary>
        protected IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> mEditor;

        #endregion

        #region Public Properties

        /// <summary>
        /// The task for this question
        /// </summary>
        public InputField<string> TaskTextContent { get; set; }

        /// <summary>
        /// The possible answers for this question as view models
        /// </summary>
        public InputField<ObservableCollection<AnswerSelectableViewModel>> Answers { get; set; }

        /// <summary>
        /// The points that are given for right answer
        /// </summary>
        public InputField<string> Points { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to add new possible answer to the question
        /// </summary>
        public ICommand AddAnswerCommand { get; protected set; }

        /// <summary>
        /// The command to remove last answer from the question
        /// </summary>
        public ICommand RemoveAnswerCommand { get; protected set; }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds new possible answer to the question
        /// </summary>
        protected void AddAnswer()
        {
            // Make sure we don't exceed maximum answer limit
            var answersCount = Answers.Value.Count;
            if (answersCount >= mEditor.Options.MaximumCount)
                return;

            // Add new answer
            Answers.Value.Add(new AnswerSelectableViewModel());
        }

        /// <summary>
        /// Removes last answer from the question
        /// </summary>
        protected void RemoveAnswer()
        {
            // Make sure we can remove the answer and still meet the requirement
            var answersCount = Answers.Value.Count;
            if (answersCount <= mEditor.Options.MinimumCount)
                return;

            // Remove the last answer
            Answers.Value.RemoveAt(answersCount - 1);
        }

        #endregion

        #region Public Methods

        public virtual Func<IQuestion> InitializeEditor(IQuestionEditor<TQuestion, TOptionsEditor, TScoringEditor> editor)
        {
            // If we don't get valid editor, we can't do anything
            if (editor == null)
                return null;

            // Get the editor itself
            mEditor = editor;

            // Return nothing since its base class
            return null;
        }

        #endregion
    }
}
