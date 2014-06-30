using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterRandomNumberTests
    {
        private OptionsSerializer optionsSerializer;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
        }

        [Test]
        public void deserialized_parameter_contains_RandomNumberValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <parameter key=""key1"">
                <randomNumber/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<RandomNumberValueProvider>());
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_format_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <parameter key=""key1"">
                <randomNumber format=""00""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.Format, Is.EqualTo("00"));
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_minValue_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <parameter key=""key1"">
                <randomNumber minValue=""3""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MinValue, Is.EqualTo(3));
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_maxValue_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <parameter key=""key1"">
                <randomNumber maxValue=""5""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MaxValue, Is.EqualTo(5));
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
