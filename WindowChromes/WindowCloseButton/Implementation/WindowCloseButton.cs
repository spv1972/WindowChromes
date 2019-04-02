using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace WindowChromes
{
    /// <inheritdoc />
    /// <summary>
    /// Close button for Window
    /// </summary>
    public class WindowCloseButton : ButtonBase
    {
        private static readonly Type OwnerType = typeof(WindowCloseButton);

        #region ThemeStyle

        public static DependencyProperty ThemeStyleProperty =
            DependencyProperty.Register("ThemeStyle", typeof(ThemeStyle), OwnerType, new PropertyMetadata(ThemeStyle.Default));


        public ThemeStyle ThemeStyle
        {
            get => (ThemeStyle)GetValue(ThemeStyleProperty);
            set => SetValue(ThemeStyleProperty, value);
        }

        #endregion

        #region InactiveBackground
        public static DependencyProperty InactiveBackgroundProperty =
            DependencyProperty.Register("InactiveBackground", typeof(Brush), OwnerType);

        public Brush InactiveBackground
        {
            get => (Brush)GetValue(InactiveBackgroundProperty);
            set => SetValue(InactiveBackgroundProperty, value);
        }
        #endregion

        #region InactiveForeground
        public static DependencyProperty InactiveForegroundProperty =
            DependencyProperty.Register("InactiveForeground", typeof(Brush), OwnerType);

        public Brush InactiveForeground
        {
            get => (Brush)GetValue(InactiveForegroundProperty);
            set => SetValue(InactiveForegroundProperty, value);
        }
        #endregion

        #region HoverBackground
        public static DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), OwnerType);

        public Brush HoverBackground
        {
            get => (Brush)GetValue(HoverBackgroundProperty);
            set => SetValue(HoverBackgroundProperty, value);
        }
        #endregion

        #region PressedBackground
        public static DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), OwnerType);

        public Brush PressedBackground
        {
            get => (Brush)GetValue(PressedBackgroundProperty);
            set => SetValue(PressedBackgroundProperty, value);
        }
        #endregion

        #region HoverForeground
        public static DependencyProperty HoverForegroundProperty =
            DependencyProperty.Register("HoverForeground", typeof(Brush), OwnerType);

        public Brush HoverForeground
        {
            get => (Brush)GetValue(HoverForegroundProperty);
            set => SetValue(HoverForegroundProperty, value);
        }
        #endregion

        #region PressedForeground
        public static DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), OwnerType);

        public Brush PressedForeground
        {
            get => (Brush)GetValue(PressedForegroundProperty);
            set => SetValue(PressedForegroundProperty, value);
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


        #region constructors
        static WindowCloseButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(OwnerType, new FrameworkPropertyMetadata(OwnerType));
        }

        public WindowCloseButton()
        {
            //var rd=new ResourceDictionary();
            // rd.Source=new Uri("WindowChromes;component/Themes/Generic.xaml",UriKind.Relative);
        }

        private bool _isAttachCommandBindings;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            if (_isAttachCommandBindings) return;

            var window = this.TryFindParent<Window>();
            if (window == null) return;

            _isAttachCommandBindings = true;

            window.CommandBindings.Add(new CommandBinding(ButtonCommands.MouseDownDragCommand, ButtonCommands.MouseDownDragCommand_Execute));
            window.CommandBindings.Add(new CommandBinding(ButtonCommands.CloseCommand, ButtonCommands.CloseCommand_Execute));
        }

        #endregion
    }
}
