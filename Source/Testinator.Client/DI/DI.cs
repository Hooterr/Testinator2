using Dna;
using Testinator.Client.Domain;
using Testinator.Core;

namespace Testinator.Client
{
    /// <summary>
    /// Dependency Injection container for Testinator.Client application
    /// </summary>
    public static class DI
    {
        #region Public Shortcuts

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the injected implementation of <see cref="ILogFactory"/>
        /// </summary>
        public static ILogFactory Logger => Framework.Service<ILogFactory>();

        /// <summary>
        /// A shortcut to access the injected implementation of <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => Framework.Service<IUIManager>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets up the DI and binds initial view models to that
        /// </summary>
        public static void InitialSetup()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                                                .AddFileLogger()
                                                .AddApplicationServices()
                                                .Build();
        }

        #endregion
    }
}
