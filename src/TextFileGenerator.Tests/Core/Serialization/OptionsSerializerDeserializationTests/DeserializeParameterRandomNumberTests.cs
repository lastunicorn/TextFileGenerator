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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using DustInTheWind.TextFileGenerator.ProjectAccess.Serialization;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerDeserializationTests
{
    [TestFixture]
    public class DeserializeParameterRandomNumberTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
        }

        [Test]
        public void deserialized_parameter_contains_RandomNumberValueProvider()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <RandomNumber />
        </Parameter>
    </Section>
</TextFileGenerator>";

            Project options = PerformTest(xml);

            Assert.That(options.Sections[0].Parameters[0].ValueProvider, Is.TypeOf<RandomNumberValueProvider>());
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_format_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <RandomNumber Format=""00""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

            Project options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.Format, Is.EqualTo("00"));
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_minValue_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <RandomNumber MinValue=""3""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

            Project options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MinValue, Is.EqualTo(3));
        }

        [Test]
        public void deserialized_RandomNumberValueProvider_contains_the_maxValue_declared_in_xml()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<TextFileGenerator xmlns=""http://alez.ro/TextFileGenerator"">
    <Section Name=""root"">
        <Text />
        <Parameter Name=""key1"">
            <RandomNumber MaxValue=""5""/>
        </Parameter>
    </Section>
</TextFileGenerator>";

            Project options = PerformTest(xml);

            RandomNumberValueProvider valueProvider = (RandomNumberValueProvider)options.Sections[0].Parameters[0].ValueProvider;
            Assert.That(valueProvider.MaxValue, Is.EqualTo(5));
        }

        private Project PerformTest(string xml)
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