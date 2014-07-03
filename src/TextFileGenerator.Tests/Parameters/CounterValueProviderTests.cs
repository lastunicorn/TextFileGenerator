using DustInTheWind.TextFileGenerator.Options;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Parameters
{
    [TestFixture]
    public class CounterValueProviderTests
    {
        private CounterValueProvider counterParameter;

        [SetUp]
        public void SetUp()
        {
            counterParameter = new CounterValueProvider();
        }

        [Test]
        public void default_Step_is_1()
        {
            Assert.That(counterParameter.Step, Is.EqualTo(1));
        }

        [Test]
        public void default_StartValue_is_1()
        {
            Assert.That(counterParameter.StartValue, Is.EqualTo(1));
        }

        [Test]
        public void returns_1_as_first_value()
        {
            string firstValue = counterParameter.GetNextValue();

            Assert.That(firstValue, Is.EqualTo("1"));
        }

        [Test]
        public void returns_2_as_second_value()
        {
            string firstValue = counterParameter.GetNextValue();
            string secondValue = counterParameter.GetNextValue();

            Assert.That(secondValue, Is.EqualTo("2"));
        }

        [Test]
        public void returns_3_as_third_value()
        {
            string firstValue = counterParameter.GetNextValue();
            string secondValue = counterParameter.GetNextValue();
            string thirdValue = counterParameter.GetNextValue();

            Assert.That(thirdValue, Is.EqualTo("3"));
        }

        [Test]
        public void returns_10_as_first_value_if_StartValue_is_set_to_10()
        {
            counterParameter.StartValue = 10;
            string firstValue = counterParameter.GetNextValue();

            Assert.That(firstValue, Is.EqualTo("10"));
        }

        [Test]
        public void returns_11_as_second_value_if_StartValue_is_set_to_10()
        {
            counterParameter.StartValue = 10;
            string firstValue = counterParameter.GetNextValue();
            string secondValue = counterParameter.GetNextValue();

            Assert.That(secondValue, Is.EqualTo("11"));
        }

        [Test]
        public void returns_15_as_second_value_if_StartValue_is_set_to_10_and_Step_to_5()
        {
            counterParameter.StartValue = 10;
            counterParameter.Step = 5;
            string firstValue = counterParameter.GetNextValue();
            string secondValue = counterParameter.GetNextValue();

            Assert.That(secondValue, Is.EqualTo("15"));
        }

        [Test]
        public void returns_formated_value_if_Format_is_set()
        {
            counterParameter.Format = "000";
            string firstValue = counterParameter.GetNextValue();

            Assert.That(firstValue, Is.EqualTo("001"));
        }
    }
}
