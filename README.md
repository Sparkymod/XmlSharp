# XmlSharp
Library for create CSharp class compatible with XmlSerializer.
![CS](https://img.shields.io/badge/C%23%20-%23239120.svg?&style=flat&logo=c%2B%2B&logoColor=white) 
![.Net](https://img.shields.io/badge/.NET_6.0-%230059b3.svg?&style=flat&logo=&logoColor=white)

## Usage

```csharp
// Read all the text from the xml file.
string xml = File.ReadAllText("file.xml");

// Create the class info and parse the xml text.
IEnumerable<Class> classInfo = XmlParser.Parse(xml);

// Since this is for .net 6 and c# 10, this is a new way to instance a class.
// Pass the info to the classWriter.
ClassWriter classWriter = new(classInfo);

// Then create a writer with the path you want to save the new csharp file.
string filePath = "C:\\ExportCSharpClass\\file.cs";
StreamWriter writer = new(filePath);

// Enable autoflush to ensure save all the text into de file.
// First you have to write the Header, wich is the 3 using statements you need, and a custom namespace you want.
writer.AutoFlush = true;
classWriter.Header(writer, "Maplecodex2.Items");

// Second you write the body.
classWriter.Write(writer);

// And finally the footer, this is for the close brackets missing.
classWriter.Footer(writer);
```
Then you will have the CSharp file with all the metadata and elements converted.

## License 
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

## Additional Info

**Special thanks to:** [Msyoung](https://github.com/msyoung/XmlToCSharp)
