﻿using System;
using System.Collections.Generic;
using System.Text;
using Testinator.Server.TestSystem.Implementation.Questions;

namespace Testinator.Server.TestSystem.Implementation
{
    [ImplementedInterface(typeof(IMultipleChoiceQuestionOptionsEditor))]
    internal class MultipleChoiceQuestionOptionsEditor : IEditor<MultipleChoiceQuestionOptions>, IMultipleChoiceQuestionOptionsEditor
    {
        public OperationResult<MultipleChoiceQuestionOptions> Build()
        {
            throw new NotImplementedException();
        }
    }
}
