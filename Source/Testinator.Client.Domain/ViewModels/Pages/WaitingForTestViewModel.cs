using System;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    /// <summary>
    /// The view model for the "waiting for test" page
    /// </summary>
    public class WaitingForTestViewModel : BaseViewModel
    {
        #region Private Members

        private readonly ITestHost mTestHost;
        private readonly IClientModel mClientModel;
        private readonly IClientNetwork mClientNetwork;

        #endregion

        #region Public Properties

        /// <summary>
        /// The current users name
        /// </summary>
        public TextEntryViewModel Name { get; set; }

        /// <summary>
        /// The current users surname
        /// </summary>
        public TextEntryViewModel Surname { get; set; }

        /// <summary>
        /// The name of the test
        /// </summary>
        public string TestName => mTestHost.IsTestReceived ? mTestHost.CurrentTest.Info.Name : "";

        /// <summary>
        /// The duration of the test
        /// </summary>
        public TimeSpan TestDuration => mTestHost.IsTestReceived ? mTestHost.CurrentTest.Info.Duration : TimeSpan.Zero;

        /// <summary>
        /// How much score can user get from this test
        /// </summary>
        public int TestPossibleScore => mTestHost.IsTestReceived ? mTestHost.CurrentTest.TotalPointScore : 0;

        /// <summary>
        /// A flag indicating if we have any test to show,
        /// to show corresponding content in the WaitingPage
        /// </summary>
        public bool IsTestReceived => mTestHost.IsTestReceived;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WaitingForTestViewModel(ITestHost testHost, IClientModel clientModel, IClientNetwork clientNetwork)
        {
            // Inject DI services
            mTestHost = testHost;
            mClientModel = clientModel;
            mClientNetwork = clientNetwork;

            // Set input data
            Name = new TextEntryViewModel { Label = "Imię", OriginalText = mClientModel.Name };
            Surname = new TextEntryViewModel { Label = "Nazwisko", OriginalText = mClientModel.LastName };

            // Listen out for test which will come from server
            mTestHost.OnTestReceived += Update;

            // Hook to the events
            Name.ValueChanged += Name_ValueChanged;
            Surname.ValueChanged += Surname_ValueChanged;
        }

        #endregion

        #region Private Events

        /// <summary>
        /// Name value changes
        /// </summary>
        private void Name_ValueChanged()
        {
            mClientModel.Name = Name.OriginalText;
            mClientNetwork.SendClientModelUpdate();
        }

        /// <summary>
        /// Surname value changes
        /// </summary>
        private void Surname_ValueChanged()
        {
            mClientModel.LastName = Surname.OriginalText;
            mClientNetwork.SendClientModelUpdate();
        }

        #endregion

        #region Public Helpers

        /// <summary>
        /// Updates this view model. As it's properties are bound to the IoC, changing 
        /// something in IoC doesn't fire PropertyChanged event in this view model.
        /// Therefore, properties need to be updated manually
        /// </summary>
        public void Update()
        {
            OnPropertyChanged(nameof(IsTestReceived));
            OnPropertyChanged(nameof(TestName));
            OnPropertyChanged(nameof(TestDuration));
            OnPropertyChanged(nameof(TestPossibleScore));
        }

        #endregion
    }
}
