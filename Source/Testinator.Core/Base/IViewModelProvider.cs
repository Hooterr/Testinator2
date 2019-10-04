namespace Testinator.Core
{
    public interface IViewModelProvider
    {
        T GetInjectedPageViewModel<T>() where T : BaseViewModel;
    }
}
