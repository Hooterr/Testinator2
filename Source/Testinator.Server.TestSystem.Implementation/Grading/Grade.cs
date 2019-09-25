using System;
using Testinator.TestSystem.Abstractions;

namespace Testinator.TestSystem.Implementation
{
    [Serializable]
    public class Grade : IGrade
    {
        public string Name { get; internal set; }

        public Grade(string name)
        {
            Name = name;
        }
    }
}
