using System;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class RandomTextValueProvider : IValueProvider
    {
        private readonly Random random;
        private string currentValue;

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public string AvailableChars { get; set; }

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

        public string GetCurrentValue()
        {
            return currentValue;
        }

        public void Reset()
        {
            currentValue = string.Empty;
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
    }
}