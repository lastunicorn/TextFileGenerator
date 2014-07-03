using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterTests
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
            generatorOptions.Sections.Add(new Section());
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
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new EmptyValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/@key", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/@key", "key1");
        }

        [Test]
        public void serialize_two_parameters_of_different_type()
        {
            generatorOptions.Sections[0].Template = "template1";
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider()
            });
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key2",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter", 2);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}
