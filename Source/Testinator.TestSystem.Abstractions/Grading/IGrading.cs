using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    public interface IGrading
    {
        /// <summary>
        /// The name of this grading
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The date when this criteria was created
        /// </summary>
        DateTime CreationDate { get; }

        /// <summary>
        /// The date when this criteria was last edited
        /// </summary>
        DateTime LastEditionDate { get; }
        
        /// <summary>
        /// Gets grade based on the point score 
        /// </summary>
        /// <param name="score">The score</param>
        /// <returns>Corresponding grade</returns>
        IGrade GetGrade(int score);
    }
}
