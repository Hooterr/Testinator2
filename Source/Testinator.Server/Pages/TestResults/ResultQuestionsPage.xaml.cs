﻿using Testinator.AnimationFramework;
using Testinator.Server.Core;
using Testinator.UICore;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for ResultQuestionsPage.xaml
    /// </summary>
    public partial class ResultQuestionsPage : BasePage<ResultQuestionsViewModel>
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResultQuestionsPage() : base()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        /// <param name="specificViewModel">The specific view model to use for this page</param>
        public ResultQuestionsPage(ResultQuestionsViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();

            // Set the default page load/unload animation
            PageLoadAnimation = ElementAnimation.SlideAndFadeInFromRight;
            PageUnloadAnimation = ElementAnimation.SlideAndFadeOutToLeft;
        }

        #endregion
    }
}
