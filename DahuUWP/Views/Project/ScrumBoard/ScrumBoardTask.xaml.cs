using DahuUWP.DahuTech.Inputs;
using DahuUWP.Services.ModelManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ScrumBoardTask : UserControl
    {
        public ScrumBoardTask()
        {
            this.InitializeComponent();
            (this.Content as FrameworkElement).DataContext = this;
        }

        public static readonly DependencyProperty DeleteTaskButtonBindingsProperty = DependencyProperty.Register("DeleteTaskButtonBindings", typeof(DahuButtonBindings), typeof(ScrumBoardColumn), null);


        public Models.ScrumBoardTask Task
        {
            get
            {
                return (Models.ScrumBoardTask)GetValue(TaskProperty);
            }
            set
            {
                SetValue(TaskProperty, value);
            }
        }
        public static readonly DependencyProperty TaskProperty = DependencyProperty.Register("Task", typeof(Models.ScrumBoardTask), typeof(ScrumBoardTask), null);

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
            string rename = await dialog.InputStringDialogAsync("Renommer la tâche: " + Task.Name, Task.Name, res.GetString("Rename"), res.GetString("Cancel"));

            if (!String.IsNullOrEmpty(rename))
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                await scrumBoardManager.EditTask(rename, Task.ScrumBoardUuid, Task.Uuid);
                string param = Task.Uuid + ";" + rename;
                if (Task.RenameTaskButtonBindings != null)
                {
                    Task.RenameTaskButtonBindings.Parameter = param;
                    Task.RenameTaskButtonBindings.LinkIt();
                }
            }
        }

        private async void MenuFlyoutItemDelete_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var res = new ResourceLoader();
            InputStringDialog dialog = new InputStringDialog();
            bool delete = await dialog.AskDialogAsync(res.GetString("DeleteTask"), res.GetString("DeleteTaskInfo") + Task.Name, res.GetString("Delete"), res.GetString("Cancel"));
            
            if (delete)
            {
                ScrumBoardManager scrumBoardManager = new ScrumBoardManager();
                await scrumBoardManager.DeleteTask(Task.ScrumBoardUuid, Task.Uuid);
                if (Task.DeleteTaskButtonBindings != null)
                {
                    Task.DeleteTaskButtonBindings.Parameter = Task.Uuid;
                    Task.DeleteTaskButtonBindings.LinkIt();
                }
            }
        }
    }
}
