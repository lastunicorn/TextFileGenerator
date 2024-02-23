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

namespace DustInTheWind.TextFileGenerator.Tests.Domain.ProjectModel.ParameterTests
{
    [TestFixture]
    public class ResetTests
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
        public void resets_the_ValueProvider()
        {
            parameter.Reset();

            valueProvider.Verify(x => x.Reset(), Times.Once());
        }
    }
}