using DahuUWP.DahuTech.ViewNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace DahuUWP.DahuTech
{
    /// <summary>
    /// Enum for the languages
    /// Comporting like a enum
    /// https://stackoverflow.com/questions/630803/associating-enums-with-strings-in-c-sharp
    /// </summary>
    public class Languages
    {
        private Languages(string value) { Value = value; }

        public string Value { get; set; }

        public static Languages French { get { return new Languages("fr-FR"); } }
        public static Languages English { get { return new Languages("en-US"); } }
    }



    public static class AppGeneral
    {

        /// <summary>
        /// Don't forget to add a PreviousNavigatePageParam in the view model. This is needed if the user go back to a page where there was a param but this param has changed
        /// 
        /// MVVM Technique:
        /// https://marcominerva.wordpress.com/2013/05/07/calling-viewmodel-methods-in-response-to-page-navigation-events-using-mvvm-light-in-winrt/
        /// </summary>
        public static object NavigatePageParam { get; set; }

        public static Type NavigateTo { get; set; }

        public static Dictionary<string, DahuNotification> UserInterfaceStatusDico { get; set; }

        //TODO : detecter le language pour pouvoir charger le bon xml selon la langue
        // XML tuto: http://www.c-sharpcorner.com/article/read-xml-file-in-windows-10-universal-app/
        static private void InitUserInterfaceStatusDico()
        {
            UserInterfaceStatusDico = new Dictionary<string, DahuNotification>();
            string pathToXml = Path.Combine(Package.Current.InstalledLocation.Path, "Strings\\" + Languages.French.Value + "\\UserInterfaceStatus.xml");
            XDocument UserInterfaceStatusXML = XDocument.Load(pathToXml);

            foreach (XElement elem in UserInterfaceStatusXML.Descendants("status"))
            {
                NotificationType elemType = new NotificationType();

                switch (elem.Attribute("type").Value)
                {
                    case "Positive":
                        elemType = NotificationType.Positive;
                        break;
                    case "Negative":
                        elemType = NotificationType.Negative;
                        break;
                    case "Info":
                        elemType = NotificationType.Info;
                        break;
                    case "Warning":
                        elemType = NotificationType.Warning;
                        break;
                }
                DahuNotification notif = new DahuNotification
                {
                    Value = elem.Attribute("value").Value,
                    PropertyName = elem.Attribute("varName").Value,
                    Type = elemType
                };  
                UserInterfaceStatusDico.Add(elem.Attribute("key").Value, notif);
            }
        }

        static AppGeneral()
        {
            InitUserInterfaceStatusDico();
        }
    }
}
