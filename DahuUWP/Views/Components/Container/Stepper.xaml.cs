using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Container;
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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Container
{
    public sealed partial class Stepper : UserControl
    {
        public Stepper()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public DahuTech.Container.Stepper StepperContent
        {
            get { return (DahuTech.Container.Stepper)GetValue(StepperContentProperty); }
            set { SetValue(StepperContentProperty, value); }
        }

        public static readonly DependencyProperty StepperContentProperty =
           DependencyProperty.Register("StepperContent", typeof(DahuTech.Container.Stepper), typeof(Stepper), new PropertyMetadata(null, new PropertyChangedCallback(StepperContentPropertyChanged)));
        private static void StepperContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Stepper stepper = d as Stepper;
            DahuTech.Container.Stepper newStepperContent = (DahuTech.Container.Stepper)e.NewValue;
            stepper.StepperFrame.Navigate(newStepperContent.Steps[0].StepView.View, newStepperContent.Steps[0].Project);

            //string[] radius = ((string)e.NewValue).Split(',');

            //dahuAllInBtn.GridImageBackground.Background.Opacity = 0.60;
            //dahuAllInBtn.GridImageBackground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + (string)e.NewValue)), Stretch = Stretch.UniformToFill, Opacity = 0.5 };
        }

        private void StepLinkProgressBarTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            Step currentStep = (Step)element.DataContext;
            switch (currentStep.Status)
            {
                case Status.Virgin:
                    if (StepperContent.CheckLastStepIsActiveOrPassed(currentStep))
                    {
                        StepperFrame.Navigate(currentStep.StepView.View, currentStep.Project);
                        StepperContent.MakeStepActive(currentStep);
                    } else
                    {
                        AppGeneral.UserInterfaceStatusDico["Cant go next step because previous step not valid."].Display();
                    }
                    break;
                case Status.Active:
                    // Doesn't do anything
                    break;
                case Status.Passed:
                    StepperFrame.Navigate(currentStep.StepView.View);
                    StepperContent.MakeStepActive(currentStep);
                    break;
            }
            
        }
    }
}
