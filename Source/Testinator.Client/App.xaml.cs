﻿using Dna;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Windows;
using Testinator.Client.Core;
using Testinator.Core;

namespace Testinator.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            ApplicationSetup();

            // Log that application is starting
            DI.Logger.Log("Application starting...");

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            // Go to the first page
            DI.Application.GoToPage(ApplicationPage.Login);
        }

        /// <summary>
        /// Notify the application about closing procedure
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            DI.Application.Close();
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Set default language
            LocalizationResource.Culture = new CultureInfo("pl-PL");

            // Setup IoC
            DI.InitialSetup();

            // Inject WPF specific services
            Framework.Construction.Services.AddSingleton<IUIManager, UIManager>();
            Framework.Construction.Services.AddSingleton<Core.ILogFactory>(new BaseLogFactory(new[]
            {
                // TODO: Add ApplicationSettings so we can set/edit a log location
                //       For now just log to the path where this application is running

                // TODO: Make log files ordered by a date, week-wise
                //       For now - random numbers for testing as it allows running multiple clients
                new Core.FileLogger(($"log{new Random().Next(100000, 99999999).ToString()}.txt"))
            }));

            // Build the final DI
            Framework.Construction.Build();
        }
    }
}
