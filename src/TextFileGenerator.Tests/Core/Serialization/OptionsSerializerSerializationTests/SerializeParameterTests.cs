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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;
        private MemoryStream actualStream;
        private FileDescriptor fileDescriptor;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
            actualStream = new MemoryStream();

            fileDescriptor = new FileDescriptor();
            fileDescriptor.Sections.Add(new Section());
        }

        [TearDown]
        public void TearDown()
        {
            if (actualStream != null)
                actualStream.Dispose();
        }

        [Test]
        public void parameter_element_contains_key_attribute()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = EmptyValueProvider.Value
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/@name", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/@name", "key1");
        }

        [Test]
        public void serialize_two_parameters_of_different_type()
        {
            fileDescriptor.Sections[0].SectionText = new TextTemplate("template1");
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new ConstantValueProvider()
            });
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key2",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter", 2);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            fileDescriptorSerializer.Serialize(actualStream, fileDescriptor);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}
