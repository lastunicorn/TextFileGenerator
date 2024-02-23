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

namespace DustInTheWind.TextFileGenerator.Tests.Domain.ProjectModel.TextTemplateTests
{
    [TestFixture]
    public class NoParameterTests
    {
        [Test]
        public void returns_whole_text_if_contains_only_one_parameter()
        {
            TextTemplate textTemplate = new TextTemplate("{param1}");

            string actual = textTemplate.Format(new Parameter[0]);

            Assert.That(actual, Is.EqualTo("{param1}"));
        }

        [Test]
        public void returns_whole_text_if_contains_one_parameter_in_the_middle_of_the_text()
        {
            TextTemplate textTemplate = new TextTemplate("aaa{param1}bbb");

            string actual = textTemplate.Format(new Parameter[0]);

            Assert.That(actual, Is.EqualTo("aaa{param1}bbb"));
        }
    }
}