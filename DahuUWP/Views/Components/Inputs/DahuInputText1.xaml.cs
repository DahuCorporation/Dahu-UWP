using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

//TODO : super site pour binding -> http://blog.jerrynixon.com/2013/07/solved-two-way-binding-inside-user.html ---> lien probleme: https://stackoverflow.com/questions/37026023/wpf-usercontrol-databinding-with-mvvm-viewmodel
//PropertyMetadata représente la valeur du champ à l'initialisation

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuInputText1 : UserControl
    {
        public DahuInputText1()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        /// <summary>
        /// Size of the input
        /// </summary>
        public int InputWidth
        {
            get { return (int)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register("InputWidth", typeof(int), typeof(DahuInputText1), new PropertyMetadata(null));

        /// <summary>
        /// TextInput
        /// </summary>
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(DahuInputText1), new PropertyMetadata(null));

        /// <summary>
        /// InputPlaceholder
        /// </summary>
        public string InputPlaceholder
        {
            get { return (string)GetValue(InputPlaceholderProperty); }
            set { SetValue(InputPlaceholderProperty, value); }
        }

        public static readonly DependencyProperty InputPlaceholderProperty =
            DependencyProperty.Register("InputPlaceholder", typeof(string), typeof(DahuInputText1), new PropertyMetadata(null));

        /// <summary>
        /// InputScope
        /// </summary>
        public string InputScope
        {
            get { return (string)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register("InputScope", typeof(string), typeof(DahuInputText1), new PropertyMetadata(null));


        public string InputIcon
        {
            get
            {
                return (string)GetValue(InputIconProperty);
            }
            set
            {
                SetValue(InputIconProperty, value);
            }
        }
        public static readonly DependencyProperty InputIconProperty = DependencyProperty.Register("InputIcon", typeof(string), typeof(DahuInputText1), new PropertyMetadata(null, new PropertyChangedCallback(ButtonImageBackgroundChanged)));
        private static void ButtonImageBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuInputText1 currentDahuInputText1 = d as DahuInputText1;
            //string[] radius = ((string)e.NewValue).Split(',');

            //dahuAllInBtn.GridImageBackground.Background.Opacity = 0.60;
            currentDahuInputText1.GridIconEnglobing.Visibility = Visibility.Visible;
            currentDahuInputText1.GirdIcon.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + (string)e.NewValue)), Stretch = Stretch.UniformToFill, Opacity = 1 };
        }

        /// <summary>
        /// IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set {
                InputText.IsReadOnly = value;
                InputText.Background = (value) ? ColorConverter.GetSolidColorBrush("#FFFAFAFA") : ColorConverter.GetSolidColorBrush("#FFFEFEFE");
                SetValue(IsReadOnlyProperty, value);
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(DahuInputText1), new PropertyMetadata(null));

        /// <summary>
        /// Tell if the input is a password type or not
        /// </summary>
        public bool InputPasswordActive
        {
            get
            {
                return (bool)GetValue(InputPasswordActiveProperty);
            }
            set
            {
                SetValue(InputPasswordActiveProperty, value);
            }
        }
        public static readonly DependencyProperty InputPasswordActiveProperty = DependencyProperty.Register("InputPasswordActive", typeof(bool), typeof(DahuInputText1), new PropertyMetadata(null, new PropertyChangedCallback(OnInputPasswordActiveChanged)));
        private static void OnInputPasswordActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuInputText1 dahuInputText1 = d as DahuInputText1;
            if ((bool)e.NewValue)
            {
                dahuInputText1.InputText.Visibility = Visibility.Collapsed;
                dahuInputText1.InputPassword.Visibility = Visibility.Visible;
            } else
            {
                dahuInputText1.InputText.Visibility = Visibility.Visible;
                dahuInputText1.InputPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void InputText_GotFocus(object sender, RoutedEventArgs e)
        {
            InputText.SelectAll();
        }
    }
}
