using Testinator.Server.Domain;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for TestCreatorGradingPresetsPage.xaml
    /// </summary>
    public partial class TestCreatorGradingPresetsPage : BasePage<TestCreatorGradingPresetsPageViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorGradingPresetsPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public TestCreatorGradingPresetsPage(TestCreatorGradingPresetsPageViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion
    }
}
