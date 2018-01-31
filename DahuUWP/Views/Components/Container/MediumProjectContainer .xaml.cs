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

namespace DahuUWP.Views.Components.Container
{
    public sealed partial class MediumProjectContainer : UserControl
    {
        public MediumProjectContainer()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string ProjectName
        {
            get { return (string)GetValue(ProjectNameProperty); }
            set { SetValue(ProjectNameProperty, value); }
        }

        public static readonly DependencyProperty ProjectNameProperty =
            DependencyProperty.Register("ProjectName", typeof(string), typeof(MediumProjectContainer), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(MediumProjectContainer), new PropertyMetadata(null));
    }
}
