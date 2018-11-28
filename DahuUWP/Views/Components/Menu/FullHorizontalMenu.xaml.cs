using DahuUWP.DahuTech.Menu;
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

namespace DahuUWP.Views.Components.Menu
{
    public sealed partial class FullHorizontalMenu : UserControl
    {
        private string GridColor = "#FFF8F8F8";
        private string ActiveGridColor = "#FFFFFFFF";
        private string HoverGridColor = "#FFFCFCFC";
        private Grid ActiveNodeMenuGrid;

        public FullHorizontalMenu()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        private void CreateMenu()
        {
            int columnNb = 0;
            int i = 0;
            while (i != MenuGrid.ColumnDefinitions.Count)
            {
                MenuGrid.ColumnDefinitions.Remove(MenuGrid.ColumnDefinitions[i]);
            }

            foreach (NodeMenu node in Menu.Nodes)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = node.Title,
                    Foreground = ColorConverter.GetSolidColorBrush("#FF3D3D3D"),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                ColumnDefinition col = new ColumnDefinition
                {
                    MaxWidth = 250,
                    MinWidth = 150,
                    Width = new GridLength(1, GridUnitType.Star)
                };
                MenuGrid.ColumnDefinitions.Add(col);

                Grid nodeGrid = new Grid();
                nodeGrid.Tag = node;
                if (!node.Active)
                {
                    nodeGrid.Background = ColorConverter.GetSolidColorBrush(GridColor);
                } else
                {
                    nodeGrid.Background = ColorConverter.GetSolidColorBrush(ActiveGridColor);
                    ActiveNodeMenuGrid = nodeGrid;
                }
                
                nodeGrid.Tapped += MenuGrid_Tapped;
                nodeGrid.PointerEntered += MenuGrid_PointerEntered;
                nodeGrid.PointerExited += MenuGrid_PointerExited;
                nodeGrid.Children.Add(textBlock);
                Grid.SetColumn(nodeGrid, columnNb);

                if (nodeGrid.Children.Count > 0)
                {
                    UIElement uiElem = nodeGrid.Children[0];
                }
                MenuGrid.Children.Add(nodeGrid);
                columnNb++;
            }
            ////MenuGrid.
            //ColumnDefinition col1 = new ColumnDefinition
            //{
            //    MaxWidth = 250,
            //    MinWidth = 150,
            //    Width = new GridLength(1, GridUnitType.Star)
            //};

            //ColumnDefinition col2 = new ColumnDefinition
            //{
            //    MaxWidth = 250,
            //    MinWidth = 150,
            //    Width = new GridLength(1, GridUnitType.Star)
            //};

            //ColumnDefinition col3 = new ColumnDefinition
            //{
            //    MaxWidth = 250,
            //    MinWidth = 150,
            //    Width = new GridLength(1, GridUnitType.Star)
            //};

            //ColumnDefinition col4 = new ColumnDefinition
            //{
            //    MaxWidth = 250,
            //    MinWidth = 150,
            //    Width = new GridLength(1, GridUnitType.Star)
            //};
            //MenuGrid.ColumnDefinitions.Add(col1);
            //MenuGrid.ColumnDefinitions.Add(col2);
            //MenuGrid.ColumnDefinitions.Add(col3);
            //MenuGrid.ColumnDefinitions.Add(col4);

            //Grid nodeGrid1 = new Grid()
            //{
            //    Background = new SolidColorBrush(Color.FromArgb(50, 100, 150, 100))
            //};

            //Grid nodeGrid2 = new Grid()
            //{
            //    Background = new SolidColorBrush(Color.FromArgb(50, 20, 150, 100))
            //};

            //Grid nodeGrid3 = new Grid()
            //{
            //    Background = new SolidColorBrush(Color.FromArgb(50, 80, 20, 100))
            //};

            //Grid nodeGrid4 = new Grid()
            //{
            //    Background = new SolidColorBrush(Color.FromArgb(20, 20, 90, 100))
            //};

            //MenuGrid.Children.Add(nodeGrid1);
            //MenuGrid.Children.Add(nodeGrid2);
            //MenuGrid.Children.Add(nodeGrid3);
            //MenuGrid.Children.Add(nodeGrid4);
            //Grid.SetColumn(nodeGrid1, 0);
            //Grid.SetColumn(nodeGrid2, 1);
            //Grid.SetColumn(nodeGrid3, 2);
            //Grid.SetColumn(nodeGrid4, 3);
        }

        public DahuUWP.DahuTech.Menu.Menu Menu
        {
            get
            {
                return (DahuUWP.DahuTech.Menu.Menu)GetValue(MenuProperty);
            }
            set
            {
                SetValue(MenuProperty, value);
            }
        }
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(DahuUWP.DahuTech.Menu.Menu), typeof(FullHorizontalMenu), new PropertyMetadata(null, new PropertyChangedCallback(OnMenuChanged)));

        private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FullHorizontalMenu fullHorMenu = d as FullHorizontalMenu;
            fullHorMenu.CreateMenu();
        }

        private void MenuGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Reset previous active grid
            ActiveNodeMenuGrid.Background = ColorConverter.GetSolidColorBrush(GridColor);
            (ActiveNodeMenuGrid.Tag as NodeMenu).Active = false;

            // Set new active grid
            Grid grid = (Grid)sender;
            grid.Background = ColorConverter.GetSolidColorBrush(ActiveGridColor);
            ActiveNodeMenuGrid = grid;
            (ActiveNodeMenuGrid.Tag as NodeMenu).Active = true;
            (ActiveNodeMenuGrid.Tag as NodeMenu).LinkIt();
        }

        private void MenuGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            grid.Background = ColorConverter.GetSolidColorBrush(HoverGridColor);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void MenuGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Grid grid = (Grid)sender;
            NodeMenu nodeMenu = grid.Tag as NodeMenu;
            grid.Background = ColorConverter.GetSolidColorBrush((!nodeMenu.Active) ? GridColor : ActiveGridColor);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
