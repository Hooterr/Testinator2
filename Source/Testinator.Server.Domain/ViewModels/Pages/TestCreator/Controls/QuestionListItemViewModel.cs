using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for a single question item (independent of question type) in the question list 
    /// </summary>
    public class QuestionListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The task of the question
        /// </summary>
        public string Task { get; set; }

        /// <summary>
        /// The icon to show in the UI for user to identify the question type
        /// </summary>
        public IconType Icon { get; set; }
    }
}
