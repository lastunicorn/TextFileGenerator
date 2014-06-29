using System.Diagnostics;
using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSubsectionsTests
    {
        private GeneratorOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new GeneratorOptions();
        }

        [Test]
        public void writes_a_subsection_if_it_is_declared()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Sections.Add(new Section
            {
                Template = "subsection"
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("subsection"));
        }

        [Test]
        public void writes_two_subsections_if_they_are_declared()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Sections.Add(new Section
            {
                Template = "subsection1"
            });
            options.Sections[0].Sections.Add(new Section
            {
                Template = "subsection2"
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("subsection1subsection2"));
        }

        [Test]
        public void if_both_Template_and_Subsections_are_declared_only_Template_is_rantered()
        {
            options.Sections.Add(new Section
            {
                Template = "Template"
            });
            options.Sections[0].Sections.Add(new Section
            {
                Template = "Subtemplate"
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("Template"));
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
                string generatedText = sr.ReadToEnd();

                Trace.Write("Generated text: " + generatedText);

                return generatedText;
            }
        }
    }
}
