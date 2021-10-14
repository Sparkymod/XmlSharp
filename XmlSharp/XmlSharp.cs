using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlSharp
{
    public class XmlSharp
    {
        public static IEnumerable<Class> Convert(string xml) => XElement.Parse(xml).ExtractClassInfo();
    }
}
