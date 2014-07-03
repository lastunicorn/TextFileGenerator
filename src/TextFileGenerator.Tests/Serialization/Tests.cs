using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Serialization2;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void deserialize_one_param()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1"" repeat=""5"" separator="";"" separatorType=""Postfix"">
            <template>asd</template>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            textFileGenerator myRoot = PerformTest(xml);

            Assert.That(myRoot.sections[0].parameter.Length, Is.EqualTo(1));
        }

        [Test]
        public void deserialize_two_params()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1"" repeat=""5"" separator="";"" separatorType=""Postfix"">
            <template>asd</template>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
            <parameter key=""key2"">
                <constant value=""value2""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            textFileGenerator myRoot = PerformTest(xml);

            Assert.That(myRoot.sections[0].parameter.Length, Is.EqualTo(2));
        }

        [Test]
        public void deserialize_subsection_and_one_param()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1"" repeat=""5"" separator="";"" separatorType=""Postfix"">
            <section name=""name1.1"" repeat=""2"" separator="","" separatorType=""Postfix"">
                <template>subsection1</template>
            </section>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            textFileGenerator myRoot = PerformTest(xml);

            Assert.That(myRoot.sections[0].parameter.Length, Is.EqualTo(1));
        }

        [Test]
        public void deserialize_subsection_and_two_params()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""name1"" repeat=""5"" separator="";"" separatorType=""Postfix"">
            <section name=""name1.1"" repeat=""2"" separator="","" separatorType=""Postfix"">
                <template>subsection1</template>
            </section>
            <parameter key=""key1"">
                <constant value=""value1""/>
            </parameter>
            <parameter key=""key2"">
                <constant value=""value2""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            textFileGenerator myRoot = PerformTest(xml);

            Assert.That(myRoot.sections[0].parameter.Length, Is.EqualTo(2));
        }

        [Test]
        public void section_contains_two_parameters_of_different_types()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <template/>
            <parameter key=""key1"">
                <constant />
            </parameter>
            <parameter key=""key2"">
                <counter />
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            textFileGenerator myRoot = PerformTest(xml);

            Assert.That(myRoot.sections[0].parameter.Length, Is.EqualTo(2));
        }

        private textFileGenerator PerformTest(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                sw.Write(xml);
                sw.Flush();

                ms.Position = 0;

                return Deserialize(ms);
            }
        }

        private textFileGenerator Deserialize(Stream inputStream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                ConformanceLevel = ConformanceLevel.Document,
                ValidationType = ValidationType.Schema,
            };

            Assembly assembly = Assembly.GetAssembly(typeof(textFileGenerator));
            const string resourceName = "DustInTheWind.TextFileGenerator.Serialization.MyRoot.xsd";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                XmlSchema xmlSchema = XmlSchema.Read(stream, (sender, args) => { });
                settings.Schemas.Add(xmlSchema);
            }

            using (XmlReader xmlReader = XmlReader.Create(inputStream, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(textFileGenerator));
                textFileGenerator sourceOptions = (textFileGenerator)serializer.Deserialize(xmlReader);

                return sourceOptions;
            }
        }
    }
}
