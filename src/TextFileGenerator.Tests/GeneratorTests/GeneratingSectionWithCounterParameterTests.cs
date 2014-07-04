using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithCounterParameterTests
    {
        private GeneratorOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new GeneratorOptions();
        }

        [Test]
        public void replaces_counter_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test {param1}" }
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
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
                Template = new Template { Value = "test{param1}" },
                RepeatCount = 2
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
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
                Template = new Template { Value = "test {param1} {param2}" }
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "alez" }
                },
                new Parameter
                {
                    Key ="param2",
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
