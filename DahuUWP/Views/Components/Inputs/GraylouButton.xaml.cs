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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class GraylouButton : UserControl
    {
        public GraylouButton()
        {
            DataContext = this;
            this.InitializeComponent();
            ButtonBackground = "#DFDFDF";
            ValueForeground = "#080808";
            InputHeight = 48;
            InputRadius = "24";
        }

        public string Value { get; set; }

        private string _icon;
        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                ButtonIconName.Background = IconConverter.IconToImageBrush(_icon);
            }
        }

        public string InputRadius
        {
            get
            {
                return (string)GetValue(InputRadiusProperty);
            }
            set
            {
                SetValue(InputRadiusProperty, value);
            }
        }
        public static readonly DependencyProperty InputRadiusProperty = DependencyProperty.Register("InputRadius", typeof(string), typeof(GraylouButton), new PropertyMetadata(null, new PropertyChangedCallback(OnInputRadiusChanged)));

        private static void OnInputRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraylouButton dahuAllInInputTxt = d as GraylouButton;
            string[] radius = ((string)e.NewValue).Split(',');
            switch (radius.Length)
            {
                case 1:
                    dahuAllInInputTxt.GraylouBorder.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]));
                    break;
                case 4:
                    dahuAllInInputTxt.GraylouBorder.CornerRadius = new CornerRadius(Convert.ToDouble(radius[0]), Convert.ToDouble(radius[1]), Convert.ToDouble(radius[2]), Convert.ToDouble(radius[3]));
                    break;
            }
        }
        public int InputHeight
        {
            get
            {
                return (int)GetValue(InputHeightProperty);
            }
            set
            {
                SetValue(InputHeightProperty, value);
            }
        }
        public static readonly DependencyProperty InputHeightProperty = DependencyProperty.Register("InputHeight", typeof(int), typeof(GraylouButton), null);

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
        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(string), typeof(GraylouButton), null);

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
        public static readonly DependencyProperty ValueForegroundProperty = DependencyProperty.Register("ValueForeground", typeof(string), typeof(GraylouButton), null);

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
