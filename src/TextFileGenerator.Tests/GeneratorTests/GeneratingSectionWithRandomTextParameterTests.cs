using System.IO;
using DustInTheWind.TextFileGenerator.Options;
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
