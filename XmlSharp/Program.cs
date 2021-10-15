using XmlSharp;


string filePath = "C:/Users/rafael.nunez/source/repos/Maplecodex2/ExportClasses/FinalXML.cs";
//string currentXml = File.ReadAllText("C:\\Users\\rafael.nunez\\source\\repos\\Maplecodex2\\Maplecodex2.DBSync\\Data\\Xml\\item\\1\\02\\10200008.xml");

string currentXml = "<ms2>" +
    "   <asset test=\"0\">" +
    "       <custom prueba=\"0\"></custom>" +
    "       <custom prueba=\"0\"></custom>" +
    "       <custom prueba=\"0\"></custom>" +
    "   </asset>" +
    "</ms2>";

IEnumerable<Class> classInfo = XmlParser.Parse(currentXml);
ClassWriter finalClass = new(classInfo);
StreamWriter writer = new(filePath);

writer.AutoFlush = true;
finalClass.Header(writer, "Maplecodex2.Items");
finalClass.Write(writer);
finalClass.Footer(writer);