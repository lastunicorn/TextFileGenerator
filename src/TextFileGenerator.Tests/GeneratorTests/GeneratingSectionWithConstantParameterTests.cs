using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionWithConstantParameterTests
    {
        private GeneratorOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new GeneratorOptions();
        }

        [Test]
        public void replaces_one_constant_parameter_in_section_template()
        {
            options.Sections.Add(new Section
            {
                Template = "test {param1}"
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
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
                Template = "test {param1} {param1}"
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
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
                Template = "test {param1} {param2}"
            });
            options.Sections[0].Parameters.AddRange(new[]
            {
                new Parameter
                {
                    Key ="param1",
                    ValueProvider = new ConstantValueProvider { Value = "value1" }
                },
                new Parameter
                {
                    Key = "param2", 
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
                StreamWriter sw = new StreamWriter(ms);
                generator.Generate(sw);

                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }
    }
}
