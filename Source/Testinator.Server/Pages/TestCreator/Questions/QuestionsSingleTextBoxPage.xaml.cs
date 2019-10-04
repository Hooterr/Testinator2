using Testinator.Server.Domain;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for QuestionsSingleTextBoxPage.xaml
    /// </summary>
    public partial class QuestionsSingleTextBoxPage : BasePage<QuestionsSingleTextBoxPageViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsSingleTextBoxPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public QuestionsSingleTextBoxPage(QuestionsSingleTextBoxPageViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion
    }
}
