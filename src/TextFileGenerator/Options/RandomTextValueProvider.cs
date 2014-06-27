using System;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class RandomTextValueProvider : IValueProvider
    {
        private readonly Random random;

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public string AvailableChars { get; set; }

        public RandomTextValueProvider()
        {
            random = new Random();

            MinLength = 1;
            MaxLength = 100;

            AvailableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        }

        public string GetValue()
        {
            int length = random.Next(MinLength, MaxLength + 1);
            char[] randomChars = GenerateRandomChars(length);

            return randomChars.ToString();
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