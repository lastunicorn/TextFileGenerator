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

using DustInTheWind.TextFileGenerator.FileDescription.ValueProviders;
using Moq;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Core.FileDescription.ValueProviders
{
    [TestFixture]
    public class AlternativeValueProviderTests
    {
        private Mock<IValueProvider> valueProvider1;
        private Mock<IValueProvider> valueProvider2;
        private AlternativeValueProvider alternativeValueProvider;

        [SetUp]
        public void SetUp()
        {
            valueProvider1 = new Mock<IValueProvider>();
            valueProvider2 = new Mock<IValueProvider>();
            alternativeValueProvider = new AlternativeValueProvider(valueProvider1.Object, valueProvider2.Object);
        }

        [Test]
        public void first_value_is_retrieved_from_valueProvider1()
        {
            valueProvider1.SetupGet(x => x.CurrentValue).Returns("value1");

            string actual = alternativeValueProvider.CurrentValue;

            valueProvider1.VerifyGet(x => x.CurrentValue, Times.Once());
            Assert.That(actual, Is.EqualTo("value1"));
        }
    }
}
