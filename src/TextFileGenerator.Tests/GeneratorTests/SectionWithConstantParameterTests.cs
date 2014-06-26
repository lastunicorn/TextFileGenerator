using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Parameters;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class SectionWithConstantParameterTests
    {
        [Test]
        public void replaces_one_constant_parameter_in_section_template()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test {param1}"
            };
            section.Parameters.AddRange(new[]
            {
                new Parameter("param1", new ConstantValueProvider { Value = "value1" })
            });
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test value1"));
        }

        [Test]
        public void replaces_two_occurences_of_one_constant_parameter_in_section_template()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test {param1} {param1}"
            };
            section.Parameters.AddRange(new[]
            {
                new Parameter("param1", new ConstantValueProvider { Value = "value1" })
            });
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test value1 value1"));
        }

        [Test]
        public void replaces_two_constant_parameters_in_section_template()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test {param1} {param2}"
            };
            section.Parameters.AddRange(new[]
            {
                new Parameter("param1", new ConstantValueProvider { Value = "value1" }),
                new Parameter("param2", new ConstantValueProvider { Value = "value2" })
            });
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test value1 value2"));
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
