﻿using System;
using System.Collections;
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
        /// Grading strategy
        /// </summary>
        IGradingStrategy Strategy { get; }
        
        /// <summary>
        /// Gets grade based on the point score 
        /// </summary>
        /// <param name="score">The score</param>
        /// <returns>Corresponding grade</returns>
        // TODO replace int with complex type with question catergories etc.
        IGrade GetGrade(int score);
    }
}