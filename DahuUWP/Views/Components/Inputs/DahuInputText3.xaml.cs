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

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuInputText3 : UserControl
    {
        public DahuInputText3()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string InputHeader
        {
            get { return (string)GetValue(InputHeaderProperty); }
            set { TextBlockInputHeader.Visibility = Visibility.Visible; SetValue(InputHeaderProperty, value); }
        }

        public static readonly DependencyProperty InputHeaderProperty =
            DependencyProperty.Register("InputHeader", typeof(string), typeof(DahuInputText3), new PropertyMetadata(null));



        public int InputWidth { get; set; }

        /// <summary>
        /// InputPlaceholder
        /// </summary>
        public string InputPlaceholder
        {
            get { return (string)GetValue(InputPlaceholderProperty); }
            set { SetValue(InputPlaceholderProperty, value); }
        }

        public static readonly DependencyProperty InputPlaceholderProperty =
            DependencyProperty.Register("InputPlaceholder", typeof(string), typeof(DahuInputText3), new PropertyMetadata(null));

        public string InputScope { get; set; }

        /// <summary>
        /// TextInput
        /// </summary>
        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(DahuInputText3), new PropertyMetadata(null));
    }
}
