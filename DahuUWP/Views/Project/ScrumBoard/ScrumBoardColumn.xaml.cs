using DahuUWP.ViewModels.Project.ScrumBoard;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

// Pour en savoir plus sur le modèle d'élément Contrôle utilisateur, consultez la page https://go.microsoft.com/fwlink/?LinkId=234236

namespace DahuUWP.Views.Project.ScrumBoard
{
    public sealed partial class ScrumBoardColumn : UserControl
    {
        public ScrumBoardColumn()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public DahuTech.ScrumBoard.ScrumBoardColumn Column
        {
            get
            {
                return (DahuTech.ScrumBoard.ScrumBoardColumn)GetValue(ColumnProperty);
            }
            set
            {
                SetValue(ColumnProperty, value);
            }
        }
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(DahuTech.ScrumBoard.ScrumBoardColumn), typeof(ScrumBoardColumn), null);

        /// <summary>
        /// Decide if item can move from column and put some args to send to the drop listner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnorganizedListView_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = string.Join(",", e.Items.Cast<DahuTech.ScrumBoard.ScrumBoardTask>().Select(i => i.Id));
            e.Data.SetText(items);
            e.Data.SetData("ColumnId", Column.Id.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        /// <summary>
        /// Find from witch column the item is comming using the id of the column
        /// </summary>
        /// <param name="originColumnId"></param>
        /// <returns></returns>
        private DahuTech.ScrumBoard.ScrumBoardColumn FindOriginColumn(int originColumnId)
        {
            ScrumBoardViewModel scrumBoardVM = ViewModelLocator.CurrentViewModel as ScrumBoardViewModel;
            foreach (var column in scrumBoardVM.Columns)
            {
                if (column.Id == originColumnId)
                {
                    return column;
                }
            }
            return null;
        }

        /// <summary>
        /// Drop the item in the demanded column and remove the item from the old column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.Text))
            {
                var id = await e.DataView.GetTextAsync();
                var originColumn = FindOriginColumn(Int32.Parse(await e.DataView.GetDataAsync("ColumnId") as string));
                if (originColumn == null)
                    return;

                var itemIdsToMove = id.Split(',');
                var destinationListView = sender as ListView;
                var listViewItemsSource = destinationListView?.ItemsSource as ObservableCollection<DahuTech.ScrumBoard.ScrumBoardTask>;
                
                if (listViewItemsSource != null)
                {
                    foreach (var itemId in itemIdsToMove)
                    {
                        var itemToMove = originColumn.Tasks.First(i => i.Id.ToString() == itemId);

                        listViewItemsSource.Add(itemToMove);
                        originColumn.Tasks.Remove(itemToMove);
                    }
                }
            }
        }

        /// <summary>
        /// Decide if the column accept the item or not. Check if dataView is text and if it's not the same column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListView_DragOver(object sender, DragEventArgs e)
        {
            var columnId = Int32.Parse(await e.DataView.GetDataAsync("ColumnId") as string);
            if (e.DataView.Contains(StandardDataFormats.Text) && this.Column.Id != columnId)
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private void ScrumBoardTask_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScrumBoardColumnListView.SelectedItems.Clear();
        }
    }
}
