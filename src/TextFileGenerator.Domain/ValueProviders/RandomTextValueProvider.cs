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
using DustInTheWind.TextFileGenerator.Domain.ProjectModel;

namespace DustInTheWind.TextFileGenerator.Domain.ValueProviders
{
    public class RandomTextValueProvider : IValueProvider
    {
        [ThreadStatic]
        private static Random random;

        private const string AvailableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private string currentValue = string.Empty;

        public int MinLength { get; set; } = 1;

        public int MaxLength { get; set; } = 100;

        public RandomTextValueProvider()
        {
            if (random == null)
                random = new Random();
        }

        public string GetNextValue()
        {
            GenerateNextValue();
            return currentValue;
        }

        private void GenerateNextValue()
        {
            int length = random.Next(MinLength, MaxLength + 1);
            char[] randomChars = GenerateRandomChars(length);

            currentValue = new string(randomChars);
        }

        private static char[] GenerateRandomChars(int length)
        {
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                int charIndex = random.Next(AvailableChars.Length);
                randomChars[i] = AvailableChars[charIndex];
            }

            return randomChars;
        }

        public void Reset()
        {
            currentValue = string.Empty;
        }
    }
}