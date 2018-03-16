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
    public sealed partial class DahuInputText2 : UserControl
    {
        public DahuInputText2()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string InputWidth
        {
            get { return (string)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public static readonly DependencyProperty InputWidthProperty =
        DependencyProperty.Register("InputWidth", typeof(string), typeof(DahuInputText2), new PropertyMetadata(null));

        public string InputPlaceholder
        {
            get { return (string)GetValue(InputPlaceholderProperty); }
            set { SetValue(InputPlaceholderProperty, value); }
        }

        public static readonly DependencyProperty InputPlaceholderProperty =
        DependencyProperty.Register("InputPlaceholder", typeof(string), typeof(DahuInputText2), new PropertyMetadata(null));

        public string InputScope
        {
            get { return (string)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public static readonly DependencyProperty InputScopeProperty =
        DependencyProperty.Register("InputScope", typeof(string), typeof(DahuInputText2), new PropertyMetadata(null));

        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty =
                DependencyProperty.Register("TextValue", typeof(string), typeof(DahuInputText2), new PropertyMetadata(null));
    }
}
