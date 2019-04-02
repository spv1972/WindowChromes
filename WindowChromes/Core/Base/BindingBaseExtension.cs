using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WindowChromes
{
    /// <inheritdoc />
    /// <summary>
    /// Base abstract class for Binding methods
    /// </summary>
    [MarkupExtensionReturnType(typeof(object))]
    public abstract class BindingBaseExtension : MarkupExtension
    {
        protected BindingBaseExtension()
        {

        }

        protected BindingBaseExtension(PropertyPath path)
        {
            Path = path;
        }

        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        public IValueConverter Converter { get; set; }
        public object ConverterParameter { get; set; }
        public string ElementName { get; set; }
        public RelativeSource RelativeSource { get; set; }
        public object Source { get; set; }
        public bool ValidatesOnDataErrors { get; set; }
        public bool ValidatesOnExceptions { get; set; }
        public BindingMode Mode { set; get; } = BindingMode.Default;// .TwoWay=0, may by not work on readonly path property
        public UpdateSourceTrigger UpdateSourceTrigger { set; get; } = UpdateSourceTrigger.Default; //.Explicit=0
        [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
        public CultureInfo ConverterCulture { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var binding = new Binding
            {
                Path = Path,
                Converter = Converter,
                ConverterCulture = ConverterCulture,
                ConverterParameter = ConverterParameter,
                ValidatesOnDataErrors = ValidatesOnDataErrors,
                ValidatesOnExceptions = ValidatesOnExceptions,
                Mode = Mode,
                UpdateSourceTrigger = UpdateSourceTrigger
            };


            if (ElementName != null)
                binding.ElementName = ElementName;
            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;
            if (Source != null)
                binding.Source = Source;

            return binding.ProvideValue(serviceProvider);
        }
    }
}
