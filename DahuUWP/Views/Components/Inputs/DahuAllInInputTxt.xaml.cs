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
    public sealed partial class DahuAllInInputTxt : UserControl
    {
        public DahuAllInInputTxt()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string InputTxtHeader
        {
            get
            {
                return (string)GetValue(InputTxtHeaderProperty);
            }
            set
            {
                SetValue(InputTxtHeaderProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtHeaderProperty = DependencyProperty.Register("InputTxtHeader", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtBackground
        {
            get
            {
                return (string)GetValue(InputTxtBackgroundProperty);
            }
            set
            {
                SetValue(InputTxtBackgroundProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtBackgroundProperty = DependencyProperty.Register("InputTxtBackground", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtWidth
        {
            get
            {
                return (string)GetValue(InputTxtWidthProperty);
            }
            set
            {
                SetValue(InputTxtWidthProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtWidthProperty = DependencyProperty.Register("InputTxtWidth", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtHeight
        {
            get
            {
                return (string)GetValue(InputTxtHeightProperty);
            }
            set
            {
                SetValue(InputTxtHeightProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtHeightProperty = DependencyProperty.Register("InputTxtHeight", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtRadius
        {
            get
            {
                return (string)GetValue(InputTxtRadiusProperty);
            }
            set
            {
                SetValue(InputTxtRadiusProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtRadiusProperty = DependencyProperty.Register("InputTxtRadius", typeof(string), typeof(DahuAllInInputTxt), new PropertyMetadata(null, new PropertyChangedCallback(OnInputTxtRadiusChanged)));
        private static void OnInputTxtRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuAllInInputTxt dahuAllInInputTxt = d as DahuAllInInputTxt;
            string[] radius = ((string)e.NewValue).Split(',');
            switch (radius.Length)
            {
                case 1:
                    dahuAllInInputTxt.InputTxtGrid.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]));
                    break;
                case 4:
                    dahuAllInInputTxt.InputTxtGrid.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]), Convert.ToDouble(radius[1]), Convert.ToDouble(radius[2]), Convert.ToDouble(radius[3]));
                    break;
            }
        }

        public string InputTxtPlaceholder
        {
            get
            {
                return (string)GetValue(InputTxtPlaceholderProperty);
            }
            set
            {
                SetValue(InputTxtPlaceholderProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtPlaceholderProperty = DependencyProperty.Register("InputTxtPlaceholder", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtValue
        {
            get
            {
                return (string)GetValue(InputTxtValueProperty);
            }
            set
            {
                SetValue(InputTxtValueProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtValueProperty = DependencyProperty.Register("InputTxtValue", typeof(string), typeof(DahuAllInInputTxt), null);

        public string InputTxtValueForeground
        {
            get
            {
                return (string)GetValue(InputTxtValueForegroundProperty);
            }
            set
            {
                SetValue(InputTxtValueForegroundProperty, value);
            }
        }
        public static readonly DependencyProperty InputTxtValueForegroundProperty = DependencyProperty.Register("InputTxtValueForeground", typeof(string), typeof(DahuAllInInputTxt), null);
    }
}
