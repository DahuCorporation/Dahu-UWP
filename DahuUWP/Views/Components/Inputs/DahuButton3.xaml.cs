using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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
    public sealed partial class DahuButton3 : UserControl
    {
        public DahuButton3()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        /// <summary>
        /// ButtonBackground est en DependencyProperty pour que DahuButton puisse être appelé en tant que style dans un fichier Resources
        /// </summary>
        public string ButtonBackground
        {
            get
            {
                return (string)GetValue(ButtonBackgroundProperty);
            }
            set
            {
                SetValue(ButtonBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(string), typeof(DahuButton3), null);

        /// <summary>
        /// Link to the func in the view model when the modul is tapped
        /// </summary>
        public Action TappedCommand
        {
            get
            {
                return (Action)GetValue(TappedCommandProperty);
            }
            set
            {
                SetValue(TappedCommandProperty, value);
            }
        }

        public static readonly DependencyProperty TappedCommandProperty = DependencyProperty.Register("TappedCommand", typeof(Action), typeof(DahuButton3), null);

        public bool IsBusy
        {
            get
            {
                return (bool)GetValue(IsBusyProperty);
            }
            set
            {
                //Mettre en place quand tu sauras comment catcher le changement d'une propriété bindé
                //if (value)
                //{
                //    ButtonBackgroundSave = ButtonBackground;
                //    ButtonBackground = "#929292";
                //}
                //else
                //{
                //    ButtonBackground = ButtonBackgroundSave;
                //}
                SetValue(IsBusyProperty, value);
            }
        }

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(DahuButton3), null);


        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(DahuButton3), null);


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
            if (!IsBusy)
            {
                TappedCommand();
            }
        }
    }
}
