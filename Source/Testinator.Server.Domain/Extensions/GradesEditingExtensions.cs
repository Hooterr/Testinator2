using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public static BindingList<GradeEditableViewModel> ToGradeViewModels(this IEnumerable<KeyValuePair<int, IGrade>> thresholds, int minimumGradesCount)
        {
            // Prepare a collection to return
            var grades = new BindingList<GradeEditableViewModel>();

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
        public static BindingList<GradeEditableViewModel> StandarizeGrades(this BindingList<GradeEditableViewModel> grades)
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

                // For any other grade...
                // Set its starting threshold based on previous grade
                grades.ElementAt(i).ThresholdFrom = grades.ElementAt(i - 1).ThresholdTo + 1;
            }

            // Return the grades
            return grades;
        }

        /// <summary>
        /// Converts given percentage grades to points
        /// </summary>
        /// <param name="percentageGrades">The grade view models with percentage thresholds</param>
        /// <param name="maxTestPoints">The maximum amount of points for the test</param>
        /// <returns>The grade view models with points thresholds</returns>
        public static BindingList<GradeEditableViewModel> ToPoints(this BindingList<GradeEditableViewModel> percentageGrades, int maxTestPoints)
        {
            // Create new points collection
            var pointsGrades = new BindingList<GradeEditableViewModel>();

            // For each grade...
            foreach (var percentageGrade in percentageGrades)
            {
                // Create new grade
                var pointsGrade = new GradeEditableViewModel
                {
                    // Rewrite the name
                    Name = percentageGrade.Name,
                    // Calculate thresholds
                    ThresholdFrom = percentageGrade.ThresholdFrom * maxTestPoints / 100,
                    ThresholdTo = percentageGrade.ThresholdTo * maxTestPoints / 100
                };

                // Add it to the collection
                pointsGrades.Add(pointsGrade);
            }

            // Make sure first grade starts at 0
            pointsGrades.First().ThresholdFrom = 0;

            // Make sure last grade ends at test points
            pointsGrades.ElementAt(pointsGrades.Count - 1).ThresholdTo = maxTestPoints;

            // Return standarized grades
            return pointsGrades.StandarizeGrades();
        }

        /// <summary>
        /// Converts given points grades to percentages
        /// </summary>
        /// <param name="percentageGrades">The grade view models with points thresholds</param>
        /// <param name="maxTestPoints">The maximum amount of points for the test</param>
        /// <returns>The grade view models with percentage thresholds</returns>
        public static BindingList<GradeEditableViewModel> ToPercentages(this BindingList<GradeEditableViewModel> pointsGrades, int maxTestPoints)
        {
            // Create new percentages collection
            var percentageGrades = new BindingList<GradeEditableViewModel>();

            // For each grade...
            foreach (var pointsGrade in pointsGrades)
            {
                // Create new grade
                var percentageGrade = new GradeEditableViewModel
                {
                    // Rewrite the name
                    Name = pointsGrade.Name,
                    // Calculate thresholds
                    ThresholdFrom = Convert.ToInt32((double)pointsGrade.ThresholdFrom / maxTestPoints * 100),
                    ThresholdTo = Convert.ToInt32((double)pointsGrade.ThresholdTo / maxTestPoints * 100)
                };

                // Add it to the collection
                percentageGrades.Add(percentageGrade);
            }

            // Make sure first grade starts at 0
            percentageGrades.First().ThresholdFrom = 0;

            // Make sure last grade ends at 100
            percentageGrades.ElementAt(percentageGrades.Count - 1).ThresholdTo = 100;

            // Return standarized grades
            return percentageGrades.StandarizeGrades();
        }
    }
}
