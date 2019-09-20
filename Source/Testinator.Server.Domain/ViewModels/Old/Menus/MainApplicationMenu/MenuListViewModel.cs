using System.Collections.Generic;
using Testinator.Core;

namespace Testinator.Server.Domain
{
    /// <summary>
    /// The view model for the side menu list with "page-changers"
    /// </summary>
    public class MenuListViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of items in this menu list
        /// </summary>
        public List<MenuListItemViewModel> Items { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuListViewModel(ApplicationViewModel applicationVM)
        {
            Items = new List<MenuListItemViewModel>
            {
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.Start,
                    Icon = IconType.Home,
                    TargetPage = ApplicationPage.Home
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.StartTest,
                    Icon = IconType.Test,
                    TargetPage = ApplicationPage.BeginTest
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.TestEditorTitle,
                    Icon = IconType.Editor,
                    TargetPage = ApplicationPage.TestEditorInitial
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.TestResults,
                    Icon = IconType.DataBase,
                    TargetPage = ApplicationPage.TestResultsInitial,
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.ScreenStream,
                    Icon = IconType.Screen,
                    TargetPage = ApplicationPage.ScreenStream
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.Settings,
                    Icon = IconType.Settings,
                    TargetPage = ApplicationPage.Settings
                },
                new MenuListItemViewModel(applicationVM)
                {
                    Name = LocalizationResource.About,
                    Icon = IconType.InfoCircle,
                    TargetPage = ApplicationPage.About
                },
            };
        }

        #endregion
    }
}