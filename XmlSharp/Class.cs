using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSharp
{
    public class Class
    {
        public string Name { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public string XmlName { get; set; }

        public override string ToString() => $"Name: {Name}, Fields: {Properties}, XmlName: {XmlName}";
    }
}
