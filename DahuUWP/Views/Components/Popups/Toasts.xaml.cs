using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Utils.Converter;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

    //TODO : Multiplication des clicks, test sur la connection, cliquer plusieur fois quand il n'y a pas d'e-mail
    //TODO : Mettre un timer pour chaque notifItem, de genre 5 sec après il disparait
namespace DahuUWP.Views.Components.Popups
{
    public sealed partial class Toasts : Page, INotifyPropertyChanged
    {
        private List<ToastNotification> items = new List<ToastNotification>();
        private DependencyPropertyWatcher<DahuNotification> watcher;

        public Toasts()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            watcher = new DependencyPropertyWatcher<DahuNotification>(this.DahuNotificationTextBox, "Text");
            watcher.PropertyChanged += NotificationNotif;

        }


        //https://stackoverflow.com/questions/11203324/is-there-an-event-that-fires-when-a-binding-has-been-set-on-a-property
        public DahuNotification DahuNotification
        {
            get { return (DahuNotification)GetValue(DahuNotificationProperty); }
            set
            {
                SetValue(DahuNotificationProperty, value);
            }
        }

        public static readonly DependencyProperty DahuNotificationProperty =
            DependencyProperty.Register("DahuNotification", typeof(DahuNotification), typeof(Toasts), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;


        private ToastNotification CreateToastNotif(string backgroundColor, string icon)
        {
            return new ToastNotification
            {
                Value = DahuNotification.Value,
                PropertyName = DahuNotification.PropertyName,
                Type = DahuNotification.Type,
                BackgroundColor = backgroundColor,
                Icon = icon
            };
        }

        private void NotificationNotif(object sender, EventArgs e)
        {
            if (DahuNotification != null)
            {
                switch (DahuNotification.Type)
                {
                    case NotificationType.Info:
                        items.Add(CreateToastNotif("#FF01cfe9", "icon"));
                        break;
                    case NotificationType.Negative:
                        items.Add(CreateToastNotif("#FFF13C4F", "icon"));
                        break;
                    case NotificationType.Positive:
                        items.Add(CreateToastNotif("#FF01bb55", "icon"));
                        break;
                    case NotificationType.Warning:
                        items.Add(CreateToastNotif("#FFfabd51", "icon"));
                        break;
                }
                DahuNotification = null;
                DahuNotificationTextBox.Text = "";
                DahuNotificationsList.ItemsSource = null;
                DahuNotificationsList.ItemsSource = items;
            }
        }

        private void CloseToastElem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            ToastNotification item = ((FontIcon)sender).Tag as ToastNotification;

            items.Remove(item);
            DahuNotificationsList.ItemsSource = null;
            DahuNotificationsList.ItemsSource = items;
        }

        private void CloseToastElem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            ((FontIcon)sender).Foreground = ColorConverter.GetSolidColorBrush("#FF2F2F2F");
        }

        private void CloseToastElem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            ((FontIcon)sender).Foreground = ColorConverter.GetSolidColorBrush("#FFFFFFFF");
        }
    }

    public class ToastNotification
    {
        public string Value { get; set; }
        public string PropertyName { get; set; }
        public NotificationType Type { get; set; }
        public string BackgroundColor { get; set; }
        public string Icon { get; set; }
    }
}


