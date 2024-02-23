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

using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using DustInTheWind.TextFileGenerator.Tests.Utils;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerSerializationTests;

[TestFixture]
public class SerializeParameterTests
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
    public void parameter_element_contains_key_attribute()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = EmptyValueProvider.Value
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/@Name", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/@Name", "key1");
    }

    [Test]
    public void serialize_two_parameters_of_different_type()
    {
        project.Sections[0].SectionText = new TextTemplate("template1");
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new ConstantValueProvider()
        });
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key2",
            ValueProvider = new CounterValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter", 2);
    }

    private XmlAssert PerformTestAndCreateAsserterOnResult()
    {
        fileDescriptorSerializer.Serialize(actualStream, project);

        actualStream.Position = 0;

        XmlAssert xmlAssert = new(actualStream);
        xmlAssert.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

        return xmlAssert;
    }
}