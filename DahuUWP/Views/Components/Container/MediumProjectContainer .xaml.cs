using DahuUWP.DahuTech.Inputs;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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

        public DahuUWP.Models.Project Project
        {
            get { return (DahuUWP.Models.Project)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }
        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project", typeof(DahuUWP.Models.Project), typeof(MediumProjectContainer), new PropertyMetadata(null));

        public DahuButtonBindings ButtonBindings
        {
            get
            {
                return (DahuButtonBindings)GetValue(ButtonBindingsProperty);
            }
            set
            {
                SetValue(ButtonBindingsProperty, value);
            }
        }
        public static readonly DependencyProperty ButtonBindingsProperty =
            DependencyProperty.Register("ButtonBindings", typeof(DahuButtonBindings), typeof(MediumProjectContainer), new PropertyMetadata(null, new PropertyChangedCallback(OnButtonBindingsPropertyChanged)));
        private static void OnButtonBindingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MediumProjectContainer mediumProj = d as MediumProjectContainer;
            if (mediumProj.ButtonBindings.Parameter != null)
                return;
            DahuButtonBindings newButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = mediumProj.ButtonBindings.Name,
                Parameter = mediumProj.Project
            };
            if (mediumProj.ButtonBindings.RedirectedLink != null)
            {
                newButtonBindings.RedirectedLink = mediumProj.ButtonBindings.RedirectedLink;
            } else
            {
                newButtonBindings.FuncListener = mediumProj.ButtonBindings.FuncListener;
            }
            mediumProj.ButtonBindings = newButtonBindings;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
