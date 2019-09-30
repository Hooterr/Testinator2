using Testinator.Server.Domain;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for QuestionsMultipleCheckBoxesPage.xaml
    /// </summary>
    public partial class QuestionsMultipleCheckBoxesPage : BasePage<QuestionsMultipleCheckBoxesPageViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public QuestionsMultipleCheckBoxesPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public QuestionsMultipleCheckBoxesPage(QuestionsMultipleCheckBoxesPageViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion
    }
}
