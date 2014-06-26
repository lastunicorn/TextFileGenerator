using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class MultipleParameterlessSectionTests
    {
        [Test]
        public void writes_both_section_templates_if_two_sections_are_declared()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section1 = new Section
            {
                Template = "section1"
            };
            Section section2 = new Section
            {
                Template = "section2"
            };
            options.Sections.Add(section1);
            options.Sections.Add(section2);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("section1section2"));
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
