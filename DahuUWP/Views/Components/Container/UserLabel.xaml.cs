using DahuUWP.DahuTech.Inputs;
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

namespace DahuUWP.Views.Components.Container
{
    public sealed partial class UserLabel : UserControl
    {
        public UserLabel()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }


        public Models.User User
        {
            get { return (Models.User)GetValue(UserFullNameProperty); }
            set { SetValue(UserFullNameProperty, value); }
        }

        public static readonly DependencyProperty UserFullNameProperty =
            DependencyProperty.Register("User", typeof(Models.User), typeof(UserLabel), new PropertyMetadata(null));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(UserLabel), new PropertyMetadata(null));

        public DahuButtonBindings DeleteMemberBinding
        {
            get { return (DahuButtonBindings)GetValue(DeleteMemberBindingProperty); }
            set { SetValue(DeleteMemberBindingProperty, value); }
        }

        public static readonly DependencyProperty DeleteMemberBindingProperty =
            DependencyProperty.Register("DeleteMemberBinding", typeof(DahuButtonBindings), typeof(UserLabel), new PropertyMetadata(null));

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (DeleteMemberBinding != null
                && !DeleteMemberBinding.IsBusy)
            {
                DeleteMemberBinding.Parameter = User;
                DeleteMemberBinding.LinkIt();
            }
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (DeleteMemberBinding != null)
                Window.Current.CoreWindow.PointerCursor = (!DeleteMemberBinding.IsBusy) ? new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1) : new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.UniversalNo, 3);

        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (DeleteMemberBinding != null)
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
