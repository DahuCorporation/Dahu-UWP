using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Dahu_UWP.Views.Component.Desktop
{
    public sealed partial class DahuTextBox1 : UserControl
    {
        private string _title;

        public DahuTextBox1()
        {
            DataContext = this;
            this.InitializeComponent();
        }

        public bool IsRequired { get; set; }

        public string Title
        {
            get { return _title; }

            set
            {
                if (_title != value)
                {
                    TextBoxTitle.Visibility = Visibility.Visible;
                    _title = value;

                }
            }
        }

        private void DahuTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsRequired == true && String.IsNullOrWhiteSpace(DahuTextBox.Text))
            {
                ErrorField.Visibility = Visibility.Visible;
            } else
            {
                ErrorField.Visibility = Visibility.Collapsed;
            }
        }
    }
}
