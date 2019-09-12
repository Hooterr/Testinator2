using Testinator.TestSystem.Abstractions;

namespace Testinator.Server.TestSystem.Implementation
{
    /// <summary>
    /// The Testinator.Server implementation of test informations required to initialize new test
    /// </summary>
    public class TestInfo : ITestInfo
    {
        // TODO: Maybe add character limitations here? Name - max 100 chars, Desc - max 500 chars??

        /// <summary>
        /// The name of the test
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the test
        /// </summary>
        public string Description { get; set; }
    }
}
