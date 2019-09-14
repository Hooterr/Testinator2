using Testinator.Core;

namespace Testinator.Client.Domain
{
    public interface IViewModelProvider
    {
        T GetInjectedPageViewModel<T>() where T : BaseViewModel;
    }
}
