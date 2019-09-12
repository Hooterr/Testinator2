namespace Testinator.TestSystem.Abstractions
{
    /// <summary>
    /// The interface for initial test informations
    /// </summary>
    public interface ITestInfo
    {
        /// <summary>
        /// The name of the test - we can't have a test without name regardless of platform
        /// </summary>
        string Name { get; }
    }
}
