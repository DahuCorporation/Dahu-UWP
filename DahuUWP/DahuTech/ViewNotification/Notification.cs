using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.DahuTech.ViewNotification
{
    public enum NotificationType
    {
        Positive,
        Negative,
        Info,
        Warning
    };

    public class Notification
    {
        public string Value { get; set; }
        public string PropertyName { get; set; }
        public NotificationType Type { get; set; }


    }
}
