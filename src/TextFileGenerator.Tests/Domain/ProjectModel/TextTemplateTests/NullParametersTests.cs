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
using NUnit.Framework;

namespace DustInTheWind.TextFileGenerator.Tests.Domain.ProjectModel.TextTemplateTests;

[TestFixture]
public class NullParametersTests
{
    [Test]
    public void returns_string_empty_if_Value_is_null()
    {
        TextTemplate textTemplate = new(null);

        string actual = textTemplate.Format(null);

        Assert.That(actual, Is.EqualTo(string.Empty));
    }

    [Test]
    public void returns_the_Value_if_provided_list_of_paramers_is_null()
    {
        TextTemplate textTemplate = new("this is a text");

        string actual = textTemplate.Format(null);

        Assert.That(actual, Is.EqualTo("this is a text"));
    }
}