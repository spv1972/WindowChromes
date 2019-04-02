using System.Windows;

namespace WindowChromes
{
    /// <inheritdoc />
    /// <summary>
    /// Binding with RatioConverter
    /// </summary>
    public class RatioBindingExtension : BindingBaseExtension
    {
        public RatioBindingExtension()
        {
        }

        public RatioBindingExtension(PropertyPath path) : base(path)
        {

            Converter = new RatioConverter();
            ConverterParameter = Ratio;
        }

        private object _ratio;
        public object Ratio
        {
            set
            {
                _ratio = value;
                Converter = new RatioConverter();
                ConverterParameter = Ratio;
            }
            get => _ratio;
        }
    }
}
