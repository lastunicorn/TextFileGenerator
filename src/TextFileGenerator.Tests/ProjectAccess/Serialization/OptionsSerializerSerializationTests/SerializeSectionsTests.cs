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
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using DustInTheWind.TextFileGenerator.Tests.Utils;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerSerializationTests;

[TestFixture]
public class SerializeSectionsTests
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
    }

    [TearDown]
    public void TearDown()
    {
        actualStream?.Dispose();
    }

    [Test]
    public void sections_element_is_not_created_if_it_contains_no_section()
    {
        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section", 0);
    }

    [Test]
    public void sections_element_contains_one_child_if_one_section_is_declared()
    {
        project.Sections.Add(new Section());

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section", 1);
    }

    [Test]
    public void sections_element_contains_two_children_if_two_sections_are_declared()
    {
        project.Sections.Add(new Section());
        project.Sections.Add(new Section());

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult();

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator/alez:Section", 2);
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