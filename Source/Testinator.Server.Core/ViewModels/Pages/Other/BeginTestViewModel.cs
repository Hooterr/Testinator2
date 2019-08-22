using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// The view model for the begin test page
    /// </summary>
    public class BeginTestViewModel : BaseViewModel
    {
        #region Private Members

        private readonly TestHost mTestHost;
        private readonly ServerNetwork mServerNetwork;

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of connected clients
        /// </summary>
        public ObservableCollection<ClientModel> ClientsConnected => mServerNetwork.ConnectedClients;

        /// <summary>
        /// All clients that are currently taking the test
        /// </summary>
        public ObservableCollection<ClientModel> ClientsTakingTheTest => mTestHost.ClientsInTest;

        /// <summary>
        /// The test which is choosen by user on the list
        /// </summary>
        public Test CurrentTest => mTestHost.CurrentTest;

        /// <summary>
        /// The number of connected clients
        /// </summary>
        public int ClientsNumber => mServerNetwork.ConnectedClientsCount;

        /// <summary>
        /// The number of the questions in the test
        /// </summary>
        public int QuestionsCount => CurrentTest.Questions.Count;

        /// <summary>
        /// A flag indicating whether server has started
        /// </summary>
        public bool IsServerStarted => mServerNetwork.IsRunning;

        /// <summary>
        /// A flag indicating whether test has started
        /// </summary>
        public bool IsTestInProgress => mTestHost.IsTestInProgress;

        /// <summary>
        /// The time that is left to the end of the test
        /// </summary>
        public TimeSpan TimeLeft => mTestHost.TimeLeft;

        /// <summary>
        /// The server's ip
        /// </summary>
        public string ServerIpAddress => mServerNetwork.IPString;

        /// <summary>
        /// The server's port
        /// </summary>
        public string ServerPort { get; set; }

        /// <summary>
        /// Indicates if the result page should be allowed for the users
        /// </summary>
        public bool ResultPageAllowed { get; set; } = true;

        /// <summary>
        /// Indicates if the test should be held in fullscreen mode
        /// </summary>
        public bool FullScreenMode { get; set; } = false;

        #region Error flags

        /// <summary>
        /// Indicates if there is not enough clients to start the test
        /// </summary>
        public bool NotEnoughClients => ClientsNumber == 0;

        /// <summary>
        /// Indicates if the test is not selected
        /// </summary>
        public bool TestNotSelected => !TestListViewModel.Instance.IsAnySelected;

        /// <summary>
        /// Indicates if the test can be sent to the clients
        /// </summary>
        public bool CanSendTest => !NotEnoughClients && !TestNotSelected;

        #endregion

        #endregion

        #region Commands

        /// <summary>
        /// The command to start the server (allows clients to connect)
        /// </summary>
        public ICommand StartServerCommand { get; private set; }

        /// <summary>
        /// The command to stop the server (disable client connections)
        /// </summary>
        public ICommand StopServerCommand { get; private set; }

        /// <summary>
        /// The command to change subpage to test list page
        /// </summary>
        public ICommand ChangePageTestListCommand { get; private set; }

        /// <summary>
        /// The command to change subpage to test info page
        /// </summary>
        public ICommand ChangePageTestInfoCommand { get; private set; }

        /// <summary>
        /// The command to start choosen test
        /// </summary>
        public ICommand BeginTestCommand { get; private set; }

        /// <summary>
        /// The command to stop the test (test disappears completely)
        /// </summary>
        public ICommand StopTestCommand { get; private set; }
        
        /// <summary>
        /// The command to finish the test (results are collected before a timer ends)
        /// </summary>
        public ICommand FinishTestCommand { get; private set; }

        /// <summary>
        /// The command to exit from the resultpage
        /// </summary>
        public ICommand ResultPageExitCommand { get; private set; }

        /// <summary>
        /// The command to add a latecommer to the current test session
        /// </summary>
        public ICommand AddLateComerCommand { get; private set; }

        #endregion

        #region Construction/Destruction

        /// <summary>
        /// Default constructor
        /// </summary>
        public BeginTestViewModel(TestHost testHost, ServerNetwork serverNetwork)
        {
            // Inject DI services
            mTestHost = testHost;
            mServerNetwork = serverNetwork;

            // Create commands
            StartServerCommand = new RelayCommand(StartServer);
            StopServerCommand = new RelayCommand(StopServer);
            ChangePageTestListCommand = new RelayCommand(ChangePageList);
            ChangePageTestInfoCommand = new RelayCommand(ChangePageInfo);
            BeginTestCommand = new RelayCommand(BeginTest);
            StopTestCommand = new RelayCommand(StopTest);
            FinishTestCommand = new RelayCommand(FinishTest);
            ResultPageExitCommand = new RelayCommand(ResultPageExit);
            AddLateComerCommand = new RelayCommand(AddLatecomers);

            // Load every test from files
            TestListViewModel.Instance.LoadItems();

            // Keep the view up-to-date
            mTestHost.OnTimerUpdated += () => UpdateView();
            mServerNetwork.OnClientConnected += (s) => UpdateView();
            mServerNetwork.OnClientDisconnected += (s) => UpdateView();
            TestListViewModel.Instance.SelectionChanged += () => UpdateView();
            ServerPort = mServerNetwork.Port.ToString();
        }

        /// <summary>
        /// Disposes this viewmodel
        /// </summary>
        public override void Dispose()
        {
            // Unsub from events
            mTestHost.OnTimerUpdated -= () => UpdateView();
            mServerNetwork.OnClientConnected -= (s) => UpdateView();
            mServerNetwork.OnClientDisconnected -= (s) => UpdateView();
            TestListViewModel.Instance.SelectionChanged -= () => UpdateView();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Adds latecomer to the current test session
        /// </summary>
        private void AddLatecomers()
        {
            var CanStartTestClients = GetPossibleTestStartingClients();

            if (CanStartTestClients.Count == 0)
            {
                DI.UI.ShowMessage(new MessageBoxDialogViewModel()
                {
                    Message = "No latecomers to add!",
                    OkText = "OK",
                    Title = "Adding users to the test",
                });
            }
            else
            {
                var viewmodel = new AddLatecomersDialogViewModel(CanStartTestClients)
                {
                    Title = "Adding users to the test",
                    Message = "Select users that you want to add to the current test",
                    AcceptText = "Add",
                    CancelText = "Cancel",
                };

                DI.UI.ShowMessage(viewmodel);

                if (viewmodel.UserResponse.Count != 0)
                    mTestHost.AddLateComers(viewmodel.UserResponse);
            }
        }

        /// <summary>
        /// Starts the server
        /// </summary>
        private void StartServer()
        {
            // If port is not valid, dont start the server
            if (!NetworkHelpers.IsPortCorrect(ServerPort))
            {
                // Show a message box with info about it
                DI.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Niepoprawne dane!",
                    Message = "Niepoprawny port.",
                    OkText = "Ok"
                });
                return;
            }

            // Set network data
            mServerNetwork.Port = int.Parse(ServerPort);

            // Start the server
            mServerNetwork.Start();

            UpdateView();
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        private void StopServer()
        {
            // Check if any test is already in progress
            if (mTestHost.IsTestInProgress)
            {
                // Ask the user if he wants to stop the test
                var vm = new DecisionDialogViewModel()
                {
                    Title = "Test w trakcie!",
                    Message = "Test jest w trakcie. Czy chcesz go przerwać?",
                    AcceptText = "Tak",
                    CancelText = "Nie",
                };
                DI.UI.ShowMessage(vm);
                
                // If he agreed
                if (vm.UserResponse)
                    // Stop the test
                    StopTestForcefully();
                else
                    return;

                UpdateView();
            }
            
            // Stop the server
            mServerNetwork.ShutDown();

            UpdateView();

            // Go to the initial page
            DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestInitial);
        }

        /// <summary>
        /// Changes the subpage to test choose page
        /// </summary>
        private void ChangePageList()
        {
            // If server isnt started, dont change the page
            if (!IsServerStarted)
            {
                // Show a message box with info about it
                DI.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Serwer nie włączony!",
                    Message = "By zmienić stronę, należy przedtem włączyć serwer.",
                    OkText = "Ok"
                });
                return;
            }

            // Simply go to target page
            DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestChoose);
        }

        /// <summary>
        /// Changes the subpage to test info page
        /// </summary>
        private void ChangePageInfo()
        {
            // Meanwhile lock the clients list and send them the test 
            mTestHost.AddClients(mServerNetwork.ConnectedClients.ToList());

            // Add selected test
            mTestHost.AddTest(TestListViewModel.Instance.SelectedItem);

            // If there is no enough users to start the test, show the message and don't send the test
            if (mTestHost.ClientsInTest.Count == 0)
            {
                DI.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    Title = "Brak użytkowników",
                    Message = "Nie można ropocząć testu. Brak użytkowników, którzy mogą go rozpocząć.",
                    OkText = "OK"
                });

                return;
            }

            mTestHost.SendTestToAll();

            // Then go to the info page
            DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestInfo);
        }

        /// <summary>
        /// Starts the test
        /// </summary>
        private void BeginTest()
        {
            // Send the args before startting
            mTestHost.ConfigureStartup(new TestOptions()
            {
                FullScreenEnabled = FullScreenMode,
                ResultsPageAllowed = ResultPageAllowed,
            });

            mTestHost.TestStart();
            DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestInProgress);
        }

        /// <summary>
        /// Stops the test (test disappears completely, like it didn't even happened)
        /// </summary>
        private void StopTest()
        {
            // Ask the user if he wants to stop the test
            var vm = new DecisionDialogViewModel()
            {
                Title = "Przerywanie testu",
                Message = "Czy na pewno chcesz przerwać test?",
                AcceptText = "Tak",
                CancelText = "Nie",
            };
            DI.UI.ShowMessage(vm);

            // If his will match
            if (vm.UserResponse)
                // Stop the test
                StopTestForcefully();
        }

        /// <summary>
        /// Finishes the test before a timer ends, results are collected here
        /// </summary>
        private void FinishTest()
        {
            // TODO: Get the results from users (even empty ones)
            //       Then finish the test etc.
        }

        /// <summary>
        /// Exits from the result page
        /// </summary>
        private void ResultPageExit()
        {
            // Go back to the main begin test page
            DI.Application.GoToPage(ApplicationPage.BeginTest);

            // Change the mini-page accordingly
            if (mServerNetwork.IsRunning)
                DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestChoose);
            else
                DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestInitial);
        }

        #endregion

        #region Private Event Methods

        /// <summary>
        /// Updates the view and all the properties
        /// </summary>
        private void UpdateView()
        {
            OnPropertyChanged(nameof(ClientsNumber));
            OnPropertyChanged(nameof(NotEnoughClients));
            OnPropertyChanged(nameof(CanSendTest));
            OnPropertyChanged(nameof(TestNotSelected));
            OnPropertyChanged(nameof(CanSendTest));
            OnPropertyChanged(nameof(TimeLeft));
            OnPropertyChanged(nameof(TestNotSelected));
            OnPropertyChanged(nameof(IsServerStarted));
        }

        /// <summary>
        /// Stops the test forcefully
        /// </summary>
        private void StopTestForcefully()
        {
            mTestHost.AbortTest();
            DI.Application.GoToBeginTestPage(ApplicationPage.BeginTestInitial);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets all the clients that can possibly start a test
        /// </summary>
        /// <returns>All the clients that can start a test right now</returns>
        private List<ClientModel> GetPossibleTestStartingClients()
        {
            var CanStartTestClients = new List<ClientModel>();

            foreach(var client in mServerNetwork.ConnectedClients)
                if (client.CanStartTest)
                    CanStartTestClients.Add(client);

            return CanStartTestClients;
        }

        #endregion
    }
}