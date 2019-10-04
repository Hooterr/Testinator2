using Dna;
using Testinator.Core;
using Testinator.Server.Domain;

namespace Testinator.Server
{
    /// <summary>
    /// Dependency Injection container for Testinator.Server application\
    /// TODO: Should probably get rid of it and make every single DI thing in constructor, if possible
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
        /// A shortcut to get appropriate view model for page with injected dependiencies by DI
        /// </summary>
        /// <typeparam name="T">Any view model that inherites <see cref="BaseViewModel"/></typeparam>
        public static T GetInjectedPageViewModel<T>() where T : BaseViewModel => Framework.Service<T>();

        #endregion
    }
}
