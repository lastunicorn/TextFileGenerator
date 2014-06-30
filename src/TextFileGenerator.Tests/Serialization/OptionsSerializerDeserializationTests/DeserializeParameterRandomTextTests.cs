using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterRandomTextTests
    {
        private OptionsSerializer optionsSerializer;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
        }

        [Test]
        public void deserialized_parameter_contains_RandomTextValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <randomText/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<RandomTextValueProvider>());
        }

        [Test]
        public void deserialized_RandomTextValueProvider_contains_the_minLength_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <randomText minLength=""3""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            RandomTextValueProvider valueProvider = (RandomTextValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MinLength, Is.EqualTo(3));
        }

        [Test]
        public void deserialized_RandomTextValueProvider_contains_the_maxLength_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <randomText maxLength=""5""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            RandomTextValueProvider valueProvider = (RandomTextValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MaxLength, Is.EqualTo(5));
        }

        private GeneratorOptions PerformTest(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                sw.Write(xml);
                sw.Flush();

                ms.Position = 0;

                return optionsSerializer.Deserialize(ms);
            }
        }
    }
}
