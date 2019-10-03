using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for a single grading preset item 
    /// </summary>
    public class GradingPresetListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The assigned name for this grading preset
        /// This is unique for the given preset and should be used to identify the grading preset
        /// </summary>
        public string Name { get; set; }
    }
}
