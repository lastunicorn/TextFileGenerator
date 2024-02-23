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
public class SerializeParameterRandomNumberTests
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
    public void parameter_element_contains_randomNumber_element_if_Parameter_has_a_RandomNumberValueProvider()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber", 1);
    }

    [Test]
    public void randomNumber_element_contains_format_attribute_if_Format_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider { Format = "###" }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@Format", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@Format", "###");
    }

    [Test]
    public void randomNumber_element_does_not_contain_format_attribute_if_Format_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@Format", 0);
    }

    [Test]
    public void randomNumber_element_contains_minValue_attribute_if_MinValue_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider { MinValue = 5 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MinValue", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MinValue", "5");
    }

    [Test]
    public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MinValue", 0);
    }

    [Test]
    public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_set_to_0()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider { MinValue = 0 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MinValue", 0);
    }

    [Test]
    public void randomNumber_element_contains_maxValue_attribute_if_MaxValue_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider { MaxValue = 3 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MaxValue", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MaxValue", "3");
    }

    [Test]
    public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MaxValue", 0);
    }

    [Test]
    public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_set_to_99()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new RandomNumberValueProvider { MaxValue = 99 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:RandomNumber/@MaxValue", 0);
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