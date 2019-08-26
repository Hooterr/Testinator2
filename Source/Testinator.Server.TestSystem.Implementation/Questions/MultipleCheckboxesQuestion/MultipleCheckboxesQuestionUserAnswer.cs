using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Questions
{
    public class MultipleCheckboxesQuestionUserAnswer : IUserAnswer
    {
        /// <summary>
        /// Selected/not selected list of user answer
        /// </summary>
        public List<bool> CheckedOptions { get; set; }
    }
}
