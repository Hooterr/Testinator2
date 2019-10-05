namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for single selectable answer control
    /// </summary>
    public class AnswerSelectableViewModel : AnswerEditableViewModel
    {
        /// <summary>
        /// The title to use to identify this answer
        /// Usually its just A or B or C or D etc.
        /// NOTE: Not used in every question type
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if this answer is selected for being the right one 
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
