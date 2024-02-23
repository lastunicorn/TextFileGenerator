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
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterRandomTextTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;
        private MemoryStream actualStream;
        private Project project;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
            actualStream = new MemoryStream();

            project = new Project();
            project.Sections.Add(new Section());
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
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText", 1);
        }

        [Test]
        public void randomText_element_contains_minLength_attribute_if_MinLength_was_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider { MinLength = 5 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MinLength", 1);
            xmlAsserter.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MinLength", "5");
        }

        [Test]
        public void randomText_element_does_not_contain_minLength_attribute_if_MinLength_was_not_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MinLength", 0);
        }

        [Test]
        public void randomText_element_does_not_contain_minLength_attribute_if_MinLength_was_set_to_1()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider { MinLength = 1 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MinLength", 0);
        }

        [Test]
        public void randomText_element_contains_maxLength_attribute_if_MaxLength_was_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider { MaxLength = 3 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MaxLength", 1);
            xmlAsserter.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MaxLength", "3");
        }

        [Test]
        public void randomText_element_does_not_contain_maxLength_attribute_if_MaxLength_was_not_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MaxLength", 0);
        }

        [Test]
        public void randomText_element_does_not_contain_maxLength_attribute_if_MaxLength_was_set_to_100()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new RandomTextValueProvider { MaxLength = 100 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomText/@MaxLength", 0);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            fileDescriptorSerializer.Serialize(actualStream, project);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}