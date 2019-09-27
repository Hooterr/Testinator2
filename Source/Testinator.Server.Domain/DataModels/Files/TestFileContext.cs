namespace Testinator.Server.Domain
{
    /// <summary>
    /// Contains test file description
    /// </summary>
    public class TestFileContext
    {
        // Can delete/add stuff here

        /// <summary>
        /// The path to the file
        /// </summary>
        public string FilePath { get;  set; }

        /// <summary>
        /// The name of the test
        /// </summary>
        public string TestName { get;  set; }

        /// <summary>
        /// The author of this test
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Test categories. [0] is the top one 
        /// </summary>
        public string[] Categories { get; set; }
    }
}
