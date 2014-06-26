using System;
using System.Globalization;
using DustInTheWind.TextFileGenerator.Options;

namespace DustInTheWind.TextFileGenerator.Parameters
{
    public class RandomNumberParameter : IParameter
    {
        private readonly Random random;

        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public string Format { get; set; }

        public RandomNumberParameter()
        {
            random = new Random();

            MinValue = 0;
            MaxValue = 100;
        }

        public string Key { get; set; }

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
