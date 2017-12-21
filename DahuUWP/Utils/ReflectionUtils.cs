using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Change value of given field
        /// </summary>
        /// <param name="obj">Class where property is</param>
        /// <param name="field">Property you wanna change</param>
        /// <param name="value">Value you wanna set</param>
        public static void SetValue(object classObj, string field, object value)
        {
            FieldInfo field_info = classObj.GetType().GetField(field,
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public);
            field_info.SetValue(classObj, value);

        }
    }
}
