using Testinator.Server.Core;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for DemoABCD.xaml
    /// </summary>
    public partial class DemoABCD : BasePage<MultipleChoiceQuestionTestEditorViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DemoABCD() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public DemoABCD(MultipleChoiceQuestionTestEditorViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion
    }
}
