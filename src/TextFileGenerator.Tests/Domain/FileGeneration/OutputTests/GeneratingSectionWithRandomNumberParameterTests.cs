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

using DustInTheWind.TextFileGenerator.Domain.FileGeneration;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Domain.FileGeneration.OutputTests;

[TestFixture]
public class GeneratingSectionWithRandomNumberParameterTests
{
    [Test]
    public void test()
    {
        Project project = new();
        project.Sections.Add(new Section
        {
            SectionText = new TextTemplate("test {param1}")
        });
        project.Sections[0].Parameters.AddRange(new[]
        {
            new Parameter
            {
                Name = "param1",
                ValueProvider = new RandomNumberValueProvider
                {
                    Format = "000",
                    MinValue = 10,
                    MaxValue = 100
                }
            }
        });

        string actual = PerformTest(project);

        Assert.That(actual, Contains.Substring("test "));
    }

    private static string PerformTest(Project project)
    {
        using MemoryStream ms = new();
        using Output output = new(ms);

        output.AddSections(project.Sections);

        ms.Position = 0;

        StreamReader sr = new(ms);
        return sr.ReadToEnd();
    }
}