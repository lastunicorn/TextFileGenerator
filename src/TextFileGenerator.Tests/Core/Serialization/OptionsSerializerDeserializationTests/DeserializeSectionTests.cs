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
using System.Text;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeSectionTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
        }

        [Test]
        public void deserialize_an_empty_section()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section/>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_section_contains_Name_if_name_is_Declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section name=""name1""/>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Name, Is.EqualTo("name1"));
        }

        [Test]
        public void deserialized_section_contains_RepeatCount_value_if_repeat_attribute_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section repeat=""7""/>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].RepeatCount, Is.EqualTo(7));
        }

        [Test]
        public void deserialized_section_contains_separator_attribute()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section separator="";""/>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Separator, Is.EqualTo(";"));
        }

        [Test]
        public void deserialized_section_contains_SeparatorType_if_separatorType_attribute_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section separatorLocation=""Postfix""/>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].SeparatorLocation, Is.EqualTo(SeparatorLocation.Postfix));
        }

        [Test]
        public void deserialized_section_contains_Template_if_the_template_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section>
        <text>template1</text>
    </section>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].SectionText.Value, Is.EqualTo("template1"));
        }

        [Test]
        public void deserialized_section_contains_Template_if_the_template_is_crlf()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section>
        <text>&#13;&#10;</text>
    </section>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].SectionText.Value, Is.EqualTo("\r\n"));
        }

        [Test]
        public void deserialized_section_contains_one_subsection_if_one_subsection_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <section name=""root"">
        <section name=""child"">
        </section>
    </section>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Sections.Count, Is.EqualTo(1));
        }

        private FileDescriptor PerformTest(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                sw.Write(xml);
                sw.Flush();

                ms.Position = 0;

                return fileDescriptorSerializer.Deserialize(ms);
            }
        }
    }
}
