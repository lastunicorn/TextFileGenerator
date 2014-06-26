using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Parameters;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests
{
    [TestFixture]
    public class SectionWithRandomTextParameterTests
    {
        [Test]
        public void test()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test {param1}"
            };
            section.Parameters.AddRange(new IParameter[]
            {
                new RandomTextParameter { Key = "param1", MinLength = 10, MaxLength = 100 }
            });
            options.Sections.Add(section);

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
