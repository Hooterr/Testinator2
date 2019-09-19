using Testinator.Server.Domain;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for DemoABCD.xaml
    /// </summary>
    public partial class DemoInfo : BasePage<TestInfoPageViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DemoInfo() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public DemoInfo(TestInfoPageViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion
    }
}
