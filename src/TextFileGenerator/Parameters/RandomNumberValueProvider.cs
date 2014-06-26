using System;
using System.Globalization;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class RandomNumberValueProvider : IValueProvider
    {
        private readonly Random random;

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string Format { get; set; }

        public RandomNumberValueProvider()
        {
            random = new Random();

            MinValue = 0;
            MaxValue = 100;
        }

        public string GetValue()
        {
            int value = random.Next(MinValue, MaxValue + 1);

            string valueAsString = Format != null
                ? value.ToString(Format, CultureInfo.CurrentCulture)
                : value.ToString(CultureInfo.CurrentCulture);

            return valueAsString;
        }
    }
}