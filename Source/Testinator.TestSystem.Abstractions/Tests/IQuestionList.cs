using System;
using System.Collections.Generic;
using System.Text;

namespace Testinator.TestSystem.Abstractions
{
    public interface IQuestionList
    {
        ICollection<IQuestionProvider> Questions { get; }
    }
}