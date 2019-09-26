using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The extensions that helps with editing grading in view models
    /// </summary>
    public static class GradesEditingExtensions
    {
        /// <summary>
        /// Converts the editor thresholds to grade view models ready to be edited
        /// </summary>
        /// <param name="thresholds">The grading thresholds from editor</param>
        /// <returns>The collection of grade view models</returns>
        public static ObservableCollection<GradeEditableViewModel> ToGradeViewModels(this IEnumerable<KeyValuePair<int, IGrade>> thresholds, int minimumGradesCount)
        {
            // Prepare a collection to return
            var grades = new ObservableCollection<GradeEditableViewModel>();

            // For each provided threshold...
            foreach (var threshold in thresholds)
            {
                // Add new grade
                grades.Add(new GradeEditableViewModel
                {

                });
            }

            // Return the final collection
            return grades;
        }

        /// <summary>
        /// Converts grade view models 
        /// </summary>
        /// <param name="grades">The collection of grade view models</param>
        /// <returns>The grading thresholds ready to pass to the editor</returns>
        public static List<KeyValuePair<int, IGrade>> ToThresholdsInEditor(this IList<GradeEditableViewModel> grades)
        {
            // Prepare a list to return
            var thresholds = new List<KeyValuePair<int, IGrade>>();



            // Return final list
            return thresholds;
        }
    }
}
