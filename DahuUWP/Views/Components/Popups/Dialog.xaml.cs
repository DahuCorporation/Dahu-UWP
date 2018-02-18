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

namespace DahuUWP.Views.Components.Popups
{
    public sealed partial class Dialog : UserControl
    {
        public Dialog()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public Action<bool> OnAction
        {
            get { return (Action<bool>)GetValue(OnActionProperty); }
            set { SetValue(OnActionProperty, value); }
        }

        public static readonly DependencyProperty OnActionProperty =
                DependencyProperty.Register("OnAction", typeof(Action<bool>), typeof(Dialog), new PropertyMetadata(null));

        private void PositiveButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OnAction(true);
        }

        private void NegativeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OnAction(false);
        }
    }
}
