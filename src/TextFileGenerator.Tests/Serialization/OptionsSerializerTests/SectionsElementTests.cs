﻿using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class SectionsElementTests
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
        public void sections_element_is_not_created_if_it_contains_no_section()
        {
            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections", 0);
        }

        [Test]
        public void sections_element_contains_one_child_if_one_section_is_declared()
        {
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section", 1);
        }

        [Test]
        public void sections_element_contains_two_children_if_two_sections_are_declared()
        {
            generatorOptions.Sections.Add(new Section());
            generatorOptions.Sections.Add(new Section());

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section", 2);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
