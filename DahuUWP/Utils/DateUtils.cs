using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Utils
{
    class DateUtils
    {
        /// <summary>
        /// Return date with format dd/M/yy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime date)
        {
            return date.Date.ToString("dd'/'M'/'yyyy");
        }

        /// <summary>
        /// Return date with the format given in param
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime date, string format)
        {
            return date.Date.ToString(format);
        }

        public static DateTime StringToDateTime(string date)
        {
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                return dateTime;
            } catch (Exception e)
            {
                return new DateTime();
            }
        }
    }
}
