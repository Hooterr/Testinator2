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

        /// <summary>
        /// The absolute physical path to preset file
        /// </summary>
        public string AbsoluteFilePath { get; set; }

        /// <summary>
        /// The number of grades that this preset contains
        /// </summary>
        public int NumberOfGrades { get; set; }
    }
}
