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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Component
{
    public sealed partial class DahuHamburgerMenu : UserControl
    {
        public DahuHamburgerMenu()
        {
            this.InitializeComponent();
            var items = new List<MenuItem>();
            items.Add(new MenuItem()
            {
                Icon = Symbol.Accept,
                Name = "Suresh M"
            });
            items.Add(new MenuItem()
            {
                Icon = Symbol.OutlineStar,
                Name = "Microsoft MVP"
            });
            items.Add(new MenuItem()
            {
                Icon = Symbol.OutlineStar,
                Name = "C# Corner MVP"
            });
            hamburgerMenuControl.ItemsSource = items;

        }

        public class MenuItem
        {
            public Symbol Icon
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
        }
        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            //process selected menu item  
        }
    }
}
