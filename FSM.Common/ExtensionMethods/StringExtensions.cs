using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSM
{
    public static class StringExtensions
    {
        public static T Substring<T>(this string value, int startIndex)
        {
            try
            {
                return (T)Convert.ChangeType(value.Substring(startIndex), typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T Substring<T>(this string value, int startIndex, int length)
        {
            try
            {
                return (T)Convert.ChangeType(value.Substring(startIndex, length), typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static T Slice<T>(this string value, int startColumn, int endColumn)
        {
            var length = (endColumn - startColumn) + 1;
            return value.Substring<T>((startColumn - 1), length);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value.Trim(), true);
        }
    }
}
