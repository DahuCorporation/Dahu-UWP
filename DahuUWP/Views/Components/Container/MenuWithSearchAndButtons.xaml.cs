using DahuUWP.DahuTech.Menu;
using DahuUWP.Views.Components.DahuSpecialSplitMenu;
using DahuUWP.Views.Components.Inputs;
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

namespace DahuUWP.Views.Components.Container
{
    public sealed partial class MenuWithSearchAndButtons : UserControl
    {
        public MenuWithSearchAndButtons()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public MenuWithSearchAndButtonsContainer Menu
        {
            get
            {
                return (MenuWithSearchAndButtonsContainer)GetValue(MenuProperty);
            }
            set
            {
                SetValue(MenuProperty, value);
            }
        }
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(MenuWithSearchAndButtonsContainer), typeof(MenuWithSearchAndButtons), null);

        private void MenuNode_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NodeMenu selectedNodeMenu = (NodeMenu)(sender as MenuButton).Tag;
            foreach (NodeMenu nodeMenu in Menu.Nodes)
            {
                if (nodeMenu.Active && nodeMenu != selectedNodeMenu)
                    nodeMenu.Active = false;
            }
            selectedNodeMenu.Active = true;
            selectedNodeMenu.LinkIt();
        }

        private void MenuButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NodeMenu selectedNodeMenu = (NodeMenu)(sender as DahuAllInBtn).Tag;
            selectedNodeMenu.LinkIt();
        }
    }
}
