// TextFileGenerator
// Copyright (C) 2009-2011 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using System.Text;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;
using DustInTheWind.TextFileGenerator.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
        }

        [Test]
        public void deserialized_section_contains_one_parameter_if_one_parameter_is_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(1));
        }

        [Test]
        public void deserialized_parameter_contains_the_key()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].Name, Is.EqualTo("key1"));
        }

        [Test]
        public void deserialized_parameter_contains_ConstantValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <constant value=""value1""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<ConstantValueProvider>());
        }

        [Test]
        public void deserialized_parameter_contains_CounterValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <counter/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<CounterValueProvider>());
        }

        [Test]
        public void section_contains_two_parameters_of_different_types()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section>
            <text/>
            <parameter name=""key1"">
                <constant />
            </parameter>
            <parameter name=""key2"">
                <counter />
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters.Count, Is.EqualTo(2));
        }

        private FileDescriptor PerformTest(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
                sw.Write(xml);
                sw.Flush();

                ms.Position = 0;

                return fileDescriptorSerializer.Deserialize(ms);
            }
        }
    }
}
