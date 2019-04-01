using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WindowChromes
{
    public class TitleBar : Control
    {

        private static readonly Type OwnerType = typeof(TitleBar);


        #region Attached property IsTransparent

        /// <summary>
        /// Attached types is Window and TitleBar, that setting in xaml
        /// </summary>
        public static readonly DependencyProperty IsTransparentProperty =
            DependencyProperty.RegisterAttached(
                "IsTransparent",
                typeof(bool),
                OwnerType,
                new FrameworkPropertyMetadata(false, OnlyClosedModePropertyChangedCallback));


        public static bool GetIsTransparent(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTransparentProperty);
        }


        public static void SetIsTransparent(DependencyObject obj, TitleBar value)
        {
            obj.SetValue(IsTransparentProperty, value);
        }


        private static void OnlyClosedModePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (!(d is TitleBar tb)) return;
            if (!(bool)e.NewValue) return;

            tb.Background = Brushes.Transparent;
            if (!(tb.Buttons is WindowButtons buttons)) return;

            buttons.Background = Brushes.Transparent;
            buttons.ButtonsBackground = Brushes.Transparent;
        }



        #endregion


        #region Attached property Icon

        /// <summary>
        /// Attached types is Window and TitleBar, that setting in xaml
        /// </summary>

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(Uri),
                OwnerType);


        public static Uri GetIcon(DependencyObject obj)
        {
            return (Uri)obj.GetValue(IconProperty);
        }


        public static void SetIcon(DependencyObject obj, Uri value)
        {
            obj.SetValue(IconProperty, value);
        }


        #endregion

        #region Attached property Title

        /// <summary>
        /// Attached types is Window and TitleBar, that setting in xaml
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached(
                "Title",
                typeof(string),
                OwnerType);


        public static string GetTitle(DependencyObject obj)
        {
            return (string)obj.GetValue(TitleProperty);
        }


        public static void SetTitle(DependencyObject obj, string value)
        {
            obj.SetValue(TitleProperty, value);
        }
        


        #endregion

       


        #region Dependency property ThemeStyle

        public static DependencyProperty ThemeStyleProperty =
            DependencyProperty.Register("ThemeStyle", typeof(ThemeStyle), OwnerType, new PropertyMetadata(ThemeStyle.Default));


        public ThemeStyle ThemeStyle
        {
            get => (ThemeStyle)GetValue(ThemeStyleProperty);
            set => SetValue(ThemeStyleProperty, value);
        }

        #endregion

        #region Dependency property IconSize
        public static DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), OwnerType, new PropertyMetadata(16.0));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }


        #endregion


        public object Buttons { set; get; }

        public object Text { set; get; }

        public object ImageIcon { set; get; }


        #region constructors

        static TitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(OwnerType, new FrameworkPropertyMetadata(OwnerType));
        }


        public TitleBar()
        {
            //default buttons
            var windowButtons=new WindowButtons();
            var binding = new Binding("ThemeStyle") {Source = this};
            windowButtons.SetBinding(WindowButtons.ThemeStyleProperty, binding);
            Buttons = windowButtons;

            //default title
            var textBlock=new TextBlock();
            var tbBinding=new Binding("Title") { Source = this };
            textBlock.SetBinding(TextBlock.TextProperty, tbBinding);
            var fbBinding =new Binding("Foreground"){Source = this};
            textBlock.SetBinding(TextBlock.ForegroundProperty, fbBinding);
            Text = textBlock;

            
            //default Icon
            var imageIcon=new ImageIcon();
            var uBinding=new Binding("Icon") {Source = this};
            imageIcon.SetBinding(WindowChromes.ImageIcon.IconProperty, uBinding);
         
            var hBinding=new Binding("IconSize") {Source = this};
            imageIcon.SetBinding(FrameworkElement.HeightProperty, hBinding);
            var wBinding = new Binding("IconSize") { Source = this };
            imageIcon.SetBinding(FrameworkElement.WidthProperty, wBinding);
            imageIcon.VerticalAlignment = VerticalAlignment.Center;
            ImageIcon = imageIcon;

            
        }

        #endregion

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            //edit IconSize
            if (ImageIcon is FrameworkElement fe)
            {
                fe.Height = IconSize;
                fe.Width = IconSize;
            }

            //edit icon position
            var image = Template.FindName("PartIconPresenter", this) as ContentPresenter;
            if(image==null) return;
          
            var top = (this.RealHeight() - IconSize) / 2;
            if (top < 3) top = 3;
            Canvas.SetTop(image, top);
            

        }
    }
}
