using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class SectionElementTests
    {
        private OptionsSerializer optionsSerializer;
        private MemoryStream actualStream;
        private GeneratorOptions generatorOptions;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
            actualStream = new MemoryStream();

            generatorOptions = new GeneratorOptions();
        }

        [TearDown]
        public void TearDown()
        {
            if (actualStream != null)
                actualStream.Dispose();
        }

        [Test]
        public void section_element_renders_name_attribute_if_Name_is_set()
        {
            generatorOptions.Sections.Add(new Section
            {
                Name = "Section1"
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@name", "Section1");
        }

        [Test]
        public void section_element_does_not_render_name_attribute_if_Name_is__not_set()
        {
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 0);
        }

        [Test]
        public void section_element_contains_repeat_attribute_if_RepeatCount_is_greater_then_1()
        {
            generatorOptions.Sections.Add(new Section
            {
                RepeatCount = 2
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@repeat", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@repeat", "2");
        }

        [Test]
        public void section_element_does_not_contain_repeat_attribute_if_RepeatCount_is_1()
        {
            generatorOptions.Sections.Add(new Section
            {
                RepeatCount = 1
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@count", 0);
        }

        [Test]
        public void section_element_contains_separator_attribute_if_Separator_is_not_null()
        {
            generatorOptions.Sections.Add(new Section
            {
                Separator = ";"
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separator", ";");
        }

        [Test]
        public void section_element_does_not_contain_separator_attribute_if_Separator_is_null()
        {
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 0);
        }

        [Test]
        public void section_element_contains_separatorType_attribute_if_SeparatorType_is_Postfix()
        {
            generatorOptions.Sections.Add(new Section
            {
                SeparatorType = SeparatorType.Postfix
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", "Postfix");
        }

        [Test]
        public void section_element_does_not_contain_separatorType_attribute_if_SeparatorType_is_Infix()
        {
            generatorOptions.Sections.Add(new Section
            {
                SeparatorType = SeparatorType.Infix
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", 0);
        }

        [Test]
        public void section_element_contains_template_child_if_Template_is_set()
        {
            generatorOptions.Sections.Add(new Section
            {
                Template = "some template"
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:template", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:template", "some template");
        }

        [Test]
        public void section_element_does_not_contain_template_child_if_Template_is_not_set()
        {
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:template", 0);
        }

        [Test]
        public void section_element_does_not_contain_parameter_child_if_Parameters_is_not_set()
        {
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 0);
        }

        [Test]
        public void one_parameter_element_is_created_if_one_Patameter_exists()
        {
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new EmptyValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 1);
        }

        [Test]
        public void two_parameter_elements_are_created_if_two_Patameters_exists()
        {
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new EmptyValueProvider()
            });
            section.Parameters.Add(new Parameter
            {
                Key = "key2",
                ValueProvider = new EmptyValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 2);
        }

        [Test]
        public void one_subsection_is_rendered_if_one_subsection_exists()
        {
            Section section = new Section();
            section.Sections.Add(new Section());
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:section", 1);
        }

        [Test]
        public void two_subsections_are_rendered_if_two_subsections_exists()
        {
            Section section = new Section();
            section.Sections.Add(new Section());
            section.Sections.Add(new Section());
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:section", 2);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
