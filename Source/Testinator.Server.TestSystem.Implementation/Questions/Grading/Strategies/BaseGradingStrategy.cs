using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Grading.Strategies
{
    public abstract class BaseGradingStrategy : IGradingStrategy
    {
        public abstract IGrade GetGrade(int pointScore);
    }
}
