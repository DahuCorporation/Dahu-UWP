using DahuUWP.DahuTech;
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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.DahuSpecialSplitMenu
{
    public sealed partial class MenuButton : UserControl
    {

        public string Green
        {
            get
            {
                //return (string)GetValue(GreenProperty);
                return (ButtonTheme == Theme.Dark) ? "Black" : "White";
            }
            set
            {
                SetValue(GreenProperty, value);
            }
        }
        public static readonly DependencyProperty GreenProperty = DependencyProperty.Register("Green", typeof(string), typeof(MenuButton), null);

        public MenuButton()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            ButtonTheme = Theme.Clear;
            Green = "Red";
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SetButtonTheme()
        {
            if (Active)
            {
                switch (ButtonTheme)
                {
                    case Theme.Dark:
                        ButtonBackground = "#3D3D3D";
                        ValueForeground = "#FFFFFF";
                        break;
                    case Theme.Clear:
                        ButtonBackground = "#FFFFFF";
                        ValueForeground = "#3D3D3D";
                        break;
                    default:
                        ButtonBackground = "#FFFFFF";
                        ValueForeground = "#3D3D3D";
                        break;
                }
            }
            else
            {
                switch (ButtonTheme)
                {
                    case Theme.Dark:
                        ButtonBackground = "#515151";
                        ValueForeground = "#FFFFFF";
                        break;
                    case Theme.Clear:
                        ButtonBackground = "#FAFAFA";
                        ValueForeground = "#3D3D3D";
                        break;
                    default:
                        ButtonBackground = "#FAFAFA";
                        ValueForeground = "#3D3D3D";
                        break;
                }
            }

        }

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
        public static readonly DependencyProperty ValueForegroundProperty = DependencyProperty.Register("ValueForeground", typeof(string), typeof(MenuButton), null);

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
        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(string), typeof(MenuButton), null);

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
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(MenuButton), null);

        public Theme ButtonTheme
        {
            get
            {
                return (Theme)GetValue(ButtonThemeProperty);
            }
            set
            {
                SetValue(ButtonThemeProperty, value);
            }
        }
        public static readonly DependencyProperty ButtonThemeProperty = DependencyProperty.Register("ButtonTheme", typeof(Theme), typeof(MenuButton), new PropertyMetadata(null, new PropertyChangedCallback(OnButtonThemeChanged)));
        private static void OnButtonThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                MenuButton menuButton = d as MenuButton;
                menuButton.SetButtonTheme();
            }
        }

        public HorizontalAlignment ValueHorizontalAlignement
        {
            get
            {
                return (HorizontalAlignment)GetValue(ValueHorizontalAlignementProperty);
            }
            set
            {
                SetValue(ValueHorizontalAlignementProperty, value);
            }
        }
        public static readonly DependencyProperty ValueHorizontalAlignementProperty = DependencyProperty.Register("ValueHorizontalAlignement", typeof(HorizontalAlignment), typeof(MenuButton), new PropertyMetadata(null, new PropertyChangedCallback(OnValueHorizontalAlignementChanged)));
        private static void OnValueHorizontalAlignementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MenuButton menuButton = d as MenuButton;
            if ((HorizontalAlignment)e.NewValue == HorizontalAlignment.Center)
            {
                menuButton.ButtonValue.Margin = new Thickness(0, 0, 0, 0);
            } else if ((HorizontalAlignment)e.NewValue == HorizontalAlignment.Left)
            {
                menuButton.ButtonValue.Margin = new Thickness(40, 0, 0, 0);
            }
        }

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
        public static readonly DependencyProperty ButtonRadiusProperty = DependencyProperty.Register("ButtonRadius", typeof(string), typeof(MenuButton), null);

        public bool Active
        {
            get
            {
                return (bool)GetValue(ActiveProperty);
            }
            set
            {
                SetValue(ActiveProperty, value);
            }
        }
        public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register("Active", typeof(bool), typeof(MenuButton), new PropertyMetadata(null, new PropertyChangedCallback(OnActiveChanged)));
        private static void OnActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MenuButton menuButton = d as MenuButton;
            bool value = (bool)e.NewValue;
            menuButton.RectangleActive.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed;
            menuButton.SetButtonTheme();
        }

        private void ButtonGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void ButtonGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }


    }
}
