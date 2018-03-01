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

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class ContentiButton : UserControl
    {
        public ContentiButton()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ContentiButton), new PropertyMetadata(null));

        public string ContentValue
        {
            get { return (string)GetValue(ContentValueProperty); }
            set { SetValue(ContentValueProperty, value); }
        }

        public static readonly DependencyProperty ContentValueProperty =
            DependencyProperty.Register("ContentValue", typeof(string), typeof(ContentiButton), new PropertyMetadata(null));


        private void Border_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ContentGridName.Visibility == Visibility.Collapsed)
            {
                ContentGridName.Visibility = Visibility.Visible;
                OpenCloseIconName.Text = "-";
            } else
            {
                ContentGridName.Visibility = Visibility.Collapsed;
                OpenCloseIconName.Text = "+";
            }
        }

        private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
