using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Testinator.Server
{
    /// <summary>
    /// Interaction logic for GradeEditableControl.xaml
    /// </summary>
    public partial class ErrorListControl : UserControl
    {

        public ObservableCollection<string> Errors
        {
            get => (ObservableCollection<string>)GetValue(ErrorsProperty);
            set => SetValue(ErrorsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Errors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorsProperty =
            DependencyProperty.Register(nameof(Errors), typeof(ObservableCollection<string>), typeof(ErrorListControl), new PropertyMetadata(new ObservableCollection<string>()));



        public ErrorListControl()
        {
            InitializeComponent();

            DataContext = Errors;
        }
    }
}
