using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WindowChromes
{

    /// <inheritdoc />
    /// <summary>
    /// Size, Close, Close buttons for Window
    /// </summary>
    public class WindowButtons : WindowButtonsBase
    {

        private static readonly Type OwnerType = typeof(WindowButtons);


        #region constructors

        static WindowButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(OwnerType, new FrameworkPropertyMetadata(OwnerType));
        }

        public WindowButtons()
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
            window.CommandBindings.Add(new CommandBinding(ButtonCommands.MinimizeCommand, ButtonCommands.MinimizeCommand_Execute));
            window.CommandBindings.Add(new CommandBinding(ButtonCommands.RestoreCommand, ButtonCommands.RestoreCommand_Execute));
            window.CommandBindings.Add(new CommandBinding(ButtonCommands.CloseCommand, ButtonCommands.CloseCommand_Execute));        
        }

        #endregion
    }
}
