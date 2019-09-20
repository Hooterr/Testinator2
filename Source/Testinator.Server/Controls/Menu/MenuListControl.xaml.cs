using System.Windows.Controls;
using Testinator.Server.Domain;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for MenuListControl.xaml
    /// </summary>
    public partial class MenuListControl : UserControl
    {
        public MenuListControl()
        {
            InitializeComponent();
            DataContext = DI.GetInjectedPageViewModel<MenuListViewModel>();
        }
    }
}
