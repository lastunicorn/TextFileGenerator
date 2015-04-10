using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Serialization.EntityTranslators;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public partial class FileDescriptorSerializer
    {
        public FileDescriptor Deserialize(Stream inputStream)
        {
            XmlReaderSettings settings = CreateDeserializationSettings();

            using (XmlReader xmlReader = XmlReader.Create(inputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                textFileGenerator sourceOptions = (textFileGenerator)serializer.Deserialize(xmlReader);

                return DescriptorTranslator.ToDomainEntity(sourceOptions);
            }
        }

        private static XmlReaderSettings CreateDeserializationSettings()
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = false,
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.Schema
            };

            using (Stream stream = GetEmbededXmlValidator())
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            return settings;
        }

        private static Stream GetEmbededXmlValidator()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "DustInTheWind.TextFileGenerator.Serialization.TextFileGenerator.xsd";

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}