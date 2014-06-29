using System;
using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeSectionElementTests
    {
        private OptionsSerializer optionsSerializer;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
        }

        [Test]
        public void deserialize_an_empty_section()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section/>
    </sections>
</textFileGenerator>";

            //            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
            //<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"" />";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_section_contains_name()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].Name, Is.EqualTo("name1"));
        }

        [Test]
        public void deserialized_section_contains_repeat_attribute()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section repeat=""7""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].RepeatCount, Is.EqualTo(7));
        }

        [Test]
        public void deserialized_section_contains_separator_attribute()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section separator="";""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].Separator, Is.EqualTo(";"));
        }

        [Test]
        public void deserialized_section_contains_separatorType_attribute()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section separatorType=""Postfix""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].SeparatorType, Is.EqualTo(SeparatorType.Postfix));
        }

        [Test]
        public void deserialized_section_contains_template_child_element()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <template>template1</template>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].Template, Is.EqualTo("template1"));
        }

        [Test]
        public void deserialized_section_contains_one_subsection()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <section name=""child"">
            </section>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].Sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_section_contains_one_parameter()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <parameter key=""key1""/>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml, optionsSerializer);

            Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(1));
        }

        private static GeneratorOptions PerformTest(string xml, OptionsSerializer optionsSerializer)
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
