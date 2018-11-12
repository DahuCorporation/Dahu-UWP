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
    public sealed partial class UserInfoBox : UserControl
    {
        public UserInfoBox()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string UserFullName
        {
            get { return (string)GetValue(UserFullNameProperty); }
            set {  SetValue(UserFullNameProperty, value); }
        }

        public static readonly DependencyProperty UserFullNameProperty =
            DependencyProperty.Register("UserFullName", typeof(string), typeof(UserInfoBox), new PropertyMetadata(null));

        public string UserBiography
        {
            get { return (string)GetValue(UserBiographyProperty); }
            set { SetValue(UserBiographyProperty, value); }
        }

        public static readonly DependencyProperty UserBiographyProperty =
            DependencyProperty.Register("UserBiography", typeof(string), typeof(UserInfoBox), new PropertyMetadata(null));


        public string UserPicture
        {
            get { return (string)GetValue(UserPictureyProperty); }
            set { SetValue(UserPictureyProperty, value); }
        }

        public static readonly DependencyProperty UserPictureyProperty =
            DependencyProperty.Register("UserPicture", typeof(string), typeof(UserInfoBox), new PropertyMetadata(null));

    }
}
