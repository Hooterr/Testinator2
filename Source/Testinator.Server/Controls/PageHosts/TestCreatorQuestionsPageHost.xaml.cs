using System.Windows;
using Testinator.Server.Domain;
using Testinator.UICore;


namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for TestCreatorQuestionsPageHost.xaml
    /// </summary>
    public partial class TestCreatorQuestionsPageHost : BasePageHost<QuestionsPage>
    {
        #region Singleton

        /// <summary>
        /// Single instance of this view model
        /// NOTE: It's the only way to call abstract methods like they were static ones, as we can't declare them as static
        /// </summary>
        public static TestCreatorQuestionsPageHost Instance { get; set; } = new TestCreatorQuestionsPageHost();

        #endregion

        #region Dependency Properties

        /// <summary>
        /// The current page to show in the page host
        /// </summary>
        public QuestionsPage CurrentPage
        {
            get => (QuestionsPage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        /// Registers <see cref="CurrentPage"/> as a dependency property
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(QuestionsPage), typeof(TestCreatorQuestionsPageHost), new UIPropertyMetadata(default(QuestionsPage), null, CurrentPagePropertyChanged));

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestCreatorQuestionsPageHost() : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Property Changed Events

        /// <summary>
        /// Called when the <see cref="CurrentPage"/> value has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            // Get target values
            var targetPage = (QuestionsPage)value;
            var targetPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            // Get the frames
            var newPageFrame = (d as TestCreatorQuestionsPageHost).NewPage;
            var oldPageFrame = (d as TestCreatorQuestionsPageHost).OldPage;

            // Change the page based on that
            Instance.ChangeFramePages(newPageFrame, oldPageFrame, targetPage, targetPageViewModel);
            
            // Return the value back to dependency property
            return value;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Override the base page convert to handle our application specific pages
        /// </summary>
        /// <param name="page">The page to convert as an enum</param>
        /// <param name="vm">The optional view model</param>
        public override BasePage BasePageConvert(QuestionsPage page, object vm = null) => page.ToBasePage(vm);

        /// <summary>
        /// Override the application page convert to handle our application specific pages
        /// </summary>
        /// <param name="page">The page to convert</param>
        public override QuestionsPage ApplicationPageConvert(BasePage page) => page.ToQuestionsPage();

        #endregion
    }
}