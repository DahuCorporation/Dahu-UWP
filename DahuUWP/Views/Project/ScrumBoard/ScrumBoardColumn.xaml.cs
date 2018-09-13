﻿using DahuUWP.DahuTech.Inputs;
using DahuUWP.Utils.Converter;
using DahuUWP.ViewModels.Project.ScrumBoard;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources;
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

        public Models.ScrumBoardColumn Column
        {
            get
            {
                return (Models.ScrumBoardColumn)GetValue(ColumnProperty);
            }
            set
            {
                SetValue(ColumnProperty, value);
            }
        }
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(Models.ScrumBoardColumn), typeof(ScrumBoardColumn), null);

        /// <summary>
        /// Decide if item can move from column and put some args to send to the drop listner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UnorganizedListView_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            var items = string.Join(",", e.Items.Cast<Models.ScrumBoardTask>().Select(i => i.Uuid));
            e.Data.SetText(items);
            e.Data.SetData("ColumnId", Column.Uuid.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        /// <summary>
        /// Find from witch column the item is comming using the id of the column
        /// </summary>
        /// <param name="originColumnId"></param>
        /// <returns></returns>
        private Models.ScrumBoardColumn FindOriginColumn(string originColumnId)
        {
            ScrumBoardViewModel scrumBoardVM = ViewModelLocator.CurrentViewModel as ScrumBoardViewModel;
            foreach (var column in scrumBoardVM.ScrumBoardColumns)
            {
                if (column.Uuid.Equals(originColumnId))
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
                var originColumn = FindOriginColumn(await e.DataView.GetDataAsync("ColumnId") as string);
                if (originColumn == null)
                    return;

                var itemIdsToMove = id.Split(',');
                var destinationListView = sender as ListView;
                var listViewItemsSource = destinationListView?.ItemsSource as ObservableCollection<Models.ScrumBoardTask>;

                if (listViewItemsSource != null)
                {
                    foreach (var itemId in itemIdsToMove)
                    {
                        var itemToMove = originColumn.Tasks.First(i => i.Uuid == itemId);

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
            var columnId = await e.DataView.GetDataAsync("ColumnId") as string;
            if (e.DataView.Contains(StandardDataFormats.Text) && this.Column.Uuid != columnId)
            {
                e.AcceptedOperation = DataPackageOperation.Move;
            }
        }

        private void ScrumBoardTask_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScrumBoardColumnListView.SelectedItems.Clear();
        }

        private async void FontIconAddTask_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string taskTitle = await dialog.InputStringDialogAsync(res.GetString("CreateNewTask"), res.GetString("NewTask"), res.GetString("Add"), res.GetString("Cancel"));
            if (!String.IsNullOrWhiteSpace(taskTitle))
            {
                Models.ScrumBoardTask task = new Models.ScrumBoardTask()
                {
                    Uuid = Column.Tasks.Count + "1",
                    Name = taskTitle
                };
                Column.Tasks.Add(task);
            }
        }

        private void TaskButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void TaskButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private async void MenuFlyoutItemRename_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            string name = await dialog.InputStringDialogAsync("Renommer la colonne: " + Column.Name, Column.Name, res.GetString("Rename"), res.GetString("Cancel"));

        }

        private async void MenuFlyoutItemDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            bool name = await dialog.AskDialogAsync(res.GetString("DeleteTaskColumn"), res.GetString("DeleteTaskColumnInfo") + Column.Name, res.GetString("Delete"), res.GetString("Cancel"));
        }
    }
}
