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

using DustInTheWind.TextFileGenerator.Domain.ProjectModel;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.FileDescription.ParameterTests
{
    [TestFixture]
    public class NextValueTests
    {
        private Parameter parameter;
        private Mock<IValueProvider> valueProvider;

        [SetUp]
        public void SetUp()
        {
            parameter = new Parameter();

            valueProvider = new Mock<IValueProvider>();
            parameter.ValueProvider = valueProvider.Object;
        }

        [Test]
        public void gets_next_value_from_the_ValueProvider()
        {
            string value = parameter.NextValue;

            valueProvider.Verify(x => x.GetNextValue(), Times.Once());
        }

        [Test]
        public void returns_the_value_obtained_from_ValueProvider()
        {
            valueProvider.Setup(x => x.GetNextValue()).Returns("value1");

            string actual = parameter.NextValue;

            Assert.That(actual, Is.EqualTo("value1"));
        }

        [Test]
        public void sets_the_CurrentValue_property()
        {
            valueProvider.Setup(x => x.GetNextValue()).Returns("value1");

            string value = parameter.NextValue;

            string actual = parameter.CurrentValue;
            Assert.That(actual, Is.EqualTo("value1"));
        }

        [Test]
        public void second_call_gets_next_value_from_the_ValueProvider()
        {
            string value1 = parameter.NextValue;

            string value2 = parameter.NextValue;

            valueProvider.Verify(x => x.GetNextValue(), Times.Exactly(2));
        }

        [Test]
        public void if_ValueChangeMode_is_Manual_second_call_does_not_advance_the_ValueProvider()
        {
            parameter.ValueChangeMode = ValueChangeMode.Manual;
            string value1 = parameter.NextValue;

            string value2 = parameter.NextValue;

            valueProvider.Verify(x => x.GetNextValue(), Times.Once());
        }
    }
}
