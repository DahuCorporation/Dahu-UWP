using DahuUWP.DahuTech.Menu;
using DahuUWP.Views.Components.DahuSpecialSplitMenu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace DahuUWP.Views.Project.ScrumBoard
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ScrumBoard : Page
    {
        public ScrumBoard()
        {
            this.InitializeComponent();
        }
        //private void UnorganizedListView_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        //{
        //    var items = string.Join(",", e.Items.Cast<DahuTech.ScrumBoard.ScrumBoardTask>().Select(i => i.Id));
        //    e.Data.SetText(items);
        //    e.Data.RequestedOperation = DataPackageOperation.Move;
        //}

        //private void UnorganizedListView_OnDragOver(object sender, DragEventArgs e)
        //{

        //}

        //private void UnorganizedListView_OnDrop(object sender, DragEventArgs e)
        //{

        //}

        //private void ListView_DragOver(object sender, DragEventArgs e)
        //{
        //    //http://www.shenchauhan.com/blog/2015/8/23/drag-and-drop-in-uwp
        //    if (e.DataView.Contains(StandardDataFormats.Text))
        //    {
        //        e.AcceptedOperation = DataPackageOperation.Move;
        //    }
        //}

        //private async System.Threading.Tasks.Task ListView_DropAsync(object sender, DragEventArgs e)
        //{
        //    if (e.DataView.Contains(StandardDataFormats.Text))
        //    {
        //        var id = await e.DataView.GetTextAsync();
        //        var itemIdsToMove = id.Split(',');

        //        var destinationListView = sender as ListView;
        //        var listViewItemsSource = destinationListView?.ItemsSource as ObservableCollection<DahuTech.ScrumBoard.ScrumBoardTask>;

        //        if (listViewItemsSource != null)
        //        {
        //            //Mettre dans le fichier scrumboardcolumn dans cs pour qu'il recup bien le MyItems demandé
        //            //foreach (var itemId in itemIdsToMove)
        //            //{
        //            //    var itemToMove = this.MyItems.First(i => i.Id.ToString() == itemId);

        //                //listViewItemsSource.Add();
        //            //    this.MyItems.Remove(itemToMove);
        //            //}
        //        }
        //    }
        //}
    }
}
