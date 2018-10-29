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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuImageSelector : UserControl
    {
        public DahuImageSelector()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty ButtonBindingsProperty = DependencyProperty.Register("ButtonBindings", typeof(DahuButtonBindings), typeof(DahuImageSelector), null);

        public DahuButtonBindings ButtonBindings
        {
            get
            {
                return (DahuButtonBindings)GetValue(ButtonBindingsProperty);
            }
            set
            {
                SetValue(ButtonBindingsProperty, value);
            }
        }

        private void GraylouButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ButtonBindings != null
                && !ButtonBindings.IsBusy)
            {
                //TappedCommand();
                ButtonBindings.LinkIt();
            }
        }
    }
}
