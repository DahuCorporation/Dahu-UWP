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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Inputs
{
    public sealed partial class DahuCalendarDatePicker : UserControl
    {
        public DahuCalendarDatePicker()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        /// <summary>
        /// Size of the input
        /// </summary>
        public DateTimeOffset Date
        {
            get { return (DateTimeOffset)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTimeOffset), typeof(DahuCalendarDatePicker), new PropertyMetadata(null));

        public string CalendarPlaceholderText
        {
            get { return (string)GetValue(CalendarPlaceholderTextProperty); }
            set { SetValue(CalendarPlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty CalendarPlaceholderTextProperty =
            DependencyProperty.Register("CalendarPlaceholderText", typeof(string), typeof(DahuCalendarDatePicker), new PropertyMetadata(null));

        public int CalendarWidth
        {
            get { return (int)GetValue(CalendarWidthProperty); }
            set { SetValue(CalendarWidthProperty, value); }
        }

        public static readonly DependencyProperty CalendarWidthProperty =
            DependencyProperty.Register("CalendarWidth", typeof(int), typeof(DahuCalendarDatePicker), new PropertyMetadata(null));
    }
}
