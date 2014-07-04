﻿using System.IO;
using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.GeneratorTests
{
    [TestFixture]
    public class GeneratingSubsectionsTests
    {
        private GeneratorOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new GeneratorOptions();
        }

        [Test]
        public void writes_a_subsection_if_it_is_declared()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "subsection" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("subsection"));
        }

        [Test]
        public void writes_two_subsections_if_they_are_declared()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "subsection1" }
            });
            options.Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "subsection2" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("subsection1subsection2"));
        }

        [Test]
        public void if_both_Template_and_Subsections_are_declared_only_Template_is_randered()
        {
            options.Sections.Add(new Section
            {
                Template = new Template { Value = "Template" }
            });
            options.Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "Subtemplate" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("Template"));
        }

        [Test]
        public void parameter_declared_in_section_is_available_in_the_subsection()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Parameters.Add(new Parameter
            {
                Key = "param1",
                ValueProvider = new ConstantValueProvider { Value = "value1" }
            });
            options.Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "{param1}" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("value1"));
        }

        [Test]
        public void parameter_declared_in_section_is_available_in_the_subsubsection()
        {
            options.Sections.Add(new Section());
            options.Sections[0].Parameters.Add(new Parameter
            {
                Key = "param1",
                ValueProvider = new ConstantValueProvider { Value = "value1" }
            });
            options.Sections[0].Sections.Add(new Section());
            options.Sections[0].Sections[0].Sections.Add(new Section
            {
                Template = new Template { Value = "{param1}" }
            });

            string actual = PerformTest();

            Assert.That(actual, Is.EqualTo("value1"));
        }

        private string PerformTest()
        {
            Generator generator = new Generator(options);

            using (MemoryStream ms = new MemoryStream())
            {
                StreamWriter sw = new StreamWriter(ms);
                generator.Generate(sw);

                ms.Position = 0;

                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
        }
    }
}
