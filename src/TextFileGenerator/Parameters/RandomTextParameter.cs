using System;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class RandomTextParameter : IParameter
    {
        private readonly Random random;

        public int MinLength { get; set; }

        public int MaxLength { get; set; }

        public string AvailableChars { get; set; }

        public RandomTextParameter()
        {
            random = new Random();

            MinLength = 1;
            MaxLength = 100;

            AvailableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        }

        public string Key { get; set; }

        public string GetValue()
        {
            int length = random.Next(MinLength, MaxLength + 1);

            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                int charIndex = random.Next(AvailableChars.Length);

                randomChars[i] = AvailableChars[charIndex];
            }

            return randomChars.ToString();
        }
    }
}
