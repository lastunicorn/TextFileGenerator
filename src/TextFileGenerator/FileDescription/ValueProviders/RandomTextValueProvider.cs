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

namespace DustInTheWind.TextFileGenerator.FileDescription.ValueProviders
{
    public class RandomTextValueProvider : IValueProvider
    {
        private readonly Random random;

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string AvailableChars { get; set; }
        public string CurrentValue { get; private set; }

        public RandomTextValueProvider()
        {
            random = new Random();

            MinLength = 1;
            MaxLength = 100;

            AvailableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            CurrentValue = string.Empty;
        }

        public void MoveToNextValue()
        {
            GenerateNextValue();
        }

        public void Reset()
        {
            CurrentValue = string.Empty;
        }

        private void GenerateNextValue()
        {
            int length = random.Next(MinLength, MaxLength + 1);
            char[] randomChars = GenerateRandomChars(length);

            CurrentValue = new string(randomChars);
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
    }
}