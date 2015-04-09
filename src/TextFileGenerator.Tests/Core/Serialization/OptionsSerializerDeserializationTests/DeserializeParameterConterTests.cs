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

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterCounterTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
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
        public void deserialized_CounterValueProvider_contains_the_format_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <counter format=""0000""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            CounterValueProvider valueProvider = (CounterValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.Format, Is.EqualTo("0000"));
        }

        [Test]
        public void deserialized_CounterValueProvider_contains_the_startValue_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <counter startValue=""7""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            CounterValueProvider valueProvider = (CounterValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.StartValue, Is.EqualTo(7));
        }

        [Test]
        public void deserialized_CounterValueProvider_contains_the_step_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<textFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <sections>
        <section name=""root"">
            <text/>
            <parameter name=""key1"">
                <counter step=""9""/>
            </parameter>
        </section>
    </sections>
</textFileGenerator>";

            FileDescriptor options = PerformTest(xml);

            CounterValueProvider valueProvider = (CounterValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.Step, Is.EqualTo(9));
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
