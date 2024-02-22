using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.Templating.TextTemplateTests
{
    [TestFixture]
    public class NullParametersTests
    {
        [Test]
        public void returns_string_empty_if_Value_is_null()
        {
            TextTemplate textTemplate = new TextTemplate(null);

            string actual = textTemplate.Format(null);

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void returns_the_Value_if_provided_list_of_paramers_is_null()
        {
            TextTemplate textTemplate = new TextTemplate("this is a text");

            string actual = textTemplate.Format(null);

            Assert.That(actual, Is.EqualTo("this is a text"));
        }
    }
}
