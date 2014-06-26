﻿using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Parameters;
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

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
            actualStream = new MemoryStream();
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
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                Name = "Section1"
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@name", "Section1");
        }

        [Test]
        public void section_element_does_not_render_name_attribute_if_Name_is__not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@name", 0);
        }

        [Test]
        public void section_element_contains_count_attribute_if_Count_is_greater_then_1()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                Count = 2
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@count", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@count", "2");
        }

        [Test]
        public void section_element_does_not_contain_count_attribute_if_Count_is_1()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                Count = 1
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@count", 0);
        }

        [Test]
        public void section_element_contains_separator_attribute_if_Separator_is_not_null()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                Separator = ";"
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separator", ";");
        }

        [Test]
        public void section_element_does_not_contain_separator_attribute_if_Separator_is_null()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separator", 0);
        }

        [Test]
        public void section_element_contains_separatorType_attribute_if_SeparatorType_is_Postfix()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                SeparatorType = SeparatorType.Postfix
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", "Postfix");
        }

        [Test]
        public void section_element_does_not_contain_separatorType_attribute_if_SeparatorType_is_Infix()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                SeparatorType = SeparatorType.Infix
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/@separatorType", 0);
        }

        [Test]
        public void section_element_contains_template_child_if_Template_is_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section
            {
                Template = "some template"
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:template", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:template", "some template");
        }

        [Test]
        public void section_element_does_not_contain_template_child_if_Template_is_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:template", 0);
        }

        [Test]
        public void section_element_does_not_contain_parameters_child_if_Parameters_is_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameters", 0);
        }

        [Test]
        public void section_element_contains_parameters_child_if_one_Parameter_is_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new ConstantParameter());
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameters", 1);
        }

        private XmlAsserter SerializeAndCreateNavigatorOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}