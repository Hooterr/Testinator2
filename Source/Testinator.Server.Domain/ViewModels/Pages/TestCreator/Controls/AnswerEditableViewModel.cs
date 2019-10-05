using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for single editable answer control
    /// </summary>
    public class AnswerEditableViewModel : BaseViewModel
    {
        /// <summary>
        /// The answer text content
        /// </summary>
        public string Answer { get; set; } = string.Empty;
    }
}
