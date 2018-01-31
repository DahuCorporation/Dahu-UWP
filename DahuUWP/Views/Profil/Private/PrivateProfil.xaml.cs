using DahuUWP.Views.Components.DahuSpecialSplitMenu;
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

namespace DahuUWP.Views.Profil.Private
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class PrivateProfil : Page
    {
        private MenuButton activeMenuButton = new MenuButton();

        public PrivateProfil()
        {
            this.InitializeComponent();
            ProfilSpecMenuFrame.Navigate(typeof(PrivateProfilMainInformation));
            activeMenuButton = DahuSpecSplitMenu_PrincipalInformation;
        }

        private void ProfilSpecMenuFrame_Navigated(object sender, NavigationEventArgs e)
        {

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

        private void DahuSpecSplitMenu_PrincipalInformation_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(PrivateProfilMainInformation));
            ActiveButton(sender);
        }

        private void DahuSpecSplitMenu_Skill_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(PrivateProfilSkills));
            ActiveButton(sender);
        }

        private void DahuSpecSplitMenu_Parameters_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ProfilSpecMenuFrame.Navigate(typeof(PrivateProfilMainInformation));
            ActiveButton(sender);
        }
    }
}
