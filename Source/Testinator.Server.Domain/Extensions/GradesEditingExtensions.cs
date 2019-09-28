using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Testinator.TestSystem.Abstractions;
using Testinator.TestSystem.Implementation;

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
        /// <param name="minimumGradesCount">The minimum amount of grades required</param>
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
                    Name = threshold.Value.Name,
                    ThresholdTo = threshold.Key
                });
            }

            // Check if we haven't met the minimum requirement of grades count
            var gradesCount = grades.Count;
            if (gradesCount < minimumGradesCount)
            {
                // Then we need to create new grades to fill the gap
                for (var i = 0; i < minimumGradesCount - gradesCount; i++)
                {
                    // Create empty grade
                    grades.Add(new GradeEditableViewModel());
                }
            }

            // Fill missing grade data and return ready collection
            return grades.StandarizeGrades();
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

            // For each grade...
            foreach (var grade in grades)
            {
                // Add new threshold
                thresholds.Add(new KeyValuePair<int, IGrade>(grade.ThresholdTo, new Grade(grade.Name)));
            }

            // Return final list
            return thresholds;
        }


        /// <summary>
        /// Ensures the correctness of grade view models, fills up missing data etc.
        /// </summary>
        /// <param name="grades">The grade view models</param>
        /// <returns>The same grade view models that were passed as a parameter</returns>
        public static ObservableCollection<GradeEditableViewModel> StandarizeGrades(this ObservableCollection<GradeEditableViewModel> grades)
        {
            // Loop each grade
            for (var i = 0; i < grades.Count; i++)
            {
                // For the very first grade...
                if (i == 0)
                {
                    // Set its starting threshold to 0
                    grades.ElementAt(i).ThresholdFrom = 0;
                    continue;
                }

                // For the last grade...
                if (i == grades.Count - 1)
                {
                    // Set its ending threshold to 100
                    grades.ElementAt(i).ThresholdTo = 100;
                    break;
                }

                // For any other grade...
                // Set its starting threshold based on previous grade
                grades.ElementAt(i).ThresholdFrom = grades.ElementAt(i - 1).ThresholdTo + 1;
            }

            // Return the grades
            return grades;
        }
    }
}
