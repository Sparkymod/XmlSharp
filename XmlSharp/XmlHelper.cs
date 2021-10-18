using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlSharp
{
    /// <summary>
    /// Internal class to help the parser.
    /// </summary>
    internal static class XmlHelper
    {
        internal static Class ElementToClass(XElement xElement, ICollection<Class> classes)
        {
            Class @class = new ()
            {
                Name = xElement.Name.LocalName,
                XmlName = xElement.Name.LocalName,
                Properties = DuplicateElementsToList(ExportProperties(xElement, classes)).ToList()
            };

            RemoveBadString(@class, classes);

            if (xElement.Parent == null || (!@classes.Contains(@class) && @class.Properties.Any()))
            {
                if (!classes.Any(c => c.Name == @class.Name))
                {
                    classes.Add(@class);
                }
            }

            return @class;
        }

        private static IEnumerable<Property> ExportProperties(XElement xElement, ICollection<Class> classes)
        {
            foreach (XElement element in xElement.Elements().ToList())
            {
                Class tempClass = ElementToClass(element, classes);
                string type = element.IsEmpty() ? "string" : tempClass.Name;

                Property property = new ()
                {
                    Name = tempClass.Name,
                    Type = type,
                    XmlName = tempClass.XmlName,
                    XmlType = XmlType.Element
                };

                yield return property;
            }

            foreach (XAttribute attribute in xElement.Attributes().ToList())
            {
                Property property = new ()
                {
                    Name = attribute.Name.LocalName,
                    XmlName = attribute.Name.LocalName,
                    Type = attribute.Value.GetType().Name.ToLower(), // TODO: verify if is string or a class.
                    XmlType = XmlType.Attribute
                };

                yield return property;
            }
        }

        private static void RemoveBadString(Class @class, IEnumerable<Class> classes)
        {
            int count = classes.Count(c => c.XmlName == @class.Name);
            if (count > 0 && !@classes.Contains(@class))
            {
                @class.Name = @class.Name.Replace("-", "");
            }
            else
            {
                @class.Name = @class.Name.Replace("-", "");
            }
        }

        private static IEnumerable<Property> DuplicateElementsToList(IEnumerable<Property> properties)
        {
            return properties.GroupBy(p => p.Name).Select(p => p.Count() <= 1 ? p.First() : new Property
            {
                Name = p.Key,
                Namespace = p.First().Namespace,
                Type = $"List<{p.First().Type.FirstLetterUpper()}>",
                XmlName = p.First().Name,
                XmlType = XmlType.Element
            });
        }
    }
}
