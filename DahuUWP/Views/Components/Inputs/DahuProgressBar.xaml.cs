using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuProgressBar : UserControl
    {
        public DahuProgressBar()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string ProgressBarBackground
        {
            get
            {
                return (string)GetValue(ProgressBarBackgroundProperty);
            }
            set
            {
                SetValue(ProgressBarBackgroundProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressBarBackgroundProperty = DependencyProperty.Register("ProgressBarBackground", typeof(string), typeof(DahuProgressBar), new PropertyMetadata(null, new PropertyChangedCallback(OnProgressBarBackgroundChanged)));
        private static void OnProgressBarBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuProgressBar currentDahuProgressBar = d as DahuProgressBar;
            currentDahuProgressBar.CDahuProgressBar.Background = ColorConverter.GetSolidColorBrush((string)e.NewValue);

        }

        public string ProgressBarForeground
        {
            get
            {
                return (string)GetValue(ProgressBarForegroundProperty);
            }
            set
            {
                SetValue(ProgressBarForegroundProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressBarForegroundProperty = DependencyProperty.Register("ProgressBarForeground", typeof(string), typeof(DahuProgressBar), new PropertyMetadata(null, new PropertyChangedCallback(OnProgressBarForegroundChanged)));
        private static void OnProgressBarForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuProgressBar currentDahuProgressBar = d as DahuProgressBar;
            currentDahuProgressBar.CDahuProgressBar.Foreground = ColorConverter.GetSolidColorBrush((string)e.NewValue);

        }

        public string ProgressBarTitle
        {
            get
            {
                return (string)GetValue(ProgressBarTitleProperty);
            }
            set
            {
                SetValue(ProgressBarTitleProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressBarTitleProperty = DependencyProperty.Register("ProgressBarTitle", typeof(string), typeof(DahuProgressBar), new PropertyMetadata(null, new PropertyChangedCallback(OnProgressBarTitleChanged)));
        private static void OnProgressBarTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuProgressBar currentDahuProgressBar = d as DahuProgressBar;
            currentDahuProgressBar.TextBoxProgressBarTitle.Visibility = Visibility.Visible;
            currentDahuProgressBar.TextBoxProgressBarInformation.Visibility = Visibility.Visible;
        }

        public string ProgressBarNumberToReash
        {
            get
            {
                return (string)GetValue(ProgressBarNumberToReashProperty);
            }
            set
            {
                SetValue(ProgressBarNumberToReashProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressBarNumberToReashProperty = DependencyProperty.Register("ProgressBarNumberToReash", typeof(string), typeof(DahuProgressBar), new PropertyMetadata(null, new PropertyChangedCallback(OnProgressBarNumberToReashChanged)));
        private static void OnProgressBarNumberToReashChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DahuProgressBar currentDahuProgressBar = d as DahuProgressBar;
            string[] tab = ((string)e.NewValue).Split('/');
            if (tab.Length == 2)
            {
                double calc = (double) Convert.ToInt32(tab[0]) / Convert.ToInt32(tab[1]);
                currentDahuProgressBar.ProgressBarValue = (double) calc * 100;
            }
        }

        

        public double ProgressBarValue
        {
            get
            {
                return (double)GetValue(ProgressBarValueProperty);
            }
            set
            {
                SetValue(ProgressBarValueProperty, value);
            }
        }
        public static readonly DependencyProperty ProgressBarValueProperty = DependencyProperty.Register("ProgressBarValue", typeof(double), typeof(DahuProgressBar), null);
    }
}
