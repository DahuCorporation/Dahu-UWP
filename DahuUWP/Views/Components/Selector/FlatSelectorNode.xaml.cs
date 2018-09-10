using DahuUWP.DahuTech.Selector;
using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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

        public Checked Checked
        {
            get { return (Checked)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly DependencyProperty CheckedProperty =
           DependencyProperty.Register("Checked", typeof(Checked), typeof(FlatSelectorNode), new PropertyMetadata(null, new PropertyChangedCallback(CheckedPropertyChanged)));
        private static void CheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlatSelectorNode flatSelectorNode = d as FlatSelectorNode;
            Checked newChecked = (Checked)e.NewValue;
            switch (newChecked)
            {
                case Checked.Blocked:
                    break;
                case Checked.False:
                    flatSelectorNode.GridRadioButton.Visibility = Visibility.Collapsed;
                    break;
                case Checked.True:
                    flatSelectorNode.GridRadioButton.Visibility = Visibility.Visible;
                    flatSelectorNode.GridRadioButton.Background = ColorConverter.GetSolidColorBrush("#FF3D3D3D");
                    break;
            }
            flatSelectorNode.ChangeElemStyleOnCheckedChange();
            //string[] radius = ((string)e.NewValue).Split(',');

            //dahuAllInBtn.GridImageBackground.Background.Opacity = 0.60;
            //dahuAllInBtn.GridImageBackground.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/" + (string)e.NewValue)), Stretch = Stretch.UniformToFill, Opacity = 0.5 };
        }

        private void ChangeElemStyleOnCheckedChange()
        {
            switch (Checked)
            {
                case Checked.Blocked:
                    break;
                case Checked.False:
                    FlatSelectorElem.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    FlatSelectorElem.Background = ColorConverter.GetSolidColorBrush("#FFFBFBFB");
                    break;
                case Checked.True:
                    FlatSelectorElem.BorderBrush = ColorConverter.GetSolidColorBrush("#FFcbcbcb");
                    FlatSelectorElem.Background = ColorConverter.GetSolidColorBrush("#FFFFFFFF");
                    //flatSelectorNode.GridRadioButton.Background = ColorConverter.GetSolidColorBrush("#FFFBFBFB");
                    break;
                case Checked.Hover:
                    FlatSelectorElem.BorderBrush = ColorConverter.GetSolidColorBrush("#FFcbcbcb");
                    FlatSelectorElem.Background = ColorConverter.GetSolidColorBrush("#FFFFFFFF");
                    break;
            }
        }

        private void FlatSelectorElem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            FlatSelectorNode currentFlatSelectorElem = (FlatSelectorNode)element.DataContext;

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            if (currentFlatSelectorElem.Checked == Checked.False)
            {
                GridRadioButton.Visibility = Visibility.Visible;
                GridRadioButton.Background = ColorConverter.GetSolidColorBrush("#FF848484");
                currentFlatSelectorElem.Checked = Checked.Hover;
                ChangeElemStyleOnCheckedChange();
            }
        }

        private void FlatSelectorElem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            FlatSelectorNode currentFlatSelectorElem = (FlatSelectorNode)element.DataContext;

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            if (currentFlatSelectorElem.Checked == Checked.Hover)
            {
                GridRadioButton.Visibility = Visibility.Collapsed;
                currentFlatSelectorElem.Checked = Checked.False;
                ChangeElemStyleOnCheckedChange();
            }
        }
    }
}
