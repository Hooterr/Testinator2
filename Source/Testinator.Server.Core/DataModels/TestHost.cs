﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Testinator.Core;

namespace Testinator.Server.Core
{
    /// <summary>
    /// Hosts the test at server side
    /// </summary>
    public class TestHost : BaseViewModel
    {
        #region Private Members 

        /// <summary>
        /// Private list of all clients that are taking the test, 
        /// NOTE: data to the clients is sent by using <see cref="ClientModel"/>, therefore this list is essential
        /// </summary>
        private List<ClientModel> _Clients = new List<ClientModel>();

        #endregion

        #region Public Properties

        /// <summary>
        /// The test that is currently hosted
        /// </summary>
        public Test Test { get; private set; }

        /// <summary>
        /// Indicates if there is any test in progress
        /// </summary>
        public bool IsTestInProgress { get; private set; }

        /// <summary>
        /// The time left for this test
        /// </summary>
        public TimeSpan TimeLeft { get; private set; }

        /// <summary>
        /// All clients that are currently taking the test
        /// </summary>
        public ObservableCollection<ClientModelExtended> Clients { get; private set; } = new ObservableCollection<ClientModelExtended>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the test
        /// </summary>
        public void Start()
        {
            if (IsTestInProgress)
                return;

            // Send package indicating the start of the test
            var data = new DataPackage(PackageType.BeginTest);
            SendToAllClients(data);

            IsTestInProgress = true;
        }

        /// <summary>
        /// Stops the test
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Sends test to the clients
        /// </summary>
        public void SendTest()
        {
            if (IsTestInProgress || Test == null)
                return;

            // Create the data package
            var dataPackage = new DataPackage(PackageType.TestForm)
            {
                Content = Test,
            };

            SendToAllClients(dataPackage);
        }

        /// <summary>
        /// Locks the client list that are currently taking the test
        /// </summary>
        public void LockClients()
        {
            if (IsTestInProgress)
                return;

            // Save all currently connected clients
            Clients = new ObservableCollection<ClientModelExtended>();
            _Clients = new List<ClientModel>();
            foreach (var client in IoCServer.Network.Clients)
            {
                _Clients.Add(client);
                Clients.Add(new ClientModelExtended(client));
            }
        }

        /// <summary>
        /// Binds a test object to this host
        /// </summary>
        /// <param name="test"></param>
        public void BindTest(Test test)
        {
            if (IsTestInProgress)
                return;

            Test = test;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Fired when any data is resived from a client
        /// </summary>
        /// <param name="client">The sender client</param>
        /// <param name="dataPackage">The data received from the client</param>
        public void OnDataRecived(ClientModel client, DataPackage dataPackage)
        {
            // If the data is from client we dont care about don't do anything
            if (!_Clients.Contains(client))
                return;

            switch (dataPackage.PackageType)
            {
                case PackageType.ReportStatus:
                    var content = dataPackage.Content as StatusPackage;
                    if (content.QuestionSolved)
                    {
                        int idx = _Clients.IndexOf(client);
                        Clients[idx].QuestionsDone++;
                    }

                    break;

                case PackageType.ResultForm:
                    // TODO: store the result
                    // Don't know what this will look like yet

                    break;                    
            }
        }

        /// <summary>
        /// Fired when a client disconnected from the server
        /// </summary>
        /// <param name="client">The client that has disconnected</param>
        public void OnClientDisconnected(ClientModel client)
        {
            // If the client that has disconnected is the one we dont care about don't do anything
            if (!_Clients.Contains(client))
                return;

            int idx = _Clients.IndexOf(client);

            // Indicate that we got connection problem with this client
            Clients[idx].ConnectionProblem = true;
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TestHost()
        {
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends data to all clients
        /// </summary>
        /// <param name="data">The data to be sent</param>
        private void SendToAllClients(DataPackage data)
        {
            // Send it to all clients
            foreach (var client in _Clients)
                IoCServer.Network.SendData(client, data);
        }

        #endregion

    }
}
