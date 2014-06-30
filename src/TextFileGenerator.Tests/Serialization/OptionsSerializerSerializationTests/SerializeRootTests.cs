﻿using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeRootTests
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
        public void root_element_textFileGenerator_is_created_in_correct_namespace()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult(generatorOptions);

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator", 1);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}