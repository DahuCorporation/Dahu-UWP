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

namespace DahuUWP.Views.Project.Chat
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Chat : Page
    {
        public Chat()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //ScrollerMessage.UpdateLayout();

            ///*
            //    ScrollViewer.ChangeView
            //        Causes the ScrollViewer to load a new view into
            //        the viewport using the specified offsets and zoom factor.
            //*/

            //// Programmatically scroll to bottom
            //ScrollerMessage.ChangeView(
            //    0.0f, // horizontalOffset
            //    double.MaxValue, // verticalOffset
            //    1.0f // zoomFactor
            //    );

        }



        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollerMessage.UpdateLayout();

            /*
                ScrollViewer.ChangeView
                    Causes the ScrollViewer to load a new view into
                    the viewport using the specified offsets and zoom factor.
            */

            // Programmatically scroll to bottom
            ScrollerMessage.ChangeView(
                0.0f, // horizontalOffset
                double.MaxValue, // verticalOffset
                1.0f // zoomFactor
                );
        }
    }
}
