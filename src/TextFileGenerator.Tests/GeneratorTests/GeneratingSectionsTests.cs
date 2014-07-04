using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSectionsTests
    {
        private GeneratorOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new GeneratorOptions();
        }

        [Test]
        public void outputs_the_section_template_of_one_section()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test"));
        }

        [Test]
        public void outputs_section_template_twice_if_one_section_with_RepeatCount_2()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test" },
                RepeatCount = 2
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("testtest"));
        }

        [Test]
        public void outputs_separator_between_two_instances_of_the_section_if_separator_was_set()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test" },
                RepeatCount = 2,
                Separator = ";"
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test;test"));
        }

        [Test]
        public void outputs_separator_after_each_instance_of_the_section_if_separatorType_is_Postfix()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test" },
                RepeatCount = 2,
                Separator = ";",
                SeparatorType = SeparatorType.Postfix
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("test;test;"));
        }

        [Test]
        public void outputs_separator_before_each_instance_of_the_section_if_separatorType_is_Prefix()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "test" },
                RepeatCount = 2,
                Separator = ";",
                SeparatorType = SeparatorType.Prefix
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo(";test;test"));
        }

        [Test]
        public void writes_two_section_templates_if_two_sections_are_declared()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "section1" }
            });
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "section2" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("section1section2"));
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
