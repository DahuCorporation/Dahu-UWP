using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace DahuUWP.Utils.Converter
{
    //TODO: log good practice: https://stackify.com/csharp-exception-handling-best-practices/
    //TODO: faire un systeme de log pour la production qui est connecté à un serveur pour savoir les erreurs des users

    public static class IconConverter
    {
        private static ResourceDictionary rd;

        public static ImageBrush IconToImageBrush(string iconName)
        {
            try
            {
                BitmapImage iconSrc = new BitmapImage(new Uri((string)rd[iconName]));
                ImageBrush iconBrush = new ImageBrush
                {
                    ImageSource = iconSrc
                };
                return iconBrush;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                return null;
            }
        }

        static IconConverter()
        {
            try
            {
                rd = new ResourceDictionary
                {
                    Source = new Uri("ms-appx:///ResourceDictionary/IconResource.xaml")
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
            }
        }
    }
}
