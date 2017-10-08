using Dahu_UWP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class DahuButton1 : UserControl
    {
        public DahuButton1()
        {
            this.InitializeComponent();
        }

        public string Text
        {
            get { return DahuButton1_TextBlock.Text; }
            set { DahuButton1_TextBlock.Text = value; }
        }

        public string BgColor
        {
            set
            {
                try
                {
                    DahuButton1_Rectangle.Fill = ColorConverter.GetSolidColorBrush(value);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0} Execption caught.", e);
                }
            }
        }
    }
}
