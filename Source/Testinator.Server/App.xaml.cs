using Dna;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Testinator.Core;
using Testinator.Server.Domain;
using Testinator.Server.Logging;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our DI and Updater immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            // https://learnwpf.com/post/2012/07/09/Adding-an-Icon-and-File-Association-to-WPF-Clickonce-Applications.aspx
            // TODO add file association

            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            ApplicationSetup();

            Framework.Service<ILogFactory>().Log("Application starting...");

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();

            // Check for updates
            if (await CheckUpdatesAsync())
            {
                Framework.Service<ILogFactory>().Log("Running updater...");

                // Run the updater
                Process.Start(new ProcessStartInfo
                {
                    FileName = "Testinator.Updater.exe",
                    Arguments = "Server" + " " + Framework.Service<ApplicationViewModel>().ApplicationLanguage + " " + "",
                    UseShellExecute = true,
                    Verb = "runas"
                });

                // Close this app
                Framework.Service<ILogFactory>().Log("Main application closing...");
                Current.Shutdown();
            }
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private void ApplicationSetup()
        {
            // Default language
            LocalizationResource.Culture = new CultureInfo("pl-PL");

            // Setup DI
            Framework.Construct<DefaultFrameworkConstruction>()
                                                .AddFileLogger()
                                                .AddDbContext()
                                                .AddApplicationServices()
                                                .Build();

            // Inject Logger
            Framework.Construction.Services.AddSingleton<ILogFactory>(new BaseLogFactory(new[]
            {
                // Set the path from Settings
                new Logging.FileLogger(DI.Settings.LogFilePath)
            }));

            // Build the final DI
            Framework.Construction.Build();

            // Initialize first application's page
            Framework.Service<IUserAccountService>().InitializeApplicationPageBasedOnUser();
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
        /// Checks if there is a new version of that application
        /// </summary>
        private async Task<bool> CheckUpdatesAsync()
        {
            try
            {
                // Set webservice's url and parameters we want to send
                var url = "http://minorsonek.pl/testinator/data/index.php";
                var parameters = $"version={ DI.Application.Version.ToString() }&type=Server";

                // Catch the result
                var result = string.Empty;

                // Send request to webservice
                using (var wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    result = wc.UploadString(url, parameters);
                }

                // Return the statement based on result...
                switch (result)
                {
                    case "New update":
                        {
                            // There is new update, but not important one
                            // Ask the user if he wants to update
                            var vm = new DecisionDialogViewModel
                            {
                                Title = LocalizationResource.NewUpdate,
                                Message = LocalizationResource.NewVersionCanDownload,
                                AcceptText = LocalizationResource.Sure,
                                CancelText = LocalizationResource.SkipUpdate
                            };
                            await Framework.Service<IUIManager>().ShowMessage(vm);

                            // Depending on the answer...
                            return vm.UserResponse;
                        }

                    case "New update IMP":
                        {
                            // An important update, inform the user and update
                            await Framework.Service<IUIManager>().ShowMessage(new MessageBoxDialogViewModel
                            {
                                Title = LocalizationResource.NewImportantUpdate,
                                Message = LocalizationResource.NewImportantUpdateInfo,
                                OkText = LocalizationResource.Ok
                            });
                            return true;
                        }

                    default:
                        // No updates
                        return false;
                }
            }
            catch
            {
                // Cannot connect to the web, no updates
                return false;
            }
        }
    }
}
