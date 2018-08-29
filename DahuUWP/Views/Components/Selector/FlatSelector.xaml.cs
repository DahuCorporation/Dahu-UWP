using DahuUWP.DahuTech.Selector;
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

namespace DahuUWP.Views.Components.Selector
{
    public sealed partial class FlatSelector : UserControl
    {
        public FlatSelector()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public ObservableCollection<FlatSelectorElem> CounterpartList
        {
            get { return (ObservableCollection<FlatSelectorElem>)GetValue(CounterpartListProperty); }
            set { SetValue(CounterpartListProperty, value); }
        }

        public static readonly DependencyProperty CounterpartListProperty =
           DependencyProperty.Register("CounterpartList", typeof(ObservableCollection<FlatSelectorElem>), typeof(FlatSelector), null);
    }
}
