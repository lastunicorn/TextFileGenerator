using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerTests
{
    [TestFixture]
    public class ParameterCounterElementTests
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
        public void parameter_element_contains_counter_element_if_Parameter_has_a_CounterValueProvider()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter", 1);
        }

        [Test]
        public void constant_element_contans_format_attribute_if_Format_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider { Format = "###" }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@format", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@format", "###");
        }

        [Test]
        public void constant_element_does_not_contan_format_attribute_if_Format_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@format", 0);
        }

        [Test]
        public void constant_element_contans_startValue_attribute_if_StartValue_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider { StartValue = 10 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@startValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@startValue", "10");
        }

        [Test]
        public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@startValue", 0);
        }

        [Test]
        public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_set_to_1()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider { StartValue = 1 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@startValue", 0);
        }

        [Test]
        public void constant_element_contans_step_attribute_if_Step_was_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider { Step = 4 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@step", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@step", "4");
        }

        [Test]
        public void constant_element_does_not_contan_step_attribute_if_Step_was_not_set()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider()
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@step", 0);
        }

        [Test]
        public void constant_element_does_not_contan_step_attribute_if_Step_was_set_to_1()
        {
            GeneratorOptions generatorOptions = new GeneratorOptions();
            Section section = new Section();
            section.Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new CounterValueProvider { Step = 1 }
            });
            generatorOptions.Sections.Add(section);

            XmlAsserter xmlAsserter = SerializeAndCreateNavigatorOnResult(generatorOptions);

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:counter/@step", 0);
        }

        private XmlAsserter SerializeAndCreateNavigatorOnResult(GeneratorOptions generatorOptions)
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            return new XmlAsserter(actualStream);
        }
    }
}
