using System.Windows.Controls;
using Testinator.Server.Domain;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for ImagesEditorControl.xaml
    /// </summary>
    public partial class ImagesEditorControl : UserControl
    {
        public ImagesEditorControl()
        {
            InitializeComponent();
            DataContext = DI.GetInjectedPageViewModel<ImagesEditorViewModel>();
        }
    }
}
