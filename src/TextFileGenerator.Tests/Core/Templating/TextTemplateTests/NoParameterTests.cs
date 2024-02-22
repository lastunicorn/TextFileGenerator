using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using DustInTheWind.TextFileGenerator.FileDescription;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Templating.TextTemplateTests
{
    [TestFixture]
    public class NoParameterTests
    {
        [Test]
        public void returns_whole_text_if_contains_only_one_parameter()
        {
            TextTemplate textTemplate = new TextTemplate("{param1}");

            string actual = textTemplate.Format(new Parameter[0]);

            Assert.That(actual, Is.EqualTo("{param1}"));
        }

        [Test]
        public void returns_whole_text_if_contains_one_parameter_in_the_middle_of_the_text()
        {
            TextTemplate textTemplate = new TextTemplate("aaa{param1}bbb");

            string actual = textTemplate.Format(new Parameter[0]);

            Assert.That(actual, Is.EqualTo("aaa{param1}bbb"));
        }
    }
}