using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Dahu_UWP
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ConnectButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Debug.WriteLine("TestBox:" + titi + "SexBox:" + tata);
        }

        private void PivotConnectionTitle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            
        }


        /**
         * TODO look for this for the is required https://stackoverflow.com/questions/18930125/wpf-validation-using-validationrule-class
         */
    }
}
