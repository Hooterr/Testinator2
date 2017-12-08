﻿using Ninject;
using Testinator.Core;
using Testinator.Network.Client;

namespace Testinator.Client.Core
{
    /// <summary>
    /// The IoC container for the client application
    /// </summary>
    public static class IoCClient
    {
        #region Public Properties

        /// <summary>
        /// The kernel for our IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => IoCClient.Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="ClientModel"/>
        /// </summary>
        public static ClientModel Client => IoCClient.Get<ClientModel>();

        /// <summary>
        /// A shortcut to access the <see cref="ClientNetwork"/>
        /// </summary>
        public static ClientNetwork Network => IoCClient.Get<ClientNetwork>();

        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        /// Just an exaple
        /// Can be deleted anytime
        //public static IUIManager UI => IoC.Get<IUIManager>();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use
        /// NOTE: Must be called as soon as your application starts up to ensure all 
        ///       services can be found
        /// </summary>
        public static void Setup()
        {
            // Bind all required view models
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton view models
        /// </summary>
        private static void BindViewModels()
        {
            // Bind to a single instance of Application view model, client network  and client model
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
            Kernel.Bind<ClientNetwork>().ToConstant(new ClientNetwork());
            Kernel.Bind<ClientModel>().ToConstant(new ClientModel());
        }

        #endregion

        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}