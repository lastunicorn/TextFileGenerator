using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class ParameterConstantElementTests
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
        public void parameter_element_contains_constant_element_if_Parameter_has_a_ConstantValueProvider()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant", 1);
        }

        [Test]
        public void constant_element_contains_value_attribute_if_Value_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider { Value = "some text" }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", "some text");
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 0);
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_set_to_empty_string()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider { Value = string.Empty }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 0);
        }

        private XmlAsserter SerializeAndCreateNavigatorOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
