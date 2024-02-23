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

using DustInTheWind.TextFileGenerator.Domain.FileGeneration;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Domain.FileGeneration.OutputTests;

[TestFixture]
public class GeneratingSubsectionsTests
{
    private Project project;

    [SetUp]
    public void SetUp()
    {
        project = new Project();
    }

    [Test]
    public void writes_a_subsection_if_it_is_declared()
    {
        project.Sections.Add(new Section());
        project.Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("subsection")
        });

        string actual = PerformTest();

        Assert.That(actual, Is.EqualTo("subsection"));
    }

    [Test]
    public void writes_two_subsections_if_they_are_declared()
    {
        project.Sections.Add(new Section());
        project.Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("subsection1")
        });
        project.Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("subsection2")
        });

        string actual = PerformTest();

        Assert.That(actual, Is.EqualTo("subsection1subsection2"));
    }

    [Test]
    public void if_both_Template_and_Subsections_are_declared_only_Template_is_randered()
    {
        project.Sections.Add(new Section
        {
            SectionText = new TextTemplate("Template")
        });
        project.Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("Subtemplate")
        });

        string actual = PerformTest();

        Assert.That(actual, Is.EqualTo("Template"));
    }

    [Test]
    public void parameter_declared_in_section_is_available_in_the_child_section()
    {
        project.Sections.Add(new Section());
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "param1",
            ValueProvider = new ConstantValueProvider { Value = "value1" }
        });
        project.Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("{param1}")
        });

        string actual = PerformTest();

        Assert.That(actual, Is.EqualTo("value1"));
    }

    [Test]
    public void parameter_declared_in_section_is_available_in_the_child_subsection()
    {
        project.Sections.Add(new Section());
        project.Sections[0].Parameters.Add(new Parameter
        {
            Name = "param1",
            ValueProvider = new ConstantValueProvider { Value = "value1" }
        });
        project.Sections[0].Sections.Add(new Section());
        project.Sections[0].Sections[0].Sections.Add(new Section
        {
            SectionText = new TextTemplate("{param1}")
        });

        string actual = PerformTest();

        Assert.That(actual, Is.EqualTo("value1"));
    }

    private string PerformTest()
    {
        using MemoryStream ms = new();
        using Output output = new(ms);

        output.AddSections(project.Sections);

        ms.Position = 0;

        StreamReader sr = new(ms);
        return sr.ReadToEnd();
    }
}