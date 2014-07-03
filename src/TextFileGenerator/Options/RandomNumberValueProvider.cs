using System;
using System.Globalization;

namespace DustInTheWind.TextFileGenerator.Options
{
    public class RandomNumberValueProvider : IValueProvider
    {
        private readonly Random random;
        private string currentValue;

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string Format { get; set; }

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

        public string GetCurrentValue()
        {
            return currentValue;
        }

        public void Reset()
        {
            currentValue = string.Empty;
        }
    }
}