using System.Windows;
using System.Windows.Input;
using WindowChromes;

namespace AcrylicChrome_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AeroGlassWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;

            var isBlurred = (bool)GetValue(AcrylicChrome.IsBlurredProperty);
            SetValue(AcrylicChrome.IsBlurredProperty, !isBlurred);
        }

    }
}
