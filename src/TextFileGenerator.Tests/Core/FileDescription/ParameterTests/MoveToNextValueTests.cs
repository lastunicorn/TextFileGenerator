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
    public class MoveToNextValueTests
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
            parameter.ValueChangeMode = ValueChangeMode.Manual;

            parameter.MoveToNextValue();

            valueProvider.Verify(x => x.GetNextValue(), Times.Once());
        }

        [Test]
        public void sets_the_CurrentValue_property_with_the_value_obtained_from_ValueProvider()
        {
            parameter.ValueChangeMode = ValueChangeMode.Manual;
            valueProvider.Setup(x => x.GetNextValue()).Returns("value1");

            parameter.MoveToNextValue();

            string actual = parameter.CurrentValue;
            Assert.That(actual, Is.EqualTo("value1"));
        }

        [Test]
        public void does_nothing_if_ValueChangeMode_is_Auto()
        {
            parameter.ValueChangeMode = ValueChangeMode.Auto;

            parameter.MoveToNextValue();

            valueProvider.Verify(x => x.GetNextValue(), Times.Never);
        }
    }
}
