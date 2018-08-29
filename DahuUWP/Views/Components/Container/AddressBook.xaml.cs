using DahuUWP.Models;
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

namespace DahuUWP.Views.Components.Container
{
    public sealed partial class AddressBook : UserControl
    {
        public AddressBook()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }
        public User User
        {
            get
            {
                return (User)GetValue(UserProperty);
            }
            set
            {
                SetValue(UserProperty, value);
            }
        }
        public static readonly DependencyProperty UserProperty = DependencyProperty.Register("User", typeof(string), typeof(AddressBook), null);
    }
}
