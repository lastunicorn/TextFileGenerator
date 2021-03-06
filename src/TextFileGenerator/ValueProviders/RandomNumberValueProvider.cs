﻿// TextFileGenerator
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

namespace DustInTheWind.TextFileGenerator.ValueProviders
{
    public class RandomNumberValueProvider : IValueProvider
    {
        [ThreadStatic]
        private static Random random;

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string Format { get; set; }
        private string currentValue;

        public RandomNumberValueProvider()
        {
            random = new Random();

            MinValue = 1;
            MaxValue = 100;

            currentValue = string.Empty;
        }

        public string GetNextValue()
        {
            GenerateNextValue();
            return currentValue;
        }

        private void GenerateNextValue()
        {
            int value = random.Next(MinValue, MaxValue + 1);

            currentValue = Format != null
                ? value.ToString(Format, CultureInfo.CurrentCulture)
                : value.ToString(CultureInfo.CurrentCulture);
        }

        public void Reset()
        {
            currentValue = string.Empty;
        }
    }
}