namespace XmlSharp
{
    /// <summary>
    /// Class use for write the exported class.
    /// </summary>
    public class ClassWriter
    {
        private readonly string Tab = string.Empty.PadRight(4);
        private readonly IEnumerable<Class> Classes;

        public ClassWriter(IEnumerable<Class> classes)
        {
            Classes = classes;
        }

        /// <summary>
        /// Create the using and the namespace specify in the param.
        /// </summary>
        /// <param name="textWriter">The writer where the file will be saved.</param>
        /// <param name="namespace">The namespace of the exported class.</param>
        public void Header(TextWriter textWriter, string @namespace = "XmlSharp")
        {
            // Required Using.
            textWriter.WriteLine("using System;");
            textWriter.WriteLine("using System.Xml.Serialization;");
            textWriter.WriteLine("using System.Collections.Generic;");
            textWriter.WriteLine("");
            textWriter.WriteLine($"namespace {@namespace}");
            textWriter.WriteLine("{");
        }

        /// <summary>
        /// Main writer for all the XElements of the XML.
        /// </summary>
        /// <param name="textWriter">The writer where the file will be saved.</param>
        public void Write(TextWriter textWriter)
        {
            foreach (Class @class in Classes)
            {
                textWriter.WriteLine("");
                textWriter.WriteLine($"{Tab}[XmlRoot(ElementName=\"{@class.XmlName}\")]");
                textWriter.WriteLine($"{Tab}public class {@class.Name.FirstLetterUpper()}");
                textWriter.WriteLine($"{Tab}" + "{");
                foreach (Property property in @class.Properties)
                {
                    // Correction for types in lowercases.
                    string typeName = property.Type == "string" ? property.Type : property.Type.FirstLetterUpper();
                    textWriter.WriteLine($"{Tab}{Tab}[Xml{property.XmlType}({property.XmlType}Name=\"{property.XmlName}\")]");
                    textWriter.WriteLine($"{Tab}{Tab}public {typeName} {property.Name.FirstLetterUpper()}" + " { get; set; }");
                }
                textWriter.WriteLine($"{Tab}" + "}"); // Class close
            }
        }

        /// <summary>
        /// Ensure the class to close the namespace.
        /// </summary>
        /// <param name="textWriter">The writer where the file will be saved.</param>
        public void Footer(TextWriter textWriter)
        {
            textWriter.WriteLine("}"); // Namespce close.
            textWriter.WriteLine("");
        }
    }
}
