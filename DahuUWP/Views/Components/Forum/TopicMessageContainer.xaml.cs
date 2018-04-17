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

namespace DahuUWP.Views.Components.Forum
{
    public sealed partial class TopicMessageContainer : UserControl
    {
        public TopicMessageContainer()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public string TheTopicMessageContainer
        {
            get
            {
                return (string)GetValue(TheTopicMessageContainerProperty);
            }
            set
            {
                SetValue(TheTopicMessageContainerProperty, value);
            }
        }
        public static readonly DependencyProperty TheTopicMessageContainerProperty = DependencyProperty.Register("TheTopicMessageContainer", typeof(DahuUWP.DahuTech.Project.Forum.TopicMessageContainer), typeof(TopicMessageContainer), null);
    }
}
