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

using System;
using System.Globalization;

namespace DustInTheWind.TextFileGenerator.FileDescription.ValueProviders
{
    public class RandomNumberValueProvider : IValueProvider
    {
        private readonly Random random;

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string Format { get; set; }
        public string CurrentValue { get; private set; }

        public RandomNumberValueProvider()
        {
            random = new Random();

            MinValue = 1;
            MaxValue = 100;

            CurrentValue = string.Empty;
        }

        public void MoveToNextValue()
        {
            GenerateNextValue();
        }

        private void GenerateNextValue()
        {
            int value = random.Next(MinValue, MaxValue + 1);

            CurrentValue = Format != null
                ? value.ToString(Format, CultureInfo.CurrentCulture)
                : value.ToString(CultureInfo.CurrentCulture);
        }

        public void Reset()
        {
            CurrentValue = string.Empty;
        }
    }
}