using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlSharp
{
    public static class XmlParser
    {
        /// <summary>
        /// Extract the Elements and Attributes from the document and parse them into a valid C# class.
        /// </summary>
        /// <param name="xml">The string that contains the XML document.</param>
        /// <returns>An iteration of Class from the xml.</returns>
        public static IEnumerable<Class> Parse(string xml) => XElement.Parse(xml).ExtractClassInfo();
    }
}
