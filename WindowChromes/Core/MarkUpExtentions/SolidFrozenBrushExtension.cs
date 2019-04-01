using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace WindowChromes
{
    /// <inheritdoc cref="MarkupExtension"/>
    /// <summary>
    /// Return new SolidColorBrush {IsFrozen = true} object.
    /// Code alternative Presentation:Freeze xaml definition.
    /// </summary>
    [MarkupExtensionReturnType(typeof(SolidColorBrush))]
    internal class SolidFrozenBrushExtension : MarkupExtension
    {

        public SolidFrozenBrushExtension()
        {

        }

        public SolidFrozenBrushExtension(Color color)
        {
            Color = color;
        }

        [ConstructorArgument("color")]
        public Color Color { set; get; }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var brush = new SolidColorBrush(Color);
            brush.Freeze();
            return brush;
        }
    }
}
