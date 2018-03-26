using DahuUWP.DahuTech.Menu;
using DahuUWP.Views.Project;
using DahuUWP.Views.Project.Managing;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Windows.UI.Xaml.Shapes;

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Menu
{
    public sealed partial class TopBarMenu : UserControl
    {
        public TopBarMenu()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
            List<NodeMenu> listNodes = new List<NodeMenu>
            {
                new NodeMenu{ Title = "Découvrir", PageLink = typeof(Discover)},
                new NodeMenu{ Title = "Mes projets", PageLink = typeof(ManageProject)},
                new NodeMenu{ Title = "Creer un projet", PageLink = typeof(CreateProject)}
            };
            //var titi = new
            //{
            //    zefezf ="ef"
            //};
            //List<object> listNodes = new List<object>()
            //{
            //    new { Title = "Découvrir", PageLink = typeof(Discover), VisibleRectangle = Visibility.Collapsed, Index = 0},
            //    new { Title = "Mes projets", PageLink = typeof(ManageProject), VisibleRectangle = Visibility.Collapsed, Index = 1},
            //    new { Title = "Creer un projet", PageLink = typeof(CreateProject), VisibleRectangle = Visibility.Collapsed, Index = 2}
            //};
            NodesMenu = new ObservableCollection<NodeMenu>(listNodes);
        }
        
        public ObservableCollection<NodeMenu> NodesMenu
        {
            get { return (ObservableCollection<NodeMenu>)GetValue(NodesMenuProperty); }
            set { SetValue(NodesMenuProperty, value); }
        }


        public static readonly DependencyProperty NodesMenuProperty =
           DependencyProperty.Register("NodesMenu", typeof(ObservableCollection<NodeMenu>), typeof(TopBarMenu), new PropertyMetadata(null));

        private void NodeMenuTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            NodeMenu nodeMenu = (NodeMenu)element.DataContext;
            HomePage.DahuFrame.Navigate(nodeMenu.PageLink);
        }

        private async void FadeHoverRectangle(object sender, bool hover)
        {
            Grid nodeGrid = (Grid)sender;
            Rectangle hoverRectangle = (Rectangle)nodeGrid.FindName("HoverRectangle");
            if (hover)
                await hoverRectangle.Fade(value: 1f, duration: 300, delay: 0).StartAsync();
            else
                await hoverRectangle.Fade(value: 0f, duration: 300, delay: 0).StartAsync();
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //FrameworkElement element = (FrameworkElement)sender;
            //TopBarNodeMenu nodeMenu = (TopBarNodeMenu)element.DataContext;
            FadeHoverRectangle(sender, true);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            FadeHoverRectangle(sender, false);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
