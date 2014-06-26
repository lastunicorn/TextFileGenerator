using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Parameters;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class SectionWithCounterParameterTests
    {
        [Test]
        public void replaces_counter_parameter_in_section_template()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test {param1}"
            };
            section.Parameters.AddRange(new IParameter[]
            {
                new Parameter("param1", new CounterValueProvider { StartValue = 10, Step = 2 })
            });
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test 10"));
        }

        [Test]
        public void replaces_counter_parameter_in_section_template_rendered_multiple_times()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test{param1}",
                RepeatCount = 2
            };
            section.Parameters.AddRange(new IParameter[]
            {
                new Parameter("param1", new CounterValueProvider { StartValue = 10, Step = 2 })
            });
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test10test12"));
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
