using System;
using System.Diagnostics;
using System.Linq;
using Testinator.Core;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Editors;
using Testinator.TestSystem.Implementation.Questions;

namespace Testinator.Server.Domain
{
    using QuestionEditorMultipleChoice = IQuestionEditor<MultipleChoiceQuestion, IMultipleChoiceQuestionOptionsEditor, IMultipleChoiceQuestionScoringEditor>;

    /// <summary>
    /// The view model for multiple choice question page in Test Creator
    /// </summary>
    public class QuestionsMultipleChoicePageViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The editor for multiple choice question
        /// </summary>
        private QuestionEditorMultipleChoice mEditor;

        #endregion

        #region Public Properties

        public InputField<string> Task { get; set; }
        public InputField<string> AnswerA { get; set; }
        public InputField<string> AnswerB { get; set; }
        public InputField<string> AnswerC { get; set; }
        public InputField<string> Points { get; set; }
        public InputField<string> RightAnswer { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes this view model with provided editor for this question
        /// </summary>
        /// <param name="editor">The editor for this type of question containing all data</param>
        public Func<IQuestion> InitializeEditor(QuestionEditorMultipleChoice editor)
        {
            // If we don't get valid editor, we can't do anything
            if (editor == null)
                return null;

            // Get the editor itself
            mEditor = editor;

            // Initialize every property based on it
            // If we are editing existing question, editor will have it's data
            // If we are creating new one, editor will be empty but its still fine at this point
            Task = mEditor.Task.Text.Content;
            AnswerA = mEditor.Options.ABCD.FirstOrDefault();
            Points = mEditor.Scoring.MaximumScore.ToString();
            RightAnswer = mEditor.Scoring.CorrectAnswerIdx.ToString();

            // Catch all the errors and display them

            // Jeśli zrobisz tak to error do Content przyjdzie tutaj
            mEditor.OnErrorFor(x => x.Task.Text.Content, (e) => Task.ErrorMessage = e);

            // Jeśli to powyższe zakomentujesz i zostawisz tylko to poniżej to wszystkie errory z Task.Text przyjdą tutaj 
            //(może przyjść więcej niż jeden więc trzeba zrobić jakąś mechaniką żeby się nie nadpisywały
            // Tak naprawdę to powyższe OnErrorFor(x => x.Task.Text.Content...) też możesz zostawić
            // wtedy te errory związne z Content pójdą do góry, a reszta związana z Task.Text pójdzie tu niżej
            // Czyli jak Task.Text.Content nie ma handlera to error jest podawany wyżej do Task.Text, jak on nie ma handlera to do Task, a jak task nie ma
            // to wrzucany jest do tej listy operation result
            // Można być dorobić metodę OnUnhandledError(...) i tam pakować te nieobsłużone
            // W teorii taka metoda to po prostu OnErrorFor(x => x) ale to jeszcze nie działa
            // Ale zrobie to na dniach
            // Jakbyś chciał grzebać w implementacji tego to uprzedzam, że jest tam mały chaos część rzeczy ma w ogóle tymczasowe nazwy itp.
            // Dont worry będą komentarze
            //mEditor.OnErrorFor(x => x.Task.Text, (e) =>
            //{
            //    Debugger.Break();
            //});

            mEditor.OnErrorFor(x => x.Options.ABCD, (e) => AnswerA.ErrorMessage = e);
            mEditor.OnErrorFor(x => x.Scoring.MaximumScore, (e) => Points.ErrorMessage = e);

            // Return the submit action for the master view model to make use of
            return Submit;
        }

        /// <summary>
        /// Tries to submit the question to the editor
        /// </summary>
        public IQuestion Submit()
        {
            // Pass all the changes user has made to the editor
            mEditor.Task.Text.Content = Task;

            // Try to build the question
            var buildOperation = mEditor.Build();

            // If it succeeded...
            if (buildOperation.Succeeded)
            {
                // Return the question
                return buildOperation.Result;
            }

            // Validation failed, return null so the master page will react accordingly
            return null;
        }

        #endregion
    }
}
