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
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Options.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithRandomTextParameterTests
    {
        [Test]
        public void test()
        {
            GeneratorOptions options = new GeneratorOptions();
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test {param1}" }
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
                    ValueProvider = new RandomTextValueProvider { MinLength = 10, MaxLength = 100 }
                }
            });

            string actual = PerformTest(options);

            Assert.That(actual, Contains.Substring("test "));
            Assert.That(actual.Length, Is.GreaterThanOrEqualTo(15));
            Assert.That(actual.Length, Is.LessThanOrEqualTo(105));
        }

        private static string PerformTest(GeneratorOptions options)
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
