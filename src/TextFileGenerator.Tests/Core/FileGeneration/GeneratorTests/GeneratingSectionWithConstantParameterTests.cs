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
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileGeneration;
using DustInTheWind.TextFileGenerator.Templating;
using DustInTheWind.TextFileGenerator.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.FileGeneration.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithConstantParameterTests
    {
        private FileDescriptor options;

        [SetUp]
        public void SetUp()
        {
            options = new FileDescriptor();
        }

        [Test]
        public void replaces_one_constant_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test {param1}")
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "value1" }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test value1"));
        }

        [Test]
        public void replaces_two_occurences_of_one_constant_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test {param1} {param1}")
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "value1" }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test value1 value1"));
        }

        [Test]
        public void replaces_two_constant_parameters_in_section_template()
        {
            options.Sections.Add(new Section
            {
                SectionText = new TextTemplate("test {param1} {param2}")
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "value1" }
                },
                new Parameter
                {
                    Name = "param2", 
                    ValueProvider = new ConstantValueProvider { Value = "value2" }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test value1 value2"));
        }

        private string PerformTest()
        {
            Generator generator = new Generator(options);

            using (MemoryStream ms = new MemoryStream())
            {
                generator.Generate(ms);

                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }
    }
}
