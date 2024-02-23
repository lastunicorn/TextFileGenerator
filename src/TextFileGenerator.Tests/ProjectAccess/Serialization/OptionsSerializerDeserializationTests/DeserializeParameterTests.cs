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

using System.Text;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerDeserializationTests;

[TestFixture]
public class DeserializeParameterTests
{
    private FileDescriptorSerializer fileDescriptorSerializer;

    [SetUp]
    public void SetUp()
    {
        fileDescriptorSerializer = new FileDescriptorSerializer();
    }

    [Test]
    public void deserialized_section_contains_one_parameter_if_one_parameter_is_declared_in_xml()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <Constant Value=""value1""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(1));
    }

    [Test]
    public void deserialized_parameter_contains_the_key()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <Constant Value=""value1""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Parameters[0].Name, Is.EqualTo("key1"));
    }

    [Test]
    public void deserialized_parameter_contains_ConstantValueProvider()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <Constant Value=""value1""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<ConstantValueProvider>());
    }

    [Test]
    public void deserialized_parameter_contains_CounterValueProvider()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <Counter />
        </Parameter>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<CounterValueProvider>());
    }

    [Test]
    public void section_contains_two_parameters_of_different_types()
    {
        const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <Section>
        <Text />
        <Parameter Name=""key1"">
            <Constant />
        </Parameter>
        <Parameter Name=""key2"">
            <Counter />
        </Parameter>
    </Section>
</TextFileGenerator>";

        Project options = PerformTest(xml);

        Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(2));
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