using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Nfh.Editor.Behaviors
{
    public static class HideCloseButtonBehavior
    {
        public static readonly DependencyProperty HideCloseButtonProperty =
            DependencyProperty.RegisterAttached(
                "HideCloseButton",
                typeof(bool),
                typeof(HideCloseButtonBehavior),
                new UIPropertyMetadata(false, OnHideCloseButtonPropertyChanged));

        public static bool GetHideCloseButton(UIElement obj) =>
            (bool)obj.GetValue(HideCloseButtonProperty);

        public static void SetHideCloseButton(UIElement obj, bool value) =>
            obj.SetValue(HideCloseButtonProperty, value);

        private static void OnHideCloseButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window window)) return;

            var hide = (bool)e.NewValue;
            if (hide)
            {
                if (!window.IsLoaded)
                {
                    window.Loaded += Window_Loaded;
                }
                else
                {
                    HideCloseButton(window);
                }
                SetHideCloseButton(window, true);
            }
            else
            {
                if (!window.IsLoaded)
                {
                    window.Loaded += Window_Loaded;
                }
                else
                {
                    ShowCloseButton(window);
                }
                SetHideCloseButton(window, false);
            }
        }

        private static void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!(sender is Window window)) return;
            HideCloseButton(window);
            window.Loaded -= Window_Loaded;
        }

        private static void HideCloseButton(Window w)
        {
            IntPtr hWnd = new WindowInteropHelper(w).Handle;
            SetWindowLong(hWnd, GWL_STYLE, GetWindowLong(hWnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private static void ShowCloseButton(Window w)
        {
            IntPtr hWnd = new WindowInteropHelper(w).Handle;
            SetWindowLong(hWnd, GWL_STYLE, GetWindowLong(hWnd, GWL_STYLE) | WS_SYSMENU);
        }

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }
}
