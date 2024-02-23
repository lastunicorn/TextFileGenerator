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
using DustInTheWind.TextFileGenerator.Tests.Utils;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterConstantTests
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
            actualStream?.Dispose();
        }

        [Test]
        public void parameter_element_contains_constant_element_if_Parameter_has_a_ConstantValueProvider()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new ConstantValueProvider()
            });

            XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

            xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Constant", 1);
        }

        [Test]
        public void constant_element_contains_value_attribute_if_Value_was_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new ConstantValueProvider { Value = "some text" }
            });

            XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

            xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Constant/@Value", 1);
            xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Constant/@Value", "some text");
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_not_set()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new ConstantValueProvider()
            });

            XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

            xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Constant/@Value", 0);
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_set_to_empty_string()
        {
            project.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new ConstantValueProvider { Value = string.Empty }
            });

            XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

            xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Constant/@Value", 0);
        }

        private XmlAssert PerformTestAndCreateAsserterOnResult()
        {
            fileDescriptorSerializer.Serialize(actualStream, project);

            actualStream.Position = 0;

            XmlAssert xmlAssert = new XmlAssert(actualStream);
            xmlAssert.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAssert;
        }
    }
}