using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlSharp
{
    public class ClassWriter
    {
        private readonly string Tab = string.Empty.PadRight(4);
        private readonly IEnumerable<Class> Classes;

        public ClassWriter(IEnumerable<Class> classes)
        {
            Classes = classes;
        }

        public void Write(TextWriter textWriter, string @namespace = "XmlSharp")
        {
            foreach (Class @class in Classes)
            {
                // Required Using.
                textWriter.WriteLine("using System;");
                textWriter.WriteLine("using System.Xml.Serialization;");
                textWriter.WriteLine("using System.Collections.Generic;");
                textWriter.WriteLine("");
                textWriter.WriteLine($"namespace {@namespace}");
                textWriter.WriteLine("{");
                textWriter.WriteLine($"{Tab}[XmlRoot(ElementName=\"{@class.XmlName}\")]");
                textWriter.WriteLine($"{Tab}public class {@class.Name.FirstLetterUpper()}");
                textWriter.WriteLine($"{Tab}" + "{");
                foreach (Property property in @class.Properties)
                {
                    textWriter.WriteLine($"{Tab}{Tab}[Xml{property.XmlType}({property.XmlType}Name=\"{property.XmlName}\")]");
                    textWriter.WriteLine($"{Tab}{Tab}public {property.Type.ToLower()} {property.Name.FirstLetterUpper()}" + " { get; set; }");
                }
                textWriter.WriteLine($"{Tab}" + "}"); // Class close
                textWriter.WriteLine("}"); // Namespce close.
                textWriter.WriteLine("");
            }
        }
    }
}
