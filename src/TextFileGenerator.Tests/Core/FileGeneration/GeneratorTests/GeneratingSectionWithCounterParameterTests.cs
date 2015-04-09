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
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;
using DustInTheWind.TextFileGenerator.FileGeneration;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.FileGeneration.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithCounterParameterTests
    {
        private FileDescriptor options;

        [SetUp]
        public void SetUp()
        {
            options = new FileDescriptor();
        }

        [Test]
        public void replaces_counter_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test {param1}" }
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test 10"));
        }

        [Test]
        public void replaces_counter_parameter_in_section_template_rendered_multiple_times()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test{param1}" },
                RepeatCount = 2
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test10test12"));
        }

        [Test]
        public void replaces_one_counter_and_one_constant_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                SectionText = new SectionText { Value = "test {param1} {param2}" }
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Name ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "alez" }
                },
                new Parameter
                {
                    Name ="param2",
                    ValueProvider = new CounterValueProvider { StartValue = 10, Step = 2 }
                }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test alez 10"));
        }

        private string PerformTest()
        {
            Generator generator = new Generator(options);

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                generator.Generate(sw);

                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }
    }
}
