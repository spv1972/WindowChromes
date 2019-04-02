using System.Windows;
using System.Windows.Input;

namespace WindowChromes
{
    /// <summary>
    /// Commands and Execute handlers
    /// for Window buttons
    /// </summary>
    public static class ButtonCommands
    {
        #region Commands

        /// <summary>
        /// Command for double click and drag event
        /// </summary>
        public static RoutedCommand MouseDownDragCommand = new RoutedCommand();

        public static RoutedCommand MinimizeCommand = new RoutedCommand();
        public static RoutedCommand RestoreCommand = new RoutedCommand();
        public static RoutedCommand CloseCommand = new RoutedCommand();
        #endregion

        #region Executers 

        public static void MouseDownDragCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)sender;
            void ChangeViewState()
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }

            var args = (MouseButtonEventArgs)e.Parameter;
            if (args.ClickCount == 2) ChangeViewState();

            if (MouseInput.IsLeftButtonPressed)
                window.DragMove();
        }

        public static void MinimizeCommand_Execute(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            window.WindowState = WindowState.Minimized;

        }

        public static void RestoreCommand_Execute(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            window.WindowState = (window.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        public static void CloseCommand_Execute(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            window.Close();
        }

        #endregion
    }
}
