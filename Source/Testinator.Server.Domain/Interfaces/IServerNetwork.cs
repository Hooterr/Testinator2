using Testinator.Core;

namespace Testinator.Server.Domain
{
    public interface IServerNetwork
    {
        System.Collections.ObjectModel.ObservableCollection<ClientModel> ConnectedClients { get; set; }

        event System.Action<ClientModel> OnClientConnected;
        event System.Action<ClientModel, ClientModel> OnClientDataUpdated;
        event System.Action<ClientModel> OnClientDisconnected;
        event System.Action<ClientModel, DataPackage> OnDataReceived;
    }
}