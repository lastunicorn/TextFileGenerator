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
using DustInTheWind.TextFileGenerator.FileGeneration;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionsTests
    {
        private FileDescriptor options;

        [SetUp]
        public void SetUp()
        {
            options = new FileDescriptor();
        }

        [Test]
        public void outputs_the_section_template_of_one_section()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test"));
        }

        [Test]
        public void outputs_section_template_twice_if_one_section_with_RepeatCount_2()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test" },
                RepeatCount = 2
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("testtest"));
        }

        [Test]
        public void outputs_separator_between_two_instances_of_the_section_if_separator_was_set()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test" },
                RepeatCount = 2,
                Separator = ";"
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test;test"));
        }

        [Test]
        public void outputs_separator_after_each_instance_of_the_section_if_separatorType_is_Postfix()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test" },
                RepeatCount = 2,
                Separator = ";",
                SeparatorLocation = SeparatorLocation.Postfix
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test;test;"));
        }

        [Test]
        public void outputs_separator_before_each_instance_of_the_section_if_separatorType_is_Prefix()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test" },
                RepeatCount = 2,
                Separator = ";",
                SeparatorLocation = SeparatorLocation.Prefix
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo(";test;test"));
        }

        [Test]
        public void writes_two_section_templates_if_two_sections_are_declared()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "section1" }
            });
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "section2" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("section1section2"));
        }

        private string PerformTest()
        {
            Generator generator = new Generator(options);

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                generator.Generate(sw);

                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }
    }
}
