﻿// TextFileGenerator
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
using DustInTheWind.TextFileGenerator.Options;
using DustInTheWind.TextFileGenerator.Options.ValueProviders;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterConstantTests
    {
        private OptionsSerializer optionsSerializer;
        private MemoryStream actualStream;
        private GeneratorOptions generatorOptions;

        [SetUp]
        public void SetUp()
        {
            optionsSerializer = new OptionsSerializer();
            actualStream = new MemoryStream();

            generatorOptions = new GeneratorOptions();
            generatorOptions.Sections.Add(new Section());
        }

        [TearDown]
        public void TearDown()
        {
            if (actualStream != null)
                actualStream.Dispose();
        }

        [Test]
        public void parameter_element_contains_constant_element_if_Parameter_has_a_ConstantValueProvider()
        {
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant", 1);
        }

        [Test]
        public void constant_element_contains_value_attribute_if_Value_was_set()
        {
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider { Value = "some text" }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", "some text");
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_not_set()
        {
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 0);
        }

        [Test]
        public void constant_element_does_not_contain_value_attribute_if_Value_was_set_to_empty_string()
        {
            generatorOptions.Sections[0].Parameters.Add(new Parameter
            {
                Key = "key1",
                ValueProvider = new ConstantValueProvider { Value = string.Empty }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:sections/alez:section/alez:parameter/alez:constant/@value", 0);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            optionsSerializer.Serialize(actualStream, generatorOptions);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}
