using DahuUWP.Models;
using DahuUWP.ViewModels;
using DahuUWP.Views.Profil.Private;
using DahuUWP.Views.Profil.Public;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DahuUWP.Views.Component
{
    public sealed partial class TopBar : UserControl
    {

        public TopBar()
        {
            this.InitializeComponent();
            //(this.Content as FrameworkElement).DataContext = this;
        }


        //public bool Connected
        //{
        //    get { return (bool)GetValue(ConnectedProperty); }
        //    set {
        //        if (value)
        //        { TextTest.Visibility = Visibility.Visible; }
        //        SetValue(ConnectedProperty, value); }
        //}

        public static readonly DependencyProperty ConnectedProperty =
            DependencyProperty.Register("Connected", typeof(bool), typeof(TopBar), new PropertyMetadata(null));

        private void SignInButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePage.DahuFrame.Navigate(typeof(Connection));
        }

        private void RegisterButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePage.DahuFrame.Navigate(typeof(Register));
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePage.DahuFrame.Navigate(typeof(PublicProfil));
        }
    }

}
