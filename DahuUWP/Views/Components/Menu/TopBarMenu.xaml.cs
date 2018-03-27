using DahuUWP.DahuTech;
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
            //ActiveLink = typeof(Discover);
            //List<TopBarNodeMenu> listNodes = CreateListNodes();
            //NodesMenu = new ObservableCollection<TopBarNodeMenu>(listNodes);
        }

        //private List<TopBarNodeMenu> CreateListNodes()
        //{
        //    List<TopBarNodeMenu> listNodes = new List<TopBarNodeMenu>
        //    {
        //        new TopBarNodeMenu{ Title = "Découvrir", PageLink = typeof(Discover)},
        //        new TopBarNodeMenu{ Title = "Mes projets", PageLink = typeof(ManageProject)},
        //        new TopBarNodeMenu{ Title = "Creer un projet", PageLink = typeof(CreateProject)}
        //    };
        //    if (ActiveLink != null)
        //    {
        //        foreach (TopBarNodeMenu topBarNodeMenu in listNodes)
        //            topBarNodeMenu.HoverRectangleOpacity = (topBarNodeMenu.PageLink == ActiveLink) ? 100 : 0;
        //    }
        //    return listNodes;
        //}

        //public Type ActiveLink
        //{
        //    get { return (Type)GetValue(ActiveLinkProperty); }
        //    set { SetValue(ActiveLinkProperty, value); }
        //}

        public static readonly DependencyProperty ActiveLinkProperty =
           DependencyProperty.Register("ActiveLink", typeof(Type), typeof(TopBarMenu), new PropertyMetadata(null));

        public ObservableCollection<TopBarNodeMenu> NodesMenu
        {
            get { return (ObservableCollection<TopBarNodeMenu>)GetValue(NodesMenuProperty); }
            set { SetValue(NodesMenuProperty, value); }
        }


        public static readonly DependencyProperty NodesMenuProperty =
           DependencyProperty.Register("NodesMenu", typeof(ObservableCollection<TopBarNodeMenu>), typeof(TopBarMenu), new PropertyMetadata(null));

        private void TopBarNodeMenuTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            TopBarNodeMenu topBarNodeMenu = (TopBarNodeMenu)element.DataContext;
            HomePage.DahuFrame.Navigate(topBarNodeMenu.PageLink);
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
            FrameworkElement element = (FrameworkElement)sender;
            TopBarNodeMenu topBarNodeMenu = (TopBarNodeMenu)element.DataContext;

            if (!topBarNodeMenu.IsActive)
                FadeHoverRectangle(sender, true);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            TopBarNodeMenu topBarNodeMenu = (TopBarNodeMenu)element.DataContext;

            if (!topBarNodeMenu.IsActive)
                FadeHoverRectangle(sender, false);
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
