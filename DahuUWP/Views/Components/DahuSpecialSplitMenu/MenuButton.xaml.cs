using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.DahuSpecialSplitMenu
{
    public sealed partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            DataContext = this;
            this.InitializeComponent();
        }


        public string Value { get; set; }

        private bool _active;

        public bool Active
        {
            get
            {
                return _active;
            }

            set
            {
                _active = value;
                if (_active)
                {
                    RectangleActive.Visibility = Visibility.Visible;
                }
                else
                {
                    RectangleActive.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ButtonGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void ButtonGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
