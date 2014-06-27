using System.IO;
using System.Xml;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization.EntityTranslators;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public class OptionsSerializer
    {
        public void Serialize(Stream outputStream, GeneratorOptions generatorOptions)
        {
            var textFileGenerator = new GeneratorOptionsTranslator(generatorOptions).Create();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";

            using (XmlWriter sw = XmlWriter.Create(outputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                serializer.Serialize(sw, textFileGenerator);
            }
        }
    }
}
