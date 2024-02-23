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

namespace DustInTheWind.TextFileGenerator.Tests.Domain.ProjectModel.TextTemplateTests;

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
        TextTemplate textTemplate = new(text);

        string actual = textTemplate.Format(parameters);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void needed_parameter_is_second_in_list()
    {
        TextTemplate textTemplate = new("{param1}");

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