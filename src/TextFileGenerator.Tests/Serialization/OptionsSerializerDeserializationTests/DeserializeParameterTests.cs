﻿using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterTests
    {
        private OptionsSerializer optionsSerializer;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
        }

        [Test]
        public void deserialized_section_contains_one_parameter_if_one_parameter_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_parameter_contains_the_key()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].Key, Is.EqualTo("key1"));
        }

        [Test]
        public void deserialized_parameter_contains_ConstantValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<ConstantValueProvider>());
        }

        [Test]
        public void deserialized_parameter_contains_CounterValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <template/>
            <parameter key=""key1"">
                <counter/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<CounterValueProvider>());
        }

        [Test]
        public void section_contains_two_parameters_of_different_types()
        {
//            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
//<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
//    <sections>
//        <section name=""root"">
//            <template/>
//            <parameter key=""key1"">
//                <constant/>
//            </parameter>
//            <parameter key=""key2"">
//                <constant/>
//            </parameter>
//        </section>
//    </sections>
//</textFileGenerator>";

            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <template>template1</template>
            <parameter key=""key1"">
                <constant />
            </parameter>
            <parameter key=""key2"">
                <counter />
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(2));
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
