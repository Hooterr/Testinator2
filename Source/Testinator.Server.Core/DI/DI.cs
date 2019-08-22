using Dna;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// Dependency Injection container for Testinator.Server application
    /// </summary>
    public static class DI
    {
        #region Public Shortcuts

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationSettingsViewModel"/>
        /// </summary>
        public static ApplicationSettingsViewModel Settings => Framework.Service<ApplicationSettingsViewModel>();

        /// <summary>
        /// A shortcut to access the injected implementation of <see cref="ILogFactory"/>
        /// </summary>
        public static ILogFactory Logger => Framework.Service<ILogFactory>();

        /// <summary>
        /// A shortcut to access the injected implementation of <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => Framework.Service<IUIManager>();

        /// <summary>
        /// A shortcut to get appropriate view model for page with injected dependiencies by DI
        /// </summary>
        /// <typeparam name="T">Any view model that inherites <see cref="BaseViewModel"/></typeparam>
        public static T GetInjectedPageViewModel<T>() where T : BaseViewModel => Framework.Service<T>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the DI and binds initial view models to that
        /// </summary>
        public static void InitialSetup()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                                                .AddFileLogger()
                                                .AddApplicationServices();
        }

        #endregion
    }
}
