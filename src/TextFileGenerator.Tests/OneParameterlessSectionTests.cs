using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests
{
    [TestFixture]
    public class OneParameterlessSectionTests
    {
        [Test]
        public void writes_section_template_if_one_section()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test"
            };
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test"));
        }

        [Test]
        public void writes_section_template_twice_if_one_section_with_count_2()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test",
                Count = 2
            };
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("testtest"));
        }

        [Test]
        public void writes_separator_between_two_instances_of_the_section_if_separator_was_set()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test",
                Count = 2,
                Separator = ";"
            };
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test;test"));
        }

        [Test]
        public void writes_separator_after_each_instance_of_the_section_if_separatorType_is_Postfix()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test",
                Count = 2,
                Separator = ";",
                SeparatorType = SeparatorType.Postfix
            };
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo("test;test;"));
        }

        [Test]
        public void writes_separator_before_each_instance_of_the_section_if_separatorType_is_Prefix()
        {
            GeneratorOptions options = new GeneratorOptions();
            Section section = new Section
            {
                Template = "test",
                Count = 2,
                Separator = ";",
                SeparatorType = SeparatorType.Prefix
            };
            options.Sections.Add(section);

            string actual = PerformTest(options);

            Assert.That(actual, Is.EqualTo(";test;test"));
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
