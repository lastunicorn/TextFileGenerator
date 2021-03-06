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

namespace DustInTheWind.TextFileGenerator.ValueProviders
{
    public class RandomTextValueProvider : IValueProvider
    {
        private readonly Random random;
        private string currentValue;

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string AvailableChars { get; }

        public RandomTextValueProvider()
        {
            random = new Random();

            MinLength = 1;
            MaxLength = 100;

            AvailableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            currentValue = string.Empty;
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

        private char[] GenerateRandomChars(int length)
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