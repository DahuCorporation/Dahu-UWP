using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
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

namespace DahuUWP.Views.Project.ScrumBoard
{
    public sealed partial class ScrumBoardColumn : UserControl
    {
        public ScrumBoardColumn()
        {
            this.InitializeComponent();
        }

        private void UnorganizedListView_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = string.Join(",", e.Items.Cast<ScrumBoardTask>().Select(i => 0));
            //var items = string.Join(",", e.Items.Cast<ScrumBoardTask>().Select(i => i.Id));
            e.Data.SetText("titi");
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void UnorganizedListView_OnDragOver(object sender, DragEventArgs e)
        {

        }

        private void UnorganizedListView_OnDrop(object sender, DragEventArgs e)
        {

        }

        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
