using DahuUWP.Utils.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace DahuUWP.DahuTech.Inputs
{
    class InputStringDialog
    {
        private ContentDialog dialog;

        public async Task<string> InputStringDialogAsync(string title, string defaultText, string okButtonText, string cancelButtonText)
        {
            //https://xamlbrewer.wordpress.com/2017/08/02/an-elementary-dialog-service-for-uwp/
            var textBox = new TextBox
            {
                AcceptsReturn = false,
                Height = 32,
                Text = defaultText,
                SelectionStart = defaultText.Length,
                BorderThickness = new Thickness(1),
                BorderBrush = ColorConverter.GetSolidColorBrush("#ffc8c8c8")
            };
            dialog = new ContentDialog
            {
                Content = textBox,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return textBox.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<bool> AskDialogAsync(string title, string textInfo, string okButtonText, string cancelButtonText)
        {
            var textBlock = new TextBlock
            {
                Height = 32,
                Text = textInfo
            };
            dialog = new ContentDialog
            {
                Content = textBlock,
                Title = title,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = okButtonText,
                SecondaryButtonText = cancelButtonText
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
