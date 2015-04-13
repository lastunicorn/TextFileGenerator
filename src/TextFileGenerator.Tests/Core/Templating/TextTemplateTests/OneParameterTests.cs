using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.TextFileGenerator.FileDescription;
using DustInTheWind.TextFileGenerator.Templating;
using DustInTheWind.TextFileGenerator.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Templating.TextTemplateTests
{
    [TestFixture]
    public class OneParameterTests
    {
        private IEnumerable<Parameter> parameters;

        [SetUp]
        public void SetUp()
        {
            parameters = CreateParameters(new[]
            {
                new Tuple<string, string>("param1", "value1")
            });
        }

        [TestCase("aaa", "aaa")]
        [TestCase("{param1}", "value1")]
        [TestCase("aaa{param1}", "aaavalue1")]
        [TestCase("{param1}bbb", "value1bbb")]
        [TestCase("aaa{param1}bbb", "aaavalue1bbb")]

        [TestCase("aaa{param1bbb", "aaa{param1bbb")]
        [TestCase("aaaparam1}bbb", "aaaparam1}bbb")]
        [TestCase("aaa}param1{bbb", "aaa}param1{bbb")]
        [TestCase("aaa{param1}bbb{param1}ccc", "aaavalue1bbbvalue1ccc")]
        [TestCase("{param1}{param1}", "value1value1")]
        [TestCase("aaa{bbb{param1}ccc", "aaa{bbbvalue1ccc")]
        public void PerformTest(string text, string expected)
        {
            TextTemplate textTemplate = new TextTemplate(text);

            string actual = textTemplate.Format(parameters);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void needed_parameter_is_second_in_list()
        {
            TextTemplate textTemplate = new TextTemplate("{param1}");

            string actual = textTemplate.Format(CreateParameters(new[]
            {
                new Tuple<string, string>("param2", "value2"), 
                new Tuple<string, string>("param1", "value1")
            }));

            Assert.That(actual, Is.EqualTo("value1"));
        }

        private static IEnumerable<Parameter> CreateParameters(IEnumerable<Tuple<string, string>> items)
        {
            return items
                .Select(x => new Parameter
                {
                    Name = x.Item1,
                    ValueProvider = new ConstantValueProvider { Value = x.Item2 }
                });
        }
    }
}
