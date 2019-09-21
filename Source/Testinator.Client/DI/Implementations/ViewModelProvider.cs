using Dna;
using Testinator.Client.Domain;
using Testinator.Core;

namespace Testinator.Client
{
    public class ViewModelProvider : IViewModelProvider
    {
        public T GetInjectedPageViewModel<T>() where T : BaseViewModel
        {
            return Framework.Service<T>();
        }
    }
}
