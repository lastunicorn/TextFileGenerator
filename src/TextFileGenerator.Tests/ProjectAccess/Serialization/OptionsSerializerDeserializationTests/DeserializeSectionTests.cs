﻿// TextFileGenerator
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

using System.Text;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerDeserializationTests;

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
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section />
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections.Count, Is.EqualTo(1));
    }

    [Test]
    public void deserialized_section_contains_Name_if_name_is_Declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""name1""/>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Name, Is.EqualTo("name1"));
    }

    [Test]
    public void deserialized_section_contains_RepeatCount_value_if_repeat_attribute_is_declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Repeat=""7""/>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].RepeatCount, Is.EqualTo(7));
    }

    [Test]
    public void deserialized_section_contains_separator_attribute()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Separator="";""/>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Separator, Is.EqualTo(";"));
    }

    [Test]
    public void deserialized_section_contains_SeparatorType_if_separatorType_attribute_is_declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section SeparatorLocation=""Postfix""/>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].SeparatorLocation, Is.EqualTo(SeparatorLocation.Postfix));
    }

    [Test]
    public void deserialized_section_contains_Template_if_the_template_is_declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section>
        <Text>template1</Text>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].SectionText.Value, Is.EqualTo("template1"));
    }

    [Test]
    public void deserialized_section_contains_Template_if_the_template_is_crlf()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section>
        <Text>&#13;&#10;</Text>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].SectionText.Value, Is.EqualTo("\r\n"));
    }

    [Test]
    public void deserialized_section_contains_one_subsection_if_one_subsection_is_declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Section Name=""child"">
        </Section>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Sections.Count, Is.EqualTo(1));
    }

    private Project PerformTest(string xml)
    {
        using MemoryStream ms = new();
        StreamWriter sw = new(ms, Encoding.UTF8);
        sw.Write(xml);
        sw.Flush();

        ms.Position = 0;

        return fileDescriptorSerializer.Deserialize(ms);
    }
}