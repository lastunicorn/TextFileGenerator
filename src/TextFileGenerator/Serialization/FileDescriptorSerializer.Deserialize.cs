// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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