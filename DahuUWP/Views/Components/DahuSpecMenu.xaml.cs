using DahuUWP.DahuTech.Inputs;
using DahuUWP.Views.Search;
using GalaSoft.MvvmLight;
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

namespace DahuUWP.Views.Components
{
    public sealed partial class DahuSpecMenu : UserControl
    {
        public DahuSpecMenu()
        {
            DataContext = this;
            this.InitializeComponent();
            ResearchButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                TappedFuncListener = Research
            };
        }

        public async void Research()
        {
            ResearchButtonBindings.IsBusy = true;
            HomePage.DahuFrame.Navigate(typeof(MainResearch));
            ResearchButtonBindings.IsBusy = false;
        }

        public DahuButtonBindings ResearchButtonBindings
        {
            get { return (DahuButtonBindings)GetValue(ResearchButtonBindingsProperty); }
            set
            {
                SetValue(ResearchButtonBindingsProperty, value);
            }
        }

        public static readonly DependencyProperty ResearchButtonBindingsProperty =
            DependencyProperty.Register("ResearchButtonBindings", typeof(DahuButtonBindings), typeof(DahuSpecMenu), new PropertyMetadata(null));
        
    }
}
