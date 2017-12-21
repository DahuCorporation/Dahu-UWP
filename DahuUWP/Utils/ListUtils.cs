using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Utils
{
    public static class ListUtils
    {
        public static List<T> EmptyList<T>(List<T> list)
        {
            list = null;
            list.TrimExcess();

            return list;
        }
    }
}
