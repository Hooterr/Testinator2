using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Editors;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The extensions that helps with editing questions in view models
    /// </summary>
    public static class QuestionsEditingExtensions
    {
        /// <summary>
        /// Builds the given editor and returns the result
        /// </summary>
        /// <param name="buildableEditor">The editor for question to build</param>
        /// <typeparam name="TQuestion">The question type to return as a build result</typeparam>
        /// <returns>Question object if build was successful, or null otherwise</returns>
        public static TQuestion BuildQuestionFromEditor<TQuestion>(this IBuildable<TQuestion> buildableEditor) 
            where TQuestion : class
        {
            // Try to build the question
            var buildOperation = buildableEditor.Build();

            // If it succeeded...
            if (buildOperation.Succeeded)
            {
                // Return the question
                return buildOperation.Result;
            }

            // Validation failed, return null so the master page will react accordingly
            return null;
        }

        /// <summary>
        /// Converts editor options to the answers view models that are displayed in UI
        /// </summary>
        /// <param name="options">The options from question editor</param>
        /// <returns>Answer view models as an observable collection</returns>
        public static ObservableCollection<AnswerSelectableViewModel> ToAnswerViewModels(this IEnumerable<string> options)
        {
            var answers = new ObservableCollection<AnswerSelectableViewModel>();

            return new ObservableCollection<AnswerSelectableViewModel>
            {
                new AnswerSelectableViewModel(),
                new AnswerSelectableViewModel()
            };
        }

        /// <summary>
        /// Converts answer view models to options suitable for question editor
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>List of options ready to pass into question editor</returns>
        public static List<string> ToOptionsInEditor(this IList<AnswerSelectableViewModel> answers)
        {
            // Prepare a list to return
            var options = new List<string>();

            // For each answer...
            foreach (var answer in answers)
            {
                // Add it's content as an option
                options.Add(answer.Answer);
            }

            // Return the final list
            return options; 
        }

        /// <summary>
        /// Finds and returns the right answers indexes from provided answers
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>The list of indexes of selected answers</returns>
        public static IEnumerable<int?> GetIndexesOfSelected(this IList<AnswerSelectableViewModel> answers)
        {
            // Prepare a list to return
            var indexes = new List<int?>();

            // Loop each answer
            foreach (var answer in answers)
            {
                // If its selected...
                if (answer.IsSelected)
                {
                    // Add the index
                    indexes.Add(answers.IndexOf(answer));
                }
            }

            // Return the final list
            return indexes;
        }
    }
}
