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

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuButton3 : UserControl
    {
        public DahuButton3()
        {
            DataContext = this;
            this.InitializeComponent();
        }

        //ButtonBackground est en DependencyProperty pour que DahuButton puisse être appelé en tant que style dans un fichier Resources
        public static readonly DependencyProperty ButtonBackgroundProperty =
            DependencyProperty.Register(
                "ButtonBackground", typeof(string),
                typeof(DahuButton3), null
            );

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

        public string Value { get; set; }
    }
}
