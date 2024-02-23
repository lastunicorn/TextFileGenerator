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
public class SerializeParameterCounterTests
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
    public void parameter_element_contains_counter_element_if_Parameter_has_a_CounterValueProvider()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter", 1);
    }

    [Test]
    public void constant_element_contans_format_attribute_if_Format_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider { Format = "###" }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Format", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Format", "###");
    }

    [Test]
    public void constant_element_does_not_contan_format_attribute_if_Format_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Format", 0);
    }

    [Test]
    public void constant_element_contans_startValue_attribute_if_StartValue_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider { StartValue = 10 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@StartValue", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@StartValue", "10");
    }

    [Test]
    public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@StartValue", 0);
    }

    [Test]
    public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_set_to_1()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider { StartValue = 1 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@StartValue", 0);
    }

    [Test]
    public void constant_element_contans_step_attribute_if_Step_was_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider { Step = 4 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Step", 1);
        xmlAssert.AssertText("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Step", "4");
    }

    [Test]
    public void constant_element_does_not_contan_step_attribute_if_Step_was_not_set()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider()
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Step", 0);
    }

    [Test]
    public void constant_element_does_not_contan_step_attribute_if_Step_was_set_to_1()
    {
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "key1",
            ValueProvider = new CounterValueProvider { Step = 1 }
        });

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section/alez:Parameter/alez:Counter/@Step", 0);
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