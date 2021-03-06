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
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Serialization;
using DustInTheWind.TextFileGenerator.Tests.TestingTools;
using DustInTheWind.TextFileGenerator.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Serialization.OptionsSerializerSerializationTests
{
    [TestFixture]
    public class SerializeParameterCounterTests
    {
        private FileDescriptorSerializer fileDescriptorSerializer;
        private MemoryStream actualStream;
        private FileDescriptor fileDescriptor;

        [SetUp]
        public void SetUp()
        {
            fileDescriptorSerializer = new FileDescriptorSerializer();
            actualStream = new MemoryStream();

            fileDescriptor = new FileDescriptor();
            fileDescriptor.Sections.Add(new Section());
        }

        [TearDown]
        public void TearDown()
        {
            if (actualStream != null)
                actualStream.Dispose();
        }

        [Test]
        public void parameter_element_contains_counter_element_if_Parameter_has_a_CounterValueProvider()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter", 1);
        }

        [Test]
        public void constant_element_contans_format_attribute_if_Format_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider { Format = "###" }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@format", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@format", "###");
        }

        [Test]
        public void constant_element_does_not_contan_format_attribute_if_Format_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@format", 0);
        }

        [Test]
        public void constant_element_contans_startValue_attribute_if_StartValue_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider { StartValue = 10 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");
            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@startValue", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@startValue", "10");
        }

        [Test]
        public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@startValue", 0);
        }

        [Test]
        public void constant_element_does_not_contan_startValue_attribute_if_StartValue_was_set_to_1()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider { StartValue = 1 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@startValue", 0);
        }

        [Test]
        public void constant_element_contans_step_attribute_if_Step_was_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider { Step = 4 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@step", 1);
            xmlAsserter.AssertText("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@step", "4");
        }

        [Test]
        public void constant_element_does_not_contan_step_attribute_if_Step_was_not_set()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider()
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@step", 0);
        }

        [Test]
        public void constant_element_does_not_contan_step_attribute_if_Step_was_set_to_1()
        {
            fileDescriptor.Sections[0].Parameters.Add(new Parameter
            {
                Name = "key1",
                ValueProvider = new CounterValueProvider { Step = 1 }
            });

            XmlAsserter xmlAsserter = PerformTestAndCreateAsserterOnResult();

            xmlAsserter.AssertNodeCount("/alez:textFileGenerator/alez:section/alez:parameter/alez:counter/@step", 0);
        }

        private XmlAsserter PerformTestAndCreateAsserterOnResult()
        {
            fileDescriptorSerializer.Serialize(actualStream, fileDescriptor);

            actualStream.Position = 0;

            XmlAsserter xmlAsserter = new XmlAsserter(actualStream);
            xmlAsserter.AddNamespace("alez", "http://alez.ro/TextFileGenerator");

            return xmlAsserter;
        }
    }
}
