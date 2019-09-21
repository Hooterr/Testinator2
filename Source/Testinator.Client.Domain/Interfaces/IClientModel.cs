using System.Net;
using Testinator.Core;

namespace Testinator.Client.Domain
{
    public interface IClientModel
    {
        string Name { get; set; }
        string LastName { get; set; }
        string MachineName { get; set; }
        IPAddress IP { get; set; }

        DataPackage GetPackage();
    }
}
