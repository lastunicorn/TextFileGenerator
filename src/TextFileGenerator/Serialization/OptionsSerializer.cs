using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization.EntityTranslators;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public class OptionsSerializer
    {
        public void Serialize(Stream outputStream, GeneratorOptions generatorOptions)
        {
            textFileGenerator textFileGenerator = OptionsTranslator.Translate(generatorOptions);

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    "
            };

            using (XmlWriter sw = XmlWriter.Create(outputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                serializer.Serialize(sw, textFileGenerator);
            }
        }

        public GeneratorOptions Deserialize(Stream inputStream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = false,
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.Schema
            };

            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "DustInTheWind.TextFileGenerator.Serialization.TextFileGenerator.xsd";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            using (XmlReader xmlReader = XmlReader.Create(inputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                textFileGenerator sourceOptions = (textFileGenerator)serializer.Deserialize(xmlReader);

                return OptionsTranslator.Translate(sourceOptions);
            }
        }
    }
}
