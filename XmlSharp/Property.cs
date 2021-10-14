using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSharp
{
    public class Property
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public XmlType XmlType { get; set; }
        public string Namespace { get; set; }
        public string XmlName { get; set; }

        public override string ToString() => $"Name: {Name}, Type: {Type}, XmlType: {XmlType}, Namespace: {Namespace}, XmlName: {XmlName}";
    }
}
