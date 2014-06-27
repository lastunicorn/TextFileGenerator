using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class ParameterRandomNumberElementTests
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
        public void parameter_element_contains_randomNumber_element_if_Parameter_has_a_RandomNumberValueProvider()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber", 1);
        }

        [Test]
        public void randomNumber_element_contains_format_attribute_if_Format_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider { Format = "###" }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@format", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@format", "###");
        }

        [Test]
        public void randomNumber_element_does_not_contain_format_attribute_if_Format_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@format", 0);
        }

        [Test]
        public void randomNumber_element_contains_minValue_attribute_if_MinValue_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider { MinValue = 5 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@minValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@minValue", "5");
        }

        [Test]
        public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@minValue", 0);
        }

        [Test]
        public void randomNumber_element_does_not_contain_minValue_attribute_if_MinValue_was_set_to_1()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider { MinValue = 1}
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@minValue", 0);
        }

        [Test]
        public void randomNumber_element_contains_maxValue_attribute_if_MaxValue_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider { MaxValue = 3 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@maxValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@maxValue", "3");
        }

        [Test]
        public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@maxValue", 0);
        }

        [Test]
        public void randomNumber_element_does_not_contain_maxValue_attribute_if_MaxValue_was_set_to_100()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomNumberValueProvider { MaxValue = 100 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomNumber/@maxValue", 0);
        }

        private XmlAsserter SerializeAndCreateNavigatorOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
