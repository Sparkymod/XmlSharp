using XmlSharp;

Property props = new Property()
{
    Name = "feature",
    XmlName = "env",
    Type = "int",
    XmlType = XmlType.Attribute
};

IEnumerable<Property> properties = new List<Property>() { props };

Class @class = new Class()
{
    Name = "ms2",
    XmlName = "element",
    Properties = properties
};

IEnumerable<Class> classes = new List<Class>() { @class };

ClassWriter classWriter = new ClassWriter(classes);

classWriter.Write(Console.Out, "Maplecodex2.Items");