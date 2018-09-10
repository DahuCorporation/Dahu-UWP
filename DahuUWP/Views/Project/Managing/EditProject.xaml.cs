using DahuUWP.ViewModels.Project.Managing;
using DahuUWP.Views.Components.DahuSpecialSplitMenu;
using DahuUWP.Views.Project.Contribute;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace DahuUWP.Views.Project.Managing
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class EditProject : Page
    {
        private MenuButton activeMenuButton = new MenuButton();
        private object ParamNavigate;

        public EditProject()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ParamNavigate = e.Parameter;
            ProfilSpecMenuFrame.Navigate(typeof(EditProjectPrincipalInformation), ParamNavigate);
            activeMenuButton = DahuSpecSplitMenu_EditProjectPrincipalInformation;
        }

        private void ActiveButton(object sender)
        {
            if (((MenuButton)sender) != activeMenuButton)
            {
                ((MenuButton)sender).Active = true;
                activeMenuButton.Active = false;
                activeMenuButton = ((MenuButton)sender);
            }
        }

        private void DahuSpecSplitMenu_EditProjectPrincipalInformation_tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(EditProjectPrincipalInformation), ParamNavigate);
            ActiveButton(sender);
        }

        private void DahuSpecSplitMenu_EditProjectMembers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(EditProjectMembers), ParamNavigate);
            ActiveButton(sender);
        }

        private void DahuSpecSplitMenu_EditProjectParameters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(EditProjectParameters), ParamNavigate);
            ActiveButton(sender);
        }

        private void GraylouButton_BackToManageProject_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HomePage.DahuFrame.Navigate(typeof(ManageProject));
        }
    }
}
