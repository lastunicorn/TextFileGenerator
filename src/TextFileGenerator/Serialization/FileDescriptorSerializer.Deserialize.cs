using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Serialization.EntityTranslators;

namespace DustInTheWind.TextFileGenerator.Serialization
{
    public partial class FileDescriptorSerializer
    {
        private const string XsdResourcePath = "DustInTheWind.TextFileGenerator.Serialization.TextFileGenerator.xsd";

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

            using (Stream stream = EmbededResources.GetEmbededStream(XsdResourcePath))
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            return settings;
        }
    }
}