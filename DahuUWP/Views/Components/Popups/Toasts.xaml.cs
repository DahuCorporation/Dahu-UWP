using DahuUWP.DahuTech.ViewNotification;
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

namespace DahuUWP.Views.Components.Popups
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Toasts : Page, INotifyPropertyChanged
    {
        private List<DahuNotification> items = new List<DahuNotification>();

        private IDisposable _contentWatcher;

        private void StartWatchingContent()
        {
            _contentWatcher = this.MyTextBox.WatchProperty("Content", myControl_ContentChanged);
        }

        private void StopWatchingContent()
        {
            _contentWatcher.Dispose();
        }

        private void myControl_ContentChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //MessageBox.Show("Content has changed!");
        }

        public Toasts()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            PersonName = "efzef";
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        //https://stackoverflow.com/questions/11203324/is-there-an-event-that-fires-when-a-binding-has-been-set-on-a-property
        public DahuNotification ToastNotif
        {
            get { return (DahuNotification)GetValue(ToastNotifProperty); }
            set
            {
                
                items.Add(new DahuNotification() { Value ="coucou1" });
                items.Add(new DahuNotification() { Value = "coucou2" });
                items.Add(new DahuNotification() { Value = "coucou3" });

                DahuNotificationsList.ItemsSource = items;
                SetValue(ToastNotifProperty, value);
            }
        }

        public static readonly DependencyProperty ToastNotifProperty =
            DependencyProperty.Register("ToastNotif", typeof(DahuNotification), typeof(Toasts), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        private void zef(object sender, RoutedEventArgs e)
        {

            watcher = new DependencyPropertyWatcher<string>(this.MyTextBox, "Text");
            watcher.PropertyChanged += OnTextChanged;
            DahuNotification titi = ToastNotif;
            object obj = items;
            string tata = "efaz";
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
           var text = watcher.Value;
            // ...
        }




        public string PersonName
        {
            get { return (string)GetValue(PersonNameProperty); }
            set
            {

                items.Add(new DahuNotification() { Value = "coucou1" });
                items.Add(new DahuNotification() { Value = "coucou2" });
                items.Add(new DahuNotification() { Value = "coucou3" });

                DahuNotificationsList.ItemsSource = items;
                SetValue(PersonNameProperty, value);
                OnPropertyChanged("PersonName");
            }
        }

        private DependencyPropertyWatcher<string> watcher;

        public static readonly DependencyProperty PersonNameProperty =
            DependencyProperty.Register("PersonName", typeof(string), typeof(Toasts), new PropertyMetadata(null));


        //Utile:
        //https://www.thomaslevesque.com/2013/04/21/detecting-dependency-property-changes-in-winrt/
        //https://blogs.msdn.microsoft.com/flaviencharlon/2012/12/07/getting-change-notifications-from-any-dependency-property-in-windows-store-apps/

        //suite:
        //https://stackoverflow.com/questions/12887858/binding-to-a-usercontrol-failed-to-assign-to-property-error-windows-8
        //https://www.codeproject.com/Articles/15822/Bind-Better-with-INotifyPropertyChanged
        // Maybe: https://social.msdn.microsoft.com/Forums/vstudio/en-US/262df30c-8383-4d5c-8d76-7b8e2cea51de/how-do-you-attach-a-change-event-to-a-dependency-property?forum=wpf
        //private string name;

        //public string PersonName
        //{
        //    get { return name; }
        //    set
        //    {
        //        name = value;
        //        // Call OnPropertyChanged whenever the property is updated
        //        OnPropertyChanged("PersonName");
        //    }
        //}

        //// Create the OnPropertyChanged method to raise the event
        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}


        //Animation
        //private async void ToastElem_TappedAsync(object sender, TappedRoutedEventArgs e)
        //{
        //    await ToastElem.Offset(offsetX: 100, duration: 1000).StartAsync();
        //    await ToastElem.Offset(offsetX: 100, offsetY: 100, duration: 1000).StartAsync();
        //    await ToastElem.Offset(offsetX: 0, offsetY: 100, duration: 1000).StartAsync();
        //    await ToastElem.Offset(duration: 1000).StartAsync();
        //}




    }


    public static class DependencyObjectExtensions
    {
        public static IDisposable WatchProperty(this DependencyObject target,
                                                string propertyPath,
                                                DependencyPropertyChangedEventHandler handler)
        {
            return new DependencyPropertyWatcher(target, propertyPath, handler);
        }

        class DependencyPropertyWatcher : DependencyObject, IDisposable
        {
            private DependencyPropertyChangedEventHandler _handler;

            public DependencyPropertyWatcher(DependencyObject target,
                                             string propertyPath,
                                             DependencyPropertyChangedEventHandler handler)
            {
                if (target == null) throw new ArgumentNullException("target");
                if (propertyPath == null) throw new ArgumentNullException("propertyPath");
                if (handler == null) throw new ArgumentNullException("handler");

                _handler = handler;

                var binding = new Binding
                {
                    Source = target,
                    Path = new PropertyPath(propertyPath),
                    Mode = BindingMode.OneWay
                };
                BindingOperations.SetBinding(this, ValueProperty, binding);
            }

            private static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register(
                    "Value",
                    typeof(object),
                    typeof(DependencyPropertyWatcher),
                    new PropertyMetadata(null, ValuePropertyChanged));

            private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var watcher = d as DependencyPropertyWatcher;
                if (watcher == null)
                    return;

                watcher.OnValueChanged(e);
            }

            private void OnValueChanged(DependencyPropertyChangedEventArgs e)
            {
                var handler = _handler;
                if (handler != null)
                    handler(this, e);
            }

            public void Dispose()
            {
                _handler = null;
                // There is no ClearBinding method, so set a dummy binding instead
                BindingOperations.SetBinding(this, ValueProperty, new Binding());
            }
        }
    }
}


