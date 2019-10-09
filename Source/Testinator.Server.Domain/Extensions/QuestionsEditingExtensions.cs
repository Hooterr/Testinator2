using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// <param name="minimumAnswersCount">The minimum amount of answers required</param>
        /// <returns>Answer view models as an observable collection</returns>
        public static ObservableCollection<AnswerSelectableViewModel> ToAnswerViewModels(this IEnumerable<string> options, int minimumAnswersCount)
        {
            // Prepare a collection to return
            var answers = new ObservableCollection<AnswerSelectableViewModel>();

            // For each given option
            foreach (var option in options)
            {
                // Create new answer
                answers.Add(new AnswerSelectableViewModel
                {
                    // Get the text content
                    Answer = option,

                    // Mark as unselected
                    IsSelected = false
                });
            }

            // Check if we haven't met the minimum requirement of answer count
            var answerCount = answers.Count;
            if (answerCount < minimumAnswersCount)
            {
                // Then we need to create new answers to fill the gap
                for (var i = 0; i < minimumAnswersCount - answerCount; i++)
                {
                    // Create new answer
                    answers.Add(new AnswerSelectableViewModel());
                }
            }

            // Set the titles of each answer
            var currentTitle = 'A';
            foreach (var answer in answers)
            {
                // Set the current title
                answer.Title = currentTitle.ToString();

                // Increment the char value for next letter
                currentTitle++;
            }

            // Return the final collection
            return answers;
        }

        /// <summary>
        /// Converts editor options to the simple answer strings that are displayed in UI
        /// </summary>
        /// <param name="options">The options from question editor</param>
        /// <param name="minimumAnswersCount">The minimum amount of answers required</param>
        /// <returns>Answers as view models with strings inside</returns>
        public static ObservableCollection<AnswerEditableViewModel> ToStringAnswers(this IEnumerable<string> options, int minimumAnswersCount)
        {
            // Prepare a collection to return
            var answers = new ObservableCollection<AnswerEditableViewModel>();

            // For each given option
            foreach (var option in options)
            {
                // Add it as an answer
                answers.Add(new AnswerEditableViewModel
                {
                    Answer = option
                });
            }

            // Check if we haven't met the minimum requirement of answer count
            var answerCount = answers.Count;
            if (answerCount < minimumAnswersCount)
            {
                // Then we need to create new answers to fill the gap
                for (var i = 0; i < minimumAnswersCount - answerCount; i++)
                {
                    // Add new empty answer
                    answers.Add(new AnswerEditableViewModel());
                }
            }

            // Return the result as collection
            return answers;
        }

        /// <summary>
        /// Converts answer view models to options suitable for question editor
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>List of options ready to pass into question editor</returns>
        public static List<string> ToOptionsInEditor(this IList<AnswerSelectableViewModel> answers)
        {
            // Copy the list and get only string values from every answer
            var options = answers.Select(answer => answer.Answer);

            // Return it as list
            return options.ToList();
        }

        /// <summary>
        /// Finds and returns the right answers indexes from provided answers
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>The list of indexes of selected answers</returns>
        public static IEnumerable<int?> GetIndexesOfSelected(this IList<AnswerSelectableViewModel> answers)
        {
            // Copy the list and get only selected answers
            var indexes = answers.Where(answer => answer.IsSelected)
                // For every selected answer, get only it's index
                .Select(answer => new int?(answers.IndexOf(answer)));

            // Return it as list
            return indexes;
        }

        /// <summary>
        /// Finds and returns the list of bools that indicate whether the answer is selected or not
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>The list of bools with true values on right answers</returns>
        public static List<bool> GetBoolsOfSelected(this IList<AnswerSelectableViewModel> answers)
        {
            // Copy the list and get only IsSelected values from every answer
            var bools = answers.Select(answer => answer.IsSelected);

            // Return it as list
            return bools.ToList();
        }

        /// <summary>
        /// Converts answer list to the editor list version with point rates
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>The list of pairs ready for editor to handle</returns>
        public static IList<KeyValuePair<string, float>> ToRatedAnswers(this IList<AnswerEditableViewModel> answers)
        {
            // Copy the list and attach the rate to every answer
            var list = answers.Select(answer => new KeyValuePair<string, float>(answer.Answer, 1f));
            
            // Return it as list
            return list.ToList();
        }
    }
}
