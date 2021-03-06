﻿using DahuUWP.ViewModels.Project.Managing;
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
    public sealed partial class ManageProject : Page
    {
        public ManageProject()
        {
            this.InitializeComponent();
            //ProjectFrame.Navigate(typeof(Forum.Forum));
        }

        //private void GraylouButton_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    HomePage.DahuFrame.Navigate(typeof(EditProject));
        //}

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ((ManageProjectViewModel)DataContext).NavigationParam = e.Parameter;
        }
    }
}
