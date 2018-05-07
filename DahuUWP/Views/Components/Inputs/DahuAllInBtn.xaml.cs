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
    public sealed partial class DahuAllInBtn : UserControl
    {
        public DahuAllInBtn()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

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
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(DahuAllInBtn), null);

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
        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(string), typeof(DahuAllInBtn), null);

        public string ButtonWidth
        {
            get
            {
                return (string)GetValue(ButtonWidthProperty);
            }
            set
            {
                SetValue(ButtonWidthProperty, value);
            }
        }
        public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register("ButtonWidth", typeof(string), typeof(DahuAllInBtn), null);

        public string ValueForeground
        {
            get
            {
                return (string)GetValue(ValueForegroundProperty);
            }
            set
            {
                SetValue(ValueForegroundProperty, value);
            }
        }
        public static readonly DependencyProperty ValueForegroundProperty = DependencyProperty.Register("ValueForeground", typeof(string), typeof(DahuAllInBtn), null);

        public string ValueFontWeight
        {
            get
            {
                return (string)GetValue(ValueFontWeightProperty);
            }
            set
            {
                SetValue(ValueFontWeightProperty, value);
            }
        }
        public static readonly DependencyProperty ValueFontWeightProperty = DependencyProperty.Register("ValueFontWeight", typeof(string), typeof(DahuAllInBtn), null);

        public int ValueFontSize
        {
            get
            {
                return (int)GetValue(ValueFontSizeProperty);
            }
            set
            {
                SetValue(ValueFontSizeProperty, value);
            }
        }
        public static readonly DependencyProperty ValueFontSizeProperty = DependencyProperty.Register("ValueFontSize", typeof(int), typeof(DahuAllInBtn), null);

        public string ButtonRadius
        {
            get
            {
                return (string)GetValue(ButtonRadiusProperty);
            }
            set
            {
                SetValue(ButtonRadiusProperty, value);
            }
        }
        public static readonly DependencyProperty ButtonRadiusProperty = DependencyProperty.Register("ButtonRadius", typeof(string), typeof(DahuAllInBtn), new PropertyMetadata(null, new PropertyChangedCallback(OnButtonRadiusChanged)));
        private static void OnButtonRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuAllInBtn dahuAllInBtn = d as DahuAllInBtn;
            string[] radius = ((string)e.NewValue).Split(',');
            switch (radius.Length)
            {
                case 1:
                    dahuAllInBtn.ButtonGrid.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]));
                    break;
                case 4:
                    dahuAllInBtn.ButtonGrid.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]), Convert.ToDouble(radius[1]), Convert.ToDouble(radius[2]), Convert.ToDouble(radius[3]));
                    break;
            }
        }

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
        public static readonly DependencyProperty ButtonBindingsProperty = DependencyProperty.Register("ButtonBindings", typeof(DahuButtonBindings), typeof(DahuAllInBtn), null);

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonBindings != null)
                Window.Current.CoreWindow.PointerCursor = (!ButtonBindings.IsBusy) ? new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1) : new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.UniversalNo, 3);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (ButtonBindings != null)
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ButtonBindings != null
                && !ButtonBindings.IsBusy)
            {
                //TappedCommand();
                if (ButtonBindings.TappedFuncListener != null)
                    ButtonBindings.TappedFuncListener();
                else
                {
                    HomePage.DahuFrame.Navigate(ButtonBindings.RedirectedLink, ButtonBindings.Parameter);
                }
            }
        }
    }
}
