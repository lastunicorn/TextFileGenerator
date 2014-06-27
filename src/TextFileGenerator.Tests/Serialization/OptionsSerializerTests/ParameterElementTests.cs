using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class ParameterElementTests
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
        public void parameter_element_contains_key_attribute()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new EmptyValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/@key", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/@key", "key1");
        }

        [Test]
        public void parameter_element_contains_randomText_element_if_Parameter_has_a_RandomTextValueProvider()
        {
            GeneratorOptions generatorOptions = CreateOptionsWithOneSection();
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new RandomTextValueProvider()
            });

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:randomText", 1);
        }

        private GeneratorOptions CreateOptionsWithOneSection()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());

            return generatorOptions;
        }

        private XmlAsserter SerializeAndCreateNavigatorOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
