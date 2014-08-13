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

using DustInTheWind.TextFileGenerator.Options.ValueProviders;
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
            string firstValue = counterParameter.MoveToNextValue();

            Assert.That(firstValue, Is.EqualTo("1"));
        }

        [Test]
        public void returns_2_as_second_value()
        {
            string firstValue = counterParameter.MoveToNextValue();
            string secondValue = counterParameter.MoveToNextValue();

            Assert.That(secondValue, Is.EqualTo("2"));
        }

        [Test]
        public void returns_3_as_third_value()
        {
            string firstValue = counterParameter.MoveToNextValue();
            string secondValue = counterParameter.MoveToNextValue();
            string thirdValue = counterParameter.MoveToNextValue();

            Assert.That(thirdValue, Is.EqualTo("3"));
        }

        [Test]
        public void returns_10_as_first_value_if_StartValue_is_set_to_10()
        {
            counterParameter.StartValue = 10;
            string firstValue = counterParameter.MoveToNextValue();

            Assert.That(firstValue, Is.EqualTo("10"));
        }

        [Test]
        public void returns_11_as_second_value_if_StartValue_is_set_to_10()
        {
            counterParameter.StartValue = 10;
            string firstValue = counterParameter.MoveToNextValue();
            string secondValue = counterParameter.MoveToNextValue();

            Assert.That(secondValue, Is.EqualTo("11"));
        }

        [Test]
        public void returns_15_as_second_value_if_StartValue_is_set_to_10_and_Step_to_5()
        {
            counterParameter.StartValue = 10;
            counterParameter.Step = 5;
            string firstValue = counterParameter.MoveToNextValue();
            string secondValue = counterParameter.MoveToNextValue();

            Assert.That(secondValue, Is.EqualTo("15"));
        }

        [Test]
        public void returns_formated_value_if_Format_is_set()
        {
            counterParameter.Format = "000";
            string firstValue = counterParameter.MoveToNextValue();

            Assert.That(firstValue, Is.EqualTo("001"));
        }
    }
}
