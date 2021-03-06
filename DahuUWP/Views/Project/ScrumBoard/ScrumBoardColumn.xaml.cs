﻿using DahuUWP.DahuTech;
using DahuUWP.DahuTech.Inputs;
using DahuUWP.Services.ModelManager;
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
            
            DeleteTaskButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Delete task",
                FuncListener = DeleteTask
            };

            RenameTaskButtonBindings = new DahuButtonBindings()
            {
                IsBusy = false,
                Name = "Rename task",
                FuncListener = RenameTask
            };
        }

        public void DeleteTask(object taskId)
        {
            if (Column.Tasks.Count > 0)
            {
                Models.ScrumBoardTask task = Column.Tasks.FirstOrDefault(x => x.Uuid == taskId.ToString());
                if (task != null)
                {
                    Column.Tasks.Remove(task);
                }
            }
        }

        public void RenameTask(object obj)
        {
            if (Column.Tasks.Count > 0)
            {
                string[] words = obj.ToString().Split(';');
                if (words.Count() == 2)
                {
                    Models.ScrumBoardTask task = Column.Tasks.FirstOrDefault(x => x.Uuid == words[0]);
                    if (task != null)
                    {
                        task.Name = words[1];
                    }
                }
            }
        }

        public DahuButtonBindings DeleteTaskButtonBindings
        {
            get
            {
                return (DahuButtonBindings)GetValue(DeleteTaskButtonBindingsProperty);
            }
            set
            {
                SetValue(DeleteTaskButtonBindingsProperty, value);
            }
        }

        public static readonly DependencyProperty DeleteTaskButtonBindingsProperty = DependencyProperty.Register("DeleteTaskButtonBindings", typeof(DahuButtonBindings), typeof(ScrumBoardColumn), null);

        public DahuButtonBindings RenameTaskButtonBindings
        {
            get
            {
                return (DahuButtonBindings)GetValue(RenameTaskButtonBindingsProperty);
            }
            set
            {
                SetValue(RenameTaskButtonBindingsProperty, value);
            }
        }

        public static readonly DependencyProperty RenameTaskButtonBindingsProperty = DependencyProperty.Register("RenameTaskButtonBindings", typeof(DahuButtonBindings), typeof(ScrumBoardColumn), null);


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
        public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(Models.ScrumBoardColumn), typeof(ScrumBoardColumn), new PropertyMetadata(null, new PropertyChangedCallback(ButtonImageBackgroundChanged)));
        private static async void ButtonImageBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScrumBoardColumn scrumBoardColumn = d as ScrumBoardColumn;

            ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
            List<Models.ScrumBoardTask> allScrumBoardColumns = await scrumBoardManager.ChargeAllTaskOfScrumBoard(scrumBoardColumn.Column.ScrumBoardUuid);
            List<Models.ScrumBoardTask> scrumBoardColumns = new List<Models.ScrumBoardTask>();
            foreach (Models.ScrumBoardTask elem in allScrumBoardColumns)
            {
                if (elem.Column != null && elem.Column.Uuid == scrumBoardColumn.Column.Uuid)
                {
                    scrumBoardColumns.Add(elem);
                }
            }
            scrumBoardColumn.Column.Tasks = new ObservableCollection<Models.ScrumBoardTask>(scrumBoardColumns);
            
            foreach (Models.ScrumBoardTask elem in scrumBoardColumn.Column.Tasks)
            {
                elem.DeleteTaskButtonBindings = scrumBoardColumn.DeleteTaskButtonBindings;
                elem.RenameTaskButtonBindings = scrumBoardColumn.RenameTaskButtonBindings;
                elem.ScrumBoardUuid = scrumBoardColumn.Column.ScrumBoardUuid;
            }
            //string[] radius = ((string)e.NewValue).Split(',');

            //dahuAllInBtn.GridImageBackground.Background.Opacity = 0.60;
        }


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
                        var cl = Column.Tasks;
                        originColumn.Tasks.Remove(itemToMove);
                        ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                        await scrumBoardManager.EditTask((itemToMove as Models.ScrumBoardTask).Name, Column.Uuid, Column.ScrumBoardUuid, (itemToMove as Models.ScrumBoardTask).Uuid);
                        cl = Column.Tasks;
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
            if (!String.IsNullOrWhiteSpace(taskTitle) && Column != null)
            {
                Models.ScrumBoardTask scrumBoardTask = new Models.ScrumBoardTask() {
                    Name = taskTitle,
                    ScrumBoardColumnUuid = Column.Uuid,
                    ScrumBoardUuid = Column.ScrumBoardUuid

                };
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                scrumBoardTask = await scrumBoardManager.CreateTask(scrumBoardTask);
                scrumBoardTask.RenameTaskButtonBindings = RenameTaskButtonBindings;
                scrumBoardTask.DeleteTaskButtonBindings = DeleteTaskButtonBindings;
                Column.Tasks.Add(scrumBoardTask);
            } else
            {
                AppGeneral.UserInterfaceStatusDico["Binding not finished."].Display();
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
            string rename = await dialog.InputStringDialogAsync("Renommer la colonne: " + Column.Name, Column.Name, res.GetString("Rename"), res.GetString("Cancel"));
            if (!String.IsNullOrEmpty(rename))
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                await scrumBoardManager.EditColumn(rename, Column.ScrumBoardUuid, Column.Uuid);
                Column.Name = rename;
            }
        }

        private async void MenuFlyoutItemDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            bool delete = await dialog.AskDialogAsync(res.GetString("DeleteTaskColumn"), res.GetString("DeleteTaskColumnInfo") + Column.Name, res.GetString("Delete"), res.GetString("Cancel"));
            if (delete)
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                await scrumBoardManager.DeleteColumn(Column.ScrumBoardUuid, Column.Uuid);
            }
        }
        
    }
}
