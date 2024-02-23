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
using DustInTheWind.TextFileGenerator.Domain.FileGeneration;
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Domain.FileGeneration.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithCounterParameterTests
    {
        private Project project;

        [SetUp]
        public void SetUp()
        {
            project = new Project();
        }

        [Test]
        public void replaces_counter_parameter_in_section_template()
        {
            project.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test {param1}")
            });
            project.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name = "param1",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test 10"));
        }

        [Test]
        public void replaces_counter_parameter_in_section_template_rendered_multiple_times()
        {
            project.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test{param1}"),
                RepeatCount = 2
            });
            project.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name = "param1",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test10test12"));
        }

        [Test]
        public void replaces_one_counter_and_one_constant_parameter_in_section_template()
        {
            project.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test {param1} {param2}")
            });
            project.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name = "param1",
                    ValueProvider = new ConstantValueProvider { Value = "alez" }
                },
                new Parameter
                {
                    Name = "param2",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test alez 10"));
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
}