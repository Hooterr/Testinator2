using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for a single item in the test list 
    /// </summary>
    public class TestListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The name of the test
        /// This is unique for the given test and should be used to identify the test
        /// </summary>
        public string Name { get; set; }

        // TODO: Add more stuff
    }
}
