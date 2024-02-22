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
using DustInTheWind.TextFileGenerator.Domain.ValueProviders;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.FileDescription.ParameterTests
{
    [TestFixture]
    public class ConstructorTests
    {
        private Parameter parameter;

        [SetUp]
        public void SetUp()
        {
            parameter = new Parameter();
        }

        [Test]
        public void initially_ValueProvider_is_EmptyValueProvider()
        {
            IValueProvider actual = parameter.ValueProvider;

            Assert.That(actual, Is.SameAs(EmptyValueProvider.Value));
        }

        [Test]
        public void initially_CurrentValue_is_null()
        {
            string actual = parameter.CurrentValue;

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void initially_Name_is_null()
        {
            string actual = parameter.Name;

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void initially_ValueChangeMode_is_null()
        {
            ValueChangeMode actual = parameter.ValueChangeMode;

            Assert.That(actual, Is.EqualTo(ValueChangeMode.Auto));
        }
    }
}
