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
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterRandomTextTests
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
        public void parameter_element_contains_randomText_element_if_Parameter_has_a_RandomTextValueProvider()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText", 1);
        }

        [Test]
        public void randomText_element_contains_minLength_attribute_if_MinLength_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider { MinLength = 5 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@minLength", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@minLength", "5");
        }

        [Test]
        public void randomText_element_does_not_contain_minLength_attribute_if_MinLength_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@minLength", 0);
        }

        [Test]
        public void randomText_element_does_not_contain_minLength_attribute_if_MinLength_was_set_to_1()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider { MinLength = 1 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@minLength", 0);
        }

        [Test]
        public void randomText_element_contains_maxLength_attribute_if_MaxLength_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider { MaxLength = 3 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@maxLength", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@maxLength", "3");
        }

        [Test]
        public void randomText_element_does_not_contain_maxLength_attribute_if_MaxLength_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@maxLength", 0);
        }

        [Test]
        public void randomText_element_does_not_contain_maxLength_attribute_if_MaxLength_was_set_to_100()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider { MaxLength = 100 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText/@maxLength", 0);
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
