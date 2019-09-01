﻿using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Attributes;
using Testinator.Server.TestSystem.Implementation.Questions.ScoringStrategy;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface IQuestionScoringEditor
    {
        [EditorProperty]
        int MaximumScore { get; set; }
    }
}