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
        /// <returns>Answers as strings</returns>
        public static ObservableCollection<string> ToStringAnswers(this IEnumerable<string> options, int minimumAnswersCount)
        {
            // Prepare a collection to return
            var answers = new ObservableCollection<string>();

            // For each given option
            foreach (var option in options)
            {
                // Add it as an answer
                answers.Add(option);
            }

            // Check if we haven't met the minimum requirement of answer count
            var answerCount = answers.Count;
            if (answerCount < minimumAnswersCount)
            {
                // Then we need to create new answers to fill the gap
                for (var i = 0; i < minimumAnswersCount - answerCount; i++)
                {
                    // Add new empty answer
                    answers.Add(string.Empty);
                }
            }

            // Return the final collection
            return answers;
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

        /// <summary>
        /// Finds and returns the list of bools that indicate whether the answer is selected or not
        /// </summary>
        /// <param name="answers">Answer view models</param>
        /// <returns>The list of bools with true values on right answers</returns>
        public static List<bool> GetBoolsOfSelected(this IList<AnswerSelectableViewModel> answers)
        {
            // Prepare a list to return
            var bools = new List<bool>();

            // Loop each answer
            foreach (var answer in answers)
            {
                // Add its value to the list
                bools.Add(answer.IsSelected);
            }

            // Return the final list
            return bools;
        }

        /// <summary>
        /// Converts answer list to the dictionary version with point rates
        /// </summary>
        /// <param name="answers"></param>
        /// <returns></returns>
        public static Dictionary<string, float> ToRatedAnswers(this IList<string> answers)
        {
            // Prepare a dictionary to return
            var dictionary = new Dictionary<string, float>();

            // Loop each answer
            foreach (var answer in answers)
            {
                // Add it to the dictionary with the rate
                dictionary.Add(answer, 1f);
            }

            // Return the ready dictionary
            return dictionary;
        }
    }
}
