using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for single editable grade control
    /// </summary>
    public class GradeEditableViewModel : BaseViewModel
    {
        /// <summary>
        /// The threshold on which this grade starts
        /// NOTE: This should not be set by a user, it should be calculated based on previous grade instead
        /// </summary>
        public int ThresholdFrom { get; set; }

        /// <summary>
        /// The threshold up to which this grade is reaching
        /// </summary>
        public int ThresholdTo { get; set; }

        /// <summary>
        /// The name of this grade
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
