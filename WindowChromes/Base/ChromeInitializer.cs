using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace WindowChromes
{
    /// <inheritdoc />
    /// <summary>
    /// Initialize Window HWndSource by Hook WndProc method
    /// </summary>
    internal class ChromeInitializer : DependencyObject
    {
        private static readonly Type OwnerType = typeof(ChromeInitializer);

        internal delegate IntPtr WindowMessageHandler(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);

        internal Dictionary<WM, WindowMessageHandler> _messageDictionary = new Dictionary<WM, WindowMessageHandler>() ;

        /// <summary>
        /// current Window instance
        /// </summary>
        private Window _window;

        /// <summary>Underlying HWndSource for the _window.</summary>
        private HwndSource _source;

        /// <summary>Underlying HWnd for the _window.</summary>
        private IntPtr _hWnd;

        /// <summary>
        ///  Attache WndProc flag
        /// </summary>
        private bool _isHooked;

        private CompositeChrome _chromeBase;

        /// <summary>
        ///Current Window Chrome
        /// </summary>
        public CompositeChrome ChromeBase
        {
            set
            {
                _chromeBase = value;
                if (_hWnd != IntPtr.Zero)
                {
                    _chromeBase.HWndSource = _source;
                }

            }
            get => _chromeBase;
        }

        #region Attached property AeroGlassChrome

        [DefaultValue("Null")]
        public static readonly DependencyProperty ChromeInitializerProperty =
            DependencyProperty.RegisterAttached(
                "ChromeInitializer",
                OwnerType,
                OwnerType,
                new FrameworkPropertyMetadata(null, ChromeInitializerPropertyChanged));

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static ChromeInitializer GetChromeInitializer(Window obj)
        {
            return (ChromeInitializer)obj.GetValue(ChromeInitializerProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Window))]
        public static void SetChromeInitializer(Window obj, ChromeInitializer value)
        {
            obj.SetValue(ChromeInitializerProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(Window))]
        private static void ChromeInitializerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (!(d is Window window)) throw new ArgumentException("d must be Window");
            if(!(e.NewValue is ChromeInitializer ch)) throw new ArgumentNullException();

            ch.Initialize(window);

        }

        private void Initialize(Window window)
        {
            _window = window;
            _source = PresentationSource.FromVisual(_window) as HwndSource;

            if (_source == null) 
            {
                _window.SourceInitialized += Window_SourceInitialized;
                return;
            }

            _hWnd = _source.Handle;

            if (_chromeBase != null) ChromeBase.HWndSource = _source;

            if (_isHooked) return;

            NativeMethods.SetInvisibleNonClientArea(_hWnd);

           if (_source.CompositionTarget != null)
               _source.CompositionTarget.BackgroundColor = Colors.Transparent;


            _source.AddHook(WndProc);
            _isHooked = true;
        }

        #endregion

       
        // constructor
        public ChromeInitializer()
        {
            _messageDictionary.Add(WM.NCACTIVATE, _HandleNCActivate);
            _messageDictionary.Add(WM.NCCALCSIZE, _HandleNCCalcSize);
            _messageDictionary.Add(WM.NCHITTEST, _HandleNCHitTest);
            _messageDictionary.Add(WM.NCRBUTTONUP, _HandleNCRButtonUp);
            _messageDictionary.Add(WM.WINDOWPOSCHANGED, _HandleWindowPosChanged);
            _messageDictionary.Add(WM.DWMCOMPOSITIONCHANGED, _HandleDwmCompositionChanged);
        }

        public IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var message = (WM) msg;
            foreach (var wm in _messageDictionary.Keys)
            {
                if (wm == message)
                {
                    return _messageDictionary[wm](hWnd, message, wParam, lParam, out handled);
                }
            }
            return IntPtr.Zero;
        }


        #region Handlers

        private IntPtr _HandleNCActivate(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            // Directly call DefWindowProc with a custom parameter
            var lRet = NativeMethods.DefWindowProc(hWnd, WM.NCACTIVATE, wParam, new IntPtr(-1));
            handled = true;
            return lRet;
        }

        private IntPtr _HandleNCCalcSize(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            handled = true;

            // Per MSDN for NCCALCSIZE, always return 0, when wParam == FALSE
            var retVal = IntPtr.Zero;
            if (wParam.ToInt32() != 0) // wParam == TRUE
            {
                retVal = new IntPtr((int)(WVR.REDRAW));
            }

            return retVal;
        }

        private IntPtr _HandleNCHitTest(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            if (_source.CompositionTarget == null)
            {
                handled = false;
                return IntPtr.Zero;
            }

            var DpiScaleX = _source.CompositionTarget.TransformFromDevice.M11;
            var DpiScaleY = _source.CompositionTarget.TransformFromDevice.M22;

            // Let the system know if we consider the mouse to be in our effective non-client area.
            var mousePosScreen = new Point(Utility.GetXlParam(lParam), Utility.GetYlParam(lParam));
            var windowPosition = _GetWindowRect(hWnd);

            var mousePosWindow = mousePosScreen;
            mousePosWindow.Offset(-windowPosition.X, -windowPosition.Y);
            mousePosWindow = DpiHelper.DevicePixelsToLogical(mousePosWindow, DpiScaleX, DpiScaleY);

 /*           
            var result = VisualTreeHelper.HitTest(_window, mousePosWindow);
            if (result != null)
            {
                var obj = result.VisualHit;

                if (CompositeChrome.GetIsHitTestVisibleInChrome((IInputElement)obj ))
                {
                    handled = true;
                    return new IntPtr((int)HT.CLIENT);
                }
                else 
                if (mousePosWindow.X < _chromeBase.CaptionHeight)
                {
                    var parent = obj.TryFindParent<Control>();
                    if (CompositeChrome.GetIsHitTestVisibleInChrome(parent))
                    {
                        handled = true;
                        return new IntPtr((int)HT.CLIENT);
                    }
                }
            }
*/
            var inputElement = _window.InputHitTest(mousePosWindow);
            if (inputElement != null)
            {
                 if (CompositeChrome.GetIsHitTestVisibleInChrome(inputElement))
                 {
                     if (inputElement is WindowButtons wb)
                     {
                         var x = wb.IsEnabled;
                     }

                     handled = true;
                     return new IntPtr((int)HT.CLIENT);
                 }
                
                if (mousePosWindow.X < _chromeBase.CaptionHeight)
                {
                    var parent = ((DependencyObject)inputElement).TryFindParent<Control>();
                    if (CompositeChrome.GetIsHitTestVisibleInChrome(parent))
                    {
                        handled = true;
                        return new IntPtr((int)HT.CLIENT);
                    }
                }

            }

            handled = true;

            var realWindowPosition = DpiHelper.DeviceRectToLogical(windowPosition, DpiScaleX, DpiScaleY);
            var realMousePosition = DpiHelper.DevicePixelsToLogical(mousePosScreen, DpiScaleX, DpiScaleY);
            var ht = _HitTestNca(realWindowPosition, realMousePosition);

            return new IntPtr((int)ht);
        }

        /// <summary>
        /// Matrix of the HT values for NC window messages.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member")]
        private static readonly HT[,] _HitTestBorders = new[,]
        {
            { HT.TOPLEFT,    HT.TOP,     HT.TOPRIGHT    },
            { HT.LEFT,       HT.CLIENT,  HT.RIGHT       },
            { HT.BOTTOMLEFT, HT.BOTTOM,  HT.BOTTOMRIGHT },
        };

        private HT _HitTestNca(Rect windowPosition, Point mousePosition)
        {
            //in the middle, HT.CLIENT
            var uRow = 1;
            var uCol = 1;
            var inResizeBorder = false;

            var resizeThickness = _chromeBase?.ResizeBorderThickness ?? new Thickness(4);
            var captionHeight = _chromeBase?.CaptionHeight ?? 0.0;

            // top or bottom 
            if (mousePosition.Y >= windowPosition.Top && mousePosition.Y < windowPosition.Top + resizeThickness.Top + captionHeight)
            {
                inResizeBorder = (mousePosition.Y < (windowPosition.Top + resizeThickness.Top));
                uRow = 0; // top (caption or resize border)
            }
            else if (mousePosition.Y < windowPosition.Bottom && mousePosition.Y >= windowPosition.Bottom - (int)resizeThickness.Bottom)
            {
                uRow = 2; // bottom
            }

            // left or right
            if (mousePosition.X >= windowPosition.Left && mousePosition.X < windowPosition.Left + (int)resizeThickness.Left)
            {
                uCol = 0; // left
            }
            else if (mousePosition.X < windowPosition.Right && mousePosition.X >= windowPosition.Right - resizeThickness.Right)
            {
                uCol = 2; // right
            }

            // If the cursor is in one of the top edges by the caption bar, but below the top resize border,
            // then resize left-right rather than diagonally.
            if (uRow == 0 && uCol != 1 && !inResizeBorder)
            {
                uRow = 1;
            }

            HT ht = _HitTestBorders[uRow, uCol];

            if (ht == HT.TOP && !inResizeBorder)
            {
                ht = HT.CAPTION;
            }

            return ht;
        }

        /// <summary>
        /// Get the bounding rectangle for the window in physical coordinates.
        /// </summary>
        /// <returns>The bounding rectangle for the window.</returns>
        private Rect _GetWindowRect(IntPtr hWnd)
        {
            // Get the window rectangle.
            RECT windowPosition = NativeMethods.GetWindowRect(hWnd);
            return new Rect(windowPosition.Left, windowPosition.Top, windowPosition.Width, windowPosition.Height);
        }

        private IntPtr _HandleNCRButtonUp(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            // Emulate the system behavior of clicking the right mouse button over the caption area
            // to bring up the system menu.
            if (HT.CAPTION == (HT)wParam.ToInt32())
            {
                NativeMethods.ShowSystemMenuPhysicalCoordinates(hWnd, new Point(Utility.GetXlParam(lParam), Utility.GetYlParam(lParam)));
            }
            handled = false;
            return IntPtr.Zero;
        }

        private IntPtr _HandleWindowPosChanged(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {
            // Still want to pass this to DefWndProc
            handled = false;
            return IntPtr.Zero;
        }

        private IntPtr _HandleDwmCompositionChanged(IntPtr hWnd, WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled)
        {

            handled = false;
            return IntPtr.Zero;
        }
        #endregion

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            if (!(sender is Window w)) return;
            _window = (Window) sender;

            _source = PresentationSource.FromVisual(_window) as HwndSource;
            if(_source==null) throw  new NullReferenceException("source");

            _hWnd = _source.Handle;

            if (_chromeBase != null) ChromeBase.HWndSource = _source;

            w.SourceInitialized -= Window_SourceInitialized;

            if (_isHooked) return;

            NativeMethods.SetInvisibleNonClientArea(_hWnd);

            if (_source.CompositionTarget != null)
                _source.CompositionTarget.BackgroundColor = Colors.Transparent;

            _source.AddHook(WndProc);
            _isHooked = true;

        }

    }
}