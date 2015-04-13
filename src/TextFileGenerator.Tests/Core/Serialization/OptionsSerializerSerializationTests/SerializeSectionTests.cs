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
using DustInTheWind.TextFileGenerator.Templating;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeSectionTests
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
        }

        [TearDown]
        public void TearDown()
        {
            if (actualStream != null)
                actualStream.Dispose();
        }

        [Test]
        public void section_element_renders_name_attribute_if_Name_is_set()
        {
            fileDescriptor.Sections.Add(new Section
            {
                Name = "Section1"
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@name", "Section1");
        }

        [Test]
        public void section_element_does_not_render_name_attribute_if_Name_is__not_set()
        {
            fileDescriptor.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 0);
        }

        [Test]
        public void section_element_contains_repeat_attribute_if_RepeatCount_is_greater_then_1()
        {
            fileDescriptor.Sections.Add(new Section
            {
                RepeatCount = 2
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@repeat", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@repeat", "2");
        }

        [Test]
        public void section_element_does_not_contain_repeat_attribute_if_RepeatCount_is_1()
        {
            fileDescriptor.Sections.Add(new Section
            {
                RepeatCount = 1
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@count", 0);
        }

        [Test]
        public void section_element_contains_separator_attribute_if_Separator_is_not_null()
        {
            fileDescriptor.Sections.Add(new Section
            {
                Separator = ";"
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separator", ";");
        }

        [Test]
        public void section_element_does_not_contain_separator_attribute_if_Separator_is_null()
        {
            fileDescriptor.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 0);
        }

        [Test]
        public void section_element_contains_separatorType_attribute_if_SeparatorType_is_Postfix()
        {
            fileDescriptor.Sections.Add(new Section
            {
                SeparatorLocation = SeparatorLocation.Postfix
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorLocation", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separatorLocation", "Postfix");
        }

        [Test]
        public void section_element_does_not_contain_separatorType_attribute_if_SeparatorType_is_Infix()
        {
            fileDescriptor.Sections.Add(new Section
            {
                SeparatorLocation = SeparatorLocation.Infix
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorLocation", 0);
        }

        [Test]
        public void section_element_contains_template_child_if_Template_is_set()
        {
            fileDescriptor.Sections.Add(new Section
            {
                SectionText = new TextTemplate("some template")
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:text", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:text", "some template");
        }

        [Test]
        public void section_element_does_not_contain_template_child_if_Template_is_not_set()
        {
            fileDescriptor.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:template", 0);
        }

        [Test]
        public void section_element_does_not_contain_parameter_child_if_Parameters_is_not_set()
        {
            fileDescriptor.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 0);
        }

        [Test]
        public void one_parameter_element_is_created_if_one_Patameter_exists()
        {
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = EmptyValueProvider.Value
            });
            fileDescriptor.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 1);
        }

        [Test]
        public void two_parameter_elements_are_created_if_two_Patameters_exists()
        {
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = EmptyValueProvider.Value
            });
            section.Parameters.Add(new Parameter
            {
                Name = "key2",
                ValueProvider = EmptyValueProvider.Value
            });
            fileDescriptor.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 2);
        }

        [Test]
        public void one_subsection_is_rendered_if_one_subsection_exists()
        {
            Section section = new Section();
            section.Sections.Add(new Section());
            fileDescriptor.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:section", 1);
        }

        [Test]
        public void two_subsections_are_rendered_if_two_subsections_exists()
        {
            Section section = new Section();
            section.Sections.Add(new Section());
            section.Sections.Add(new Section());
            fileDescriptor.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:section", 2);
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
