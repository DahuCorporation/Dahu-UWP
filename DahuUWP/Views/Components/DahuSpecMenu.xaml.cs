using DahuUWP.DahuTech.Inputs;
using DahuUWP.DahuTech.Menu;
using DahuUWP.Views.Search;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            ResearchButtonBindings = new DahuButtonBindings
            {
                IsBusy = false,
                TappedFuncListener = Research
            };
        }

        public async void Research()
        {
            ResearchButtonBindings.IsBusy = true;
            HomePage.DahuFrame.Navigate(typeof(MainResearch), ResearchValue);
            ResearchButtonBindings.IsBusy = false;
        }

        public ObservableCollection<TopBarNodeMenu> NodesTopBarMenu
        {
            get { return (ObservableCollection<TopBarNodeMenu>)GetValue(DahuSpecMenuProperty); }
            set { SetValue(DahuSpecMenuProperty, value); }
        }
        public static readonly DependencyProperty DahuSpecMenuProperty =
           DependencyProperty.Register("NodesTopBarMenu", typeof(ObservableCollection<TopBarNodeMenu>), typeof(DahuSpecMenu), new PropertyMetadata(null));


        public DahuButtonBindings ResearchButtonBindings
        {
            get { return (DahuButtonBindings)GetValue(ResearchButtonBindingsProperty); }
            set { SetValue(ResearchButtonBindingsProperty, value); }
        }
        public static readonly DependencyProperty ResearchButtonBindingsProperty =
           DependencyProperty.Register("ResearchButtonBindings", typeof(DahuButtonBindings), typeof(DahuSpecMenu), new PropertyMetadata(null));

        public string ResearchValue
        {
            get { return (string)GetValue(ResearchValuesProperty); }
            set { SetValue(ResearchValuesProperty, value); }
        }
        public static readonly DependencyProperty ResearchValuesProperty =
           DependencyProperty.Register("ResearchValue", typeof(string), typeof(DahuSpecMenu), new PropertyMetadata(null));

        public Visibility ReasearchVisibility
        {
            get { return (Visibility)GetValue(ReasearchVisibilityProperty); }
            set { SetValue(ReasearchVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ReasearchVisibilityProperty =
           DependencyProperty.Register("ReasearchVisibility", typeof(Visibility), typeof(DahuSpecMenu), new PropertyMetadata(null));

        public Visibility DahuSpecMenuVisibility
        {
            get { return (Visibility)GetValue(DahuSpecMenuVisibilityProperty); }
            set { SetValue(DahuSpecMenuVisibilityProperty, value); }
        }
        public static readonly DependencyProperty DahuSpecMenuVisibilityProperty =
           DependencyProperty.Register("DahuSpecMenuVisibility", typeof(Visibility), typeof(DahuSpecMenu), new PropertyMetadata(null));

    }
}
