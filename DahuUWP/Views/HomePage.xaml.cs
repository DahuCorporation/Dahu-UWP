﻿using DahuUWP.Models;
using DahuUWP.ViewModels;
using DahuUWP.Views.Profil.Private;
using DahuUWP.Views.Profil.Public;
using DahuUWP.Views.Project;
using DahuUWP.Views.Project.Contribute;
using DahuUWP.Views.Project.Managing;
using DahuUWP.Views.Search;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace DahuUWP.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            AppStaticInfo.Account = new Account();
            DahuFrame = DahuBurgerFrame;
            DahuFrame.Navigate(typeof(Connection));
            DahuBurgerMenu.ItemsSource = MenuItem.GetMainItems();
            DahuBurgerMenu.OptionsItemsSource = MenuItem.GetOptionsItems();
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            //this.Frame.Navigate(menuItem.PageType);
            if (menuItem.PageType != null)
            {
                DahuBurgerFrame.Navigate(menuItem.PageType);
            } else if (menuItem.TappedFuncListener != null)
            {
                menuItem.TappedFuncListener.Invoke();
            }
            DahuBurgerMenu.IsPaneOpen = false;
        }

        private void DahuBurgerFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                DahuBurgerFrame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (DahuBurgerFrame.CanGoBack)
            {
                e.Handled = true;
                DahuBurgerFrame.GoBack();
            }
        }

        public static Frame DahuFrame { get; set; }
    }



    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public Type PageType { get; set; }
        public Action TappedFuncListener { get; set; }

        public static List<MenuItem> GetMainItems()
        {
            var items = new List<MenuItem>
            {
                new MenuItem() { Icon = Symbol.Find, Name = "Découvrir", PageType = typeof(Discover) },
                new MenuItem() { Icon = Symbol.Library, Name = "Mes projet", PageType = typeof(ManageProject) },
                new MenuItem() { Icon = Symbol.Send, Name = "Creer un nouveau projet", PageType = typeof(CreateProject) }
            };
            return items;
        }

        public static List<MenuItem> GetOptionsItems()
        {
            var items = new List<MenuItem>();
            items.Add(new MenuItem() { Icon = Symbol.DisconnectDrive, Name = "Déconnexion", TappedFuncListener = ((HomePageViewModel)ViewModelLocator.HomePageViewModel).Disconnection });
            return items;
        }

    }
}
