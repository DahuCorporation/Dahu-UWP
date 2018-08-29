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

namespace DahuUWP.Views.Components.Selector
{
    public sealed partial class FlatSelectorNode : UserControl
    {
        public FlatSelectorNode()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register("Title", typeof(string), typeof(FlatSelectorNode), null);

        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static readonly DependencyProperty PriceProperty =
           DependencyProperty.Register("Price", typeof(string), typeof(FlatSelectorNode), null);

        public string ContentElem
        {
            get { return (string)GetValue(ContentElemProperty); }
            set { SetValue(ContentElemProperty, value); }
        }

        public static readonly DependencyProperty ContentElemProperty =
           DependencyProperty.Register("ContentElem", typeof(string), typeof(FlatSelectorNode), null);
    }
}
