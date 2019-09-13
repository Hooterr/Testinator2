using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    public class Grade : IGrade
    {
        public string Name { get; internal set; }

        public Grade(string name)
        {
            Name = name;
        }
    }
}
