using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for single selectable answer control
    /// </summary>
    public class AnswerSelectableViewModel : BaseViewModel
    {
        public string Answer { get; set; }

        public bool IsSelected { get; set; }
    }
}
