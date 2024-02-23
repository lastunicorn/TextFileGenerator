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

using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using DustInTheWind.TextFileGenerator.Tests.Utils;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.ProjectAccess.Serialization.OptionsSerializerSerializationTests;

[TestFixture]
public class SerializeRootTests
{
    private FileDescriptorSerializer fileDescriptorSerializer;
    private MemoryStream actualStream;

    [SetUp]
    public void SetUp()
    {
        fileDescriptorSerializer = new FileDescriptorSerializer();
        actualStream = new MemoryStream();
    }

    [TearDown]
    public void TearDown()
    {
        actualStream?.Dispose();
    }

    [Test]
    public void root_element_textFileGenerator_is_created_in_correct_namespace()
    {
        Project project = new();

        XmlAssert xmlAssert = PerformTestAndCreateAsserterOnResult(project);

        xmlAssert.AssertNodeCount("/alez:TextFileGenerator", 1);
    }

    private XmlAssert PerformTestAndCreateAsserterOnResult(Project project)
    {
        fileDescriptorSerializer.Serialize(actualStream, project);

        actualStream.Position = 0;

        XmlAssert xmlAssert = new(actualStream);
        xmlAssert.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

        return xmlAssert;
    }
}