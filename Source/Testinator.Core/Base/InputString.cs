using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Testinator.Core
{
    public class InputField<T> : BaseViewModel
    {
        public T Value { get; set; }
        public ObservableCollection<string> ErrorMessages { get; set; }

        public InputField(T value)
        {
            Value = value;
            ErrorMessages = new ObservableCollection<string>();
        }

        public static implicit operator T(InputField<T> d)
        {
            return d != null ? d.Value : default;
        }

        public static implicit operator InputField<T>(T d)
        {
            return new InputField<T>(d);
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : string.Empty;
        }
    }
}
