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
    public class SerializeParameterRandomNumberTests
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
        public void parameter_element_contains_randomNumber_element_if_Parameter_has_a_RandomNumberValueProvider()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber", 1);
        }

        [Test]
        public void randomNumber_element_contains_format_attribute_if_Format_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider { Format = "###" }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@format", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@format", "###");
        }

        [Test]
        public void randomNumber_element_does_not_contain_format_attribute_if_Format_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@format", 0);
        }

        [Test]
        public void randomNumber_element_contains_minValue_attribute_if_MinValue_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider { MinValue = 5 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@minValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@minValue", "5");
        }

        [Test]
        public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@minValue", 0);
        }

        [Test]
        public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_set_to_1()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider { MinValue = 1 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@minValue", 0);
        }

        [Test]
        public void randomNumber_element_contains_maxValue_attribute_if_MaxValue_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider { MaxValue = 3 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@maxValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@maxValue", "3");
        }

        [Test]
        public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@maxValue", 0);
        }

        [Test]
        public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_set_to_100()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomNumberValueProvider { MaxValue = 100 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:randomNumber/@maxValue", 0);
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
