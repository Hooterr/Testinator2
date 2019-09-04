using System;
using System.Collections.Generic;
using System.Text;
using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation.Grading
{
    public class Grade : IGrade
    {
        public string Name { get; private set; }

        public Grade(string name)
        {
            Name = name;
        }
    }
}
