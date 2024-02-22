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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.EntityTranslators;
using XTextFileGenerator = DustInTheWind.TextFileGenerator.Serialization.TextFileGenerator;

namespace DustInTheWind.TextFileGenerator.ProjectAccess.Serialization
{
    internal partial class FileDescriptorSerializer
    {
        private const string XsdResourcePath = "DustInTheWind.TextFileGenerator.ProjectAccess.Serialization.TextFileGenerator.xsd";

        public Project Deserialize(Stream inputStream)
        {
            XmlReaderSettings settings = CreateDeserializationSettings();

            using (XmlReader xmlReader = XmlReader.Create(inputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(XTextFileGenerator));
                XTextFileGenerator sourceOptions = (XTextFileGenerator)serializer.Deserialize(xmlReader);

                return sourceOptions.ToDomainEntity();
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

            using (Stream stream = EmbeddedResources.GetEmbeddedStream(XsdResourcePath))
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            return settings;
        }
    }
}