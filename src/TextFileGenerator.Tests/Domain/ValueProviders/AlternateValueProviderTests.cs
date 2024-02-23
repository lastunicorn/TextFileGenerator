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
using Moq;
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Domain.ValueProviders;

[TestFixture]
public class AlternateValueProviderTests
{
    private Mock<IValueProvider> valueProvider1;
    private Mock<IValueProvider> valueProvider2;
    private AlternateValueProvider alternateValueProvider;

    [SetUp]
    public void SetUp()
    {
        valueProvider1 = new Mock<IValueProvider>();
        valueProvider2 = new Mock<IValueProvider>();
        alternateValueProvider = new AlternateValueProvider(valueProvider1.Object, valueProvider2.Object);
    }

    [Test]
    public void first_value_is_retrieved_from_valueProvider1()
    {
        valueProvider1.Setup(x => x.GetNextValue()).Returns("value1");

        string actual = alternateValueProvider.GetNextValue();

        valueProvider1.Verify(x => x.GetNextValue(), Times.Once());
        Assert.That(actual, Is.EqualTo("value1"));
    }

    [Test]
    public void second_value_is_retrieved_from_valueProvider2()
    {
        valueProvider2.Setup(x => x.GetNextValue()).Returns("value2");
        alternateValueProvider.GetNextValue();

        string actual = alternateValueProvider.GetNextValue();

        valueProvider2.Verify(x => x.GetNextValue(), Times.Once());
        Assert.That(actual, Is.EqualTo("value2"));
    }

    [Test]
    public void third_value_is_retrieved_from_valueProvider1()
    {
        string[] values1 = { "value1_1", "value1_2" };
        valueProvider1
            .Setup(x => x.GetNextValue())
            .Returns(values1[0]);

        string actual = alternateValueProvider.GetNextValue();

        valueProvider1.Verify(x => x.GetNextValue(), Times.Once());
        Assert.That(actual, Is.EqualTo("value1_1"));
    }
}