using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeSectionTests
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

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_section_contains_Name_if_name_is_Declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Name, Is.EqualTo("name1"));
        }

        [Test]
        public void deserialized_section_contains_RepeatCount_value_if_repeat_attribute_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section repeat=""7""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

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

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Separator, Is.EqualTo(";"));
        }

        [Test]
        public void deserialized_section_contains_SeparatorType_if_separatorType_attribute_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section separatorType=""Postfix""/>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].SeparatorType, Is.EqualTo(SeparatorType.Postfix));
        }

        [Test]
        public void deserialized_section_contains_Template_if_the_template_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <template>template1</template>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Template.Value, Is.EqualTo("template1"));
        }

        [Test]
        public void deserialized_section_contains_Template_if_the_template_is_crlf()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <template>&#13;&#10;</template>
        </section>
    </sections>
</textFileGenerator>";

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Template.Value, Is.EqualTo("\r\n"));
        }

        [Test]
        public void deserialized_section_contains_one_subsection_if_one_subsection_is_declared_in_xml()
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

            GeneratorOptions options = PerformTest(xml);

            Assert.That(options.Sections[0].Sections.Count, Is.EqualTo(1));
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
