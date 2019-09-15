using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions.Tests;

namespace Testinator.Server.TestSystem.Implementation
{
    public interface ITestEditor : IBuildable<ITest>
    {
        ITestInfoEditor Info { get; }

        ITestOptionsEditor Options { get; }

        IGradingEditor Grading { get; }

        IQuestionEditorCollection Questions { get; }
    }
}
