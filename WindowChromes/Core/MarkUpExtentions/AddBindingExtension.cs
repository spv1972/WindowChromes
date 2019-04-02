using System.Windows;

namespace WindowChromes
{
    /// <inheritdoc/>
    /// <summary>
    /// Binding with AddConverter
    /// </summary>
    public class AddBindingExtension : BindingBaseExtension
    {

        public AddBindingExtension()
        {
        }

        public AddBindingExtension(PropertyPath path) : base(path)
        {

            Converter = new AddConverter();
            ConverterParameter = Addend;
        }

        private object _addend;
        public object Addend
        {
            set
            {
                _addend = value;
                Converter = new AddConverter();
                ConverterParameter = Addend;
            }
            get => _addend;
        }
    }
}
