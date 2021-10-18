using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlSharp
{
    public static class Extensions
    {
        /// <summary>
        /// Lower all other strings, except the first one.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Same <see langword="string"/> all lower except the first one.</returns>
        public static string FirstLetterUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            char[] letters = input.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }

        public static IEnumerable<Class> ExtractClassInfo(this XElement element)
        {
            HashSet<Class> @classes = new ();
            XmlHelper.ElementToClass(element, classes);
            return @classes;
        }

        public static bool IsEmpty(this XElement element)
        {
            if (!element.HasAttributes)
            {
                return !element.HasElements;
            }
            return false;
        }
    }
}
