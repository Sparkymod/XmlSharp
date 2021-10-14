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

            RemoveBadString(@class, @classes);

            if (xElement.Parent == null || (!@classes.Contains(@class) && @class.Properties.Any()))
            {
                @classes.Add(@class);
            }

            return @class;
        }

        private static IEnumerable<Property> ExportProperties(XElement xElement, ICollection<Class> classes)
        {
            foreach (XElement element in xElement.Elements().ToList())
            {
                Class tempClass = ElementToClass(element, classes);
                string type = element.IsEmpty() ? "string" : tempClass.Name;

                yield return new Property
                {
                    Name = tempClass.Name,
                    Type = type,
                    XmlName = tempClass.XmlName,
                    XmlType = XmlType.Element
                };
            }

            foreach (XAttribute attribute in xElement.Attributes().ToList())
            {
                yield return new Property
                {
                    Name = attribute.Name.LocalName,
                    XmlName = attribute.Name.LocalName,
                    Type = attribute.Value.GetType().Name,
                    XmlType = XmlType.Attribute
                };
            }
        }

        private static void RemoveBadString(Class @class, IEnumerable<Class> classes)
        {
            int count = classes.Count(c => c.XmlName == @class.Name);
            if (count > 0 && !@classes.Contains(@class))
            {
                @class.Name = @class.Name.Replace("-", "") + (count + 1);
            }
            else
            {
                @class.Name = @class.Name.Replace("-", "");
            }
        }

        private static IEnumerable<Property> DuplicateElementsToList(IEnumerable<Property> properties)
        {
            return properties.GroupBy(properties => properties.Name, properties => properties, (key, group) => group.Count() > 1 ? new Property()
                        {
                            Name = key,
                            Namespace = group.First().Namespace,
                            Type = string.Format("List<{0}>", group.First().Type),
                            XmlName = group.First().Type,
                            XmlType = XmlType.Element
                        } : group.First()).ToList();
        }
    }
}
