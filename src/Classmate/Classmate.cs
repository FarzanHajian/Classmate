//
// This file is published under BSD 3-Clause License.
// Visit https://github.com/FarzanHajian/Classmate/blob/main/LICENSE for details.
//

using System;
using System.Reflection;
using System.Text;

namespace FarzanHajian.Classmate
{
    public static class Classmate
    {
        private static readonly StringBuilder result = new StringBuilder();

        public static string Classes(params object[] classes)
        {
            result.Clear();
            foreach (object cls in classes) ExtractClassFromObject(cls);
            if (result.Length == 0) return "";
            return result.ToString().Trim();
        }

        public static string Classes(bool predicateValue, object yes, object no)
        {
            result.Clear();
            ExtractClassFromObject(predicateValue ? yes : no);
            if (result.Length == 0) return "";
            return result.ToString().Trim();
        }

        public static string Classes(Func<bool> predicate, object yes, object no)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            result.Clear();
            ExtractClassFromObject(predicate() ? yes : no);
            if (result.Length == 0) return "";
            return result.ToString().Trim();
        }

        public static string If(this string str, bool include, string @else = "") => include ? str : @else;

        public static string If(this string str, Func<bool> include, string @else = "") => include() ? str : @else;

        public static bool If(Func<bool> include) => include();

        private static void ExtractClassFromObject(object cls)
        {
            if (cls is string str)
            {
                if (str != "")
                {
                    result.Append(' ');
                    result.Append(str.Trim());
                }
            }
            else if (cls is object obj)
            {
                var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                foreach (var property in properties)
                {
                    if (property.GetValue(obj) is bool value && value)
                    {
                        result.Append(' ');
                        result.Append(property.Name);
                    }
                }
            }
        }
    }
}