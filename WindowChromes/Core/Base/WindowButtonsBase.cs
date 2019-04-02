using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WindowChromes
{
    /// <inheritdoc />
    /// <summary>
    /// Base  abstract class for WindowButtons
    /// </summary>
    public abstract class WindowButtonsBase : Control
    {
        private static readonly Type OwnerType = typeof(WindowButtonsBase);

        #region Depencincy properties

        #region ThemeStyle

        public static DependencyProperty ThemeStyleProperty =
            DependencyProperty.Register("ThemeStyle", typeof(ThemeStyle), OwnerType, new PropertyMetadata(ThemeStyle.Default));


        public ThemeStyle ThemeStyle
        {
            get => (ThemeStyle)GetValue(ThemeStyleProperty);
            set => SetValue(ThemeStyleProperty, value);
        }

        #endregion


        #region ButtonsBackground
        public static DependencyProperty ButtonsBackgroundProperty =
            DependencyProperty.Register("ButtonsBackground", typeof(Brush), OwnerType);

        public Brush ButtonsBackground
        {
            get => (Brush)GetValue(ButtonsBackgroundProperty);
            set => SetValue(ButtonsBackgroundProperty, value);
        }
        #endregion

        #region ButtonsForeground
        public static DependencyProperty ButtonsForegroundProperty =
            DependencyProperty.Register("ButtonsForeground", typeof(Brush), OwnerType);

        public Brush ButtonsForeground
        {
            get => (Brush)GetValue(ButtonsForegroundProperty);
            set => SetValue(ButtonsForegroundProperty, value);
        }
        #endregion

        #region ButtonsHoverBackground
        public static DependencyProperty ButtonsHoverBackgroundProperty =
            DependencyProperty.Register("ButtonsHoverBackground", typeof(Brush), OwnerType);

        public Brush ButtonsHoverBackground
        {
            get => (Brush)GetValue(ButtonsHoverBackgroundProperty);
            set => SetValue(ButtonsHoverBackgroundProperty, value);
        }
        #endregion

        #region ButtonsHoverForeground
        public static DependencyProperty ButtonsHoverForegroundProperty =
            DependencyProperty.Register("ButtonsHoverForeground", typeof(Brush), OwnerType);

        public Brush ButtonsHoverForeground
        {
            get => (Brush)GetValue(ButtonsHoverForegroundProperty);
            set => SetValue(ButtonsHoverForegroundProperty, value);
        }
        #endregion

        #region ButtonsPressedForeground
        public static DependencyProperty ButtonsPressedForegroundProperty =
            DependencyProperty.Register("ButtonsPressedForeground", typeof(Brush), OwnerType);

        public Brush ButtonsPressedForeground
        {
            get => (Brush)GetValue(ButtonsPressedForegroundProperty);
            set => SetValue(ButtonsPressedForegroundProperty, value);
        }
        #endregion

        #region ButtonsPressedBackground
        public static DependencyProperty ButtonsPressedBackgroundProperty =
            DependencyProperty.Register("ButtonsPressedBackground", typeof(Brush), OwnerType);

        public Brush ButtonsPressedBackground
        {
            get => (Brush)GetValue(ButtonsPressedBackgroundProperty);
            set => SetValue(ButtonsPressedBackgroundProperty, value);
        }
        #endregion

        #region ButtonsInactiveBackground
        public static DependencyProperty ButtonsInactiveBackgroundProperty =
            DependencyProperty.Register("ButtonsInactiveBackground", typeof(Brush), OwnerType);

        public Brush ButtonsInactiveBackground
        {
            get => (Brush)GetValue(ButtonsInactiveBackgroundProperty);
            set => SetValue(ButtonsInactiveBackgroundProperty, value);
        }
        #endregion

        #region ButtonsInactiveForeground
        public static DependencyProperty ButtonsInactiveForegroundProperty =
            DependencyProperty.Register("ButtonsInactiveForeground", typeof(Brush), OwnerType);

        public Brush ButtonsInactiveForeground
        {
            get => (Brush)GetValue(ButtonsInactiveForegroundProperty);
            set => SetValue(ButtonsInactiveForegroundProperty, value);
        }
        #endregion

        
        #region CloseButtonHoverBackground
        public static DependencyProperty CloseButtonHoverBackgroundProperty =
            DependencyProperty.Register("CloseButtonHoverBackground", typeof(Brush), OwnerType);

        public Brush CloseButtonHoverBackground
        {
            get => (Brush)GetValue(CloseButtonHoverBackgroundProperty);
            set => SetValue(CloseButtonHoverBackgroundProperty, value);
        }
        #endregion

        #region CloseButtonPressedBackground
        public static DependencyProperty CloseButtonPressedBackgroundProperty =
            DependencyProperty.Register("CloseButtonPressedBackground", typeof(Brush), OwnerType);

        public Brush CloseButtonPressedBackground
        {
            get => (Brush)GetValue(CloseButtonPressedBackgroundProperty);
            set => SetValue(CloseButtonPressedBackgroundProperty, value);
        }
        #endregion

        #region CloseButtonHoverForeground
        public static DependencyProperty CloseButtonHoverForegroundProperty =
            DependencyProperty.Register("CloseButtonHoverForeground", typeof(Brush), OwnerType);

        public Brush CloseButtonHoverForeground
        {
            get => (Brush)GetValue(CloseButtonHoverForegroundProperty);
            set => SetValue(CloseButtonHoverForegroundProperty, value);
        }
        #endregion

        #region CloseButtonPressedForeground
        public static DependencyProperty CloseButtonPressedForegroundProperty =
            DependencyProperty.Register("CloseButtonPressedForeground", typeof(Brush), OwnerType);

        public Brush CloseButtonPressedForeground
        {
            get => (Brush)GetValue(CloseButtonPressedForegroundProperty);
            set => SetValue(CloseButtonPressedForegroundProperty, value);
        }
        #endregion

        #region CloseButtonTone
        public static DependencyProperty CloseButtonToneProperty =
            DependencyProperty.Register("CloseButtonTone", typeof(CloseButtonTone), OwnerType);

        public CloseButtonTone CloseButtonTone
        {
            get => (CloseButtonTone)GetValue(CloseButtonToneProperty);
            set => SetValue(CloseButtonToneProperty, value);
        }
        #endregion

        #region ButtonsWidth
        [TypeConverter(typeof(LengthConverter))]
        public static DependencyProperty ButtonsWidthProperty =
            DependencyProperty.Register("ButtonsWidth", typeof(double), OwnerType, new PropertyMetadata(double.NaN));


        [TypeConverter(typeof(LengthConverter))]
        public double ButtonsWidth
        {
            get => (double)GetValue(ButtonsWidthProperty);
            set => SetValue(ButtonsWidthProperty, value);
        }
        #endregion

        #region SymbolFontSize
        [TypeConverter(typeof(LengthConverter))]
        public static DependencyProperty SymbolFontSizeProperty =
            DependencyProperty.Register("SymbolFontSize", typeof(double), OwnerType, new PropertyMetadata(double.NaN));


        [TypeConverter(typeof(LengthConverter))]
        public double SymbolFontSize
        {
            get => (double)GetValue(SymbolFontSizeProperty);
            set => SetValue(SymbolFontSizeProperty, value);
        }
        #endregion

        #region RealHeight
        public double RealHeight => !double.IsNaN(Height) ? Height : ActualHeight;
        #endregion

        #region CloseButtonTone
        public static DependencyProperty ResizeModeProperty =
            DependencyProperty.Register("ResizeMode", typeof(ResizeMode), OwnerType, new PropertyMetadata(ResizeMode.CanResize));

        public ResizeMode ResizeMode
        {
            get => (ResizeMode)GetValue(ResizeModeProperty);
            set => SetValue(ResizeModeProperty, value);
        }
        #endregion

        #endregion
       
    }
}
