using System;
using System.Net;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    /// <summary>
    /// Defines behaviour of a client in network
    /// </summary>
    public interface IClientNetwork
    {
        int Attempts { get; }
        int BufferSize { get; set; }
        bool CancellingConncetion { get; }
        IPAddress IPAddress { get; set; }
        string IPString { get; }
        bool IsConnected { get; }
        bool IsTryingToConnect { get; }
        int Port { get; set; }

        /// <summary>
        /// Connects to the remote server
        /// </summary>
        void ConnectAsync();

        /// <summary>
        /// Disconnects from the remote server if connected
        /// </summary>
        void Disconnect();
        void Initialize(IPAddress ip, int port);
        void Initialize(string ip, int port);

        /// <summary>
        /// Sends data to the remote server
        /// </summary>
        /// <param name="Data"></param>
        void SendData(DataPackage Data);

        /// <summary>
        /// Fired when there is data received from the remote server
        /// </summary>
        event Action<DataPackage> DataReceived;

        /// <summary>
        /// Fired when client establishes connection with the server
        /// </summary>
        event Action Connected;

        /// <summary>
        /// Fired when client is disconnected from the server
        /// </summary>
        event Action Disconnected;

        /// <summary>
        /// Fired when maximum connections attempt is reachd
        /// </summary>
        event Action AttemptsTimeout;

        /// <summary>
        /// Fired when attempting to connect to the server is completed
        /// </summary>
        event Action ConnectionFinished;

        /// <summary>
        /// Fired when <see cref="Attempts"/> counter updates 
        /// </summary>
        event Action AttemptCounterUpdated;

        void StopReconnecting();

        void SendClientModelUpdate();
    }
}
