using DahuUWP.Utils.Converter;
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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Components.Decoration
{
    public sealed partial class Banner : UserControl
    {
        public Banner()
        {
            DataContext = this;
            this.InitializeComponent();
        }

        public string Value { get; set; }

        private string _icon;
        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                BannerIcon.Background = IconConverter.IconToImageBrush(_icon);
            }
        }
    }
}
